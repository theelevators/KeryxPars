using System;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Patient demographics with nested objects.
/// Demonstrates complex field mapping with [HL7Complex].
/// </summary>
[HL7Message("ADT^A01", "ADT^A04", "ADT^A08")]
public class PatientWithNestedObjects
{
    // Simple fields
    [HL7Field("PID.3")]
    public string PatientId { get; set; } = string.Empty;

    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime DateOfBirth { get; set; }

    [HL7Field("PID.8")]
    public Gender Gender { get; set; }

    // Nested complex fields
    [HL7Complex(BaseFieldPath = "PID.5")]
    public PersonName Name { get; set; } = new();

    [HL7Complex(BaseFieldPath = "PID.11")]
    public Address HomeAddress { get; set; } = new();

    [HL7Complex(BaseFieldPath = "PID.13")]
    public PhoneNumber HomePhone { get; set; } = new();

    // Visit info
    [HL7Field("PV1.2")]
    public PatientClass? PatientClass { get; set; }

    [HL7Field("PV1.19")]
    public string VisitNumber { get; set; } = string.Empty;
}


