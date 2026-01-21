using System;
using System.Collections.Generic;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.Examples;

/// <summary>
/// COMPLETE REAL-WORLD EXAMPLE - Emergency Department Patient
/// Demonstrates ALL KeryxPars features in one cohesive model!
/// </summary>
[HL7Message("ADT^A01", "ADT^A04")]
public class EmergencyPatient
{
    // ========== BASIC MAPPING ==========
    
    [HL7Field("PID.3")]
    public string MRN { get; set; } = string.Empty;
    
    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime DateOfBirth { get; set; }
    
    [HL7Field("PID.8")]
    public Gender Gender { get; set; }
    
    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }
    
    // ========== NESTED OBJECTS ==========
    
    [HL7Complex(BaseFieldPath = "PID.5")]
    public PersonName Name { get; set; } = new();
    
    [HL7Complex(BaseFieldPath = "PID.11")]
    public Address HomeAddress { get; set; } = new();
    
    // ========== PRIORITY FALLBACK (Multi-level!) ==========
    
    /// <summary>
    /// Try phone numbers in priority order:
    /// 1. Home phone (PID.13)
    /// 2. Work phone (PID.14)
    /// 3. Mobile phone (PID.40)
    /// </summary>
    [HL7Field("PID.13", Priority = 0)]
    [HL7Field("PID.14", Priority = 1)]
    [HL7Field("PID.40", Priority = 2)]
    public string? ContactPhone { get; set; }
    
    /// <summary>
    /// Try patient identifiers in order:
    /// 1. SSN (PID.19)
    /// 2. Driver's License (PID.20)
    /// 3. Passport (PID.21.1)
    /// 4. Patient ID (PID.2)
    /// </summary>
    [HL7Field("PID.19", Priority = 0)]
    [HL7Field("PID.20", Priority = 1)]
    [HL7Field("PID.21.1", Priority = 2)]
    [HL7Field("PID.2", Priority = 3)]
    public string? PrimaryIdentifier { get; set; }
    
    // ========== SIMPLE FALLBACK ==========
    
    /// <summary>
    /// Try primary email (PID.13.6), fallback to work email (PID.14.6)
    /// </summary>
    [HL7Field("PID.13.6", FallbackField = "PID.14.6")]
    public string? Email { get; set; }
    
    /// <summary>
    /// Try NK1.2 (next of kin), fallback to PID.25 (emergency contact)
    /// Cross-segment fallback!
    /// </summary>
    [HL7Field("NK1.2", FallbackField = "PID.25")]
    public string? EmergencyContactName { get; set; }
    
    // ========== CONDITIONAL MAPPING ==========
    
    /// <summary>
    /// ONLY map pregnancy status if patient is female
    /// </summary>
    [HL7Field("PID.31", Condition = "PID.8 == F")]
    public string? PregnancyStatus { get; set; }
    
    /// <summary>
    /// ONLY map bed assignment if patient is inpatient
    /// </summary>
    [HL7Field("PV1.3", Condition = "PV1.2 == I")]
    public string? BedAssignment { get; set; }
    
    // ========== TYPED CONDITIONAL DEFAULTS ==========
    
    /// <summary>
    /// Priority based on patient class:
    /// - Emergency ? 999 (CRITICAL!)
    /// - Inpatient ? 100
    /// - Outpatient ? 10
    /// </summary>
    [HL7Field("PV1.25")]
    [HL7ConditionalDefault(999, Condition = "PV1.2 == E")]
    [HL7ConditionalDefault(100, Condition = "PV1.2 == I")]
    [HL7ConditionalDefault(10, Condition = "PV1.2 == O")]
    public int Priority { get; set; }
    
    /// <summary>
    /// Risk level with string conditional defaults
    /// </summary>
    [HL7Field("PV1.50")]
    [HL7ConditionalDefault("CRITICAL", Condition = "PV1.2 == E")]
    [HL7ConditionalDefault("MEDIUM", Condition = "PV1.2 == I")]
    [HL7ConditionalDefault("LOW")]  // Unconditional fallback for anything else!
    public string RiskLevel { get; set; } = string.Empty;
    
    /// <summary>
    /// Requires immediate review if emergency or ICU
    /// </summary>
    [HL7Field("PV1.16")]
    [HL7ConditionalDefault(true, Condition = "PV1.2 == E || PV1.3.1 == ICU")]
    [HL7ConditionalDefault(false)]  // Unconditional default = false
    public bool RequiresImmediateReview { get; set; }
    
    /// <summary>
    /// Upgrade ICD9 to ICD10 automatically using enum defaults!
    /// </summary>
    [HL7Field("DG1.2")]
    [HL7ConditionalDefault(CodingSystem.ICD10, Condition = "DG1.2 == ICD9")]
    [HL7ConditionalDefault(CodingSystem.SNOMED_CT)]  // Default to SNOMED for unknown
    public CodingSystem DiagnosisCodingSystem { get; set; }
    
    // ========== CONDITIONAL ONLY ==========
    
    /// <summary>
    /// ONLY set triage level for known patient classes
    /// Unknown classes stay at default (0)
    /// </summary>
    [HL7Field("PV1.52", ConditionalOnly = true)]
    [HL7ConditionalDefault(5, Condition = "PV1.2 == E")]
    [HL7ConditionalDefault(3, Condition = "PV1.2 == I")]
    [HL7ConditionalDefault(1, Condition = "PV1.2 == O")]
    public int TriageLevel { get; set; }
    
    // ========== COLLECTIONS ==========
    
    /// <summary>
    /// All diagnoses from DG1 segments
    /// </summary>
    [HL7Segments("DG1")]
    public List<Diagnosis> Diagnoses { get; set; } = new();
    
    /// <summary>
    /// All observations from OBX segments
    /// </summary>
    [HL7Segments("OBX")]
    public List<Observation> Observations { get; set; } = new();
    
    // ========== PRIORITY FALLBACK FOR DOCTOR ==========
    
    /// <summary>
    /// Try doctors in priority order:
    /// 1. Attending physician (PV1.7)
    /// 2. Admitting physician (PV1.17)
    /// 3. Consulting physician (PV1.9)
    /// </summary>
    [HL7Field("PV1.7", Priority = 0)]
    [HL7Field("PV1.17", Priority = 1)]
    [HL7Field("PV1.9", Priority = 2)]
    public string? ResponsibleDoctor { get; set; }
}

// ========== NESTED COMPLEX TYPES ==========

public class PersonName
{
    [HL7Component(1)]
    public string FamilyName { get; set; } = string.Empty;
    
    [HL7Component(2)]
    public string GivenName { get; set; } = string.Empty;
    
    [HL7Component(3)]
    public string MiddleName { get; set; } = string.Empty;
}

public class Address
{
    [HL7Component(1)]
    public string Street { get; set; } = string.Empty;
    
    [HL7Component(3)]
    public string City { get; set; } = string.Empty;
    
    [HL7Component(4)]
    public string State { get; set; } = string.Empty;
    
    [HL7Component(5)]
    public string ZipCode { get; set; } = string.Empty;
}

// ========== COLLECTION ITEM TYPES ==========

[HL7Message("DG1")]
public class Diagnosis
{
    [HL7Field("DG1.1")]
    public int SetID { get; set; }
    
    [HL7Field("DG1.2")]
    public string CodingMethod { get; set; } = string.Empty;
    
    [HL7Field("DG1.3.1")]
    public string DiagnosisCode { get; set; } = string.Empty;
    
    [HL7Field("DG1.3.2")]
    public string DiagnosisDescription { get; set; } = string.Empty;
}

[HL7Message("OBX")]
public class Observation
{
    [HL7Field("OBX.1")]
    public int SetID { get; set; }
    
    [HL7Field("OBX.3.1")]
    public string ObservationIdentifier { get; set; } = string.Empty;
    
    [HL7Field("OBX.5")]
    public string ObservationValue { get; set; } = string.Empty;
    
    [HL7Field("OBX.6.1")]
    public string Units { get; set; } = string.Empty;
}

// ========== ENUMS ==========

public enum Gender { M, F, O, U }
public enum PatientClass { E, I, O, P, R, B }
public enum CodingSystem { ICD9, ICD10, SNOMED_CT }
