using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Timing/Quantity Relationship
    /// </summary>
    public class TQ2 : ISegment
    {
        public string SegmentId => nameof(TQ2);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// TQ2.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// TQ2.2
        /// </summary>
        public string SequenceResultsFlag { get; set; }

        /// <summary>
        /// TQ2.3
        /// </summary>
        public string RelatedPlacerNumber { get; set; }

        /// <summary>
        /// TQ2.4
        /// </summary>
        public string RelatedFillerNumber { get; set; }

        /// <summary>
        /// TQ2.5
        /// </summary>
        public string RelatedPlacerGroupNumber { get; set; }

        /// <summary>
        /// TQ2.6
        /// </summary>
        public string SequenceConditionCode { get; set; }

        /// <summary>
        /// TQ2.7
        /// </summary>
        public string CyclicEntryExitIndicator { get; set; }

        /// <summary>
        /// TQ2.8
        /// </summary>
        public string SequenceConditionTimeInterval { get; set; }

        /// <summary>
        /// TQ2.9
        /// </summary>
        public string CyclicGroupMaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// TQ2.10
        /// </summary>
        public string SpecialServiceRequestRelationship { get; set; }

        // Constructors
        public TQ2()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = string.Empty;
            SequenceResultsFlag = string.Empty;
            RelatedPlacerNumber = string.Empty;
            RelatedFillerNumber = string.Empty;
            RelatedPlacerGroupNumber = string.Empty;
            SequenceConditionCode = string.Empty;
            CyclicEntryExitIndicator = string.Empty;
            SequenceConditionTimeInterval = string.Empty;
            CyclicGroupMaximumNumberOfRepeats = string.Empty;
            SpecialServiceRequestRelationship = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: SequenceResultsFlag = value; break;
                case 3: RelatedPlacerNumber = value; break;
                case 4: RelatedFillerNumber = value; break;
                case 5: RelatedPlacerGroupNumber = value; break;
                case 6: SequenceConditionCode = value; break;
                case 7: CyclicEntryExitIndicator = value; break;
                case 8: SequenceConditionTimeInterval = value; break;
                case 9: CyclicGroupMaximumNumberOfRepeats = value; break;
                case 10: SpecialServiceRequestRelationship = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                SequenceResultsFlag,
                RelatedPlacerNumber,
                RelatedFillerNumber,
                RelatedPlacerGroupNumber,
                SequenceConditionCode,
                CyclicEntryExitIndicator,
                SequenceConditionTimeInterval,
                CyclicGroupMaximumNumberOfRepeats,
                SpecialServiceRequestRelationship
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => SequenceResultsFlag,
                3 => RelatedPlacerNumber,
                4 => RelatedFillerNumber,
                5 => RelatedPlacerGroupNumber,
                6 => SequenceConditionCode,
                7 => CyclicEntryExitIndicator,
                8 => SequenceConditionTimeInterval,
                9 => CyclicGroupMaximumNumberOfRepeats,
                10 => SpecialServiceRequestRelationship,
                _ => null
            };
        }
    }
}
