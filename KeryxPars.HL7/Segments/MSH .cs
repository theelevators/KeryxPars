using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Message Header
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class MSH : ISegment
    {
        public string SegmentId => nameof(MSH);

        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// MSH.1 - Field Separator
        /// </summary>
        public ST FieldSeparator { get; set; }

        /// <summary>
        /// MSH.2 - Encoding Characters (MSH.1 is field separator)
        /// </summary>
        public ST EncodingCharacters { get; set; }

        /// <summary>
        /// MSH.3 - Sending Application
        /// </summary>
        public HD SendingApplication { get; set; }

        /// <summary>
        /// MSH.4 - Sending Facility
        /// </summary>
        public HD SendingFacility { get; set; }

        /// <summary>
        /// MSH.5 - Receiving Application
        /// </summary>
        public HD ReceivingApplication { get; set; }

        /// <summary>
        /// MSH.6 - Receiving Facility
        /// </summary>
        public HD ReceivingFacility { get; set; }

        /// <summary>
        /// MSH.7 - Date/Time of Message
        /// </summary>
        public DTM DateTimeOfMessage { get; set; }

        /// <summary>
        /// MSH.8 - Security
        /// </summary>
        public ST Security { get; set; }

        /// <summary>
        /// MSH.9 - Message Type
        /// </summary>
        public ST MessageType { get; set; }

        /// <summary>
        /// MSH.10 - Message Control ID
        /// </summary>
        public ST MessageControlID { get; set; }

        /// <summary>
        /// MSH.11 - Processing ID
        /// </summary>
        public ST ProcessingID { get; set; }

        /// <summary>
        /// MSH.12 - Version ID
        /// </summary>
        public ST VersionID { get; set; }

        /// <summary>
        /// MSH.13 - Sequence Number
        /// </summary>
        public NM SequenceNumber { get; set; }

        public MSH()
        {
            SegmentType = SegmentType.Universal;
            FieldSeparator = new ST("|");
            EncodingCharacters = new ST("^~\\&");
            SendingApplication = default;
            SendingFacility = default;
            ReceivingApplication = default;
            ReceivingFacility = default;
            DateTimeOfMessage = default;
            Security = default;
            MessageType = default;
            MessageControlID = default;
            ProcessingID = default;
            VersionID = default;
            SequenceNumber = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;

            switch (element)
            {
                case 1: EncodingCharacters = new ST(value); break;
                case 2:
                    var hd2 = new HD();
                    hd2.Parse(value.AsSpan(), delimiters);
                    SendingApplication = hd2;
                    break;
                case 3:
                    var hd3 = new HD();
                    hd3.Parse(value.AsSpan(), delimiters);
                    SendingFacility = hd3;
                    break;
                case 4:
                    var hd4 = new HD();
                    hd4.Parse(value.AsSpan(), delimiters);
                    ReceivingApplication = hd4;
                    break;
                case 5:
                    var hd5 = new HD();
                    hd5.Parse(value.AsSpan(), delimiters);
                    ReceivingFacility = hd5;
                    break;
                case 6: DateTimeOfMessage = new DTM(value); break;
                case 7: Security = new ST(value); break;
                case 8: MessageType = new ST(value); break;
                case 9: MessageControlID = new ST(value); break;
                case 10: ProcessingID = new ST(value); break;
                case 11: VersionID = new ST(value); break;
                case 12: SequenceNumber = new NM(value); break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;

            return
            [
                SegmentId,
                FieldSeparator,
                EncodingCharacters.ToHL7String(delimiters),
                SendingApplication.ToHL7String(delimiters),
                SendingFacility.ToHL7String(delimiters),
                ReceivingApplication.ToHL7String(delimiters),
                ReceivingFacility.ToHL7String(delimiters),
                DateTimeOfMessage.ToHL7String(delimiters),
                Security.ToHL7String(delimiters),
                MessageType.ToHL7String(delimiters),
                MessageControlID.ToHL7String(delimiters),
                ProcessingID.ToHL7String(delimiters),
                VersionID.ToHL7String(delimiters),
                SequenceNumber.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => FieldSeparator, // Field separator
                2 => EncodingCharacters.Value,
                3 => SendingApplication.ToHL7String(HL7Delimiters.Default),
                4 => SendingFacility.ToHL7String(HL7Delimiters.Default),
                5 => ReceivingApplication.ToHL7String(HL7Delimiters.Default),
                6 => ReceivingFacility.ToHL7String(HL7Delimiters.Default),
                7 => DateTimeOfMessage.Value,
                8 => Security.Value,
                9 => MessageType.Value,
                10 => MessageControlID.Value,
                11 => ProcessingID.Value,
                12 => VersionID.Value,
                13 => SequenceNumber.Value,
                _ => null
            };
        }
    }
}
