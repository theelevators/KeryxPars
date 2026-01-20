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

            // Check if class has HL7MessageAttribute
            var hl7MessageAttribute = classSymbol.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7MessageAttributeSymbol));

            // Check if class has any properties with HL7FieldAttribute (for collection items)
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

            // Generate mapper if it has HL7Message OR has HL7Field attributes OR has HL7Component attributes
            if (hl7MessageAttribute != null || hasFieldAttributes || hasComponentAttributes)
            {
                // Generate the mapper for this class
                var sourceCode = GenerateMapperClass(classSymbol, hl7MessageAttribute, compilation);

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
        var messageTypes = hl7MessageAttribute?.ConstructorArguments[0].Values
            .Select(v => v.Value?.ToString())
            .Where(v => v != null)
            .ToList() ?? new System.Collections.Generic.List<string?>();

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

        sb.AppendLine("}");

        return sb.ToString();
    }

    private void GenerateFieldNotationCache(StringBuilder sb, ImmutableArray<MappedProperty> properties)
    {
        sb.AppendLine("    #region Field Notation Cache");
        sb.AppendLine();

        foreach (var prop in properties)
        {
            if (prop.FieldPath != null)
            {
                var fieldName = $"_{prop.PropertyName}Notation";
                sb.AppendLine($"    private static readonly FieldNotation {fieldName} = ");
                sb.AppendLine($"        FieldNotation.Parse(\"{prop.FieldPath}\");");
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
                Attribute = p.GetAttributes()
                    .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7ComponentAttributeSymbol))
            })
            .Where(x => x.Attribute != null)
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

        foreach (var componentProp in componentProperties)
        {
            var componentIndex = componentProp.Attribute!.ConstructorArguments[0].Value;
            var propertyName = componentProp.Property.Name;
            var propertyType = componentProp.Property.Type.ToDisplayString();

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
        sb.AppendLine($"        {{");
        sb.AppendLine($"            var value = HL7SpanParser.GetValue({messageVarName}, {notationVar});");

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

        // Handle default value if specified
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

    private void GenerateComplexMapping(StringBuilder sb, MappedProperty prop, string messageVarName)
    {
        sb.AppendLine($"        // Map complex object {prop.PropertyName} from {prop.BaseFieldPath}");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            var fieldValue = HL7SpanParser.GetValue({messageVarName}, \"{prop.BaseFieldPath}\");");
        sb.AppendLine($"            if (!HL7SpanParser.IsNullOrWhiteSpace(fieldValue))");
        sb.AppendLine($"            {{");
        sb.AppendLine($"                result.{prop.PropertyName} = {prop.ComplexType}Mapper.MapFromField(fieldValue);");
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

            // Check for HL7FieldAttribute
            var fieldAttribute = property.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7FieldAttributeSymbol));

            // Check for HL7SegmentsAttribute (collection mapping)
            var segmentsAttribute = property.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7SegmentsAttributeSymbol));

            // Check for HL7ComplexAttribute (nested object mapping)
            var complexAttribute = property.GetAttributes()
                .FirstOrDefault(ad => SymbolEqualityComparer.Default.Equals(ad.AttributeClass, hl7ComplexAttributeSymbol));

            if (fieldAttribute == null && segmentsAttribute == null && complexAttribute == null)
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

            // Handle regular field mapping
            if (fieldAttribute != null)
            {
                // Get field path from attribute
                var fieldPath = fieldAttribute.ConstructorArguments[0].Value?.ToString();

                if (string.IsNullOrEmpty(fieldPath))
                    continue;

                // Get optional properties
                var format = GetNamedArgument<string>(fieldAttribute, "Format");
                var defaultValue = GetNamedArgument<string>(fieldAttribute, "DefaultValue");
                var required = GetNamedArgument<bool>(fieldAttribute, "Required");

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
                    IsCollection = false
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
    }
}
