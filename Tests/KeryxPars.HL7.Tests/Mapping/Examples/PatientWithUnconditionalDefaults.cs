using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Demonstrates UNCONDITIONAL defaults - the final else fallback!
/// </summary>
[HL7Message("ADT^A01")]
public class PatientWithUnconditionalDefaults
{
    [HL7Field("PID.3")]
    public string MRN { get; set; } = string.Empty;

    [HL7Field("PID.8")]
    public Gender Gender { get; set; }

    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }

    // ========== UNCONDITIONAL DEFAULTS ==========

    /// <summary>
    /// Priority with unconditional fallback.
    /// If Emergency ? 999
    /// If Inpatient ? 100
    /// ELSE (anything else) ? 10
    /// </summary>
    [HL7Field("PV1.25")]
    [HL7ConditionalDefault(999, Condition = "PV1.2 == E", Priority = 1)]
    [HL7ConditionalDefault(100, Condition = "PV1.2 == I", Priority = 2)]
    [HL7ConditionalDefault(10)]  // ? NO CONDITION = unconditional else!
    public int Priority { get; set; }

    /// <summary>
    /// Risk level with string unconditional default.
    /// </summary>
    [HL7Field("PV1.50")]
    [HL7ConditionalDefault("HIGH", Condition = "PV1.2 == E")]
    [HL7ConditionalDefault("MEDIUM", Condition = "PV1.2 == I")]
    [HL7ConditionalDefault("LOW")]  // ? Unconditional else = "LOW"
    public string RiskLevel { get; set; } = string.Empty;

    /// <summary>
    /// Boolean with unconditional default.
    /// </summary>
    [HL7Field("PV1.16")]
    [HL7ConditionalDefault(true, Condition = "PV1.2 == E")]
    [HL7ConditionalDefault(false)]  // ? Unconditional else = false
    public bool RequiresReview { get; set; }

    /// <summary>
    /// Enum with unconditional default.
    /// </summary>
    [HL7Field("DG1.2")]
    [HL7ConditionalDefault(DiagnosisCodingSystem.ICD10, Condition = "DG1.2 == ICD9")]
    [HL7ConditionalDefault(DiagnosisCodingSystem.SNOMED_CT)]  // ? Unconditional else = SNOMED_CT
    public DiagnosisCodingSystem CodingSystem { get; set; }
}

public enum DiagnosisCodingSystem
{
    ICD9,
    ICD10,
    SNOMED_CT
}
