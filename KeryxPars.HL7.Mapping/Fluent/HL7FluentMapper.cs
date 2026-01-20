using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using KeryxPars.HL7.Mapping.Parsers;

namespace KeryxPars.HL7.Mapping.Fluent;

/// <summary>
/// Runtime HL7 mapper that uses fluent configuration profiles.
/// Provides an alternative to attribute-based mapping with dynamic configuration.
/// </summary>
public class HL7FluentMapper
{
    private readonly ConcurrentDictionary<Type, object> _profiles = new();

    /// <summary>
    /// Registers a mapping profile.
    /// </summary>
    public void RegisterProfile<TProfile>() where TProfile : class, new()
    {
        var profile = new TProfile();
        var profileType = typeof(TProfile);
        
        // Get the destination type from the base generic type
        var baseType = profileType.BaseType;
        while (baseType != null && (!baseType.IsGenericType || baseType.GetGenericTypeDefinition() != typeof(HL7MappingProfile<>)))
        {
            baseType = baseType.BaseType;
        }

        if (baseType == null)
            throw new InvalidOperationException($"{profileType.Name} must inherit from HL7MappingProfile<T>");

        var destinationType = baseType.GetGenericArguments()[0];
        _profiles[destinationType] = profile;
    }

    /// <summary>
    /// Maps an HL7 message to a domain model using registered profiles.
    /// </summary>
    public TDestination Map<TDestination>(string hl7Message) where TDestination : class, new()
    {
        if (hl7Message == null)
            throw new ArgumentNullException(nameof(hl7Message));

        return MapFromSpan<TDestination>(hl7Message.AsSpan());
    }

    /// <summary>
    /// Maps an HL7 message span to a domain model using registered profiles.
    /// </summary>
    public TDestination MapFromSpan<TDestination>(ReadOnlySpan<char> hl7Message) where TDestination : class, new()
    {
        if (!_profiles.TryGetValue(typeof(TDestination), out var profileObj))
        {
            throw new InvalidOperationException(
                $"No profile registered for {typeof(TDestination).Name}. Call RegisterProfile<T>() first.");
        }

        var profile = (HL7MappingProfile<TDestination>)profileObj;
        return MapUsingProfile(hl7Message, profile);
    }

    private TDestination MapUsingProfile<TDestination>(
        ReadOnlySpan<char> hl7Message,
        HL7MappingProfile<TDestination> profile) where TDestination : class, new()
    {
        var result = new TDestination();

        // Map simple properties
        foreach (var propMapping in profile.PropertyMappings)
        {
            if (string.IsNullOrEmpty(propMapping.FieldPath))
                continue;

            var value = HL7SpanParser.GetValue(hl7Message, propMapping.FieldPath);
            
            if (HL7SpanParser.IsNullOrWhiteSpace(value))
            {
                if (propMapping.Required)
                {
                    throw new HL7MappingException(
                        $"Required field {propMapping.FieldPath} is empty",
                        propMapping.PropertyName,
                        propMapping.FieldPath);
                }

                if (propMapping.DefaultValue != null)
                {
                    SetPropertyValue(result, propMapping.PropertyName, propMapping.DefaultValue);
                }
                continue;
            }

            // Fluent API: convert to string for reflection compatibility
            var valueStr = value.ToString();
            object? convertedValue = valueStr;

            // TODO: Full converter support - for now use ToString()
            // Source-generated mappers handle full type conversion with zero overhead

            SetPropertyValue(result, propMapping.PropertyName, convertedValue);
        }

        // Map complex properties
        // NOTE: Complex mapping via reflection has limitations with ReadOnlySpan
        // Use source-generated mappers for full performance and complex type support
        foreach (var complexMapping in profile.ComplexMappings)
        {
            // TODO: Implement via compiled expressions in future version
            // For now, use attribute-based source generators for complex types
        }

        // Map collections  
        // NOTE: Collection mapping via reflection has limitations with ReadOnlySpan
        // Use source-generated mappers for full performance and collection support
        foreach (var collectionMapping in profile.CollectionMappings)
        {
            // TODO: Implement via compiled expressions in future version
            // For now, use attribute-based source generators for collections
        }

        return result;
    }

    private static void SetPropertyValue(object target, string propertyName, object? value)
    {
        var property = target.GetType().GetProperty(propertyName);
        if (property != null && property.CanWrite)
        {
            property.SetValue(target, value);
        }
    }
}

