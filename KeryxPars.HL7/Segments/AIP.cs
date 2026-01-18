using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Appointment Information - Personnel Resource
    /// </summary>
    public class AIP : ISegment
    {
        public string SegmentId => nameof(AIP);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// AIP.1 - Set ID - AIP
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// AIP.2 - Segment Action Code
        /// </summary>
        public ID SegmentActionCode { get; set; }

        /// <summary>
        /// AIP.3 - Personnel Resource ID (repeating)
        /// </summary>
        public XCN[] PersonnelResourceID { get; set; }

        /// <summary>
        /// AIP.4 - Resource Type
        /// </summary>
        public CE ResourceType { get; set; }

        /// <summary>
        /// AIP.5 - Resource Group
        /// </summary>
        public CE ResourceGroup { get; set; }

        /// <summary>
        /// AIP.6 - Start Date/Time
        /// </summary>
        public DTM StartDateTime { get; set; }

        /// <summary>
        /// AIP.7 - Start Date/Time Offset
        /// </summary>
        public NM StartDateTimeOffset { get; set; }

        /// <summary>
        /// AIP.8 - Start Date/Time Offset Units
        /// </summary>
        public CE StartDateTimeOffsetUnits { get; set; }

        /// <summary>
        /// AIP.9 - Duration
        /// </summary>
        public NM Duration { get; set; }

        /// <summary>
        /// AIP.10 - Duration Units
        /// </summary>
        public CE DurationUnits { get; set; }

        /// <summary>
        /// AIP.11 - Allow Substitution Code
        /// </summary>
        public IS AllowSubstitutionCode { get; set; }

        /// <summary>
        /// AIP.12 - Filler Status Code
        /// </summary>
        public CE FillerStatusCode { get; set; }

        public AIP()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            SegmentActionCode = default;
            PersonnelResourceID = [];
            ResourceType = default;
            ResourceGroup = default;
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
                case 3: PersonnelResourceID = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    ResourceType = ce4;
                    break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    ResourceGroup = ce5;
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
                SegmentFieldHelper.JoinRepeatingField(PersonnelResourceID, delimiters),
                ResourceType.ToHL7String(delimiters),
                ResourceGroup.ToHL7String(delimiters),
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
                3 => SegmentFieldHelper.JoinRepeatingField(PersonnelResourceID, delimiters),
                4 => ResourceType.ToHL7String(delimiters),
                5 => ResourceGroup.ToHL7String(delimiters),
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
