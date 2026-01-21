using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Represents an address (HL7 XAD data type).
/// </summary>
[HL7Message("XAD")] // Marker for source generator to create mapper
public class Address
{
    [HL7Component(1)]
    public string StreetAddress { get; set; } = string.Empty;

    [HL7Component(2)]
    public string OtherDesignation { get; set; } = string.Empty;

    [HL7Component(3)]
    public string City { get; set; } = string.Empty;

    [HL7Component(4)]
    public string StateOrProvince { get; set; } = string.Empty;

    [HL7Component(5)]
    public string ZipOrPostalCode { get; set; } = string.Empty;

    [HL7Component(6)]
    public string Country { get; set; } = string.Empty;

    [HL7Component(7)]
    public string AddressType { get; set; } = string.Empty;

    public override string ToString() => 
        $"{StreetAddress}, {City}, {StateOrProvince} {ZipOrPostalCode}".Trim();
}
