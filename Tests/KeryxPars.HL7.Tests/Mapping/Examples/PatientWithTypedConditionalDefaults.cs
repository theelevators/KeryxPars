using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

// ========== ENUMS FOR TESTING ==========

public enum CodingSystem
{
    ICD9,
    ICD10,
    SNOMED_CT,
    CPT
}

public enum PaymentMethod
{
    CASH,
    INSURANCE,
    SELF_PAY,
    MEDICARE,
    MEDICAID
}

public enum PriorityLevel
{
    LOW = 1,
    MEDIUM = 50,
    HIGH = 100,
    CRITICAL = 999
}

/// <summary>
/// Demonstrates TYPED CONDITIONAL DEFAULTS with enums and primitives!
/// The ultimate mapping power!
/// </summary>
[HL7Message("ADT^A01", "ADT^A04")]
public class PatientWithTypedConditionalDefaults
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

    // ========== ENUM CONDITIONAL DEFAULTS ==========

    /// <summary>
    /// Coding system - defaults to ICD10 enum if message has ICD9.
    /// ENUM SUPPORT!
    /// </summary>
    [HL7Field("DG1.2")]
    [HL7ConditionalDefault(CodingSystem.ICD10, Condition = "DG1.2 == ICD9")]
    [HL7ConditionalDefault(CodingSystem.ICD10, Condition = "DG1.2 == ''")]
    public CodingSystem? CodingSystemEnum { get; set; }

    /// <summary>
    /// Payment method - enum-based payment type.
    /// </summary>
    [HL7Field("PV1.18")]
    [HL7ConditionalDefault(PaymentMethod.SELF_PAY, Condition = "IN1.1 == ''")]
    [HL7ConditionalDefault(PaymentMethod.MEDICARE, Condition = "IN1.1 == 1 && IN1.3 == MEDICARE")]
    public PaymentMethod PaymentType { get; set; }

    // ========== INT CONDITIONAL DEFAULTS ==========

    /// <summary>
    /// Priority level as integer - different values for different patient classes.
    /// INT SUPPORT!
    /// </summary>
    [HL7Field("PV1.25")]
    [HL7ConditionalDefault(999, Condition = "PV1.2 == E", Priority = 1)]  // Emergency = CRITICAL
    [HL7ConditionalDefault(100, Condition = "PV1.3.1 == ICU", Priority = 2)]  // ICU = HIGH
    [HL7ConditionalDefault(50, Condition = "PV1.2 == I", Priority = 3)]   // Inpatient = MEDIUM
    [HL7ConditionalDefault(1, Condition = "PV1.2 == O", Priority = 4)]    // Outpatient = LOW
    public int Priority { get; set; }

    /// <summary>
    /// Priority as enum - using enum with integer values.
    /// </summary>
    [HL7Field("PV1.26")]
    [HL7ConditionalDefault(PriorityLevel.CRITICAL, Condition = "PV1.2 == E")]
    [HL7ConditionalDefault(PriorityLevel.HIGH, Condition = "PV1.3.1 == ICU")]
    [HL7ConditionalDefault(PriorityLevel.MEDIUM, Condition = "PV1.2 == I")]
    public PriorityLevel PriorityEnum { get; set; }

    // ========== BOOL CONDITIONAL DEFAULTS ==========

    /// <summary>
    /// Is critical - boolean based on conditions.
    /// BOOL SUPPORT!
    /// </summary>
    [HL7Field("PV1.16")]
    [HL7ConditionalDefault(true, Condition = "PV1.2 == E")]
    [HL7ConditionalDefault(true, Condition = "PV1.3.1 == ICU")]
    [HL7ConditionalDefault(false, Condition = "PV1.2 == O")]
    public bool IsCritical { get; set; }

    /// <summary>
    /// Requires authorization - boolean logic.
    /// </summary>
    [HL7Field("PV1.50")]
    [HL7ConditionalDefault(true, Condition = "IN1.1 == ''")]  // No insurance = needs auth
    [HL7ConditionalDefault(false, Condition = "PV1.2 == E")]  // Emergency = no auth needed
    public bool RequiresAuthorization { get; set; }

    // ========== DECIMAL CONDITIONAL DEFAULTS ==========

    /// <summary>
    /// Charge amount - decimal based on conditions.
    /// DECIMAL SUPPORT!
    /// </summary>
    [HL7Field("FT1.22")]
    [HL7ConditionalDefault(0.0, Condition = "IN1.1 == ''")]  // No insurance = free
    [HL7ConditionalDefault(50.00, Condition = "PV1.2 == E")] // Emergency copay
    public decimal ChargeAmount { get; set; }

    /// <summary>
    /// Discount percentage.
    /// </summary>
    [HL7Field("FT1.24")]
    [HL7ConditionalDefault(100.0, Condition = "IN1.3 == MEDICAID")]  // 100% covered
    [HL7ConditionalDefault(80.0, Condition = "IN1.3 == MEDICARE")]   // 80% covered
    [HL7ConditionalDefault(0.0, Condition = "IN1.1 == ''")]          // No coverage
    public decimal DiscountPercentage { get; set; }

    // Supporting fields
    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }

    [HL7Field("PV1.3.1")]
    public string? Location { get; set; }

    [HL7Field("IN1.1")]
    public string? InsuranceSetId { get; set; }

    [HL7Field("IN1.3")]
    public string? InsuranceCompanyId { get; set; }

    [HL7Field("DG1.1")]
    public string? DiagnosisSetId { get; set; }

    [HL7Field("DG1.2")]
    public string? CodingSystemString { get; set; }
}
