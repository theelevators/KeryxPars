using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Timing Quantity
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class TQ1 : ISegment
    {
        public string SegmentId => nameof(TQ1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// TQ1.1 - Set ID - TQ1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// TQ1.2 - Quantity
        /// </summary>
        public CQ Quantity { get; set; }

        /// <summary>
        /// TQ1.3 - Repeat Pattern
        /// </summary>
        public ST[] RepeatPattern { get; set; }

        /// <summary>
        /// TQ1.4 - Explicit Time
        /// </summary>
        public TM[] ExplicitTime { get; set; }

        /// <summary>
        /// TQ1.5 - Relative Time and Units
        /// </summary>
        public CQ[] RelativeTimeAndUnits { get; set; }

        /// <summary>
        /// TQ1.6 - Service Duration
        /// </summary>
        public CQ ServiceDuration { get; set; }

        /// <summary>
        /// TQ1.7 - Start Date/Time
        /// </summary>
        public DTM StartDateTime { get; set; }

        /// <summary>
        /// TQ1.8 - End Date/Time
        /// </summary>
        public DTM EndDateTime { get; set; }

        /// <summary>
        /// TQ1.9 - Priority
        /// </summary>
        public CWE[] Priority { get; set; }

        /// <summary>
        /// TQ1.10 - Condition Text
        /// </summary>
        public TX ConditionText { get; set; }

        /// <summary>
        /// TQ1.11 - Text Instruction
        /// </summary>
        public TX TextInstruction { get; set; }

        /// <summary>
        /// TQ1.12 - Conjunction
        /// </summary>
        public ID Conjunction { get; set; }

        /// <summary>
        /// TQ1.13 - Occurrence Duration
        /// </summary>
        public CQ OccurrenceDuration { get; set; }

        /// <summary>
        /// TQ1.14 - Total Occurrences
        /// </summary>
        public NM TotalOccurrences { get; set; }

        public TQ1()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = default;
            Quantity = default;
            RepeatPattern = [];
            ExplicitTime = [];
            RelativeTimeAndUnits = [];
            ServiceDuration = default;
            StartDateTime = default;
            EndDateTime = default;
            Priority = [];
            ConditionText = default;
            TextInstruction = default;
            Conjunction = default;
            OccurrenceDuration = default;
            TotalOccurrences = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2:
                    var cq2 = new CQ();
                    cq2.Parse(value.AsSpan(), delimiters);
                    Quantity = cq2;
                    break;
                case 3:
                    RepeatPattern = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters);
                    break;
                case 4:
                    ExplicitTime = SegmentFieldHelper.ParseRepeatingField<TM>(value, delimiters);
                    break;
                case 5:
                    RelativeTimeAndUnits = SegmentFieldHelper.ParseRepeatingField<CQ>(value, delimiters);
                    break;
                case 6:
                    var cq6 = new CQ();
                    cq6.Parse(value.AsSpan(), delimiters);
                    ServiceDuration = cq6;
                    break;
                case 7: StartDateTime = new DTM(value); break;
                case 8: EndDateTime = new DTM(value); break;
                case 9:
                    Priority = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters);
                    break;
                case 10: ConditionText = new TX(value); break;
                case 11: TextInstruction = new TX(value); break;
                case 12: Conjunction = new ID(value); break;
                case 13:
                    var cq13 = new CQ();
                    cq13.Parse(value.AsSpan(), delimiters);
                    OccurrenceDuration = cq13;
                    break;
                case 14: TotalOccurrences = new NM(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                Quantity.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(RepeatPattern, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ExplicitTime, delimiters),
                SegmentFieldHelper.JoinRepeatingField(RelativeTimeAndUnits, delimiters),
                ServiceDuration.ToHL7String(delimiters),
                StartDateTime.ToHL7String(delimiters),
                EndDateTime.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Priority, delimiters),
                ConditionText.ToHL7String(delimiters),
                TextInstruction.ToHL7String(delimiters),
                Conjunction.ToHL7String(delimiters),
                OccurrenceDuration.ToHL7String(delimiters),
                TotalOccurrences.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => Quantity.ToHL7String(delimiters),
                3 => SegmentFieldHelper.JoinRepeatingField(RepeatPattern, delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(ExplicitTime, delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(RelativeTimeAndUnits, delimiters),
                6 => ServiceDuration.ToHL7String(delimiters),
                7 => StartDateTime.Value,
                8 => EndDateTime.Value,
                9 => SegmentFieldHelper.JoinRepeatingField(Priority, delimiters),
                10 => ConditionText.Value,
                11 => TextInstruction.Value,
                12 => Conjunction.Value,
                13 => OccurrenceDuration.ToHL7String(delimiters),
                14 => TotalOccurrences.Value,
                _ => null
            };
        }
    }
}
