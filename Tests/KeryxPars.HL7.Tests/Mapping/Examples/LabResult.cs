using System;
using System.Collections.Generic;
using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Lab result message (ORU^R01) with multiple observations.
/// Demonstrates collection mapping with [HL7Segments].
/// </summary>
[HL7Message("ORU^R01")]
public class LabResult
{
    // Patient info
    [HL7Field("PID.3")]
    public string PatientId { get; set; } = string.Empty;

    [HL7Field("PID.5.1")]
    public string LastName { get; set; } = string.Empty;

    [HL7Field("PID.5.2")]
    public string FirstName { get; set; } = string.Empty;

    // Order info
    [HL7Field("OBR.2")]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    [HL7Field("OBR.4.1")]
    public string TestName { get; set; } = string.Empty;

    [HL7Field("OBR.7", Format = "yyyyMMddHHmmss")]
    public DateTime? ObservationDateTime { get; set; }

    // Collection of observations (OBX segments)
    [HL7Segments("OBX")]
    public List<Observation> Observations { get; set; } = new();
}

/// <summary>
/// Single observation from an OBX segment.
/// </summary>
[HL7Message("OBX")]  // Marker for source generator
public class Observation
{
    [HL7Field("OBX.1")]
    public string SetId { get; set; } = string.Empty;

    [HL7Field("OBX.2")]
    public string ValueType { get; set; } = string.Empty;

    [HL7Field("OBX.3.1")]
    public string ObservationIdentifier { get; set; } = string.Empty;

    [HL7Field("OBX.3.2")]
    public string ObservationText { get; set; } = string.Empty;

    [HL7Field("OBX.5")]
    public string ObservationValue { get; set; } = string.Empty;

    [HL7Field("OBX.6.1")]
    public string Units { get; set; } = string.Empty;

    [HL7Field("OBX.7")]
    public string ReferenceRange { get; set; } = string.Empty;

    [HL7Field("OBX.8")]
    public string AbnormalFlags { get; set; } = string.Empty;

    [HL7Field("OBX.11")]
    public string ObservationResultStatus { get; set; } = string.Empty;
}
