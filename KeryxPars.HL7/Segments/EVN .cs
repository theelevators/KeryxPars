using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Event Type
    /// </summary>
    public class EVN : ISegment
    {
        public string SegmentId => nameof(EVN);

        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// EVN.1
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// EVN.2
        /// </summary>
        public string RecordedDateTime { get; set; }

        /// <summary>
        /// EVN.3
        /// </summary>
        public string DateTimePlannedEvent { get; set; }

        /// <summary>
        /// EVN.4
        /// </summary>
        public string EventReasonCode { get; set; }

        /// <summary>
        /// EVN.5
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// EVN.6
        /// </summary>
        public string EventOccurred { get; set; }

        /// <summary>
        /// EVN.7
        /// </summary>
        public string EventFacility { get; set; }

        // Constructors
        public EVN()
        {
            EventType = string.Empty;
            RecordedDateTime = string.Empty;
            DateTimePlannedEvent = string.Empty;
            EventReasonCode = string.Empty;
            OperatorID = string.Empty;
            EventOccurred = string.Empty;
            EventFacility = string.Empty;
        }
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: EventType = value; break;
                case 2: RecordedDateTime = value; break;
                case 3: DateTimePlannedEvent = value; break;
                case 4: EventReasonCode = value; break;
                case 5: OperatorID = value; break;
                case 6: EventOccurred = value; break;
                case 7: EventFacility = value; break;
                default: break;
            }
        }
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                EventType,
                RecordedDateTime,
                DateTimePlannedEvent,
                EventReasonCode,
                OperatorID,
                EventOccurred,
                EventFacility
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => EventType,
                2 => RecordedDateTime,
                3 => DateTimePlannedEvent,
                4 => EventReasonCode,
                5 => OperatorID,
                6 => EventOccurred,
                7 => EventFacility,
                _ => null
            };
        }
    }
}
