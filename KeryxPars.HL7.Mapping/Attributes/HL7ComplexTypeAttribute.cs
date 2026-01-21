using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Marks a class as an HL7 complex type that can be used within an HL7 message.
/// Complex types use HL7Field attributes to map fields from the full message.
/// 
/// Supports TWO modes:
/// 
/// 1. WITH BaseFieldPath (relative + absolute paths):
///    - Relative paths (e.g., "1", "3") are relative to BaseFieldPath
///    - Absolute paths (e.g., "PID.12") override the base
/// 
/// 2. WITHOUT BaseFieldPath (absolute paths only):
///    - All HL7Field paths must be absolute (e.g., "PID.11.1", "PID.12")
/// 
/// This is different from:
/// - HL7MessageAttribute: For root-level message classes
/// - HL7Component: For field components (old approach, still supported)
/// </summary>
/// <example>
/// <code>
/// // Example 1: WITH BaseFieldPath (relative + absolute)
/// [HL7ComplexType(BaseFieldPath = "PID.11")]
/// public class Address
/// {
///     [HL7Field("1")]  // Relative ? PID.11.1 (Street)
///     public string? Street { get; set; }
///     
///     [HL7Field("3")]  // Relative ? PID.11.3 (City)
///     public string? City { get; set; }
///     
///     [HL7Field("PID.12")]  // Absolute (County from different field)
///     public string? County { get; set; }
/// }
/// 
/// // Example 2: WITHOUT BaseFieldPath (absolute only)
/// [HL7ComplexType]
/// public class PatientInfo
/// {
///     [HL7Field("PID.11.1")]  // Absolute (Street)
///     public string? Street { get; set; }
///     
///     [HL7Field("PID.12")]  // Absolute (County)
///     public string? County { get; set; }
/// }
/// 
/// // Used in a message like this:
/// [HL7Message]
/// public class Patient
/// {
///     [HL7Complex]
///     public Address? HomeAddress { get; set; }
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class HL7ComplexTypeAttribute : Attribute
{
    /// <summary>
    /// Optional base field path for relative field references.
    /// When set, numeric field paths (e.g., "1", "3") are relative to this base.
    /// Absolute paths (starting with segment name) override the base.
    /// </summary>
    public string? BaseFieldPath { get; set; }

    /// <summary>
    /// Marks this class as an HL7 complex type.
    /// </summary>
    public HL7ComplexTypeAttribute()
    {
    }

    /// <summary>
    /// Marks this class as an HL7 complex type with a base field path.
    /// Allows relative field references.
    /// </summary>
    /// <param name="baseFieldPath">Base field path (e.g., "PID.11")</param>
    public HL7ComplexTypeAttribute(string baseFieldPath)
    {
        BaseFieldPath = baseFieldPath;
    }
}


