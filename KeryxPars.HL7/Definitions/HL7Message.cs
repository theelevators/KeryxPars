using KeryxPars.HL7.Segments;
using System.Text.Json;

namespace KeryxPars.HL7.Definitions;

/// <summary>
/// Incoming HL7 message. Can be either ADT or Medication type.
/// </summary>
public class HL7Message
{
    public string MessageControlID { get; internal set; }
    public IncomingMessageType MessageType { get; internal set; }

    public MSH Msh { get; set; } = new();
    public EVN Evn { get; set; } = new();
    public PID Pid { get; set; } = new();
    public PV1 Pv1 { get; set; } = new();
    public PV2 Pv2 { get; set; } = new();
    public MSH MshResponse { get; set; } = new();

    // These segments are repeatable in messages
    public List<ERR> Errors { get; set; } = [];
    public List<AL1> Allergies { get; set; } = [];
    public List<DG1> Diagnoses { get; set; } = [];
    public List<IN1> Insurance { get; set; } = [];
    public List<OrderGroup> Orders { get; set; } = [];

    public EventType EventType { get; set; } = EventType.Unknown;

    public string Dump()
    {
        return JsonSerializer.Serialize(this);
    }
}