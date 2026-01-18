using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Message Acknowledgement
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class MSA : ISegment
    {
        public string SegmentId => nameof(MSA);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// MSA.1 - Acknowledgement Code
        /// </summary>
        public ID AcknowledgementCode { get; set; }

        /// <summary>
        /// MSA.2 - Message Control ID
        /// </summary>
        public ST MessageControlID { get; set; }

        /// <summary>
        /// MSA.3 - Text Message
        /// </summary>
        public ST TextMessage { get; set; }

        /// <summary>
        /// MSA.4 - Expected Sequence Number
        /// </summary>
        public NM ExpectedSequenceNumber { get; set; }

        /// <summary>
        /// MSA.5 - Delayed Acknowledgment Type
        /// </summary>
        public ID DelayedAcknowledgementType { get; set; }

        /// <summary>
        /// MSA.6 - Error Condition
        /// </summary>
        public CE ErrorCondition { get; set; }

        /// <summary>
        /// MSA.7 - Message Waiting Number
        /// </summary>
        public NM MessageWaitingNumber { get; set; }

        /// <summary>
        /// MSA.8 - Message Waiting Priority
        /// </summary>
        public ID MessageWaitingPriority { get; set; }

        public MSA()
        {
            SegmentType = SegmentType.Universal;
            AcknowledgementCode = default;
            MessageControlID = default;
            TextMessage = default;
            ExpectedSequenceNumber = default;
            DelayedAcknowledgementType = default;
            ErrorCondition = default;
            MessageWaitingNumber = default;
            MessageWaitingPriority = default;
        }

        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: AcknowledgementCode = new ID(value); break;
                case 2: MessageControlID = new ST(value); break;
                case 3: TextMessage = new ST(value); break;
                case 4: ExpectedSequenceNumber = new NM(value); break;
                case 5: DelayedAcknowledgementType = new ID(value); break;
                case 6: ErrorCondition = value; break;
                case 7: MessageWaitingNumber = new NM(value); break;
                case 8: MessageWaitingPriority = new ID(value); break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                AcknowledgementCode.ToHL7String(delimiters),
                MessageControlID.ToHL7String(delimiters),
                TextMessage.ToHL7String(delimiters),
                ExpectedSequenceNumber.ToHL7String(delimiters),
                DelayedAcknowledgementType.ToHL7String(delimiters),
                ErrorCondition.ToHL7String(delimiters),
                MessageWaitingNumber.ToHL7String(delimiters),
                MessageWaitingPriority.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
