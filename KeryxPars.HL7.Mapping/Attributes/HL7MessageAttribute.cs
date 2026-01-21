using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Marks a class as an HL7 message mapping target.
/// Triggers source generation of mapping code.
/// 
/// USAGE:
/// 1. No arguments - accepts ANY HL7 message type:
///    [HL7Message]
/// 
/// 2. Allows specific types ONLY:
///    [HL7Message(Allows = new[] { "ADT^A01", "ADT^A04" })]
/// 
/// 3. Allows everything EXCEPT specific types:
///    [HL7Message(NotAllows = new[] { "BAR^P01", "BAR^P02" })]
/// 
/// 4. Legacy support - constructor with types (equivalent to Allows):
///    [HL7Message("ADT^A01", "ADT^A04")]
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class HL7MessageAttribute : Attribute
{
    /// <summary>
    /// Allowed message types (whitelist).
    /// If null or empty, all message types are allowed (unless NotAllows is set).
    /// </summary>
    public string[]? Allows { get; set; }

    /// <summary>
    /// Disallowed message types (blacklist).
    /// Takes precedence over Allows if both are set.
    /// </summary>
    public string[]? NotAllows { get; set; }

    /// <summary>
    /// LEGACY: Supported message types from constructor.
    /// Kept for backward compatibility.
    /// Internally mapped to Allows property.
    /// </summary>
    public string[]? MessageTypes => Allows;

    /// <summary>
    /// Marks this class for HL7 message mapping.
    /// Accepts ANY HL7 message type by default.
    /// </summary>
    public HL7MessageAttribute()
    {
        // No restrictions - accepts ANY HL7 message!
        Allows = null;
        NotAllows = null;
    }

    /// <summary>
    /// LEGACY: Marks this class for specific HL7 message types.
    /// Equivalent to using Allows property.
    /// Kept for backward compatibility.
    /// </summary>
    /// <param name="messageTypes">Allowed message type codes (e.g., "ADT^A01")</param>
    public HL7MessageAttribute(params string[] messageTypes)
    {
        if (messageTypes == null || messageTypes.Length == 0)
        {
            throw new ArgumentException("At least one message type must be specified when using constructor", nameof(messageTypes));
        }

        Allows = messageTypes;
        NotAllows = null;
    }

    /// <summary>
    /// Validates if a message type is allowed for this mapping.
    /// </summary>
    /// <param name="messageType">Message type to check (e.g., "ADT^A01")</param>
    /// <returns>True if allowed, false otherwise</returns>
    public bool IsAllowed(string messageType)
    {
        if (string.IsNullOrWhiteSpace(messageType))
            return false;

        // If NotAllows is set, check blacklist first
        if (NotAllows != null && NotAllows.Length > 0)
        {
            foreach (var notAllowed in NotAllows)
            {
                if (messageType.Equals(notAllowed, StringComparison.OrdinalIgnoreCase))
                    return false; // Explicitly disallowed
            }
            // Not in blacklist - allowed!
            return true;
        }

        // If Allows is set (even if empty), check whitelist
        if (Allows != null)
        {
            // Empty whitelist = reject everything
            if (Allows.Length == 0)
                return false;
            
            foreach (var allowed in Allows)
            {
                if (messageType.Equals(allowed, StringComparison.OrdinalIgnoreCase))
                    return true; // Explicitly allowed
            }
            return false; // Not in whitelist
        }

        // No restrictions - allow everything!
        return true;
    }
}

