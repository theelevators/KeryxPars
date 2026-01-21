using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Demonstrates ConditionalOnly - ONLY map if conditions match!
/// If no condition matches, property stays at default value (null/0/false).
/// </summary>
[HL7Message("ADT^A01")]
public class PatientWithConditionalOnly
{
    [HL7Field("PID.3")]
    public string MRN { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    [HL7Field("PID.8")]
    public Gender Gender { get; set; }

    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }

    // ========== CONDITIONAL ONLY FIELDS ==========

    /// <summary>
    /// Priority - ONLY set if we recognize the patient class.
    /// If PV1.2 is something unexpected, leave priority at 0 (default).
    /// ConditionalOnly = true means: ONLY use conditional defaults, never the message value!
    /// </summary>
    [HL7Field("PV1.25", ConditionalOnly = true)]
    [HL7ConditionalDefault(999, Condition = "PV1.2 == E")]  // Emergency
    [HL7ConditionalDefault(100, Condition = "PV1.2 == I")]  // Inpatient
    [HL7ConditionalDefault(10, Condition = "PV1.2 == O")]   // Outpatient
    public int Priority { get; set; }

    /// <summary>
    /// Risk level - ONLY set for known risk categories.
    /// If condition doesn't match, stays null!
    /// </summary>
    [HL7Field("PV1.50", ConditionalOnly = true)]
    [HL7ConditionalDefault("HIGH", Condition = "PV1.2 == E")]
    [HL7ConditionalDefault("MEDIUM", Condition = "PV1.2 == I")]
    [HL7ConditionalDefault("LOW", Condition = "PV1.2 == O")]
    public string? RiskLevel { get; set; }

    /// <summary>
    /// Requires review - ONLY set to true for specific scenarios.
    /// Otherwise stays false (default).
    /// </summary>
    [HL7Field("PV1.16", ConditionalOnly = true)]
    [HL7ConditionalDefault(true, Condition = "PV1.2 == E")]
    [HL7ConditionalDefault(true, Condition = "PV1.3.1 == ICU")]
    public bool RequiresReview { get; set; }

    [HL7Field("PV1.3.1")]
    public string? Location { get; set; }
}
