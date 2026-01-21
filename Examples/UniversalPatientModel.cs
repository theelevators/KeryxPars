using System;
using System.Collections.Generic;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.Examples;

/// <summary>
/// UNIVERSAL PATIENT MODEL - Works with ANY HL7 message type!
/// Maps whatever fields are available, gracefully handles missing segments.
/// NO MESSAGE TYPE RESTRICTIONS - accepts ADT, OMP, ORU, and MORE!
/// </summary>
[HL7Message]  // ? NO arguments = accepts ANY HL7 message!
public class UniversalPatient
{
    // ========== PATIENT DEMOGRAPHICS (Available in ALL message types) ==========
    
    [HL7Field("PID.3")]
    public string? MRN { get; set; }
    
    [HL7Field("PID.5.1")]
    public string? LastName { get; set; }
    
    [HL7Field("PID.5.2")]
    public string? FirstName { get; set; }
    
    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime? DateOfBirth { get; set; }
    
    [HL7Field("PID.8")]
    public Gender? Gender { get; set; }
    
    [HL7Complex(BaseFieldPath = "PID.5")]
    public PersonName? Name { get; set; }
    
    // ========== VISIT INFORMATION (ADT only, but safe if missing) ==========
    
    /// <summary>
    /// Patient class - only in ADT messages, null in pharmacy/lab
    /// </summary>
    [HL7Field("PV1.2")]
    public PatientClass? PatientClass { get; set; }
    
    /// <summary>
    /// Bed assignment - only for inpatients, null otherwise
    /// </summary>
    [HL7Field("PV1.3", Condition = "PV1.2 == I")]
    public string? BedAssignment { get; set; }
    
    /// <summary>
    /// Attending physician - may be in ADT or Lab, null in pharmacy
    /// </summary>
    [HL7Field("PV1.7")]
    public string? AttendingPhysician { get; set; }
    
    // ========== PHARMACY ORDERS (OMP only, empty in ADT/Lab) ==========
    
    /// <summary>
    /// Pharmacy orders - ONLY in OMP messages
    /// Empty list if not present - NO ERRORS!
    /// </summary>
    [HL7Segments("RXO")]
    public List<PharmacyOrder> PharmacyOrders { get; set; } = new();
    
    /// <summary>
    /// Dispenses - ONLY in OMP messages
    /// </summary>
    [HL7Segments("RXD")]
    public List<PharmacyDispense> PharmacyDispenses { get; set; } = new();
    
    // ========== LAB RESULTS (ORU only, empty in ADT/Pharmacy) ==========
    
    /// <summary>
    /// Lab observations - ONLY in ORU messages
    /// Empty list if not present - NO ERRORS!
    /// </summary>
    [HL7Segments("OBX")]
    public List<Observation> LabResults { get; set; } = new();
    
    /// <summary>
    /// Order details - in ORU messages
    /// </summary>
    [HL7Segments("OBR")]
    public List<OrderRequest> OrderRequests { get; set; } = new();
    
    // ========== DIAGNOSES (Available in ADT and sometimes Lab) ==========
    
    /// <summary>
    /// Diagnoses - in ADT, sometimes in Lab, empty in pharmacy
    /// </summary>
    [HL7Segments("DG1")]
    public List<Diagnosis> Diagnoses { get; set; } = new();
    
    // ========== INSURANCE (ADT and Pharmacy, not in Lab) ==========
    
    /// <summary>
    /// Insurance segments - in ADT and pharmacy
    /// </summary>
    [HL7Segments("IN1")]
    public List<Insurance> InsuranceInfo { get; set; } = new();
    
    // ========== ALLERGIES (Can be in any message type!) ==========
    
    /// <summary>
    /// Allergies - AL1 segments
    /// Can be in ADT, pharmacy orders, or lab results
    /// </summary>
    [HL7Segments("AL1")]
    public List<Allergy> Allergies { get; set; } = new();
    
    // ========== CONTACT INFO (Usually ADT, but safe everywhere) ==========
    
    [HL7Field("PID.13", Priority = 0)]
    [HL7Field("PID.14", Priority = 1)]
    public string? ContactPhone { get; set; }
    
    [HL7Field("PID.13.6", FallbackField = "PID.14.6")]
    public string? Email { get; set; }
    
    // ========== MESSAGE METADATA (Available in ALL) ==========
    
    [HL7Field("MSH.9")]
    public string? MessageType { get; set; }
    
    [HL7Field("MSH.7", Format = "yyyyMMddHHmmss")]
    public DateTime? MessageDateTime { get; set; }
    
    [HL7Field("MSH.10")]
    public string? MessageControlID { get; set; }
}

// ========== SUPPORTING TYPES ==========

public class PersonName
{
    [HL7Component(1)]
    public string? FamilyName { get; set; }
    
    [HL7Component(2)]
    public string? GivenName { get; set; }
}

[HL7Message("RXO")]
public class PharmacyOrder
{
    [HL7Field("RXO.1.1")]
    public string? DrugCode { get; set; }
    
    [HL7Field("RXO.1.2")]
    public string? DrugName { get; set; }
    
    [HL7Field("RXO.2")]
    public string? RequestedGiveAmountMinimum { get; set; }
    
    [HL7Field("RXO.4")]
    public string? RequestedGiveUnits { get; set; }
    
    [HL7Field("RXO.21.1")]
    public string? OrderingProviderID { get; set; }
}

[HL7Message("RXD")]
public class PharmacyDispense
{
    [HL7Field("RXD.1")]
    public string? DispenseSubID { get; set; }
    
    [HL7Field("RXD.2.1")]
    public string? DispenseCode { get; set; }
    
    [HL7Field("RXD.2.2")]
    public string? DispenseName { get; set; }
    
    [HL7Field("RXD.4")]
    public string? ActualDispenseAmount { get; set; }
}

[HL7Message("OBX")]
public class Observation
{
    [HL7Field("OBX.1")]
    public int SetID { get; set; }
    
    [HL7Field("OBX.2")]
    public string? ValueType { get; set; }
    
    [HL7Field("OBX.3.1")]
    public string? ObservationIdentifier { get; set; }
    
    [HL7Field("OBX.3.2")]
    public string? ObservationText { get; set; }
    
    [HL7Field("OBX.5")]
    public string? ObservationValue { get; set; }
    
    [HL7Field("OBX.6.1")]
    public string? Units { get; set; }
    
    [HL7Field("OBX.8")]
    public string? AbnormalFlags { get; set; }
}

[HL7Message("OBR")]
public class OrderRequest
{
    [HL7Field("OBR.1")]
    public int SetID { get; set; }
    
    [HL7Field("OBR.4.1")]
    public string? UniversalServiceID { get; set; }
    
    [HL7Field("OBR.4.2")]
    public string? UniversalServiceName { get; set; }
    
    [HL7Field("OBR.7", Format = "yyyyMMddHHmmss")]
    public DateTime? ObservationDateTime { get; set; }
}

[HL7Message("DG1")]
public class Diagnosis
{
    [HL7Field("DG1.1")]
    public int SetID { get; set; }
    
    [HL7Field("DG1.3.1")]
    public string? DiagnosisCode { get; set; }
    
    [HL7Field("DG1.3.2")]
    public string? DiagnosisDescription { get; set; }
}

[HL7Message("IN1")]
public class Insurance
{
    [HL7Field("IN1.1")]
    public int SetID { get; set; }
    
    [HL7Field("IN1.3.1")]
    public string? InsuranceCompanyID { get; set; }
    
    [HL7Field("IN1.4")]
    public string? InsuranceCompanyName { get; set; }
}

[HL7Message("AL1")]
public class Allergy
{
    [HL7Field("AL1.1")]
    public int SetID { get; set; }
    
    [HL7Field("AL1.3.1")]
    public string? AllergenCode { get; set; }
    
    [HL7Field("AL1.3.2")]
    public string? AllergenDescription { get; set; }
    
    [HL7Field("AL1.4")]
    public string? AllergySeverity { get; set; }
}

public enum Gender { M, F, O, U }
public enum PatientClass { E, I, O, P, R, B }
