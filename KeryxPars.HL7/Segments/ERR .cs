using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Error Information
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class ERR : ISegment
    {
        public string SegmentId => nameof(ERR);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// ERR.1 - Error Code and Location (deprecated in 2.5, use ERR.2 and ERR.3)
        /// </summary>
        public ST ErrorCodeAndLocation { get; set; }

        /// <summary>
        /// ERR.2 - Error Location
        /// </summary>
        public ST[] ErrorLocation { get; set; }

        /// <summary>
        /// ERR.3 - HL7 Error Code
        /// </summary>
        public CWE Hl7ErrorCode { get; set; }

        /// <summary>
        /// ERR.4 - Severity
        /// </summary>
        public ID Severity { get; set; }

        /// <summary>
        /// ERR.5 - Application Error Code
        /// </summary>
        public CWE ApplicationErrorCode { get; set; }

        /// <summary>
        /// ERR.6 - Application Error Parameter
        /// </summary>
        public ST[] ApplicationErrorParameter { get; set; }

        /// <summary>
        /// ERR.7 - Diagnostic Information
        /// </summary>
        public TX DiagnosticInformation { get; set; }

        /// <summary>
        /// ERR.8 - User Message
        /// </summary>
        public TX UserMessage { get; set; }

        /// <summary>
        /// ERR.9 - Inform Person Indicator
        /// </summary>
        public IS[] InformPersonIndicator { get; set; }

        /// <summary>
        /// ERR.10 - Override Type
        /// </summary>
        public CWE OverrideType { get; set; }

        /// <summary>
        /// ERR.11 - Override Reason Code
        /// </summary>
        public CWE[] OverrideReasonCode { get; set; }

        /// <summary>
        /// ERR.12 - Help Desk Contact Point
        /// </summary>
        public XTN[] HelpDeskContactPoint { get; set; }

        public ERR()
        {
            SegmentType = SegmentType.Universal;
            ErrorCodeAndLocation = default;
            ErrorLocation = [];
            Hl7ErrorCode = default;
            Severity = default;
            ApplicationErrorCode = default;
            ApplicationErrorParameter = [];
            DiagnosticInformation = default;
            UserMessage = default;
            InformPersonIndicator = [];
            OverrideType = default;
            OverrideReasonCode = [];
            HelpDeskContactPoint = [];
        }

        public ERR(string severity, string message)
        {
            SegmentType = SegmentType.Universal;
            Severity = new ID(severity);
            UserMessage = new TX(message);
            ErrorCodeAndLocation = default;
            ErrorLocation = [];
            Hl7ErrorCode = default;
            ApplicationErrorCode = default;
            ApplicationErrorParameter = [];
            DiagnosticInformation = default;
            InformPersonIndicator = [];
            OverrideType = default;
            OverrideReasonCode = [];
            HelpDeskContactPoint = [];
        }

        public ERR(string severity, string medIdentifier, string message)
        {
            SegmentType = SegmentType.Universal;
            Severity = new ID(severity);
            DiagnosticInformation = new TX(medIdentifier);
            UserMessage = new TX(message);
            ErrorCodeAndLocation = default;
            ErrorLocation = [];
            Hl7ErrorCode = default;
            ApplicationErrorCode = default;
            ApplicationErrorParameter = [];
            InformPersonIndicator = [];
            OverrideType = default;
            OverrideReasonCode = [];
            HelpDeskContactPoint = [];
        }

        public ERR(string severity, ErrorCode errorCode, string message)
        {
            SegmentType = SegmentType.Universal;
            Severity = new ID(severity);
            UserMessage = new TX(message);
            Hl7ErrorCode = new CWE(identifier: new ST(Convert.ToString((int)errorCode)));
            ErrorCodeAndLocation = default;
            ErrorLocation = [];
            ApplicationErrorCode = default;
            ApplicationErrorParameter = [];
            DiagnosticInformation = default;
            InformPersonIndicator = [];
            OverrideType = default;
            OverrideReasonCode = [];
            HelpDeskContactPoint = [];
        }

        public ERR(string severity, ErrorCode errorCode, string message, string segment, char separator, int field)
        {
            SegmentType = SegmentType.Universal;
            ErrorLocation = [new ST(segment + separator + separator + field.ToString())];
            Severity = new ID(severity);
            UserMessage = new TX(message);
            Hl7ErrorCode = new CWE(identifier: new ST(Convert.ToString((int)errorCode)));
            ErrorCodeAndLocation = default;
            ApplicationErrorCode = default;
            ApplicationErrorParameter = [];
            DiagnosticInformation = default;
            InformPersonIndicator = [];
            OverrideType = default;
            OverrideReasonCode = [];
            HelpDeskContactPoint = [];
        }

        public ERR(string severity, ErrorCode errorCode, string message, string segment, char separator, int field, int component)
        {
            SegmentType = SegmentType.Universal;
            ErrorLocation = [new ST(segment + separator + separator + field.ToString() + separator + separator + component.ToString())];
            Severity = new ID(severity);
            UserMessage = new TX(message);
            Hl7ErrorCode = new CWE(identifier: new ST(Convert.ToString((int)errorCode)));
            ErrorCodeAndLocation = default;
            ApplicationErrorCode = default;
            ApplicationErrorParameter = [];
            DiagnosticInformation = default;
            InformPersonIndicator = [];
            OverrideType = default;
            OverrideReasonCode = [];
            HelpDeskContactPoint = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: ErrorCodeAndLocation = new ST(value); break;
                case 2:
                    ErrorLocation = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters);
                    break;
                case 3:
                    var cwe3 = new CWE();
                    cwe3.Parse(value.AsSpan(), delimiters);
                    Hl7ErrorCode = cwe3;
                    break;
                case 4: Severity = new ID(value); break;
                case 5:
                    var cwe5 = new CWE();
                    cwe5.Parse(value.AsSpan(), delimiters);
                    ApplicationErrorCode = cwe5;
                    break;
                case 6:
                    ApplicationErrorParameter = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters);
                    break;
                case 7: DiagnosticInformation = new TX(value); break;
                case 8: UserMessage = new TX(value); break;
                case 9:
                    InformPersonIndicator = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters);
                    break;
                case 10:
                    var cwe10 = new CWE();
                    cwe10.Parse(value.AsSpan(), delimiters);
                    OverrideType = cwe10;
                    break;
                case 11:
                    OverrideReasonCode = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters);
                    break;
                case 12:
                    HelpDeskContactPoint = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters);
                    break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                ErrorCodeAndLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ErrorLocation, delimiters),
                Hl7ErrorCode.ToHL7String(delimiters),
                Severity.ToHL7String(delimiters),
                ApplicationErrorCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ApplicationErrorParameter, delimiters),
                DiagnosticInformation.ToHL7String(delimiters),
                UserMessage.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(InformPersonIndicator, delimiters),
                OverrideType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OverrideReasonCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(HelpDeskContactPoint, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => ErrorCodeAndLocation.Value,
                2 => SegmentFieldHelper.JoinRepeatingField(ErrorLocation, delimiters),
                3 => Hl7ErrorCode.ToHL7String(delimiters),
                4 => Severity.Value,
                5 => ApplicationErrorCode.ToHL7String(delimiters),
                6 => SegmentFieldHelper.JoinRepeatingField(ApplicationErrorParameter, delimiters),
                7 => DiagnosticInformation.Value,
                8 => UserMessage.Value,
                9 => SegmentFieldHelper.JoinRepeatingField(InformPersonIndicator, delimiters),
                10 => OverrideType.ToHL7String(delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(OverrideReasonCode, delimiters),
                12 => SegmentFieldHelper.JoinRepeatingField(HelpDeskContactPoint, delimiters),
                _ => null
            };
        }
    }
}
