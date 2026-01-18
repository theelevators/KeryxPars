using KeryxPars.HL7.Segments;

namespace KeryxPars.HL7.Definitions;

public record MedicationOrder()
{
    public ORC Orc { get; set; } = new();
    public RXE Rxe { get; set; } = new();
    public RXO Rxo { get; set; } = new();

    public List<RXR> RXR { get; set; } = [];
    public List<RXC> RXC { get; set; } = [];
    public List<TQ1> TQ1 { get; set; } = [];
}
