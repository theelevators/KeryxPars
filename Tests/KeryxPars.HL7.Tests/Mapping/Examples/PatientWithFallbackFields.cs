using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Demonstrates FALLBACK FIELD mapping - try primary, fallback if empty!
/// </summary>
[HL7Message("ADT^A01")]
public class PatientWithFallbackFields
{
    [HL7Field("PID.3")]
    public string MRN { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    [HL7Field("PID.8")]
    public Gender Gender { get; set; }

    // ========== FALLBACK FIELD EXAMPLES ==========

    /// <summary>
    /// Phone number - try home phone (PID.13), fallback to work phone (PID.14) if empty.
    /// </summary>
    [HL7Field("PID.13", FallbackField = "PID.14")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Email - try primary email (PID.13.6), fallback to secondary (PID.14.6).
    /// </summary>
    [HL7Field("PID.13.6", FallbackField = "PID.14.6")]
    public string? Email { get; set; }

    /// <summary>
    /// Emergency contact - try NK1.2, fallback to PID.25 if empty.
    /// Cross-segment fallback!
    /// </summary>
    [HL7Field("NK1.2", FallbackField = "PID.25")]
    public string? EmergencyContactName { get; set; }

    /// <summary>
    /// SSN - try PID.19, fallback to PID.2 (patient ID).
    /// </summary>
    [HL7Field("PID.19", FallbackField = "PID.2")]
    public string? SSN { get; set; }

    /// <summary>
    /// Attending doctor - try PV1.7, fallback to PV1.17 (admitting doctor).
    /// </summary>
    [HL7Field("PV1.7.1", FallbackField = "PV1.17.1")]
    public string? AttendingDoctor { get; set; }

    // Supporting fields
    [HL7Field("PV1.2")]
    public PatientClass PatientClass { get; set; }
}
