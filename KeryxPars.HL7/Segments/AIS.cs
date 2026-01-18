using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Appointment Information - Service
    /// </summary>
    public class AIS : ISegment
    {
        public string SegmentId => nameof(AIS);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// AIS.1 - Set ID - AIS
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// AIS.2 - Segment Action Code
        /// </summary>
        public ID SegmentActionCode { get; set; }

        /// <summary>
        /// AIS.3 - Universal Service Identifier
        /// </summary>
        public CE UniversalServiceIdentifier { get; set; }

        /// <summary>
        /// AIS.4 - Start Date/Time
        /// </summary>
        public DTM StartDateTime { get; set; }

        /// <summary>
        /// AIS.5 - Start Date/Time Offset
        /// </summary>
        public NM StartDateTimeOffset { get; set; }

        /// <summary>
        /// AIS.6 - Start Date/Time Offset Units
        /// </summary>
        public CE StartDateTimeOffsetUnits { get; set; }

        /// <summary>
        /// AIS.7 - Duration
        /// </summary>
        public NM Duration { get; set; }

        /// <summary>
        /// AIS.8 - Duration Units
        /// </summary>
        public CE DurationUnits { get; set; }

        /// <summary>
        /// AIS.9 - Allow Substitution Code
        /// </summary>
        public IS AllowSubstitutionCode { get; set; }

        /// <summary>
        /// AIS.10 - Filler Status Code
        /// </summary>
        public CE FillerStatusCode { get; set; }

        /// <summary>
        /// AIS.11 - Placer Supplemental Service Information (repeating)
        /// </summary>
        public CE[] PlacerSupplementalServiceInformation { get; set; }

        /// <summary>
        /// AIS.12 - Filler Supplemental Service Information (repeating)
        /// </summary>
        public CE[] FillerSupplementalServiceInformation { get; set; }

        public AIS()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            SegmentActionCode = default;
            UniversalServiceIdentifier = default;
            StartDateTime = default;
            StartDateTimeOffset = default;
            StartDateTimeOffsetUnits = default;
            Duration = default;
            DurationUnits = default;
            AllowSubstitutionCode = default;
            FillerStatusCode = default;
            PlacerSupplementalServiceInformation = [];
            FillerSupplementalServiceInformation = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: SegmentActionCode = new ID(value); break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    UniversalServiceIdentifier = ce3;
                    break;
                case 4: StartDateTime = new DTM(value); break;
                case 5: StartDateTimeOffset = new NM(value); break;
                case 6:
                    var ce6 = new CE();
                    ce6.Parse(value.AsSpan(), delimiters);
                    StartDateTimeOffsetUnits = ce6;
                    break;
                case 7: Duration = new NM(value); break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    DurationUnits = ce8;
                    break;
                case 9: AllowSubstitutionCode = new IS(value); break;
                case 10:
                    var ce10 = new CE();
                    ce10.Parse(value.AsSpan(), delimiters);
                    FillerStatusCode = ce10;
                    break;
                case 11: PlacerSupplementalServiceInformation = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 12: FillerSupplementalServiceInformation = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
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
                UniversalServiceIdentifier.ToHL7String(delimiters),
                StartDateTime.ToHL7String(delimiters),
                StartDateTimeOffset.ToHL7String(delimiters),
                StartDateTimeOffsetUnits.ToHL7String(delimiters),
                Duration.ToHL7String(delimiters),
                DurationUnits.ToHL7String(delimiters),
                AllowSubstitutionCode.ToHL7String(delimiters),
                FillerStatusCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PlacerSupplementalServiceInformation, delimiters),
                SegmentFieldHelper.JoinRepeatingField(FillerSupplementalServiceInformation, delimiters)
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
                3 => UniversalServiceIdentifier.ToHL7String(delimiters),
                4 => StartDateTime.Value,
                5 => StartDateTimeOffset.Value,
                6 => StartDateTimeOffsetUnits.ToHL7String(delimiters),
                7 => Duration.Value,
                8 => DurationUnits.ToHL7String(delimiters),
                9 => AllowSubstitutionCode.Value,
                10 => FillerStatusCode.ToHL7String(delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(PlacerSupplementalServiceInformation, delimiters),
                12 => SegmentFieldHelper.JoinRepeatingField(FillerSupplementalServiceInformation, delimiters),
                _ => null
            };
        }
    }
}
