using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Enhanced patient admission with DateTime and Enum support.
/// Demonstrates type conversion capabilities.
/// </summary>
[HL7Message("ADT^A01", "ADT^A04", "ADT^A08")]
public class PatientAdmissionEnhanced
{
    // Basic fields
    [HL7Field("PID.3")]
    public string PatientId { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    [HL7Field("PID.5.2")]
    public string FirstName { get; set; } = string.Empty;

    // DateTime fields with format
    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime DateOfBirth { get; set; }

    [HL7Field("MSH.7", Format = "yyyyMMddHHmmss")]
    public DateTime? MessageDateTime { get; set; }

    [HL7Field("EVN.2", Format = "yyyyMMddHHmmss")]
    public DateTime? EventDateTime { get; set; }

    // Enum field
    [HL7Field("PID.8")]
    public Gender Gender { get; set; }

    [HL7Field("PV1.2")]
    public PatientClass? PatientClass { get; set; }

    // Address components
    [HL7Field("PID.11.1")]
    public string StreetAddress { get; set; } = string.Empty;

    [HL7Field("PID.11.3")]
    public string City { get; set; } = string.Empty;

    [HL7Field("PID.11.4")]
    public string State { get; set; } = string.Empty;

    [HL7Field("PID.11.5")]
    public string ZipCode { get; set; } = string.Empty;

    // Visit information
    [HL7Field("PV1.3.1")]
    public string AssignedLocation { get; set; } = string.Empty;

    [HL7Field("PV1.19")]
    public string VisitNumber { get; set; } = string.Empty;
}

/// <summary>
/// Gender enum matching HL7 values.
/// </summary>
public enum Gender
{
    Unknown = 0,
    Male = 1,
    Female = 2,
    Other = 3,
    /// <summary>M</summary>
    M = Male,
    /// <summary>F</summary>
    F = Female,
    /// <summary>O</summary>
    O = Other,
    /// <summary>U</summary>
    U = Unknown
}

/// <summary>
/// Patient class enum matching HL7 values.
/// </summary>
public enum PatientClass
{
    /// <summary>Emergency</summary>
    E,
    /// <summary>Inpatient</summary>
    I,
    /// <summary>Outpatient</summary>
    O,
    /// <summary>Preadmit</summary>
    P,
    /// <summary>Recurring patient</summary>
    R,
    /// <summary>Observation</summary>
    B
}
