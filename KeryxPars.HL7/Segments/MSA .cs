using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Message Acknowledgement
    /// </summary>
    public class MSA : ISegment
    {
        public string SegmentId => nameof(MSA);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// MSA.1
        /// </summary>
        public string AcknowledgementCode { get; set; }

        /// <summary>
        /// MSA.2
        /// </summary>
        public string MessageControlID { get; set; }

        /// <summary>
        /// MSA.3
        /// </summary>
        public string TextMessage { get; set; }

        /// <summary>
        /// MSA.4
        /// </summary>
        public string ExpectedSequenceNumber { get; set; }

        /// <summary>
        /// MSA.5
        /// </summary>
        public string DelayedAcknowledgementType { get; set; }

        /// <summary>
        /// MSA.6
        /// </summary>
        public string ErrorCondition { get; set; }

        /// <summary>
        /// MSA.7
        /// </summary>
        public string MessageWaitingNumber { get; set; }

        /// <summary>
        /// MSA.8
        /// </summary>
        public string MessageWaitingPriority { get; set; }

        // Constructors
        public MSA()
        {
            SegmentType = SegmentType.Universal;

            AcknowledgementCode = string.Empty;
            MessageControlID = string.Empty;
            TextMessage = string.Empty;
            ExpectedSequenceNumber = string.Empty;
            DelayedAcknowledgementType = string.Empty;
            ErrorCondition = string.Empty;
            MessageWaitingNumber = string.Empty;
            MessageWaitingPriority = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: AcknowledgementCode = value; break;
                case 2: MessageControlID = value; break;
                case 3: TextMessage = value; break;
                case 4: ExpectedSequenceNumber = value; break;
                case 5: DelayedAcknowledgementType = value; break;
                case 6: ErrorCondition = value; break;
                case 7: MessageWaitingNumber = value; break;
                case 8: MessageWaitingPriority = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                AcknowledgementCode,
                MessageControlID,
                TextMessage,
                ExpectedSequenceNumber,
                DelayedAcknowledgementType,
                ErrorCondition,
                MessageWaitingNumber,
                MessageWaitingPriority
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => AcknowledgementCode,
                2 => MessageControlID,
                3 => TextMessage,
                4 => ExpectedSequenceNumber,
                5 => DelayedAcknowledgementType,
                6 => ErrorCondition,
                7 => MessageWaitingNumber,
                8 => MessageWaitingPriority,
                _ => null
            };
        }
    }
}
