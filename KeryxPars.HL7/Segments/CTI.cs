using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Clinical Trial Identification
    /// </summary>
    public class CTI : ISegment
    {
        public string SegmentId => nameof(CTI);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// CTI.1 - Sponsor Study ID
        /// </summary>
        public EI SponsorStudyID { get; set; }

        /// <summary>
        /// CTI.2 - Study Phase Identifier
        /// </summary>
        public CE StudyPhaseIdentifier { get; set; }

        /// <summary>
        /// CTI.3 - Study Scheduled Time Point
        /// </summary>
        public CE StudyScheduledTimePoint { get; set; }

        public CTI()
        {
            SegmentType = SegmentType.Universal;
            SponsorStudyID = default;
            StudyPhaseIdentifier = default;
            StudyScheduledTimePoint = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ei1 = new EI();
                    ei1.Parse(value.AsSpan(), delimiters);
                    SponsorStudyID = ei1;
                    break;
                case 2:
                    var ce2 = new CE();
                    ce2.Parse(value.AsSpan(), delimiters);
                    StudyPhaseIdentifier = ce2;
                    break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    StudyScheduledTimePoint = ce3;
                    break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SponsorStudyID.ToHL7String(delimiters),
                StudyPhaseIdentifier.ToHL7String(delimiters),
                StudyScheduledTimePoint.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SponsorStudyID.ToHL7String(delimiters),
                2 => StudyPhaseIdentifier.ToHL7String(delimiters),
                3 => StudyScheduledTimePoint.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
