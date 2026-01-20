using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Marks a class as an HL7 message mapping target.
/// Triggers source generation of mapping code.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class HL7MessageAttribute : Attribute
{
    /// <summary>
    /// Supported message types (e.g., "ADT^A01", "ORU^R01").
    /// </summary>
    public string[] MessageTypes { get; }

    /// <summary>
    /// Marks this class for HL7 message mapping.
    /// </summary>
    /// <param name="messageTypes">One or more message type codes (e.g., "ADT^A01")</param>
    public HL7MessageAttribute(params string[] messageTypes)
    {
        MessageTypes = messageTypes ?? throw new ArgumentNullException(nameof(messageTypes));
        
        if (messageTypes.Length == 0)
        {
            throw new ArgumentException("At least one message type must be specified", nameof(messageTypes));
        }
    }
}
