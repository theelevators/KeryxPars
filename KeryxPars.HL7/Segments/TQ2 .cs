using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Timing/Quantity Relationship
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class TQ2 : ISegment
    {
        public string SegmentId => nameof(TQ2);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// TQ2.1 - Set ID - TQ2
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// TQ2.2 - Sequence/Results Flag
        /// </summary>
        public ID SequenceResultsFlag { get; set; }

        /// <summary>
        /// TQ2.3 - Related Placer Number
        /// </summary>
        public EI[] RelatedPlacerNumber { get; set; }

        /// <summary>
        /// TQ2.4 - Related Filler Number
        /// </summary>
        public EI[] RelatedFillerNumber { get; set; }

        /// <summary>
        /// TQ2.5 - Related Placer Group Number
        /// </summary>
        public EI[] RelatedPlacerGroupNumber { get; set; }

        /// <summary>
        /// TQ2.6 - Sequence Condition Code
        /// </summary>
        public ID SequenceConditionCode { get; set; }

        /// <summary>
        /// TQ2.7 - Cyclic Entry/Exit Indicator
        /// </summary>
        public ID CyclicEntryExitIndicator { get; set; }

        /// <summary>
        /// TQ2.8 - Sequence Condition Time Interval
        /// </summary>
        public CQ SequenceConditionTimeInterval { get; set; }

        /// <summary>
        /// TQ2.9 - Cyclic Group Maximum Number of Repeats
        /// </summary>
        public NM CyclicGroupMaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// TQ2.10 - Special Service Request Relationship
        /// </summary>
        public ID SpecialServiceRequestRelationship { get; set; }

        public TQ2()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = default;
            SequenceResultsFlag = default;
            RelatedPlacerNumber = [];
            RelatedFillerNumber = [];
            RelatedPlacerGroupNumber = [];
            SequenceConditionCode = default;
            CyclicEntryExitIndicator = default;
            SequenceConditionTimeInterval = default;
            CyclicGroupMaximumNumberOfRepeats = default;
            SpecialServiceRequestRelationship = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: SequenceResultsFlag = new ID(value); break;
                case 3:
                    RelatedPlacerNumber = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters);
                    break;
                case 4:
                    RelatedFillerNumber = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters);
                    break;
                case 5:
                    RelatedPlacerGroupNumber = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters);
                    break;
                case 6: SequenceConditionCode = new ID(value); break;
                case 7: CyclicEntryExitIndicator = new ID(value); break;
                case 8:
                    var cq8 = new CQ();
                    cq8.Parse(value.AsSpan(), delimiters);
                    SequenceConditionTimeInterval = cq8;
                    break;
                case 9: CyclicGroupMaximumNumberOfRepeats = new NM(value); break;
                case 10: SpecialServiceRequestRelationship = new ID(value); break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                SequenceResultsFlag.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(RelatedPlacerNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(RelatedFillerNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(RelatedPlacerGroupNumber, delimiters),
                SequenceConditionCode.ToHL7String(delimiters),
                CyclicEntryExitIndicator.ToHL7String(delimiters),
                SequenceConditionTimeInterval.ToHL7String(delimiters),
                CyclicGroupMaximumNumberOfRepeats.ToHL7String(delimiters),
                SpecialServiceRequestRelationship.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => SequenceResultsFlag.Value,
                3 => SegmentFieldHelper.JoinRepeatingField(RelatedPlacerNumber, delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(RelatedFillerNumber, delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(RelatedPlacerGroupNumber, delimiters),
                6 => SequenceConditionCode.Value,
                7 => CyclicEntryExitIndicator.Value,
                8 => SequenceConditionTimeInterval.ToHL7String(delimiters),
                9 => CyclicGroupMaximumNumberOfRepeats.Value,
                10 => SpecialServiceRequestRelationship.Value,
                _ => null
            };
        }
    }
}
