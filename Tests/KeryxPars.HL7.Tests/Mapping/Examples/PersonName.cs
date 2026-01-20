using KeryxPars.HL7.Mapping;

namespace KeryxPars.HL7.Tests.Mapping.Examples;

/// <summary>
/// Represents a person's name (HL7 XPN data type).
/// </summary>
[HL7Message("XPN")] // Marker for source generator to create mapper
public class PersonName
{
    [HL7Component(1)]
    public string LastName { get; set; } = string.Empty;

    [HL7Component(2)]
    public string FirstName { get; set; } = string.Empty;

    [HL7Component(3)]
    public string MiddleName { get; set; } = string.Empty;

    [HL7Component(4)]
    public string Suffix { get; set; } = string.Empty;

    [HL7Component(5)]
    public string Prefix { get; set; } = string.Empty;

    [HL7Component(6)]
    public string Degree { get; set; } = string.Empty;

    public override string ToString() => 
        $"{Prefix} {FirstName} {MiddleName} {LastName} {Suffix} {Degree}".Trim();
}
