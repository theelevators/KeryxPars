using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Timing Quantity
    /// </summary>
    public class TQ1 : ISegment
    {
        public string SegmentId => nameof(TQ1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// TQ1.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// TQ1.2
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// TQ1.3
        /// </summary>
        public string RepeatPattern { get; set; }

        /// <summary>
        /// TQ1.4
        /// </summary>
        public string ExplicitTime { get; set; }

        /// <summary>
        /// TQ1.5
        /// </summary>
        public string RelativeTimeAndUnits { get; set; }

        /// <summary>
        /// TQ1.6
        /// </summary>
        public string ServiceDuration { get; set; }

        /// <summary>
        /// TQ1.7
        /// </summary>
        public string StartDateTime { get; set; }

        /// <summary>
        /// TQ1.8
        /// </summary>
        public string EndDateTime { get; set; }

        /// <summary>
        /// TQ1.9
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// TQ1.10
        /// </summary>
        public string ConditionText { get; set; }

        /// <summary>
        /// TQ1.11
        /// </summary>
        public string TextInstruction { get; set; }

        /// <summary>
        /// TQ1.12
        /// </summary>
        public string Conjunction { get; set; }

        /// <summary>
        /// TQ1.13
        /// </summary>
        public string OccurrenceDuration { get; set; }

        /// <summary>
        /// TQ1.14
        /// </summary>
        public string TotalOccurrences { get; set; }

        // Constructors
        public TQ1()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = string.Empty;
            Quantity = string.Empty;
            RepeatPattern = string.Empty;
            ExplicitTime = string.Empty;
            RelativeTimeAndUnits = string.Empty;
            ServiceDuration = string.Empty;
            StartDateTime = string.Empty;
            EndDateTime = string.Empty;
            Priority = string.Empty;
            ConditionText = string.Empty;
            TextInstruction = string.Empty;
            Conjunction = string.Empty;
            OccurrenceDuration = string.Empty;
            TotalOccurrences = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: Quantity = value; break;
                case 3: RepeatPattern = value; break;
                case 4: ExplicitTime = value; break;
                case 5: RelativeTimeAndUnits = value; break;
                case 6: ServiceDuration = value; break;
                case 7: StartDateTime = value; break;
                case 8: EndDateTime = value; break;
                case 9: Priority = value; break;
                case 10: ConditionText = value; break;
                case 11: TextInstruction = value; break;
                case 12: Conjunction = value; break;
                case 13: OccurrenceDuration = value; break;
                case 14: TotalOccurrences = value; break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                Quantity,
                RepeatPattern,
                ExplicitTime,
                RelativeTimeAndUnits,
                ServiceDuration,
                StartDateTime,
                EndDateTime,
                Priority,
                ConditionText,
                TextInstruction,
                Conjunction,
                OccurrenceDuration,
                TotalOccurrences
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => Quantity,
                3 => RepeatPattern,
                4 => ExplicitTime,
                5 => RelativeTimeAndUnits,
                6 => ServiceDuration,
                7 => StartDateTime,
                8 => EndDateTime,
                9 => Priority,
                10 => ConditionText,
                11 => TextInstruction,
                12 => Conjunction,
                13 => OccurrenceDuration,
                14 => TotalOccurrences,
                _ => null
            };
        }
    }
}
