using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Demonstrates CONDITIONAL DEFAULT VALUES - the game-changing feature!
/// Use default values based on runtime conditions.
/// </summary>
[HL7Message("ADT^A01", "ADT^A04")]
public class PatientWithConditionalDefaults
{
    [HL7Field("PID.3")]
    public string MRN { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    [HL7Field("PID.5.2")]
    public string FirstName { get; set; } = string.Empty;

    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime DateOfBirth { get; set; }

    [HL7Field("PID.8")]
    public Gender Gender { get; set; }

    // ========== CONDITIONAL DEFAULTS ==========

    /// <summary>
    /// Coding system - defaults to ICD10 if message uses old ICD9 format.
    /// Real-world use case: Upgrading from ICD-9 to ICD-10.
    /// </summary>
    [HL7Field("DG1.2")]
    [HL7ConditionalDefault("ICD10", Condition = "DG1.2 == ICD9")]
    [HL7ConditionalDefault("ICD10", Condition = "DG1.2 == ''")]
    public string CodingSystem { get; set; } = string.Empty;

    /// <summary>
    /// Payment type - defaults to SELF-PAY if no insurance segment.
    /// </summary>
    [HL7Field("PV1.18")]
    [HL7ConditionalDefault("SELF-PAY", Condition = "IN1.1 == ''")]
    public string? PaymentType { get; set; }

    /// <summary>
    /// Hospital service - multiple conditional defaults with priority.
    /// First matching condition wins!
    /// </summary>
    [HL7Field("PV1.10")]
    [HL7ConditionalDefault("CARDIOLOGY", Condition = "PV1.3.1 == CCU", Priority = 1)]
    [HL7ConditionalDefault("CARDIOLOGY", Condition = "PV1.3.1 == CATH", Priority = 2)]
    [HL7ConditionalDefault("EMERGENCY", Condition = "PV1.2 == E", Priority = 3)]
    [HL7ConditionalDefault("GENERAL", Condition = "PV1.2 == I", Priority = 4)]
    public string? HospitalService { get; set; }

    /// <summary>
    /// Admission type - defaults based on patient class.
    /// </summary>
    [HL7Field("PV1.4")]
    [HL7ConditionalDefault("EMERGENCY", Condition = "PV1.2 == E")]
    [HL7ConditionalDefault("ELECTIVE", Condition = "PV1.2 == I")]
    [HL7ConditionalDefault("OBSERVATION", Condition = "PV1.2 == O")]
    public string? AdmissionType { get; set; }

    /// <summary>
    /// Discharge disposition - defaults to HOME for outpatients.
    /// </summary>
    [HL7Field("PV1.36")]
    [HL7ConditionalDefault("HOME", Condition = "PV1.2 == O")]
    public string? DischargeDisposition { get; set; }

    // Supporting fields for conditions
    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }

    [HL7Field("PV1.3.1")]
    public string? Location { get; set; }

    [HL7Field("IN1.1")]
    public string? InsuranceSetId { get; set; }

    [HL7Field("DG1.1")]
    public string? DiagnosisSetId { get; set; }
}
