namespace KeryxPars.HL7.Definitions;

using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Segments;
using System.Text.Json;

/// <summary>
/// Abstract base class for all HL7 message types.
/// Provides common functionality and properties shared across all HL7 messages.
/// </summary>
public abstract class HL7BaseMessage : IHL7Message
{
    public string MessageControlID { get; set; } = string.Empty;
    public IncomingMessageType MessageType { get; set; }
    public MSH Msh { get; set; } = new();
    public EVN? Evn { get; set; }
    public PID? Pid { get; set; }
    public EventType EventType { get; set; } = EventType.Unknown;
    public List<ERR> Errors { get; set; } = [];
    public MSH? MshResponse { get; set; }

    /// <summary>
    /// Virtual method to allow derived classes to customize JSON serialization
    /// </summary>
    public virtual string Dump()
    {
        return JsonSerializer.Serialize(this, this.GetType(), new JsonSerializerOptions 
        { 
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });
    }
}
