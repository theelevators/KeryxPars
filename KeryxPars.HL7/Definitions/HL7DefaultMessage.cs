namespace KeryxPars.HL7.Definitions;

using KeryxPars.HL7.Segments;

/// <summary>
/// Default HL7 message implementation with comprehensive segment support.
/// Previously known as HL7Message - this is the general-purpose message class
/// that supports all common HL7 segments for ADT, medication orders, and other message types.
/// </summary>
public class HL7DefaultMessage : HL7BaseMessage
{
    public PV1 Pv1 { get; set; } = new();
    public PV2 Pv2 { get; set; } = new();

    // Repeatable segments
    public List<AL1> Allergies { get; set; } = [];
    public List<DG1> Diagnoses { get; set; } = [];
    public List<IN1> Insurance { get; set; } = [];
    public List<OrderGroup> Orders { get; set; } = [];
}
