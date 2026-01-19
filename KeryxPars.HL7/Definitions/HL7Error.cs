using KeryxPars.Core.Models;

namespace KeryxPars.HL7.Definitions;

public record HL7Error(ErrorSeverity Severity, ErrorCode Code, string Message) : Error
{
    public static HL7Error EventNotSupported => new(ErrorSeverity.Error, ErrorCode.UnsupportedEventCode, "HL7 event type not supported");

    public static HL7Error InvalidMessageStart => new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Invalid message start");

    public static HL7Error MultipleEVNSegments => new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple EVN segments found");

    public static HL7Error MultipleMSHSegments => new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple MSH segments found");

    public static HL7Error MultiplePIDSegments => new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple PID segments found");

    public static HL7Error MultiplePV1Segments => new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple PV1 segments found");

    public static HL7Error MultiplePV2Segments => new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple PV2 segments found");

    public static HL7Error NoEVNSegment => new(ErrorSeverity.Error, ErrorCode.RequiredFieldMissing, "No EVN segment found");

    public static HL7Error NoPIDSegment => new(ErrorSeverity.Error, ErrorCode.RequiredFieldMissing, "No PID segment found");
    public static HL7Error NoPV1Segment => new(ErrorSeverity.Error, ErrorCode.RequiredFieldMissing, "No PV1 segment found");

    public static HL7Error ControlCharactersNotProvided => new(ErrorSeverity.Error, ErrorCode.RequiredFieldMissing, "Control characters not provided");

    public static HL7Error UnknownError => new(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, "An unknown error occurred");

}

