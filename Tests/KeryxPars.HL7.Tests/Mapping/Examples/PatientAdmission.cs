using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Example domain model for testing the mapping framework.
/// This class demonstrates how to use the HL7 mapping attributes.
/// </summary>
[HL7Message("ADT^A01", "ADT^A04")]
public class PatientAdmission
{
    [HL7Field("PID.3")]
    public string PatientId { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    [HL7Field("PID.5.2")]
    public string FirstName { get; set; } = string.Empty;

    [HL7Field("PID.5.3")]
    public string MiddleName { get; set; } = string.Empty;

    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public string DateOfBirth { get; set; } = string.Empty;

    [HL7Field("PID.8")]
    public string Gender { get; set; } = string.Empty;

    [HL7Field("PID.11.1")]
    public string StreetAddress { get; set; } = string.Empty;

    [HL7Field("PID.11.3")]
    public string City { get; set; } = string.Empty;

    [HL7Field("PID.11.4")]
    public string State { get; set; } = string.Empty;

    [HL7Field("PID.11.5")]
    public string ZipCode { get; set; } = string.Empty;

    [HL7Field("PV1.2")]
    public string PatientClass { get; set; } = string.Empty;

    [HL7Field("PV1.3.1")]
    public string AssignedLocation { get; set; } = string.Empty;
}
