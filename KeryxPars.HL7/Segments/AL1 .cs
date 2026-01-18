using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Allergy Information
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class AL1 : ISegment
    {
        public string SegmentId => nameof(AL1);

        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// AL1.1 - Set ID - AL1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// AL1.2 - Allergen Type Code
        /// </summary>
        public CE AllergenTypeCode { get; set; }

        /// <summary>
        /// AL1.3 - Allergen Code/Mnemonic/Description
        /// </summary>
        public CE AllergenCodeMnemonicDescription { get; set; }

        /// <summary>
        /// AL1.4 - Allergy Severity Code
        /// </summary>
        public CE AllergySeverityCode { get; set; }

        /// <summary>
        /// AL1.5 - Allergy Reaction Code
        /// </summary>
        public ST[] AllergyReactionCode { get; set; }

        /// <summary>
        /// AL1.6 - Identification Date
        /// </summary>
        public DT IdentificationDate { get; set; }

        public AL1()
        {
            SegmentType = SegmentType.ADT;
            SetID = default;
            AllergenTypeCode = default;
            AllergenCodeMnemonicDescription = default;
            AllergySeverityCode = default;
            AllergyReactionCode = [];
            IdentificationDate = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2:
                    var ce2 = new CE();
                    ce2.Parse(value.AsSpan(), delimiters);
                    AllergenTypeCode = ce2;
                    break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    AllergenCodeMnemonicDescription = ce3;
                    break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    AllergySeverityCode = ce4;
                    break;
                case 5:
                    AllergyReactionCode = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters);
                    break;
                case 6: IdentificationDate = new DT(value); break;
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
                AllergenTypeCode.ToHL7String(delimiters),
                AllergenCodeMnemonicDescription.ToHL7String(delimiters),
                AllergySeverityCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AllergyReactionCode, delimiters),
                IdentificationDate.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => AllergenTypeCode.ToHL7String(delimiters),
                3 => AllergenCodeMnemonicDescription.ToHL7String(delimiters),
                4 => AllergySeverityCode.ToHL7String(delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(AllergyReactionCode, delimiters),
                6 => IdentificationDate.Value,
                _ => null
            };
        }
    }
}
