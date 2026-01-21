using System;

namespace KeryxPars.HL7.Mapping.Fluent;

/// <summary>
/// Fluent builder for configuring property mappings.
/// </summary>
public class PropertyMappingBuilder<TDestination, TProperty>
{
    private readonly PropertyMapping<TDestination> _mapping;

    internal PropertyMappingBuilder(PropertyMapping<TDestination> mapping)
    {
        _mapping = mapping;
    }

    /// <summary>
    /// Specifies the HL7 field path to map from (e.g., "PID.5.1").
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> FromField(string fieldPath)
    {
        _mapping.FieldPath = fieldPath;
        return this;
    }

    /// <summary>
    /// Specifies a format string for type conversion (e.g., date format).
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> WithFormat(string format)
    {
        _mapping.Format = format;
        return this;
    }

    /// <summary>
    /// Specifies a default value if the field is empty.
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> WithDefault(TProperty defaultValue)
    {
        _mapping.DefaultValue = defaultValue;
        return this;
    }

    /// <summary>
    /// Marks this field as required (throws if empty).
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> Required()
    {
        _mapping.Required = true;
        return this;
    }

    /// <summary>
    /// Specifies a custom type converter.
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> Using<TConverter>()
    {
        _mapping.ConverterType = typeof(TConverter);
        return this;
    }

    /// <summary>
    /// Specifies a custom conversion function (works with string input).
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> ConvertUsing(Func<string, TProperty> converter)
    {
        _mapping.CustomConverter = converter;
        return this;
    }

    /// <summary>
    /// Configures DateTime conversion with a specific format.
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> AsDateTime(string format = "yyyyMMdd")
    {
        _mapping.Format = format;
        _mapping.ConverterType = typeof(Converters.DateTimeConverter);
        return this;
    }

    /// <summary>
    /// Configures enum conversion.
    /// </summary>
    public PropertyMappingBuilder<TDestination, TProperty> AsEnum<TEnum>() where TEnum : struct, Enum
    {
        _mapping.ConverterType = typeof(Converters.EnumConverter<TEnum>);
        return this;
    }
}

/// <summary>
/// Fluent builder for configuring complex property mappings.
/// </summary>
public class ComplexMappingBuilder<TDestination, TProperty> where TProperty : class, new()
{
    private readonly ComplexPropertyMapping<TDestination> _mapping;

    internal ComplexMappingBuilder(ComplexPropertyMapping<TDestination> mapping)
    {
        _mapping = mapping;
    }

    /// <summary>
    /// Specifies the base field path for the complex type (e.g., "PID.5").
    /// </summary>
    public ComplexMappingBuilder<TDestination, TProperty> FromField(string baseFieldPath)
    {
        _mapping.BaseFieldPath = baseFieldPath;
        return this;
    }

    /// <summary>
    /// Specifies a custom mapper type for the complex property.
    /// </summary>
    public ComplexMappingBuilder<TDestination, TProperty> Using(Type mapperType)
    {
        _mapping.MapperType = mapperType;
        return this;
    }
}

/// <summary>
/// Fluent builder for configuring collection mappings.
/// </summary>
public class CollectionMappingBuilder<TDestination, TItem> where TItem : class, new()
{
    private readonly CollectionMapping<TDestination> _mapping;

    internal CollectionMappingBuilder(CollectionMapping<TDestination> mapping)
    {
        _mapping = mapping;
        _mapping.ItemType = typeof(TItem);
    }

    /// <summary>
    /// Specifies which segments to map from (e.g., "OBX").
    /// </summary>
    public CollectionMappingBuilder<TDestination, TItem> FromSegments(string segmentId)
    {
        _mapping.SegmentId = segmentId;
        return this;
    }

    /// <summary>
    /// Specifies a custom mapper for collection items.
    /// </summary>
    public CollectionMappingBuilder<TDestination, TItem> UsingItemMapper(Type mapperType)
    {
        _mapping.ItemMapperType = mapperType;
        return this;
    }
}
