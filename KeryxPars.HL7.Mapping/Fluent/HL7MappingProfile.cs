using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KeryxPars.HL7.Mapping.Fluent;

/// <summary>
/// Base class for defining HL7 mapping profiles using a fluent API.
/// Provides an alternative to attribute-based mapping with runtime configuration.
/// </summary>
/// <typeparam name="TDestination">The domain model type to map to</typeparam>
public abstract class HL7MappingProfile<TDestination> where TDestination : class, new()
{
    private readonly List<string> _messageTypes = new();
    private readonly List<PropertyMapping<TDestination>> _propertyMappings = new();
    private readonly List<ComplexPropertyMapping<TDestination>> _complexMappings = new();
    private readonly List<CollectionMapping<TDestination>> _collectionMappings = new();

    /// <summary>
    /// Gets the message types this profile applies to (e.g., "ADT^A01", "ORU^R01").
    /// </summary>
    public IReadOnlyList<string> MessageTypes => _messageTypes.AsReadOnly();

    /// <summary>
    /// Gets the configured property mappings.
    /// </summary>
    public IReadOnlyList<PropertyMapping<TDestination>> PropertyMappings => _propertyMappings.AsReadOnly();

    /// <summary>
    /// Gets the configured complex property mappings.
    /// </summary>
    public IReadOnlyList<ComplexPropertyMapping<TDestination>> ComplexMappings => _complexMappings.AsReadOnly();

    /// <summary>
    /// Gets the configured collection mappings.
    /// </summary>
    public IReadOnlyList<CollectionMapping<TDestination>> CollectionMappings => _collectionMappings.AsReadOnly();

    /// <summary>
    /// Specifies which HL7 message types this profile applies to.
    /// </summary>
    public void ForMessages(params string[] messageTypes)
    {
        _messageTypes.AddRange(messageTypes);
    }

    /// <summary>
    /// Configures mapping for a simple property.
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> Map<TProperty>(
        Expression<Func<TDestination, TProperty>> propertyExpression)
    {
        var mapping = new PropertyMapping<TDestination>
        {
            PropertyExpression = propertyExpression,
            PropertyName = GetPropertyName(propertyExpression)
        };

        _propertyMappings.Add(mapping);
        return new PropertyMappingBuilder<TDestination, TProperty>(mapping);
    }

    /// <summary>
    /// Configures mapping for a complex/nested property.
    /// </summary>
    public ComplexMappingBuilder<TDestination, TProperty> MapComplex<TProperty>(
        Expression<Func<TDestination, TProperty>> propertyExpression)
        where TProperty : class, new()
    {
        var mapping = new ComplexPropertyMapping<TDestination>
        {
            PropertyExpression = propertyExpression,
            PropertyName = GetPropertyName(propertyExpression)
        };

        _complexMappings.Add(mapping);
        return new ComplexMappingBuilder<TDestination, TProperty>(mapping);
    }

    /// <summary>
    /// Configures mapping for a collection property.
    /// </summary>
    public CollectionMappingBuilder<TDestination, TItem> MapCollection<TItem>(
        Expression<Func<TDestination, ICollection<TItem>>> propertyExpression)
        where TItem : class, new()
    {
        var mapping = new CollectionMapping<TDestination>
        {
            PropertyExpression = propertyExpression,
            PropertyName = GetPropertyName(propertyExpression)
        };

        _collectionMappings.Add(mapping);
        return new CollectionMappingBuilder<TDestination, TItem>(mapping);
    }

    private static string GetPropertyName<TProperty>(Expression<Func<TDestination, TProperty>> expression)
    {
        if (expression.Body is MemberExpression memberExpr)
        {
            return memberExpr.Member.Name;
        }

        throw new ArgumentException("Expression must be a simple property accessor", nameof(expression));
    }
}

/// <summary>
/// Represents a configured property mapping.
/// </summary>
public class PropertyMapping<TDestination>
{
    public Expression? PropertyExpression { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string? FieldPath { get; set; }
    public string? Format { get; set; }
    public object? DefaultValue { get; set; }
    public bool Required { get; set; }
    public Type? ConverterType { get; set; }
    public Delegate? CustomConverter { get; set; }
}

/// <summary>
/// Represents a configured complex property mapping.
/// </summary>
public class ComplexPropertyMapping<TDestination>
{
    public Expression? PropertyExpression { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string? BaseFieldPath { get; set; }
    public Type? MapperType { get; set; }
    public Delegate? CustomMapper { get; set; }
}

/// <summary>
/// Represents a configured collection mapping.
/// </summary>
public class CollectionMapping<TDestination>
{
    public Expression? PropertyExpression { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string? SegmentId { get; set; }
    public Type? ItemType { get; set; }
    public Type? ItemMapperType { get; set; }
    public Delegate? ItemMapper { get; set; }
}
