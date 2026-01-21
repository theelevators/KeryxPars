using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.Examples;

/// <summary>
/// COMPREHENSIVE EXAMPLES - All 3 HL7Message attribute patterns!
/// Demonstrates the ultimate flexibility of KeryxPars!
/// </summary>
public class HL7MessageAttributeExamples
{
    // ========== PATTERN 1: NO RESTRICTIONS (Accept ANY HL7 message!) ==========
    
    /// <summary>
    /// Pattern 1: Accept ANY HL7 message type!
    /// Perfect for: Universal models, logging, auditing, data aggregation
    /// </summary>
    [HL7Message]  // ? No arguments = accepts EVERYTHING!
    public class UniversalPatient
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        [HL7Field("PID.5.1")]
        public string? LastName { get; set; }
        
        [HL7Field("MSH.9")]
        public string? MessageType { get; set; }
        
        // Works with ADT, OMP, ORU, SIU, MDM, BAR, DFT... ANYTHING!
    }
    
    // ========== PATTERN 2: WHITELIST (Allow ONLY specific types) ==========
    
    /// <summary>
    /// Pattern 2a: WHITELIST using Allows property
    /// Only accepts ADT admission messages
    /// Perfect for: Specific workflows, strict validation
    /// </summary>
    [HL7Message(Allows = new[] { "ADT^A01", "ADT^A04", "ADT^A05" })]
    public class AdmissionPatient
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        [HL7Field("PV1.2")]
        public string? PatientClass { get; set; }
        
        [HL7Field("PV1.3")]
        public string? BedAssignment { get; set; }
        
        // ONLY works with ADT^A01, ADT^A04, ADT^A05
        // Rejects ADT^A03 (discharge), OMP, ORU, etc.
    }
    
    /// <summary>
    /// Pattern 2b: WHITELIST using constructor (legacy, backward compatible)
    /// Same as using Allows property
    /// </summary>
    [HL7Message("ADT^A01", "ADT^A04", "ADT^A05")]
    public class AdmissionPatientLegacy
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        // Exactly the same behavior as Pattern 2a
        // Kept for backward compatibility
    }
    
    /// <summary>
    /// Pattern 2c: Lab results ONLY
    /// Perfect for: Lab-specific processing
    /// </summary>
    [HL7Message(Allows = new[] { "ORU^R01", "ORU^R03", "ORU^R32" })]
    public class LabResult
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        [HL7Segments("OBX")]
        public List<Observation> Observations { get; set; } = new();
        
        // ONLY accepts lab result messages
    }
    
    /// <summary>
    /// Pattern 2d: Pharmacy ONLY
    /// Perfect for: Medication management
    /// </summary>
    [HL7Message(Allows = new[] { "RDE^O11", "RDE^O25", "OMP^O09" })]
    public class PharmacyOrder
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        [HL7Segments("RXO")]
        public List<MedicationOrder> Orders { get; set; } = new();
        
        // ONLY accepts pharmacy messages
    }
    
    // ========== PATTERN 3: BLACKLIST (Allow everything EXCEPT specific types) ==========
    
    /// <summary>
    /// Pattern 3a: BLACKLIST using NotAllows property
    /// Accept anything EXCEPT billing messages
    /// Perfect for: Clinical systems that don't handle billing
    /// </summary>
    [HL7Message(NotAllows = new[] { "BAR^P01", "BAR^P02", "DFT^P03" })]
    public class ClinicalPatient
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        [HL7Field("PID.5.1")]
        public string? LastName { get; set; }
        
        // Accepts: ADT, OMP, ORU, SIU, MDM, etc.
        // Rejects: BAR (billing), DFT (financial)
    }
    
    /// <summary>
    /// Pattern 3b: Exclude test/debug messages
    /// Accept production messages, reject test messages
    /// Perfect for: Production systems
    /// </summary>
    [HL7Message(NotAllows = new[] { "TST^T01", "DBG^D01" })]
    public class ProductionPatient
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        // Accepts all real HL7 messages
        // Rejects test/debug messages
    }
    
    /// <summary>
    /// Pattern 3c: Exclude specific ADT types
    /// Accept all ADT EXCEPT cancellations
    /// Perfect for: Systems that handle cancellations separately
    /// </summary>
    [HL7Message(NotAllows = new[] { "ADT^A11", "ADT^A12", "ADT^A13" })]
    public class NonCancellationADT
    {
        [HL7Field("PID.3")]
        public string? MRN { get; set; }
        
        [HL7Field("PV1.2")]
        public string? PatientClass { get; set; }
        
        // Accepts: ADT^A01, ADT^A02, ADT^A03, etc.
        // Rejects: ADT^A11 (cancel admit), ADT^A12 (cancel transfer), ADT^A13 (cancel discharge)
    }
}

// ========== REAL-WORLD USE CASES ==========

/// <summary>
/// USE CASE 1: Multi-System Integration Hub
/// Receives messages from ANY system, ANY type
/// </summary>
[HL7Message]  // Accept EVERYTHING!
public class IntegrationHubMessage
{
    [HL7Field("MSH.3")]
    public string? SendingApplication { get; set; }
    
    [HL7Field("MSH.4")]
    public string? SendingFacility { get; set; }
    
    [HL7Field("MSH.9")]
    public string? MessageType { get; set; }
    
    [HL7Field("MSH.10")]
    public string? MessageControlID { get; set; }
    
    [HL7Field("PID.3")]
    public string? MRN { get; set; }
    
    // Maps whatever is available from ANY message type!
}

/// <summary>
/// USE CASE 2: Admission/Discharge/Transfer Workflow
/// ONLY ADT messages related to patient movement
/// </summary>
[HL7Message(Allows = new[] {
    "ADT^A01",  // Admit
    "ADT^A02",  // Transfer
    "ADT^A03",  // Discharge
    "ADT^A04",  // Registration
    "ADT^A08"   // Update
})]
public class ADTWorkflowPatient
{
    [HL7Field("PID.3")]
    public string? MRN { get; set; }
    
    [HL7Field("PV1.2")]
    public string? PatientClass { get; set; }
    
    [HL7Field("PV1.3")]
    public string? Location { get; set; }
    
    [HL7Field("MSH.9")]
    public string? EventType { get; set; }
    
    // Strict workflow - ONLY these ADT types allowed
}

/// <summary>
/// USE CASE 3: Clinical Data Repository
/// Accept all clinical data, reject administrative/billing
/// </summary>
[HL7Message(NotAllows = new[] {
    "BAR^P01", "BAR^P02", "BAR^P05", "BAR^P06",  // Billing
    "DFT^P03", "DFT^P11",                         // Financial
    "MFN^M01", "MFN^M02"                          // Master file notifications
})]
public class ClinicalDataPatient
{
    [HL7Field("PID.3")]
    public string? MRN { get; set; }
    
    [HL7Segments("DG1")]
    public List<Diagnosis> Diagnoses { get; set; } = new();
    
    [HL7Segments("OBX")]
    public List<Observation> LabResults { get; set; } = new();
    
    [HL7Segments("RXO")]
    public List<MedicationOrder> Medications { get; set; } = new();
    
    // Clinical focus - administrative stuff handled elsewhere
}

/// <summary>
/// USE CASE 4: Audit Trail / Logger
/// Log EVERYTHING that comes through
/// </summary>
[HL7Message]  // No restrictions!
public class AuditLogEntry
{
    [HL7Field("MSH.7", Format = "yyyyMMddHHmmss")]
    public DateTime? MessageDateTime { get; set; }
    
    [HL7Field("MSH.9")]
    public string? MessageType { get; set; }
    
    [HL7Field("MSH.10")]
    public string? MessageControlID { get; set; }
    
    [HL7Field("PID.3")]
    public string? PatientMRN { get; set; }
    
    // Audit EVERYTHING - no message type restrictions!
}

// ========== SUPPORTING TYPES ==========

public class Observation
{
    [HL7Field("OBX.3.1")]
    public string? ObservationID { get; set; }
    
    [HL7Field("OBX.5")]
    public string? Value { get; set; }
}

public class MedicationOrder
{
    [HL7Field("RXO.1.2")]
    public string? DrugName { get; set; }
}

public class Diagnosis
{
    [HL7Field("DG1.3.1")]
    public string? Code { get; set; }
}
