using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KeryxPars.Core.Models;
using KeryxPars.HL7.Mapping.Converters;

namespace KeryxPars.HL7.Mapping.Fluent;

/// <summary>
/// Fluent builder for creating HL7 mappers without profile classes.
/// Provides an inline, chainable API for mapping HL7 messages.
/// </summary>
/// <typeparam name="TDestination">The type to map to.</typeparam>
/// <remarks>
/// <para>
/// This builder allows you to configure mappings inline without creating
/// a separate profile class. Perfect for simple, one-off mappings.
/// </para>
/// <para>
/// Uses Result&lt;T, E&gt; pattern instead of exceptions for better performance
/// and functional programming style.
/// </para>
/// <example>
/// <code>
/// var result = HL7FluentMapper.Create&lt;Patient&gt;()
///     .Map(x => x.PatientId).FromField("PID.3")
///     .Map(x => x.DateOfBirth).FromField("PID.7").AsDateTime("yyyyMMdd")
///     .Parse(hl7Message);
///     
/// if (result.IsSuccess)
/// {
///     var patient = result.Value;
/// }
/// </code>
/// </example>
/// </remarks>
public class HL7MappingBuilder<TDestination> where TDestination : class, new()
{
    private readonly HL7MappingProfile<TDestination> _profile;
    private readonly HL7FluentMapper _mapper;

    internal HL7MappingBuilder()
    {
        _profile = new InlineProfile<TDestination>();
        _mapper = new HL7FluentMapper();
    }

    /// <summary>
    /// Starts mapping a property.
    /// </summary>
    /// <typeparam name="TProperty">The property type.</typeparam>
    /// <param name="propertyExpression">Expression selecting the property.</param>
    /// <returns>A property mapping builder for fluent configuration.</returns>
    public PropertyMappingBuilder<TDestination, TProperty> Map<TProperty>(
        Expression<Func<TDestination, TProperty>> propertyExpression)
    {
        return _profile.Map(propertyExpression);
    }

    /// <summary>
    /// Starts mapping a complex property.
    /// </summary>
    /// <typeparam name="TProperty">The complex property type.</typeparam>
    /// <param name="propertyExpression">Expression selecting the property.</param>
    /// <returns>A complex mapping builder for fluent configuration.</returns>
    public ComplexMappingBuilder<TDestination, TProperty> MapComplex<TProperty>(
        Expression<Func<TDestination, TProperty>> propertyExpression) 
        where TProperty : class, new()
    {
        return _profile.MapComplex(propertyExpression);
    }

    /// <summary>
    /// Starts mapping a collection property.
    /// </summary>
    /// <typeparam name="TItem">The collection item type.</typeparam>
    /// <param name="propertyExpression">Expression selecting the property.</param>
    /// <returns>A collection mapping builder for fluent configuration.</returns>
    public CollectionMappingBuilder<TDestination, TItem> MapCollection<TItem>(
        Expression<Func<TDestination, ICollection<TItem>>> propertyExpression)
        where TItem : class, new()
    {
        return _profile.MapCollection(propertyExpression);
    }

    /// <summary>
    /// Specifies which message types this mapper handles.
    /// </summary>
    /// <param name="messageTypes">Message type strings (e.g., "ADT^A01").</param>
    /// <returns>This builder for chaining.</returns>
    public HL7MappingBuilder<TDestination> ForMessages(params string[] messageTypes)
    {
        _profile.ForMessages(messageTypes);
        return this;
    }

    /// <summary>
    /// Parses an HL7 message using the configured mappings.
    /// Returns a Result instead of throwing exceptions.
    /// </summary>
    /// <param name="hl7Message">The HL7 message as a string.</param>
    /// <returns>Result containing the mapped instance or error.</returns>
    public Result<TDestination, HL7MappingError> Parse(string hl7Message)
    {
        if (hl7Message == null)
            return new HL7MappingError("HL7 message cannot be null", "N/A", "N/A");

        return Parse(hl7Message.AsSpan());
    }

    /// <summary>
    /// Parses an HL7 message using the configured mappings.
    /// Returns a Result instead of throwing exceptions.
    /// </summary>
    /// <param name="hl7Message">The HL7 message as a read-only character span.</param>
    /// <returns>Result containing the mapped instance or error.</returns>
    public Result<TDestination, HL7MappingError> Parse(ReadOnlySpan<char> hl7Message)
    {
        try
        {
            _mapper.RegisterProfile(_profile);
            var result = _mapper.MapFromSpan<TDestination>(hl7Message);
            return Result<TDestination, HL7MappingError>.Success(result);
        }
        catch (HL7MappingException ex)
        {
            return new HL7MappingError(ex.Message, ex.FieldPath, ex.TargetType);
        }
        catch (Exception ex)
        {
            return new HL7MappingError($"Unexpected error: {ex.Message}", "N/A", typeof(TDestination).Name);
        }
    }

    /// <summary>
    /// Legacy method for backward compatibility. Use Parse() which returns Result instead.
    /// </summary>
    [Obsolete("Use Parse() which returns Result<T, E> instead of throwing exceptions")]
    public bool TryParse(string? hl7Message, out TDestination? result)
    {
        if (hl7Message == null)
        {
            result = null;
            return false;
        }

        return TryParse(hl7Message.AsSpan(), out result);
    }

    /// <summary>
    /// Legacy method for backward compatibility. Use Parse() which returns Result instead.
    /// </summary>
    [Obsolete("Use Parse() which returns Result<T, E> instead of throwing exceptions")]
    public bool TryParse(ReadOnlySpan<char> hl7Message, out TDestination? result)
    {
        var parseResult = Parse(hl7Message);
        if (parseResult.IsSuccess)
        {
            result = parseResult.Value;
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Builds a reusable mapper function that can be called multiple times.
    /// Returns Result instead of throwing exceptions.
    /// </summary>
    /// <returns>A function that maps HL7 message strings to Result.</returns>
    /// <remarks>
    /// Use this when you need to map many messages with the same configuration.
    /// The returned function is thread-safe and can be cached.
    /// </remarks>
    public Func<string, Result<TDestination, HL7MappingError>> Build()
    {
        _mapper.RegisterProfile(_profile);
        return (hl7Message) =>
        {
            try
            {
                var result = _mapper.Map<TDestination>(hl7Message);
                return Result<TDestination, HL7MappingError>.Success(result);
            }
            catch (HL7MappingException ex)
            {
                return new HL7MappingError(ex.Message, ex.FieldPath, ex.TargetType);
            }
            catch (Exception ex)
            {
                return new HL7MappingError($"Unexpected error: {ex.Message}", "N/A", typeof(TDestination).Name);
            }
        };
    }
}

/// <summary>
/// Internal profile used by the builder.
/// </summary>
internal class InlineProfile<TDestination> : HL7MappingProfile<TDestination> where TDestination : class, new()
{
    public InlineProfile()
    {
        // No default configuration
    }
}


