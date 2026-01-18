using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Appointment Information - Location Resource
    /// </summary>
    public class AIL : ISegment
    {
        public string SegmentId => nameof(AIL);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// AIL.1 - Set ID - AIL
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// AIL.2 - Segment Action Code
        /// </summary>
        public ID SegmentActionCode { get; set; }

        /// <summary>
        /// AIL.3 - Location Resource ID (repeating)
        /// </summary>
        public PL[] LocationResourceID { get; set; }

        /// <summary>
        /// AIL.4 - Location Type - AIL
        /// </summary>
        public CE LocationTypeAIL { get; set; }

        /// <summary>
        /// AIL.5 - Location Group
        /// </summary>
        public CE LocationGroup { get; set; }

        /// <summary>
        /// AIL.6 - Start Date/Time
        /// </summary>
        public DTM StartDateTime { get; set; }

        /// <summary>
        /// AIL.7 - Start Date/Time Offset
        /// </summary>
        public NM StartDateTimeOffset { get; set; }

        /// <summary>
        /// AIL.8 - Start Date/Time Offset Units
        /// </summary>
        public CE StartDateTimeOffsetUnits { get; set; }

        /// <summary>
        /// AIL.9 - Duration
        /// </summary>
        public NM Duration { get; set; }

        /// <summary>
        /// AIL.10 - Duration Units
        /// </summary>
        public CE DurationUnits { get; set; }

        /// <summary>
        /// AIL.11 - Allow Substitution Code
        /// </summary>
        public IS AllowSubstitutionCode { get; set; }

        /// <summary>
        /// AIL.12 - Filler Status Code
        /// </summary>
        public CE FillerStatusCode { get; set; }

        public AIL()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            SegmentActionCode = default;
            LocationResourceID = [];
            LocationTypeAIL = default;
            LocationGroup = default;
            StartDateTime = default;
            StartDateTimeOffset = default;
            StartDateTimeOffsetUnits = default;
            Duration = default;
            DurationUnits = default;
            AllowSubstitutionCode = default;
            FillerStatusCode = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: SegmentActionCode = new ID(value); break;
                case 3: LocationResourceID = SegmentFieldHelper.ParseRepeatingField<PL>(value, delimiters); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    LocationTypeAIL = ce4;
                    break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    LocationGroup = ce5;
                    break;
                case 6: StartDateTime = new DTM(value); break;
                case 7: StartDateTimeOffset = new NM(value); break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    StartDateTimeOffsetUnits = ce8;
                    break;
                case 9: Duration = new NM(value); break;
                case 10:
                    var ce10 = new CE();
                    ce10.Parse(value.AsSpan(), delimiters);
                    DurationUnits = ce10;
                    break;
                case 11: AllowSubstitutionCode = new IS(value); break;
                case 12:
                    var ce12 = new CE();
                    ce12.Parse(value.AsSpan(), delimiters);
                    FillerStatusCode = ce12;
                    break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                SegmentActionCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(LocationResourceID, delimiters),
                LocationTypeAIL.ToHL7String(delimiters),
                LocationGroup.ToHL7String(delimiters),
                StartDateTime.ToHL7String(delimiters),
                StartDateTimeOffset.ToHL7String(delimiters),
                StartDateTimeOffsetUnits.ToHL7String(delimiters),
                Duration.ToHL7String(delimiters),
                DurationUnits.ToHL7String(delimiters),
                AllowSubstitutionCode.ToHL7String(delimiters),
                FillerStatusCode.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => SegmentActionCode.Value,
                3 => SegmentFieldHelper.JoinRepeatingField(LocationResourceID, delimiters),
                4 => LocationTypeAIL.ToHL7String(delimiters),
                5 => LocationGroup.ToHL7String(delimiters),
                6 => StartDateTime.Value,
                7 => StartDateTimeOffset.Value,
                8 => StartDateTimeOffsetUnits.ToHL7String(delimiters),
                9 => Duration.Value,
                10 => DurationUnits.ToHL7String(delimiters),
                11 => AllowSubstitutionCode.Value,
                12 => FillerStatusCode.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
