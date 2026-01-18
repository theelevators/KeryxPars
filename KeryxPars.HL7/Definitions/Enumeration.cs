
namespace KeryxPars.HL7.Definitions;

public enum ErrorSeverity
{
    /// <summary>
    /// Transaction successful, but includes information.
    /// </summary>
    Info,
    /// <summary>
    /// Transaction successful, but there may be issues.
    /// </summary>
    Warning,
    /// <summary>
    /// Transaction was unsuccessful.
    /// </summary>
    Error,
    /// <summary>
    /// Message not processed due to application.
    /// </summary>
    InternalError
}

/// <summary>
/// Specific HL7 error codes to be used in acknowledgement messages
/// </summary>
public enum ErrorCode
{
    // Used for HL7 ERR.4 element
    MessageAccepted = 0,
    SegmentSequenceError = 100,
    RequiredFieldMissing = 101, // Used if mandatory data element is not included
    DataTypeError = 102, // Used for type mismatches, as well as general data errors (such as multiple primary diagnoses)
    TableValueNotFound = 103, // Used when referencing value sets
    ValueTooLong = 104,
    UnsupportedMessageType = 200,
    UnsupportedEventCode = 201,
    UnsupportedProcessingID = 202,
    UnsupportedVersionID = 203,
    UnknownKeyIdentifier = 204, // Patient not found, used for all messages except A01
    DuplicateKeyIdentifier = 205, // Patient exists, used for only A01 messages
    ApplicationRecordLocked = 206, // Use for error in DAL
    ApplicationInternalError = 207 // Use for all other errors
}

/// <summary>
/// HL7 messages types
/// </summary>
public enum EventType
{
    /// <summary>
    /// A01/A04
    /// </summary>
    NewAdmit,

    /// <summary>
    /// A08
    /// </summary>
    Update,

    /// <summary>
    /// A03
    /// </summary>
    Discharge,

    /// <summary>
    /// A05/A14
    /// </summary>
    Preadmit,

    /// <summary>
    /// O11, O01, O09
    /// </summary>
    MedicationOrder,

    /// <summary>
    /// A10
    /// </summary>
    TrackingTrigger,

    /// <summary>
    /// Message type not defined
    /// </summary>
    Unknown
}

/// <summary>
/// Incoming message type for the message.
/// </summary>
public enum IncomingMessageType
{
    ADT = 1,
    MedOrder = 2,
    Others = 3
}