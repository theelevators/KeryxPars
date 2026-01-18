using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    public class AL1 : ISegment
    {
        public string SegmentId => nameof(AL1);

        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// AL1.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// AL1.2
        /// </summary>
        public string AllergenTypeCode { get; set; }

        /// <summary>
        /// AL1.3
        /// </summary>
        public string AllergenCodeMnemonicDescription { get; set; }

        /// <summary>
        /// AL1.4
        /// </summary>
        public string AllergySeverityCode { get; set; }

        /// <summary>
        /// AL1.5
        /// </summary>
        public string AllergyReactionCode { get; set; }

        /// <summary>
        /// AL1.6
        /// </summary>
        public string IdentificationDate { get; set; }


        // Constructors
        public AL1()
        {
            SegmentType = SegmentType.ADT;

            SetID = string.Empty;
            AllergenTypeCode = string.Empty;
            AllergenCodeMnemonicDescription = string.Empty;
            AllergySeverityCode = string.Empty;
            AllergyReactionCode = string.Empty;
            IdentificationDate = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: AllergenTypeCode = value; break;
                case 3: AllergenCodeMnemonicDescription = value; break;
                case 4: AllergySeverityCode = value; break;
                case 5: AllergyReactionCode = value; break;
                case 6: IdentificationDate = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                AllergenTypeCode,
                AllergenCodeMnemonicDescription,
                AllergySeverityCode,
                AllergyReactionCode,
                IdentificationDate
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => AllergenTypeCode,
                3 => AllergenCodeMnemonicDescription,
                4 => AllergySeverityCode,
                5 => AllergyReactionCode,
                6 => IdentificationDate,
                _ => null
            };
        }
    }
}
