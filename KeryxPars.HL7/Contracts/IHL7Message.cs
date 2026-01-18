namespace KeryxPars.HL7.Contracts;

using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;

/// <summary>
/// Base interface for all HL7 message types.
/// Defines the common structure shared across all HL7 messages.
/// </summary>
public interface IHL7Message
{
    /// <summary>
    /// Message control ID from MSH segment
    /// </summary>
    string MessageControlID { get; set; }

    /// <summary>
    /// Type of incoming message (ADT, MedOrder, Others)
    /// </summary>
    IncomingMessageType MessageType { get; set; }

    /// <summary>
    /// Message Header segment - Required in all HL7 messages
    /// </summary>
    MSH Msh { get; set; }

    /// <summary>
    /// Event Type segment - Common in most HL7 messages
    /// </summary>
    EVN? Evn { get; set; }

    /// <summary>
    /// Patient Identification segment - Common in most HL7 messages
    /// </summary>
    PID? Pid { get; set; }

    /// <summary>
    /// Event type parsed from message
    /// </summary>
    EventType EventType { get; set; }

    /// <summary>
    /// Error segments encountered during processing
    /// </summary>
    List<ERR> Errors { get; set; }

    /// <summary>
    /// Response MSH segment for acknowledgment messages
    /// </summary>
    MSH? MshResponse { get; set; }

    /// <summary>
    /// Serializes the message to a JSON string for debugging/logging
    /// </summary>
    string Dump();
}
