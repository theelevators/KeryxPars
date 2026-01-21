using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Demonstrates CONDITIONAL MAPPING - a killer feature!
/// Fields are only mapped when specific conditions are met.
/// </summary>
[HL7Message("ADT^A01", "ADT^A04")]
public class PatientWithConditionalFields
{
    // Always mapped
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

    // ========== CONDITIONAL FIELDS ==========

    /// <summary>
    /// Pregnancy status - ONLY mapped if patient is FEMALE.
    /// Condition: PID.8 == F
    /// </summary>
    [HL7Field("PID.31", Condition = "PID.8 == F")]
    public string? PregnancyStatus { get; set; }

    /// <summary>
    /// Bed assignment - ONLY mapped if patient is INPATIENT.
    /// Condition: PV1.2 == I
    /// </summary>
    [HL7Field("PV1.3", Condition = "PV1.2 == I")]
    public string BedAssignment { get; set; } = string.Empty;

    /// <summary>
    /// Room number - ONLY for inpatients.
    /// Condition: PV1.2 == I
    /// </summary>
    [HL7Field("PV1.3.1", Condition = "PV1.2 == I")]
    public string RoomNumber { get; set; } = string.Empty;

    /// <summary>
    /// Discharge date - ONLY if patient class is NOT outpatient.
    /// Condition: PV1.2 != O
    /// </summary>
    [HL7Field("PV1.45", Format = "yyyyMMdd", Condition = "PV1.2 != O")]
    public DateTime? DischargeDate { get; set; }

    /// <summary>
    /// SSN - ONLY required for US patients.
    /// Uses SkipIfEmpty to avoid errors when not present.
    /// </summary>
    [HL7Field("PID.19", SkipIfEmpty = true)]
    public string SSN { get; set; } = string.Empty;

    // Other fields
    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }

    [HL7Field("PV1.19")]
    public string VisitNumber { get; set; } = string.Empty;
}
