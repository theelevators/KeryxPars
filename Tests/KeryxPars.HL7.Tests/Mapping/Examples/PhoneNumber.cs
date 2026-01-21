using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Represents a phone number (HL7 XTN data type).
/// </summary>
[HL7Message("XTN")] // Marker for source generator to create mapper
public class PhoneNumber
{
    [HL7Component(1)]
    public string PhoneNumberFormatted { get; set; } = string.Empty;

    [HL7Component(2)]
    public string TelecommunicationUseCode { get; set; } = string.Empty;

    [HL7Component(3)]
    public string TelecommunicationEquipmentType { get; set; } = string.Empty;

    [HL7Component(4)]
    public string EmailAddress { get; set; } = string.Empty;

    [HL7Component(5)]
    public string CountryCode { get; set; } = string.Empty;

    [HL7Component(6)]
    public string AreaCityCode { get; set; } = string.Empty;

    [HL7Component(7)]
    public string LocalNumber { get; set; } = string.Empty;

    public override string ToString() => 
        !string.IsNullOrEmpty(PhoneNumberFormatted) 
            ? PhoneNumberFormatted 
            : $"+{CountryCode} ({AreaCityCode}) {LocalNumber}".Trim();
}
