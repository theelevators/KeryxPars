using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Dietary Orders, Supplements, and Preferences
    /// </summary>
    public class ODS : ISegment
    {
        public string SegmentId => nameof(ODS);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// ODS.1 - Type
        /// </summary>
        public ID Type { get; set; }

        /// <summary>
        /// ODS.2 - Service Period (repeating)
        /// </summary>
        public CE[] ServicePeriod { get; set; }

        /// <summary>
        /// ODS.3 - Diet, Supplement, or Preference Code (repeating)
        /// </summary>
        public CE[] DietSupplementOrPreferenceCode { get; set; }

        /// <summary>
        /// ODS.4 - Text Instruction (repeating)
        /// </summary>
        public ST[] TextInstruction { get; set; }

        public ODS()
        {
            SegmentType = SegmentType.Universal;
            Type = default;
            ServicePeriod = [];
            DietSupplementOrPreferenceCode = [];
            TextInstruction = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: Type = new ID(value); break;
                case 2: ServicePeriod = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 3: DietSupplementOrPreferenceCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 4: TextInstruction = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                Type.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ServicePeriod, delimiters),
                SegmentFieldHelper.JoinRepeatingField(DietSupplementOrPreferenceCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(TextInstruction, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => Type.Value,
                2 => SegmentFieldHelper.JoinRepeatingField(ServicePeriod, delimiters),
                3 => SegmentFieldHelper.JoinRepeatingField(DietSupplementOrPreferenceCode, delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(TextInstruction, delimiters),
                _ => null
            };
        }
    }
}
