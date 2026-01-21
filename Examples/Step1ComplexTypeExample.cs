using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.Examples;

/// <summary>
/// STEP 1: Complex types with ABSOLUTE paths only (no BaseFieldPath yet).
/// This demonstrates cross-segment enrichment using pure absolute paths.
/// </summary>
[HL7Message]
public class PatientWithDemographics
{
    [HL7Field("PID.3")]
    public string? MRN { get; set; }
    
    [HL7Field("PID.5.1")]
    public string? LastName { get; set; }
    
    /// <summary>
    /// Complex type that pulls from multiple segments!
    /// </summary>
    [HL7Complex]
    public PatientDemographics? Demographics { get; set; }
}

/// <summary>
/// Step 1 Complex Type: Uses ONLY absolute paths.
/// No BaseFieldPath - all HL7Field paths must be fully qualified.
/// </summary>
[HL7ComplexType]
public class PatientDemographics
{
    /// <summary>
    /// Patient's date of birth from PID.7
    /// </summary>
    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Patient's gender from PID.8
    /// </summary>
    [HL7Field("PID.8")]
    public string? Gender { get; set; }
    
    /// <summary>
    /// Patient class from PV1.2 (cross-segment!)
    /// </summary>
    [HL7Field("PV1.2")]
    public string? PatientClass { get; set; }
    
    /// <summary>
    /// Admit date from PV1.44
    /// </summary>
    [HL7Field("PV1.44", Format = "yyyyMMddHHmmss")]
    public DateTime? AdmitDateTime { get; set; }
    
    /// <summary>
    /// Marital status from PID.16
    /// </summary>
    [HL7Field("PID.16")]
    public string? MaritalStatus { get; set; }
}

/// <summary>
/// Another example: Financial information across segments
/// </summary>
[HL7ComplexType]
public class FinancialInfo
{
    [HL7Field("PV1.20")]
    public string? FinancialClass { get; set; }
    
    [HL7Field("IN1.2.1")]
    public string? InsuranceCompanyId { get; set; }
    
    [HL7Field("IN1.3.1")]
    public string? InsurancePlanId { get; set; }
    
    [HL7Field("IN1.17", Format = "yyyyMMdd")]
    public DateTime? PolicyEffectiveDate { get; set; }
}
