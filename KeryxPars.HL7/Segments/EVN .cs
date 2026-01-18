using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Event Type
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class EVN : ISegment
    {
        public string SegmentId => nameof(EVN);

        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// EVN.1 - Event Type Code
        /// </summary>
        public ID EventType { get; set; }

        /// <summary>
        /// EVN.2 - Recorded Date/Time
        /// </summary>
        public DTM RecordedDateTime { get; set; }

        /// <summary>
        /// EVN.3 - Date/Time Planned Event
        /// </summary>
        public DTM DateTimePlannedEvent { get; set; }

        /// <summary>
        /// EVN.4 - Event Reason Code
        /// </summary>
        public IS EventReasonCode { get; set; }

        /// <summary>
        /// EVN.5 - Operator ID (repeating)
        /// </summary>
        public XCN[] OperatorID { get; set; }

        /// <summary>
        /// EVN.6 - Event Occurred
        /// </summary>
        public DTM EventOccurred { get; set; }

        /// <summary>
        /// EVN.7 - Event Facility
        /// </summary>
        public HD EventFacility { get; set; }

        // Constructors
        public EVN()
        {
            SegmentType = SegmentType.Universal;
            EventType = default;
            RecordedDateTime = default;
            DateTimePlannedEvent = default;
            EventReasonCode = default;
            EventOccurred = default;
            EventFacility = default;
            OperatorID = Array.Empty<XCN>();
        }
        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: EventType = new ID(value); break;
                case 2: RecordedDateTime = new DTM(value); break;
                case 3: DateTimePlannedEvent = new DTM(value); break;
                case 4: EventReasonCode = new IS(value); break;
                case 5: OperatorID = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 6: EventOccurred = new DTM(value); break;
                case 7:
                    var hd = new HD();
                    hd.Parse(value.AsSpan(), delimiters);
                    EventFacility = hd;
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
                EventType.ToHL7String(delimiters),
                RecordedDateTime.ToHL7String(delimiters),
                DateTimePlannedEvent.ToHL7String(delimiters),
                EventReasonCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OperatorID, delimiters),
                EventOccurred.ToHL7String(delimiters),
                EventFacility.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
