using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Maps a property to an HL7 field, component, or subcomponent.
/// Supports multi-level notation: PID.5, PID.5.1, PID.5.1.1, PID.3[0], etc.
/// Can be applied multiple times to a property for priority-based fallback (use Priority property).
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
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
    /// Example: "PV1.2 == I" (only map if patient class is Inpatient)
    /// Supports: ==, !=, field path references
    /// </summary>
    public string? When { get; set; }

    /// <summary>
    /// Alternative condition syntax. Field is mapped only if condition evaluates to true.
    /// Example: "PID.8 == M && PV1.2 == I"
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Skip mapping if the source field value is null or empty.
    /// Useful for optional fields.
    /// </summary>
    public bool SkipIfEmpty { get; set; }

    /// <summary>
    /// When true, ONLY use conditional defaults - do NOT fall back to message value.
    /// If no conditional default matches, the property is left at its default value.
    /// Use this when you only want to map if specific conditions are met.
    /// </summary>
    public bool ConditionalOnly { get; set; }

    /// <summary>
    /// Fallback field path to use if the primary field is empty.
    /// Enables field-level fallback: try primary field first, then fallback.
    /// Example: "PID.14" to use work phone if home phone (PID.13) is empty.
    /// </summary>
    public string? FallbackField { get; set; }

    /// <summary>
    /// Priority for multiple field mappings (lower = higher priority).
    /// When multiple [HL7Field] attributes are present, they are tried in priority order.
    /// First non-empty field wins. Default priority is 0.
    /// </summary>
    public int Priority { get; set; } = 0;

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
