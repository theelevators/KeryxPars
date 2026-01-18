using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Error Information
    /// </summary>
    public class ERR : ISegment
    {
        public string SegmentId => nameof(ERR);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-properties
        /// <summary>
        /// ERR.1
        /// </summary>
        public string ErrorCodeAndLocation { get; set; }

        /// <summary>
        /// ERR.2
        /// </summary>
        public string ErrorLocation { get; set; }

        /// <summary>
        /// ERR.3
        /// </summary>
        public string Hl7ErrorCode { get; set; }

        /// <summary>
        /// ERR.4
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// ERR.5
        /// </summary>
        public string ApplicationErrorCode { get; set; }

        /// <summary>
        /// ERR.6
        /// </summary>
        public string ApplicationErrorParameter { get; set; }

        /// <summary>
        /// ERR.7
        /// </summary>
        public string DiagnosticInformation { get; set; }

        /// <summary>
        /// ERR.8
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// ERR.9
        /// </summary>
        public string InformPersonIndicator { get; set; }

        /// <summary>
        /// ERR.10
        /// </summary>
        public string OverrideType { get; set; }

        /// <summary>
        /// ERR.11
        /// </summary>
        public string OverrideReasonCode { get; set; }

        /// <summary>
        /// ERR.12
        /// </summary>
        public string HelpDeskContactPoint { get; set; }

        // Constructors

        public ERR()
        {
            SegmentType = SegmentType.Universal;
        }

        public ERR(string severity, string message)
        {
            SegmentType = SegmentType.Universal;
            Severity = severity;
            UserMessage = message;
        }

        public ERR(string severity, string medIdentifier, string message)
        {
            SegmentType = SegmentType.Universal;
            Severity = severity;
            DiagnosticInformation = medIdentifier;
            UserMessage = message;
        }

        public ERR(string severity, ErrorCode errorCode, string message)
        {
            SegmentType = SegmentType.Universal;
            Severity = severity;
            UserMessage = message;
            Hl7ErrorCode = Convert.ToString((int)errorCode);
        }

        public ERR(string severity, ErrorCode errorCode, string message, string segment, char separator, int field)
        {
            SegmentType = SegmentType.Universal;
            ErrorLocation = segment + separator + separator + field.ToString();
            Severity = severity;
            UserMessage = message;
            Hl7ErrorCode = Convert.ToString((int)errorCode);
        }

        public ERR(string severity, ErrorCode errorCode, string message, string segment, char separator, int field, int component)
        {
            SegmentType = SegmentType.Universal;
            ErrorLocation = segment + separator + separator + field.ToString() + separator + separator + component.ToString();
            Severity = severity;
            UserMessage = message;
            Hl7ErrorCode = Convert.ToString((int)errorCode);
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: ErrorCodeAndLocation = value; break;
                case 2: ErrorLocation = value; break;
                case 3: Hl7ErrorCode = value; break;
                case 4: Severity = value; break;
                case 5: ApplicationErrorCode = value; break;
                case 6: ApplicationErrorParameter = value; break;
                case 7: DiagnosticInformation = value; break;
                case 8: UserMessage = value; break;
                case 9: InformPersonIndicator = value; break;
                case 10: OverrideType = value; break;
                case 11: OverrideReasonCode = value; break;
                case 12: HelpDeskContactPoint = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                ErrorCodeAndLocation,
                ErrorLocation,
                Hl7ErrorCode,
                Severity,
                ApplicationErrorCode,
                ApplicationErrorParameter,
                DiagnosticInformation,
                UserMessage,
                InformPersonIndicator,
                OverrideType,
                OverrideReasonCode,
                HelpDeskContactPoint
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => ErrorCodeAndLocation,
                2 => ErrorLocation,
                3 => Hl7ErrorCode,
                4 => Severity,
                5 => ApplicationErrorCode,
                6 => ApplicationErrorParameter,
                7 => DiagnosticInformation,
                8 => UserMessage,
                9 => InformPersonIndicator,
                10 => OverrideType,
                11 => OverrideReasonCode,
                12 => HelpDeskContactPoint,
                _ => null
            };
        }
    }
}
