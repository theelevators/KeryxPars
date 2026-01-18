using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Accident Information
    /// </summary>
    public class ACC : ISegment
    {
        public string SegmentId => nameof(ACC);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// ACC.1 - Accident Date/Time
        /// </summary>
        public DTM AccidentDateTime { get; set; }

        /// <summary>
        /// ACC.2 - Accident Code
        /// </summary>
        public CE AccidentCode { get; set; }

        /// <summary>
        /// ACC.3 - Accident Location
        /// </summary>
        public ST AccidentLocation { get; set; }

        /// <summary>
        /// ACC.4 - Auto Accident State
        /// </summary>
        public CE AutoAccidentState { get; set; }

        /// <summary>
        /// ACC.5 - Accident Job Related Indicator
        /// </summary>
        public ID AccidentJobRelatedIndicator { get; set; }

        /// <summary>
        /// ACC.6 - Accident Death Indicator
        /// </summary>
        public ID AccidentDeathIndicator { get; set; }

        public ACC()
        {
            SegmentType = SegmentType.Universal;
            AccidentDateTime = default;
            AccidentCode = default;
            AccidentLocation = default;
            AutoAccidentState = default;
            AccidentJobRelatedIndicator = default;
            AccidentDeathIndicator = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: AccidentDateTime = new DTM(value); break;
                case 2:
                    var ce2 = new CE();
                    ce2.Parse(value.AsSpan(), delimiters);
                    AccidentCode = ce2;
                    break;
                case 3: AccidentLocation = new ST(value); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    AutoAccidentState = ce4;
                    break;
                case 5: AccidentJobRelatedIndicator = new ID(value); break;
                case 6: AccidentDeathIndicator = new ID(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                AccidentDateTime.ToHL7String(delimiters),
                AccidentCode.ToHL7String(delimiters),
                AccidentLocation.ToHL7String(delimiters),
                AutoAccidentState.ToHL7String(delimiters),
                AccidentJobRelatedIndicator.ToHL7String(delimiters),
                AccidentDeathIndicator.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => AccidentDateTime.Value,
                2 => AccidentCode.ToHL7String(delimiters),
                3 => AccidentLocation.Value,
                4 => AutoAccidentState.ToHL7String(delimiters),
                5 => AccidentJobRelatedIndicator.Value,
                6 => AccidentDeathIndicator.Value,
                _ => null
            };
        }
    }
}
