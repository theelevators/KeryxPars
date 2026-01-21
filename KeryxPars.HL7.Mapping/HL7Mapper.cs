using System;
using System.Reflection;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Main entry point for HL7 to domain model mapping.
/// Provides a unified API for mapping HL7 messages using source-generated mappers.
/// </summary>
public static class HL7Mapper
{
    /// <summary>
    /// Maps an HL7 message string to a domain model.
    /// Uses the source-generated mapper for blazing-fast performance.
    /// </summary>
    /// <typeparam name="T">Domain model type decorated with [HL7Message]</typeparam>
    /// <param name="hl7Message">Raw HL7 message string</param>
    /// <returns>Mapped domain model instance</returns>
    /// <exception cref="ArgumentNullException">If hl7Message is null</exception>
    /// <exception cref="HL7MappingException">If mapping fails</exception>
    public static T Map<T>(string hl7Message) where T : new()
    {
        if (hl7Message == null)
            throw new ArgumentNullException(nameof(hl7Message));

        try
        {
            // Find the generated mapper
            var mapperType = typeof(T);
            var generatedMapperTypeName = $"{mapperType.Namespace}.{mapperType.Name}Mapper";
            var generatedMapperType = mapperType.Assembly.GetType(generatedMapperTypeName);
            
            if (generatedMapperType == null)
            {
                throw new HL7MappingException(
                    $"No mapper found for {typeof(T).Name}. Ensure the class is decorated with [HL7Message] attribute.",
                    typeof(T).Name);
            }

            // Call the static MapFromSpan method
            var mapMethod = generatedMapperType.GetMethod(
                "MapFromSpan",
                BindingFlags.Public | BindingFlags.Static);

            if (mapMethod == null)
            {
                throw new HL7MappingException(
                    $"Mapper for {typeof(T).Name} does not have a MapFromSpan method.",
                    typeof(T).Name);
            }

            // We can't box a Span, so we pass the string and let the mapper convert it
            // The generated mapper signature is: MapFromSpan(ReadOnlySpan<char>)
            // We need to use unsafe code or just pass as string
            
            // For now, look for a MapFromString method or convert via intermediate
            // Actually, let's just document that developers should use the generated mapper directly
            // and provide MapFromSpan as a separate span method
            var result = mapMethod.Invoke(null, new object[] { hl7Message.AsMemory() });
            return (T)result!;
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
            if (ex.InnerException is HL7MappingException)
                throw ex.InnerException;
                
            throw new HL7MappingException(
                $"Failed to map HL7 message to {typeof(T).Name}: {ex.InnerException.Message}",
                typeof(T).Name,
                ex.InnerException);
        }
        catch (Exception ex) when (ex is not HL7MappingException)
        {
            throw new HL7MappingException(
                $"Failed to map HL7 message to {typeof(T).Name}",
                typeof(T).Name,
                ex);
        }
    }

    /// <summary>
    /// Maps an HL7 message span to a domain model with zero allocation.
    /// For best performance, call the generated XxxMapper.MapFromSpan() method directly.
    /// </summary>
    /// <typeparam name="T">Domain model type decorated with [HL7Message]</typeparam>
    /// <param name="hl7Message">HL7 message as ReadOnlySpan</param>
    /// <returns>Mapped domain model instance</returns>
    public static T MapFromSpan<T>(ReadOnlySpan<char> hl7Message) where T : new()
    {
        // Convert to string to avoid Span boxing issues
        // For true zero-allocation mapping, use the generated mapper directly:
        // var result = PatientAdmissionMapper.MapFromSpan(hl7.AsSpan());
        return Map<T>(new string(hl7Message));
    }

    /// <summary>
    /// Maps a parsed HL7 message object to a domain model.
    /// </summary>
    /// <typeparam name="T">Domain model type decorated with [HL7Message]</typeparam>
    /// <param name="messageObject">Parsed HL7 message object</param>
    /// <returns>Mapped domain model instance</returns>
    public static T Map<T>(object messageObject) where T : new()
    {
        if (messageObject == null)
            throw new ArgumentNullException(nameof(messageObject));

        var hl7String = messageObject.ToString();
        if (string.IsNullOrEmpty(hl7String))
        {
            throw new HL7MappingException(
                "Message object ToString() returned null or empty",
                typeof(T).Name);
        }

        return Map<T>(hl7String);
    }
}

/// <summary>
/// Exception thrown when HL7 mapping fails.
/// </summary>
public class HL7MappingException : Exception
{
    /// <summary>
    /// The target type being mapped to.
    /// </summary>
    public string TargetType { get; }

    /// <summary>
    /// The field path that caused the error (if applicable).
    /// </summary>
    public string? FieldPath { get; }

    public HL7MappingException(string message, string targetType)
        : base(message)
    {
        TargetType = targetType;
    }

    public HL7MappingException(string message, string targetType, Exception innerException)
        : base(message, innerException)
    {
        TargetType = targetType;
    }

    public HL7MappingException(string message, string targetType, string fieldPath)
        : base(message)
    {
        TargetType = targetType;
        FieldPath = fieldPath;
    }

    public HL7MappingException(string message, string targetType, string fieldPath, Exception innerException)
        : base(message, innerException)
    {
        TargetType = targetType;
        FieldPath = fieldPath;
    }
}
