using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeryxPars.HL7.SourceGenerators;

/// <summary>
/// Generates optimized AddSegment() methods for HL7 message classes.
/// Detects properties of type ISegment or List&lt;ISegment&gt; and creates
/// a switch statement for efficient segment assignment.
/// </summary>
[Generator]
public class SegmentHandlerGenerator : IIncrementalGenerator
{
    private const string GenerateSegmentHandlersAttribute = "KeryxPars.HL7.Attributes.GenerateSegmentHandlersAttribute";
    private const string IgnoreSegmentAttribute = "KeryxPars.HL7.Attributes.IgnoreSegmentAttribute";
    private const string SegmentHandlerAttribute = "KeryxPars.HL7.Attributes.SegmentHandlerAttribute";
    private const string ISegmentInterface = "KeryxPars.HL7.Contracts.ISegment";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Find classes with [GenerateSegmentHandlers]
        var classDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                GenerateSegmentHandlersAttribute,
                predicate: (node, _) => node is ClassDeclarationSyntax,
                transform: GetClassToGenerate)
            .Where(m => m is not null);

        context.RegisterSourceOutput(classDeclarations,
            (spc, source) => Execute(source!, spc));
    }

    private static ClassToGenerate? GetClassToGenerate(
        GeneratorAttributeSyntaxContext context,
        System.Threading.CancellationToken ct)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.TargetNode;
        var classSymbol = context.TargetSymbol as INamedTypeSymbol;

        if (classSymbol is null) return null;

        // Find all properties that are ISegment or List<ISegment>
        var segmentProperties = classSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => IsSegmentProperty(p, context.SemanticModel.Compilation))
            .Select(p => GetPropertyInfo(p, context.SemanticModel.Compilation))
            .OrderBy(p => p.Priority)
            .ThenBy(p => p.Name)
            .ToList();

        if (segmentProperties.Count == 0)
            return null;

        return new ClassToGenerate(
            classSymbol.Name,
            classSymbol.ContainingNamespace.ToDisplayString(),
            segmentProperties);
    }

    private static bool IsSegmentProperty(IPropertySymbol property, Compilation compilation)
    {
        // Check if property has [IgnoreSegment]
        if (property.GetAttributes().Any(a =>
            a.AttributeClass?.ToDisplayString() == IgnoreSegmentAttribute))
            return false;

        var iSegmentSymbol = compilation.GetTypeByMetadataName(ISegmentInterface);
        if (iSegmentSymbol is null)
            return false;

        // Check if type is ISegment
        if (ImplementsInterface(property.Type, iSegmentSymbol))
            return true;

        // Check if type is List<ISegment>
        if (property.Type is INamedTypeSymbol namedType &&
            namedType.IsGenericType &&
            namedType.ConstructedFrom.ToDisplayString() == "System.Collections.Generic.List<T>")
        {
            var elementType = namedType.TypeArguments[0];
            return ImplementsInterface(elementType, iSegmentSymbol);
        }

        return false;
    }

    private static bool ImplementsInterface(ITypeSymbol type, INamedTypeSymbol interfaceSymbol)
    {
        if (SymbolEqualityComparer.Default.Equals(type, interfaceSymbol))
            return true;

        return type.AllInterfaces.Any(i => SymbolEqualityComparer.Default.Equals(i, interfaceSymbol));
    }

    private static PropertyInfo GetPropertyInfo(IPropertySymbol property, Compilation compilation)
    {
        var isList = false;
        ITypeSymbol segmentType = property.Type;

        // Check if it's a List<T>
        if (property.Type is INamedTypeSymbol namedType &&
            namedType.IsGenericType &&
            namedType.ConstructedFrom.ToDisplayString() == "System.Collections.Generic.List<T>")
        {
            isList = true;
            segmentType = namedType.TypeArguments[0];
        }

        // Get priority from attribute
        var priority = 0;
        var handlerAttr = property.GetAttributes()
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == SegmentHandlerAttribute);
        
        if (handlerAttr is not null)
        {
            var priorityArg = handlerAttr.NamedArguments.FirstOrDefault(na => na.Key == "Priority");
            if (priorityArg.Value.Value is int priorityValue)
                priority = priorityValue;
        }

        var segmentTypeName = segmentType.Name;
        var localVarName = char.ToLower(segmentTypeName[0]) + segmentTypeName.Substring(1);

        return new PropertyInfo(
            property.Name,
            segmentTypeName,
            isList,
            localVarName,
            priority);
    }

    private static void Execute(ClassToGenerate classToGenerate, SourceProductionContext context)
    {
        var source = GenerateSource(classToGenerate);
        context.AddSource($"{classToGenerate.ClassName}.g.cs", source);
    }

    private static string GenerateSource(ClassToGenerate data)
    {
        var sb = new StringBuilder();

        sb.AppendLine("// <auto-generated/>");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine($"namespace {data.Namespace}");
        sb.AppendLine("{");
        sb.AppendLine($"    partial class {data.ClassName}");
        sb.AppendLine("    {");
        sb.AppendLine("        /// <summary>");
        sb.AppendLine("        /// Generated method to add segments to the message.");
        sb.AppendLine("        /// Switch statement optimized by priority and segment frequency.");
        sb.AppendLine("        /// </summary>");
        sb.AppendLine("        public override void AddSegment(global::KeryxPars.HL7.Contracts.ISegment segment)");
        sb.AppendLine("        {");
        sb.AppendLine("            switch (segment)");
        sb.AppendLine("            {");

        // Generate cases for each property
        foreach (var prop in data.Properties)
        {
            var assignment = prop.IsList
                ? $"{prop.Name}.Add({prop.LocalVarName});"
                : $"{prop.Name} = {prop.LocalVarName};";

            sb.AppendLine($"                case global::KeryxPars.HL7.Segments.{prop.SegmentType} {prop.LocalVarName}:");
            sb.AppendLine($"                    {assignment}");
            sb.AppendLine("                    return;");
        }

        sb.AppendLine("                default:");
        sb.AppendLine("                    base.AddSegment(segment);");
        sb.AppendLine("                    return;");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }
}

internal record ClassToGenerate(
    string ClassName,
    string Namespace,
    List<PropertyInfo> Properties);

internal record PropertyInfo(
    string Name,
    string SegmentType,
    bool IsList,
    string LocalVarName,
    int Priority);
