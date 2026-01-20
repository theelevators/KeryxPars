using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Maps a property to an HL7 field, component, or subcomponent.
/// Supports multi-level notation: PID.5, PID.5.1, PID.5.1.1, PID.3[0], etc.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class HL7FieldAttribute : Attribute
{
    /// <summary>
    /// Field path using HL7 notation.
    /// Examples: "PID.3", "PID.5.1", "PID.5.1.1", "PID.3[0]", "PID.3[1].4"
    /// </summary>
    public string FieldPath { get; }

    /// <summary>
    /// Optional custom type converter.
    /// </summary>
    public Type? Converter { get; set; }

    /// <summary>
    /// Date/time format string (for DateTime properties).
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Default value if field is missing or empty.
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Whether this field is required (validation during mapping).
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// Custom error message for validation failures.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Condition for mapping (simple expression).
    /// Example: "PV1.2 = I" (only map if patient class is Inpatient)
    /// </summary>
    public string? When { get; set; }

    /// <summary>
    /// Maps a property to an HL7 field.
    /// </summary>
    /// <param name="fieldPath">HL7 field notation (e.g., "PID.5.1")</param>
    public HL7FieldAttribute(string fieldPath)
    {
        if (string.IsNullOrWhiteSpace(fieldPath))
        {
            throw new ArgumentException("Field path cannot be null or empty", nameof(fieldPath));
        }

        FieldPath = fieldPath;
    }
}
