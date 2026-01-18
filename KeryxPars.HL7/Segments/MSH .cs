using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Message Header
    /// </summary>
    public class MSH : ISegment
    {
        public string SegmentId => nameof(MSH);
        
        public SegmentType SegmentType { get; private set; }

        #region Properties

        /// <summary>
        /// MSA.1
        /// </summary>
        public string SendingApplication { get; set; }

        /// <summary>
        /// MSA.2
        /// </summary>
        public string SendingFacility { get; set; }

        /// <summary>
        /// MSA.3
        /// </summary>
        public string ReceivingApplication { get; set; }

        /// <summary>
        /// MSA.4
        /// </summary>
        public string ReceivingFacility { get; set; }

        /// <summary>
        /// MSA.5
        /// </summary>
        public string DateTimeOfMessage { get; set; }

        /// <summary>
        /// MSA.6
        /// </summary>
        public string Security { get; set; }

        /// <summary>
        /// MSA.7
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// MSA.8
        /// </summary>
        public string MessageControlID { get; set; }

        /// <summary>
        /// MSA.9
        /// </summary>
        public string ProcessingID { get; set; }

        /// <summary>
        /// MSA.10
        /// </summary>
        public string VersionID { get; set; }

        /// <summary>
        /// MSA.11
        /// </summary>
        public string SequenceNumber { get; set; }

        #endregion

        // Constructors
        public MSH()
        {
            SegmentType = SegmentType.Universal;
            SendingApplication = string.Empty;
            SendingFacility = string.Empty;
            ReceivingApplication = string.Empty;
            ReceivingFacility = string.Empty;
            DateTimeOfMessage = string.Empty;
            Security = string.Empty;
            MessageType = string.Empty;
            MessageControlID = string.Empty;
            ProcessingID = string.Empty;
            VersionID = string.Empty;
            SequenceNumber = string.Empty;
        }

        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 2: SendingApplication = value; break;
                case 3: SendingFacility = value; break;
                case 4: ReceivingApplication = value; break;
                case 5: ReceivingFacility = value; break;
                case 6: DateTimeOfMessage = value; break;
                case 7: Security = value; break;
                case 8: MessageType = value; break;
                case 9: MessageControlID = value; break;
                case 10: ProcessingID = value; break;
                case 11: VersionID = value; break;
                case 12: SequenceNumber = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                null, // MSH.1 is field separator (not stored in properties)
                SendingApplication,
                SendingFacility,
                ReceivingApplication,
                ReceivingFacility,
                DateTimeOfMessage,
                Security,
                MessageType,
                MessageControlID,
                ProcessingID,
                VersionID,
                SequenceNumber
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                2 => SendingApplication,
                3 => SendingFacility,
                4 => ReceivingApplication,
                5 => ReceivingFacility,
                6 => DateTimeOfMessage,
                7 => Security,
                8 => MessageType,
                9 => MessageControlID,
                10 => ProcessingID,
                11 => VersionID,
                12 => SequenceNumber,
                _ => null
            };
        }
    }
}
