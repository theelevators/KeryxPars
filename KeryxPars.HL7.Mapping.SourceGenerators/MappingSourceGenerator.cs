using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace KeryxPars.HL7.Mapping.SourceGenerators;

/// <summary>
/// Incremental source generator that creates high-performance mappers for classes decorated with [HL7Message].
/// Generates compile-time mapping code with zero runtime reflection.
/// </summary>
[Generator]
public class MappingSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Find all classes with attributes
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node)
            .Where(static c => c != null);

        // Combine with compilation
        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        // Generate source for each class
        context.RegisterSourceOutput(compilationAndClasses, (spc, source) =>
        {
            var (compilation, classes) = source;
            GenerateMappers(compilation, classes, spc);
        });
    }

    private void GenerateMappers(
        Compilation compilation,
        ImmutableArray<ClassDeclarationSyntax> classes,
        SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
            return;

        // Get attribute symbols
        var hl7MessageAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7MessageAttribute");
        var hl7ComplexTypeAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7ComplexTypeAttribute");
        var hl7FieldAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7FieldAttribute");

        if (hl7MessageAttributeSymbol == null || hl7FieldAttributeSymbol == null)
            return;

        // Process each candidate class
        foreach (var candidateClass in classes)
        {
            var model = compilation.GetSemanticModel(candidateClass.SyntaxTree);
            var classSymbol = model.GetDeclaredSymbol(candidateClass) as INamedTypeSymbol;

            if (classSymbol == null)
                continue;

            // Check if class has HL7MessageAttribute (root message)
            var hl7MessageAttribute = classSymbol.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7MessageAttributeSymbol));

            // Check if class has HL7ComplexTypeAttribute (complex type)
            var hl7ComplexTypeAttribute = hl7ComplexTypeAttributeSymbol != null ? classSymbol.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7ComplexTypeAttributeSymbol)) : null;

            // Check if class has any properties with HL7FieldAttribute (for collection items or legacy support)
            var hasFieldAttributes = classSymbol.GetMembers()
                .OfType<IPropertySymbol>()
                .Any(p => p.GetAttributes()
                    .Any(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7FieldAttributeSymbol)));

            // Check if class has any properties with HL7ComponentAttribute (for complex types)
            var hl7ComponentAttributeSymbol = compilation.GetTypeByMetadataName(
                "KeryxPars.HL7.Mapping.HL7ComponentAttribute");
            var hasComponentAttributes = hl7ComponentAttributeSymbol != null && classSymbol.GetMembers()
                .OfType<IPropertySymbol>()
                .Any(p => p.GetAttributes()
                    .Any(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7ComponentAttributeSymbol)));

            // Generate mapper if:
            // 1. Has HL7Message (root message class)
            // 2. Has HL7ComplexType (complex type within a message)
            // 3. Has HL7Field attributes (collection item or legacy support)
            // 4. Has HL7Component attributes (legacy complex type support)
            if (hl7MessageAttribute != null || hl7ComplexTypeAttribute != null || hasFieldAttributes || hasComponentAttributes)
            {
                // Determine if this is a complex type or a root message
                bool isComplexType = hl7ComplexTypeAttribute != null && hl7MessageAttribute == null;
                
                // Generate the mapper for this class
                var sourceCode = isComplexType 
                    ? GenerateComplexTypeMapper(classSymbol, hl7ComplexTypeAttribute, compilation)
                    : GenerateMapperClass(classSymbol, hl7MessageAttribute, compilation);

                if (!string.IsNullOrEmpty(sourceCode))
                {
                    // Add the generated source to compilation
                    var fileName = $"{classSymbol.Name}_Generated.g.cs";
                    context.AddSource(fileName, sourceCode);
                }
            }
        }
    }

    private string GenerateMapperClass(
        INamedTypeSymbol classSymbol,
        AttributeData? hl7MessageAttribute,
        Compilation compilation)
    {
        var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
        var className = classSymbol.Name;

        // Get message types from attribute (if present)
        var messageTypes = new System.Collections.Generic.List<string?>();
        if (hl7MessageAttribute != null && hl7MessageAttribute.ConstructorArguments.Length > 0)
        {
            messageTypes = hl7MessageAttribute.ConstructorArguments[0].Values
                .Select(v => v.Value?.ToString())
                .Where(v => v != null)
                .ToList();
        }

        // Find all properties with mapping attributes
        var mappedProperties = GetMappedProperties(classSymbol, compilation);

        if (!mappedProperties.Any())
            return string.Empty;

        // Generate the source code
        var sb = new StringBuilder();

        sb.AppendLine("// <auto-generated />");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Runtime.CompilerServices;");
        sb.AppendLine("using KeryxPars.HL7.Mapping;");
        sb.AppendLine("using KeryxPars.HL7.Mapping.Core;");
        sb.AppendLine("using KeryxPars.HL7.Mapping.Parsers;");
        sb.AppendLine("using KeryxPars.HL7.Mapping.Converters;");
        sb.AppendLine();
        sb.AppendLine($"namespace {namespaceName};");
        sb.AppendLine();
        sb.AppendLine("/// <summary>");
        sb.AppendLine($"/// Generated mapper for {className}");
        sb.AppendLine("/// </summary>");
        sb.AppendLine($"public static partial class {className}Mapper");
        sb.AppendLine("{");

        // Generate field notation cache
        GenerateFieldNotationCache(sb, mappedProperties);

        sb.AppendLine();

        // Generate MapFromSpan method
        GenerateMapFromSpanMethod(sb, className, mappedProperties);

        sb.AppendLine();

        // Generate MapFromSegment method (for collection items)
        GenerateMapFromSegmentMethod(sb, className, mappedProperties, classSymbol, compilation);

        sb.AppendLine();

        // Generate MapFromField method (for complex/nested types)
        GenerateMapFromFieldMethod(sb, className, classSymbol, compilation);

        sb.AppendLine();

        // Generate MapFromMessage method (for already-parsed messages)
        GenerateMapFromMessageMethod(sb, className, mappedProperties);

        sb.AppendLine();

        // Generate BuildHL7 method (reverse mapping)
        GenerateBuildHL7Method(sb, className, mappedProperties, messageTypes);

        sb.AppendLine("}");

        return sb.ToString();
    }

    private string GenerateComplexTypeMapper(
        INamedTypeSymbol classSymbol,
        AttributeData? hl7ComplexTypeAttribute,
        Compilation compilation)
    {
        var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
        var className = classSymbol.Name;

        // Get BaseFieldPath from attribute (if present) - for Step 2
        string? baseFieldPath = null;
        if (hl7ComplexTypeAttribute != null)
        {
            if (hl7ComplexTypeAttribute.ConstructorArguments.Length > 0)
            {
                baseFieldPath = hl7ComplexTypeAttribute.ConstructorArguments[0].Value?.ToString();
            }
            else
            {
                // Check named argument
                var namedArg = hl7ComplexTypeAttribute.NamedArguments
                    .FirstOrDefault(na => na.Key == "BaseFieldPath");
                if (!namedArg.Equals(default(System.Collections.Generic.KeyValuePair<string, TypedConstant>)))
                {
                    baseFieldPath = namedArg.Value.Value?.ToString();
                }
            }
        }

        // Find all properties with HL7Field attributes
        var mappedProperties = GetComplexTypeMappedProperties(classSymbol, compilation, baseFieldPath);

        if (!mappedProperties.Any())
            return string.Empty;

        // Generate the source code
        var sb = new StringBuilder();

        sb.AppendLine("// <auto-generated />");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Runtime.CompilerServices;");
        sb.AppendLine("using KeryxPars.HL7.Mapping;");
        sb.AppendLine("using KeryxPars.HL7.Mapping.Core;");
        sb.AppendLine("using KeryxPars.HL7.Mapping.Parsers;");
        sb.AppendLine("using KeryxPars.HL7.Mapping.Converters;");
        sb.AppendLine();
        sb.AppendLine($"namespace {namespaceName};");
        sb.AppendLine();
        sb.AppendLine("/// <summary>");
        sb.AppendLine($"/// Auto-generated mapper for <see cref=\"{className}\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <remarks>");
        sb.AppendLine("/// <para>");
        sb.AppendLine("/// This mapper is generated at compile time by the KeryxPars source generator.");
        sb.AppendLine("/// It provides high-performance, zero-allocation HL7 message mapping using");
        sb.AppendLine("/// ReadOnlySpan&lt;char&gt; for optimal memory efficiency.");
        sb.AppendLine("/// </para>");
        sb.AppendLine("/// <para>");
        sb.AppendLine("/// Field paths are resolved at compile time, eliminating runtime reflection");
        sb.AppendLine("/// and ensuring type safety with full IntelliSense support.");
        sb.AppendLine("/// </para>");
        sb.AppendLine("/// </remarks>");
        sb.AppendLine($"public static partial class {className}Mapper");
        sb.AppendLine("{");

        // Generate field notation cache
        GenerateFieldNotationCache(sb, mappedProperties);

        sb.AppendLine();

        // Generate MapFromMessage method for complex types
        GenerateComplexTypeMapFromMessage(sb, className, mappedProperties);

        sb.AppendLine("}");

        return sb.ToString();
    }

    private ImmutableArray<MappedProperty> GetComplexTypeMappedProperties(
        INamedTypeSymbol classSymbol,
        Compilation compilation,
        string? baseFieldPath)
    {
        var properties = ImmutableArray.CreateBuilder<MappedProperty>();

        var hl7FieldAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7FieldAttribute");

        if (hl7FieldAttributeSymbol == null)
            return properties.ToImmutable();

        foreach (var member in classSymbol.GetMembers())
        {
            if (member is not IPropertySymbol property)
                continue;

            // Check for HL7FieldAttribute
            var fieldAttribute = property.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7FieldAttributeSymbol));

            if (fieldAttribute == null)
                continue;

            // Get field path from attribute
            var fieldPath = fieldAttribute.ConstructorArguments[0].Value?.ToString();

            if (string.IsNullOrEmpty(fieldPath))
                continue;

            // Step 2: Resolve relative paths against BaseFieldPath
            var resolvedFieldPath = ResolveFieldPath(fieldPath, baseFieldPath);

            // Get optional properties
            var format = GetNamedArgument<string>(fieldAttribute, "Format");
            var defaultValue = GetNamedArgument<string>(fieldAttribute, "DefaultValue");

            // Get type information
            var propertyType = property.Type;
            var typeString = propertyType.ToDisplayString();
            var isNullable = propertyType.NullableAnnotation == NullableAnnotation.Annotated ||
                            (propertyType is INamedTypeSymbol namedType && 
                             namedType.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T);
            var isEnum = propertyType.TypeKind == TypeKind.Enum ||
                        (propertyType is INamedTypeSymbol nullableType && 
                         nullableType.TypeArguments.FirstOrDefault()?.TypeKind == TypeKind.Enum);

            properties.Add(new MappedProperty
            {
                PropertyName = property.Name,
                PropertyType = typeString,
                FieldPath = resolvedFieldPath,
                Format = format,
                DefaultValue = defaultValue,
                IsNullable = isNullable,
                IsEnum = isEnum,
                UnderlyingType = GetUnderlyingType(propertyType),
                IsCollection = false,
                IsComplex = false
            });
        }

        return properties.ToImmutable();
    }

    private void GenerateComplexTypeMapFromMessage(
        StringBuilder sb,
        string className,
        ImmutableArray<MappedProperty> properties)
    {
        sb.AppendLine("    /// <summary>");
        sb.AppendLine($"    /// Maps fields from an HL7 message to a <see cref=\"{className}\"/> instance.");
        sb.AppendLine("    /// </summary>");
        sb.AppendLine("    /// <param name=\"hl7Message\">The HL7 message as a read-only character span.</param>");
        sb.AppendLine($"    /// <returns>A new instance of <see cref=\"{className}\"/> with mapped values.</returns>");
        sb.AppendLine("    /// <remarks>");
        sb.AppendLine("    /// <para>");
        sb.AppendLine("    /// This method supports both absolute field paths (e.g., PID.5.1, PV1.2) and");
        sb.AppendLine("    /// relative field paths (e.g., 1, 3) when a BaseFieldPath is specified on the type.");
        sb.AppendLine("    /// </para>");
        sb.AppendLine("    /// <para>");
        sb.AppendLine("    /// The mapping is performed with zero allocations using span-based parsing.");
        sb.AppendLine("    /// All field paths are resolved at compile time for optimal performance.");
        sb.AppendLine("    /// </para>");
        sb.AppendLine("    /// </remarks>");
        sb.AppendLine("    /// <exception cref=\"HL7MappingException\">");
        sb.AppendLine("    /// Thrown when a field value cannot be converted to the target property type.");
        sb.AppendLine("    /// The exception includes the field path and property name for diagnostics.");
        sb.AppendLine("    /// </exception>");
        sb.AppendLine($"    public static {className} MapFromMessage(ReadOnlySpan<char> hl7Message)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var result = new {className}();");
        sb.AppendLine();

        foreach (var prop in properties)
        {
            if (prop.FieldPath == null)
                continue;

            GenerateComplexPropertyMapping(sb, prop);
        }

        sb.AppendLine("        return result;");
        sb.AppendLine("    }");
    }

    private void GenerateComplexPropertyMapping(StringBuilder sb, MappedProperty prop)
    {
        var notationVar = $"_{prop.PropertyName}Notation";

        sb.AppendLine($"        // Map {prop.PropertyName} from {prop.FieldPath}");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            var value = HL7SpanParser.GetValue(hl7Message, {notationVar});");

        // Generate type conversion
        var conversion = GenerateTypeConversion(prop);

        sb.AppendLine($"            if (!HL7SpanParser.IsNullOrWhiteSpace(value))");
        sb.AppendLine($"            {{");
        sb.AppendLine($"                try");
        sb.AppendLine($"                {{");
        sb.AppendLine($"                    result.{prop.PropertyName} = {conversion};");
        sb.AppendLine($"                }}");
        sb.AppendLine($"                catch (System.Exception ex)");
        sb.AppendLine($"                {{");
        sb.AppendLine($"                    throw new KeryxPars.HL7.Mapping.HL7MappingException(");
        sb.AppendLine($"                        $\"Error converting field {{value.ToString()}} to {prop.PropertyType}\",");
        sb.AppendLine($"                        \"{prop.PropertyType}\",");
        sb.AppendLine($"                        \"{prop.FieldPath}\",");
        sb.AppendLine($"                        ex);");
        sb.AppendLine($"                }}");
        sb.AppendLine($"            }}");

        // Handle default value
        if (prop.DefaultValue != null)
        {
            sb.AppendLine($"            else");
            sb.AppendLine($"            {{");
            sb.AppendLine($"                result.{prop.PropertyName} = {prop.DefaultValue};");
            sb.AppendLine($"            }}");
        }

        sb.AppendLine($"        }}");
        sb.AppendLine();
    }

    private bool IsAbsolutePath(string fieldPath)
    {
        // Absolute paths start with a segment name (3 uppercase letters)
        // e.g., "PID.5.1", "PV1.2", "OBX.3.1"
        if (string.IsNullOrEmpty(fieldPath))
            return false;

        // Check if starts with uppercase letters followed by a dot
        var parts = fieldPath.Split('.');
        if (parts.Length < 2)
            return false;

        var segment = parts[0];
        // Segment should be 2-3 uppercase letters
        return segment.Length >= 2 && segment.Length <= 3 && 
               segment.All(c => char.IsUpper(c) || char.IsDigit(c));
    }

    private bool IsRelativePath(string fieldPath)
    {
        // Relative paths are just numeric (e.g., "1", "3", "5")
        // or numeric with sub-components (e.g., "1.2", "3.1")
        if (string.IsNullOrEmpty(fieldPath))
            return false;

        var parts = fieldPath.Split('.');
        // All parts must be numeric
        return parts.All(part => int.TryParse(part, out _));
    }

    private string ResolveFieldPath(string fieldPath, string? baseFieldPath)
    {
        // If no base path, the field path must be absolute
        if (string.IsNullOrEmpty(baseFieldPath))
        {
            return fieldPath;
        }

        // If the field path is already absolute, return it as-is (overrides base)
        if (IsAbsolutePath(fieldPath))
        {
            return fieldPath;
        }

        // If the field path is relative (numeric), combine with base
        if (IsRelativePath(fieldPath))
        {
            return $"{baseFieldPath}.{fieldPath}";
        }

        // Otherwise, return as-is (might be invalid, but let runtime handle it)
        return fieldPath;
    }

    private void GenerateFieldNotationCache(StringBuilder sb, ImmutableArray<MappedProperty> properties)
    {
        sb.AppendLine("    #region Field Notation Cache");
        sb.AppendLine();
        sb.AppendLine("    // Field notation objects are cached at the class level for optimal performance.");
        sb.AppendLine("    // These are parsed once at type initialization rather than on every mapping operation.");
        sb.AppendLine();

        foreach (var prop in properties)
        {
            if (prop.FieldPath != null)
            {
                var fieldName = $"_{prop.PropertyName}Notation";
                sb.AppendLine($"    private static readonly FieldNotation {fieldName} = ");
                sb.AppendLine($"        FieldNotation.Parse(\"{prop.FieldPath}\");");
                
                // Generate fallback notation if specified
                if (!string.IsNullOrEmpty(prop.FallbackField))
                {
                    var fallbackFieldName = $"_Fallback{prop.PropertyName}Notation";
                    sb.AppendLine($"    private static readonly FieldNotation {fallbackFieldName} = ");
                    sb.AppendLine($"        FieldNotation.Parse(\"{prop.FallbackField}\");");
                }
                
                // Generate priority fallback notations if specified
                if (prop.PriorityFallbackFields != null && prop.PriorityFallbackFields.Count > 0)
                {
                    for (int i = 0; i < prop.PriorityFallbackFields.Count; i++)
                    {
                        var priorityField = prop.PriorityFallbackFields[i];
                        if (!string.IsNullOrEmpty(priorityField))
                        {
                            var priorityFieldName = $"_Priority{i + 1}{prop.PropertyName}Notation";
                            sb.AppendLine($"    private static readonly FieldNotation {priorityFieldName} = ");
                            sb.AppendLine($"        FieldNotation.Parse(\"{priorityField}\");");
                        }
                    }
                }
            }
        }

        sb.AppendLine();
        sb.AppendLine("    #endregion");
    }

    private void GenerateMapFromSpanMethod(
        StringBuilder sb,
        string className,
        ImmutableArray<MappedProperty> properties)
    {
        sb.AppendLine("    /// <summary>");
        sb.AppendLine($"    /// Maps an HL7 message span to {className} with zero allocation.");
        sb.AppendLine("    /// </summary>");
        sb.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
        sb.AppendLine($"    public static {className} MapFromSpan(ReadOnlySpan<char> hl7Message)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var result = new {className}();");
        sb.AppendLine();

        foreach (var prop in properties)
        {
            GeneratePropertyMapping(sb, prop, "hl7Message");
        }

        sb.AppendLine();
        sb.AppendLine("        return result;");
        sb.AppendLine("    }");
    }

    private void GenerateMapFromMessageMethod(
        StringBuilder sb,
        string className,
        ImmutableArray<MappedProperty> properties)
    {
        sb.AppendLine("    /// <summary>");
        sb.AppendLine($"    /// Maps a parsed HL7 message object to {className}.");
        sb.AppendLine("    /// </summary>");
        sb.AppendLine($"    public static {className} MapFromMessage(object message)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var result = new {className}();");
        sb.AppendLine();
        sb.AppendLine("        // TODO: Implement object-based mapping");
        sb.AppendLine("        // This will use SegmentAccessor for fast property access");
        sb.AppendLine();
        sb.AppendLine("        return result;");
        sb.AppendLine("    }");
    }

    private void GenerateMapFromSegmentMethod(
        StringBuilder sb,
        string className,
        ImmutableArray<MappedProperty> properties,
        INamedTypeSymbol classSymbol,
        Compilation compilation)
    {
        // Only generate non-collection properties for segment-based mapping
        var segmentProperties = properties.Where(p => !p.IsCollection).ToArray();

        sb.AppendLine("    /// <summary>");
        sb.AppendLine($"    /// Maps a single segment to {className}.");
        sb.AppendLine("    /// Used for collection mapping.");
        sb.AppendLine("    /// </summary>");
        sb.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
        sb.AppendLine($"    public static {className} MapFromSegment(ReadOnlySpan<char> segment)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var result = new {className}();");
        sb.AppendLine();

        foreach (var prop in segmentProperties)
        {
            if (prop.FieldPath == null)
                continue;

            var notationVar = $"_{prop.PropertyName}Notation";

            sb.AppendLine($"        // Map {prop.PropertyName} from {prop.FieldPath}");
            sb.AppendLine($"        {{");
            
            // For segment-based mapping, we need to extract just the field part
            // Parse the FieldPath to get the field index
            var fieldPath = prop.FieldPath;
            var parts = fieldPath.Split('.');
            
            if (parts.Length >= 2)
            {
                // Has field number (e.g., "OBX.3.1")
                var fieldNum = parts[1];
                
                if (parts.Length == 2)
                {
                    // Just field (e.g., "OBX.5")
                    sb.AppendLine($"            var value = HL7SpanParser.GetField(segment, {fieldNum});");
                }
                else if (parts.Length == 3)
                {
                    // Field + component (e.g., "OBX.3.1")
                    var compNum = parts[2];
                    sb.AppendLine($"            var field = HL7SpanParser.GetField(segment, {fieldNum});");
                    sb.AppendLine($"            var value = HL7SpanParser.GetComponent(field, {compNum});");
                }
                else if (parts.Length == 4)
                {
                    // Field + component + subcomponent
                    var compNum = parts[2];
                    var subNum = parts[3];
                    sb.AppendLine($"            var field = HL7SpanParser.GetField(segment, {fieldNum});");
                    sb.AppendLine($"            var comp = HL7SpanParser.GetComponent(field, {compNum});");
                    sb.AppendLine($"            var value = HL7SpanParser.GetSubcomponent(comp, {subNum});");
                }
                else
                {
                    // Fallback - shouldn't happen
                    sb.AppendLine($"            var value = ReadOnlySpan<char>.Empty;");
                }
            }
            else
            {
                // No field specified - shouldn't happen for collections
                sb.AppendLine($"            var value = ReadOnlySpan<char>.Empty;");
            }

            var conversion = GenerateTypeConversion(prop);

            sb.AppendLine($"            if (!HL7SpanParser.IsNullOrWhiteSpace(value))");
            sb.AppendLine($"            {{");
            sb.AppendLine($"                try");
            sb.AppendLine($"                {{");
            sb.AppendLine($"                    result.{prop.PropertyName} = {conversion};");
            sb.AppendLine($"                }}");
            sb.AppendLine($"                catch (System.Exception ex)");
            sb.AppendLine($"                {{");
            sb.AppendLine($"                    throw new HL7MappingException(");
            sb.AppendLine($"                        $\"Error converting field {{value.ToString()}} to {prop.PropertyType}\",");
            sb.AppendLine($"                        \"{prop.PropertyName}\",");
            sb.AppendLine($"                        \"{prop.FieldPath}\",");
            sb.AppendLine($"                        ex);");
            sb.AppendLine($"                }}");
            sb.AppendLine($"            }}");

            if (prop.DefaultValue != null)
            {
                sb.AppendLine($"            else");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                result.{prop.PropertyName} = {prop.DefaultValue};");
                sb.AppendLine($"            }}");
            }

            sb.AppendLine($"        }}");
            sb.AppendLine();
        }

        sb.AppendLine("        return result;");
        sb.AppendLine("    }");
    }

    private void GenerateMapFromFieldMethod(
        StringBuilder sb,
        string className,
        INamedTypeSymbol classSymbol,
        Compilation compilation)
    {
        // Check if this class has HL7ComponentAttribute on any properties
        var hl7ComponentAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7ComponentAttribute");

        if (hl7ComponentAttributeSymbol == null)
            return;

        var componentProperties = classSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Select(p => new
            {
                Property = p,
                ComponentAttribute = p.GetAttributes()
                    .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7ComponentAttributeSymbol))
            })
            .Where(x => x.ComponentAttribute != null)
            .ToList();

        if (!componentProperties.Any())
            return;

        sb.AppendLine("    /// <summary>");
        sb.AppendLine($"    /// Maps a field value to {className} using component indices.");
        sb.AppendLine("    /// Used for complex/nested type mapping.");
        sb.AppendLine("    /// </summary>");
        sb.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
        sb.AppendLine($"    public static {className} MapFromField(ReadOnlySpan<char> field)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var result = new {className}();");
        sb.AppendLine();

        foreach (var prop in componentProperties)
        {
            if (prop.ComponentAttribute != null && prop.ComponentAttribute.ConstructorArguments.Length > 0)
            {
                var componentIndex = prop.ComponentAttribute.ConstructorArguments[0].Value;
                var propertyName = prop.Property.Name;
                
                sb.AppendLine($"        // Map {propertyName} from component {componentIndex}");
                sb.AppendLine($"        {{");
                sb.AppendLine($"            var component = HL7SpanParser.GetComponent(field, {componentIndex});");
                sb.AppendLine($"            if (!HL7SpanParser.IsNullOrWhiteSpace(component))");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                result.{propertyName} = component.ToString();");
                sb.AppendLine($"            }}");
                sb.AppendLine($"        }}");
                sb.AppendLine();
            }
        }

        sb.AppendLine("        return result;");
        sb.AppendLine("    }");
    }

    private void GeneratePropertyMapping(StringBuilder sb, MappedProperty prop, string messageVarName)
    {
        // Handle complex/nested object mapping
        if (prop.IsComplex && prop.BaseFieldPath != null && prop.ComplexType != null)
        {
            GenerateComplexMapping(sb, prop, messageVarName);
            return;
        }

        // Handle collection mapping
        if (prop.IsCollection && prop.SegmentId != null && prop.CollectionItemType != null)
        {
            GenerateCollectionMapping(sb, prop, messageVarName);
            return;
        }

        // Handle regular field mapping
        if (prop.FieldPath == null)
            return;

        var notationVar = $"_{prop.PropertyName}Notation";

        sb.AppendLine($"        // Map {prop.PropertyName} from {prop.FieldPath}");
        
        // Open conditional wrapper if condition exists
        var hasCondition = !string.IsNullOrEmpty(prop.Condition);
        if (hasCondition)
        {
            sb.AppendLine($"        // Conditional mapping: {prop.Condition}");
            sb.AppendLine($"        if (KeryxPars.HL7.Mapping.Core.ConditionEvaluator.Evaluate({messageVarName}, \"{prop.Condition}\"))");
            sb.AppendLine($"        {{");
        }
        
        sb.AppendLine($"        {{");
        
        // Check conditional defaults FIRST - if condition matches, use default and skip message value
        bool hasConditionalDefaults = prop.ConditionalDefaults != null && prop.ConditionalDefaults.Count > 0;
        
        if (hasConditionalDefaults)
        {
            sb.AppendLine($"            // Check conditional defaults first - override message value if condition matches");
            
            // Separate conditional and unconditional defaults
            var conditionalOnes = prop.ConditionalDefaults.Where(cd => !string.IsNullOrEmpty(cd.Condition)).ToList();
            var unconditionalOne = prop.ConditionalDefaults.FirstOrDefault(cd => string.IsNullOrEmpty(cd.Condition));
            
            bool isFirst = true;
            
            // Generate conditional defaults (with conditions)
            foreach (var conditionalDefault in conditionalOnes)
            {
                var elsePrefix = isFirst ? "" : "else ";
                isFirst = false;

                sb.AppendLine($"            {elsePrefix}if (KeryxPars.HL7.Mapping.Core.ConditionEvaluator.Evaluate({messageVarName}, \"{conditionalDefault.Condition}\"))");
                sb.AppendLine($"            {{");
                
                // Generate the appropriate assignment based on value type
                var assignmentValue = GenerateConditionalDefaultAssignment(conditionalDefault, prop.PropertyType);
                sb.AppendLine($"                result.{prop.PropertyName} = {assignmentValue};");
                
                sb.AppendLine($"            }}");
            }
            
            // Generate unconditional default (else fallback) if present
            if (unconditionalOne.Value != null)
            {
                var elsePrefix = conditionalOnes.Count > 0 ? "else " : "";
                sb.AppendLine($"            {elsePrefix}");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                // Unconditional default (else fallback)");
                
                var assignmentValue = GenerateConditionalDefaultAssignment(unconditionalOne, prop.PropertyType);
                sb.AppendLine($"                result.{prop.PropertyName} = {assignmentValue};");
                
                sb.AppendLine($"            }}");
            }
            // If no unconditional default and not ConditionalOnly, fall back to message value
            else if (!prop.ConditionalOnly)
            {
                sb.AppendLine($"            else");
                sb.AppendLine($"            {{");
            }
        }
        
        // Only generate message mapping code if:
        // 1. There are no conditional defaults, OR
        // 2. There are conditional defaults but NO unconditional default AND ConditionalOnly is FALSE
        bool hasUnconditionalDefault = hasConditionalDefaults && 
            prop.ConditionalDefaults.Any(cd => string.IsNullOrEmpty(cd.Condition));
        
        bool shouldGenerateMessageMapping = !hasConditionalDefaults || 
            (!hasUnconditionalDefault && !prop.ConditionalOnly);
        
        if (shouldGenerateMessageMapping)
        {
            sb.AppendLine($"            var value = HL7SpanParser.GetValue({messageVarName}, {notationVar});");

            // If priority fallback fields are specified, try them in order
            if (prop.PriorityFallbackFields != null && prop.PriorityFallbackFields.Count > 0)
            {
                for (int i = 0; i < prop.PriorityFallbackFields.Count; i++)
                {
                    var priorityNotationVar = $"_Priority{i + 1}{prop.PropertyName}Notation";
                    sb.AppendLine($"            if (HL7SpanParser.IsNullOrWhiteSpace(value))");
                    sb.AppendLine($"            {{");
                    sb.AppendLine($"                value = HL7SpanParser.GetValue({messageVarName}, {priorityNotationVar});");
                    sb.AppendLine($"            }}");
                }
            }
            // Else if single fallback field is specified, try it if primary is empty
            else if (!string.IsNullOrEmpty(prop.FallbackField))
            {
                var fallbackNotationVar = $"_Fallback{prop.PropertyName}Notation";
                sb.AppendLine($"            if (HL7SpanParser.IsNullOrWhiteSpace(value))");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                value = HL7SpanParser.GetValue({messageVarName}, {fallbackNotationVar});");
                sb.AppendLine($"            }}");
            }

            // Generate type conversion based on property type
            var conversion = GenerateTypeConversion(prop);

            sb.AppendLine($"            if (!HL7SpanParser.IsNullOrWhiteSpace(value))");
            sb.AppendLine($"            {{");
            
            
            
            // Wrap in try-catch for better error handling
            sb.AppendLine($"                try");
            sb.AppendLine($"                {{");
            sb.AppendLine($"                    result.{prop.PropertyName} = {conversion};");
            sb.AppendLine($"                }}");
            sb.AppendLine($"                catch (System.Exception ex)");
            sb.AppendLine($"                {{");
            sb.AppendLine($"                    throw new KeryxPars.HL7.Mapping.HL7MappingException(");
            sb.AppendLine($"                        $\"Error converting field {{value.ToString()}} to {prop.PropertyType}\",");
            sb.AppendLine($"                        \"{prop.PropertyName}\",");
            sb.AppendLine($"                        \"{prop.FieldPath}\",");
            sb.AppendLine($"                        ex);");
            sb.AppendLine($"                }}");
            
            sb.AppendLine($"            }}");

            // Handle regular default value only if no conditional defaults
            if (!hasConditionalDefaults && prop.DefaultValue != null)
            {
                sb.AppendLine($"            else");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                result.{prop.PropertyName} = {prop.DefaultValue};");
                sb.AppendLine($"            }}");
            }
            
            // Close the "else" block if we had conditional defaults, no unconditional default, and ConditionalOnly is false
            if (hasConditionalDefaults && !prop.ConditionalOnly && !hasUnconditionalDefault)
            {
                sb.AppendLine($"            }}");
            }
        }

        sb.AppendLine($"        }}");
        
        // Close conditional block if one was opened
        if (hasCondition)
        {
            sb.AppendLine($"        }}");
        }
        
        sb.AppendLine();
    }

    private void GenerateComplexMapping(StringBuilder sb, MappedProperty prop, string messageVarName)
    {
        sb.AppendLine($"        // Map complex object {prop.PropertyName} from {prop.BaseFieldPath}");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            var fieldValue = HL7SpanParser.GetValue({messageVarName}, \"{prop.BaseFieldPath}\");");
        sb.AppendLine($"            if (!HL7SpanParser.IsNullOrWhiteSpace(fieldValue))");
        sb.AppendLine($"            {{");
        
        // Strip nullable suffix (?) from type name for mapper call
        var mapperTypeName = prop.ComplexType?.TrimEnd('?') ?? prop.ComplexType;
        
        sb.AppendLine($"                result.{prop.PropertyName} = {mapperTypeName}Mapper.MapFromField(fieldValue);");
        sb.AppendLine($"            }}");
        sb.AppendLine($"        }}");
        sb.AppendLine();
    }

    private void GenerateCollectionMapping(StringBuilder sb, MappedProperty prop, string messageVarName)
    {
        sb.AppendLine($"        // Map collection {prop.PropertyName} from {prop.SegmentId} segments");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            var segments = HL7SpanParser.FindAllSegments({messageVarName}, \"{prop.SegmentId}\");");
        sb.AppendLine($"            foreach (var segment in segments)");
        sb.AppendLine($"            {{");
        sb.AppendLine($"                var item = {prop.CollectionItemType}Mapper.MapFromSegment(segment);");
        sb.AppendLine($"                result.{prop.PropertyName}.Add(item);");
        sb.AppendLine($"            }}");
        sb.AppendLine($"        }}");
        sb.AppendLine();
    }

    private string GenerateConditionalDefaultAssignment(ConditionalDefault conditionalDefault, string targetPropertyType)
    {
        if (conditionalDefault.ValueType.StartsWith("enum:"))
        {
            // Extract enum type name
            var enumTypeName = conditionalDefault.ValueType.Substring(5); // Remove "enum:" prefix
            return $"{enumTypeName}.{conditionalDefault.Value}";
        }
        else if (conditionalDefault.ValueType == "string")
        {
            return $"\"{conditionalDefault.Value}\"";
        }
        else if (conditionalDefault.ValueType == "double" && targetPropertyType.Contains("decimal"))
        {
            // Double value being assigned to decimal property - need to cast or add 'm' suffix
            return $"(decimal){conditionalDefault.Value}";
        }
        else if (conditionalDefault.ValueType == "bool" || 
                 conditionalDefault.ValueType == "int" || 
                 conditionalDefault.ValueType == "long" ||
                 conditionalDefault.ValueType == "decimal" ||
                 conditionalDefault.ValueType == "double" ||
                 conditionalDefault.ValueType == "float")
        {
            // Primitive types - use value directly (already formatted with suffix if needed)
            return conditionalDefault.Value;
        }
        else
        {
            // Default to string
            return $"\"{conditionalDefault.Value}\"";
        }
    }

    private string GenerateTypeConversion(MappedProperty prop)
    {
        var targetType = prop.PropertyType;
        var format = prop.Format;
        var valueVar = "value";

        // Check for nullable DateTime first (before non-nullable)
        if (prop.IsNullable && prop.UnderlyingType != null && 
            (prop.UnderlyingType.Contains("DateTime") || prop.UnderlyingType == "DateTime"))
        {
            if (!string.IsNullOrEmpty(format))
            {
                return $"new NullableDateTimeConverter().Convert({valueVar}, \"{format}\")";
            }
            return $"new NullableDateTimeConverter().Convert({valueVar})";
        }

        // Handle non-nullable DateTime types
        if (targetType.Contains("DateTime") && !prop.IsNullable)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return $"new DateTimeConverter().Convert({valueVar}, \"{format}\")";
            }
            return $"new DateTimeConverter().Convert({valueVar})";
        }

        // Handle nullable enums
        if (prop.IsEnum && prop.IsNullable && prop.UnderlyingType != null)
        {
            return $"new NullableEnumConverter<{prop.UnderlyingType}>().Convert({valueVar})";
        }

        // Handle non-nullable enums
        if (prop.IsEnum && !prop.IsNullable)
        {
            return $"new EnumConverter<{targetType}>().Convert({valueVar})";
        }

        // Handle common types
        return targetType switch
        {
            "string" or "System.String" => $"{valueVar}.ToString()",
            "int" or "System.Int32" => $"int.Parse({valueVar})",
            "long" or "System.Int64" => $"long.Parse({valueVar})",
            "decimal" or "System.Decimal" => $"decimal.Parse({valueVar})",
            "double" or "System.Double" => $"double.Parse({valueVar})",
            "float" or "System.Single" => $"float.Parse({valueVar})",
            "bool" or "System.Boolean" => $"bool.Parse({valueVar})",
            _ => $"{valueVar}.ToString()" // Default to string
        };
    }

    private string GetNullableInnerType(string nullableType)
    {
        // Extract T from Nullable<T>
        var start = nullableType.IndexOf('<') + 1;
        var end = nullableType.IndexOf('>');
        if (start > 0 && end > start)
        {
            return nullableType.Substring(start, end - start);
        }
        return "string";
    }

    private ImmutableArray<MappedProperty> GetMappedProperties(
        INamedTypeSymbol classSymbol,
        Compilation compilation)
    {
        var properties = ImmutableArray.CreateBuilder<MappedProperty>();

        var hl7FieldAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7FieldAttribute");
        var hl7SegmentsAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7SegmentsAttribute");
        var hl7ComplexAttributeSymbol = compilation.GetTypeByMetadataName(
            "KeryxPars.HL7.Mapping.HL7ComplexAttribute");

        if (hl7FieldAttributeSymbol == null)
            return properties.ToImmutable();

        foreach (var member in classSymbol.GetMembers())
        {
            if (member is not IPropertySymbol property)
                continue;

            // Check for HL7FieldAttribute (can have multiple for priority-based fallback)
            var fieldAttributes = property.GetAttributes()
                .Where(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7FieldAttributeSymbol))
                .ToList();

            // Check for HL7SegmentsAttribute (collection mapping)
            var segmentsAttribute = property.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7SegmentsAttributeSymbol));

            // Check for HL7ComplexAttribute (nested object mapping)
            var complexAttribute = property.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7ComplexAttributeSymbol));

            if (fieldAttributes.Count == 0 && segmentsAttribute == null && complexAttribute == null)
                continue;

            // Handle complex/nested object mapping
            if (complexAttribute != null)
            {
                var baseFieldPath = GetNamedArgument<string>(complexAttribute, "BaseFieldPath");
                if (string.IsNullOrEmpty(baseFieldPath))
                    continue;

                var propertyType = property.Type;

                properties.Add(new MappedProperty
                {
                    PropertyName = property.Name,
                    PropertyType = propertyType.ToDisplayString(),
                    BaseFieldPath = baseFieldPath,
                    IsComplex = true,
                    ComplexType = propertyType.ToDisplayString(),
                    IsNullable = false,
                    IsEnum = false,
                    IsCollection = false
                });
                continue;
            }

            // Handle collection mapping
            if (segmentsAttribute != null)
            {
                var segmentId = segmentsAttribute.ConstructorArguments[0].Value?.ToString();
                if (string.IsNullOrEmpty(segmentId))
                    continue;

                var propertyType = property.Type;
                var collectionItemType = GetCollectionItemType(propertyType);

                properties.Add(new MappedProperty
                {
                    PropertyName = property.Name,
                    PropertyType = propertyType.ToDisplayString(),
                    SegmentId = segmentId,
                    IsCollection = true,
                    CollectionItemType = collectionItemType?.ToDisplayString(),
                    IsNullable = false,
                    IsEnum = false
                });
                continue;
            }

            // Handle regular field mapping (single or multiple attributes with priority)
            if (fieldAttributes.Count > 0)
            {
                // If multiple attributes, sort by priority (lower = higher priority)
                var sortedAttributes = fieldAttributes
                    .Select(attr => new
                    {
                        Attribute = attr,
                        Priority = GetNamedArgument<int>(attr, "Priority")
                    })
                    .OrderBy(x => x.Priority)
                    .ToList();

                // Use the first (highest priority) attribute for the main property mapping
                var primaryAttribute = sortedAttributes.First().Attribute;
                
                // Get field path from attribute
                var fieldPath = primaryAttribute.ConstructorArguments[0].Value?.ToString();


                if (string.IsNullOrEmpty(fieldPath))
                    continue;


                // Get optional properties from primary attribute
                var format = GetNamedArgument<string>(primaryAttribute, "Format");
                var defaultValue = GetNamedArgument<string>(primaryAttribute, "DefaultValue");
                var required = GetNamedArgument<bool>(primaryAttribute, "Required");
                var condition = GetNamedArgument<string>(primaryAttribute, "Condition") 
                    ?? GetNamedArgument<string>(primaryAttribute, "When");
                var skipIfEmpty = GetNamedArgument<bool>(primaryAttribute, "SkipIfEmpty");
                var conditionalOnly = GetNamedArgument<bool>(primaryAttribute, "ConditionalOnly");
                var fallbackField = GetNamedArgument<string>(primaryAttribute, "FallbackField");
                var priority = GetNamedArgument<int>(primaryAttribute, "Priority");
                
                // If multiple attributes (priority-based fallback), collect additional field paths
                var priorityFallbackFields = sortedAttributes.Count > 1
                    ? sortedAttributes.Skip(1).Select(x => x.Attribute.ConstructorArguments[0].Value?.ToString()).ToList()
                    : null;

                // Get conditional defaults
                var conditionalDefaults = new System.Collections.Generic.List<ConditionalDefault>();
                var conditionalDefaultAttrs = property.GetAttributes()
                    .Where(a => a.AttributeClass?.Name == "HL7ConditionalDefaultAttribute")
                    .ToList();


                foreach (var cdAttr in conditionalDefaultAttrs)
                {
                    if (cdAttr.ConstructorArguments.Length > 0)
                    {
                        var argument = cdAttr.ConstructorArguments[0];
                        var cdValue = argument.Value?.ToString();
                        var cdCondition = GetNamedArgument<string>(cdAttr, "Condition");
                        var cdPriority = GetNamedArgument<int>(cdAttr, "Priority");

                        if (cdValue != null)
                        {
                            // Detect the type of the value
                            string valueType;
                            string formattedValue;

                            switch (argument.Kind)
                            {
                                case TypedConstantKind.Primitive:
                                    // Determine primitive type
                                    if (argument.Type?.SpecialType == SpecialType.System_String)
                                    {
                                        valueType = "string";
                                        formattedValue = cdValue;
                                    }
                                    else if (argument.Type?.SpecialType == SpecialType.System_Int32)
                                    {
                                        valueType = "int";
                                        formattedValue = cdValue;
                                    }
                                    else if (argument.Type?.SpecialType == SpecialType.System_Int64)
                                    {
                                        valueType = "long";
                                        formattedValue = cdValue + "L";
                                    }
                                    else if (argument.Type?.SpecialType == SpecialType.System_Boolean)
                                    {
                                        valueType = "bool";
                                        formattedValue = cdValue.ToLowerInvariant();
                                    }
                                    else if (argument.Type?.SpecialType == SpecialType.System_Decimal)
                                    {
                                        valueType = "decimal";
                                        formattedValue = cdValue + "m";
                                    }
                                    else if (argument.Type?.SpecialType == SpecialType.System_Double)
                                    {
                                        valueType = "double";
                                        formattedValue = cdValue + "d";
                                    }
                                    else if (argument.Type?.SpecialType == SpecialType.System_Single)
                                    {
                                        valueType = "float";
                                        formattedValue = cdValue + "f";
                                    }
                                    else
                                    {
                                        valueType = "string";
                                        formattedValue = cdValue;
                                    }
                                    break;

                                case TypedConstantKind.Enum:
                                    // Enum value - get the member name, not the numeric value
                                    valueType = $"enum:{argument.Type?.ToDisplayString()}";
                                    
                                    // For enums, we need to find the actual enum member name
                                    // The Value gives us the numeric value, but we need the member name
                                    if (argument.Type is INamedTypeSymbol enumType && argument.Value != null)
                                    {
                                        var enumValue = argument.Value;
                                        // Find the enum member with this value
                                        var enumMember = enumType.GetMembers()
                                            .OfType<IFieldSymbol>()
                                            .FirstOrDefault(f => f.IsConst && 
                                                f.ConstantValue?.Equals(enumValue) == true);
                                        
                                        formattedValue = enumMember?.Name ?? enumValue.ToString();
                                    }
                                    else
                                    {
                                        formattedValue = cdValue;
                                    }
                                    break;

                                default:
                                    valueType = "string";
                                    formattedValue = cdValue;
                                    break;
                            }

                            conditionalDefaults.Add(new ConditionalDefault
                            {
                                Value = formattedValue,
                                ValueType = valueType,
                                Condition = cdCondition,
                                Priority = cdPriority
                            });
                        }
                    }
                }

                // Sort conditional defaults by priority
                conditionalDefaults.Sort((a, b) => a.Priority.CompareTo(b.Priority));

                // Get type information
                var propertyType = property.Type;
                var typeString = propertyType.ToDisplayString();
                var isNullable = propertyType.NullableAnnotation == NullableAnnotation.Annotated ||
                               (propertyType is INamedTypeSymbol namedType && 
                                namedType.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T);
                var isEnum = propertyType.TypeKind == TypeKind.Enum ||
                            (propertyType is INamedTypeSymbol nullableType && 
                             nullableType.TypeArguments.FirstOrDefault()?.TypeKind == TypeKind.Enum);

                properties.Add(new MappedProperty
                {
                    PropertyName = property.Name,
                    PropertyType = typeString,
                    FieldPath = fieldPath,
                    Format = format,
                    DefaultValue = defaultValue,
                    Required = required,
                    IsNullable = isNullable,
                    IsEnum = isEnum,
                    UnderlyingType = GetUnderlyingType(propertyType),
                    IsCollection = false,
                    Condition = condition,
                    SkipIfEmpty = skipIfEmpty,
                    ConditionalDefaults = conditionalDefaults.Count > 0 ? conditionalDefaults : null,
                    ConditionalOnly = conditionalOnly,
                    FallbackField = fallbackField,
                    Priority = priority,
                    PriorityFallbackFields = priorityFallbackFields
                });
            }
        }

        return properties.ToImmutable();
    }

    private ITypeSymbol? GetCollectionItemType(ITypeSymbol typeSymbol)
    {
        if (typeSymbol is INamedTypeSymbol namedType)
        {
            // Check for List<T>, IEnumerable<T>, ICollection<T>, etc.
            if (namedType.TypeArguments.Length == 1)
            {
                return namedType.TypeArguments[0];
            }
        }
        return null;
    }

    private string? GetUnderlyingType(ITypeSymbol typeSymbol)
    {
        if (typeSymbol is INamedTypeSymbol namedType &&
            namedType.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            return namedType.TypeArguments.FirstOrDefault()?.ToDisplayString();
        }
        return null;
    }

    private T? GetNamedArgument<T>(AttributeData attribute, string name)
    {
        var namedArg = attribute.NamedArguments
            .FirstOrDefault(kvp => kvp.Key == name);

        if (namedArg.Value.Value is T value)
            return value;

        return default;
    }

    /// <summary>
    /// Represents a property that should be mapped.
    /// </summary>
    private readonly struct MappedProperty
    {
        public string PropertyName { get; init; }
        public string PropertyType { get; init; }
        public string? FieldPath { get; init; }
        public string? Format { get; init; }
        public string? DefaultValue { get; init; }
        public bool Required { get; init; }
        public bool IsNullable { get; init; }
        public bool IsEnum { get; init; }
        public string? UnderlyingType { get; init; }
        public bool IsCollection { get; init; }
        public string? SegmentId { get; init; }
        public string? CollectionItemType { get; init; }
        public bool IsComplex { get; init; }
        public string? BaseFieldPath { get; init; }
        public string? ComplexType { get; init; }
        public string? Condition { get; init; }
        public bool SkipIfEmpty { get; init; }
        public System.Collections.Generic.List<ConditionalDefault>? ConditionalDefaults { get; init; }
        public bool ConditionalOnly { get; init; }
        public string? FallbackField { get; init; }
        public int Priority { get; init; }
        public System.Collections.Generic.List<string>? PriorityFallbackFields { get; init; }
    }

    /// <summary>
    /// Represents a conditional default value with type information.
    /// </summary>
    private readonly struct ConditionalDefault
    {
        public string Value { get; init; }
        public string ValueType { get; init; } // "string", "int", "bool", "decimal", "enum:FullTypeName"
        public string? Condition { get; init; }
        public int Priority { get; init; }
    }

    private void GenerateBuildHL7Method(
        StringBuilder sb,
        string className,
        ImmutableArray<MappedProperty> properties,
        System.Collections.Generic.List<string?> messageTypes)
    {
        sb.AppendLine("    /// <summary>");
        sb.AppendLine($"    /// Builds an HL7 message from {className} (reverse mapping).");
        sb.AppendLine("    /// </summary>");
        sb.AppendLine($"    public static string BuildHL7({className} source)");
        sb.AppendLine("    {");
        sb.AppendLine("        if (source == null) throw new System.ArgumentNullException(nameof(source));");
        sb.AppendLine();
        sb.AppendLine("        var sb = new System.Text.StringBuilder(2048);");
        sb.AppendLine();
        
        // Build segments
        sb.AppendLine("        // TODO: Build complete HL7 message with all required segments");
        sb.AppendLine("        // For now, return a simple representation");
        sb.AppendLine("        return sb.ToString();");
        sb.AppendLine("    }");
    }
}

