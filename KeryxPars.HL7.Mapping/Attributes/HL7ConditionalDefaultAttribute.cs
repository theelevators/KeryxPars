using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Specifies a default value to use when a condition is met.
/// Can be applied multiple times with different conditions - first matching condition wins.
/// </summary>
/// <example>
/// <code>
/// // Use ICD-10 if message has ICD-9 coding
/// [HL7Field("DG1.3")]
/// [HL7ConditionalDefault("ICD10", Condition = "DG1.2 == ICD9")]
/// public string CodingSystem { get; set; }
/// 
/// // Multiple conditional defaults
/// [HL7Field("PV1.10")]
/// [HL7ConditionalDefault("CARDIOLOGY", Condition = "PV1.3.1 == CCU")]
/// [HL7ConditionalDefault("EMERGENCY", Condition = "PV1.2 == E")]
/// [HL7ConditionalDefault("GENERAL", Condition = "PV1.2 == I")]
/// public string? HospitalService { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class HL7ConditionalDefaultAttribute : Attribute
{
    /// <summary>
    /// The default value to use when the condition is met.
    /// </summary>
    public object DefaultValue { get; }

    /// <summary>
    /// Condition that must be true for this default to be applied.
    /// Uses same syntax as HL7Field Condition property.
    /// Examples: "PID.8 == F", "PV1.2 != O", "IN1.1 == ''"
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Priority order when multiple conditional defaults exist.
    /// Lower numbers are checked first. Default is 0.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Optional description of when this default applies.
    /// Used for documentation and error messages.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Creates a conditional default value.
    /// </summary>
    /// <param name="defaultValue">Value to use when condition is met</param>
    public HL7ConditionalDefaultAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Creates a conditional default value with a string literal.
    /// </summary>
    public HL7ConditionalDefaultAttribute(string defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Creates a conditional default value with an integer.
    /// </summary>
    public HL7ConditionalDefaultAttribute(int defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Creates a conditional default value with a boolean.
    /// </summary>
    public HL7ConditionalDefaultAttribute(bool defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Creates a conditional default value with a decimal.
    /// </summary>
    public HL7ConditionalDefaultAttribute(decimal defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Creates a conditional default value with a double.
    /// </summary>
    public HL7ConditionalDefaultAttribute(double defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Creates a conditional default value with a long.
    /// </summary>
    public HL7ConditionalDefaultAttribute(long defaultValue)
    {
        DefaultValue = defaultValue;
    }
}
