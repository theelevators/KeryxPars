using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Next of Kin / Associated Parties
    /// </summary>
    public class NK1 : ISegment
    {
        public string SegmentId => nameof(NK1);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// NK1.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// NK1.2
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// NK1.3
        /// </summary>
        public string Relationship { get; set; }

        /// <summary>
        /// NK1.4
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// NK1.5
        /// </summary>
        public string PhoneNumber { get; set; }


        public NK1()
        {
            SegmentType = SegmentType.ADT;
            ClearValues();
        }

        // Methods
        public void ClearValues()
        {
            SetID = string.Empty;
            Name = string.Empty;
            Relationship = string.Empty;
            Address = string.Empty;
            PhoneNumber = string.Empty;
        }
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: Name = value; break;
                case 3: Relationship = value; break;
                case 4: Address = value; break;
                case 5: PhoneNumber = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                Name,
                Relationship,
                Address,
                PhoneNumber
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => Name,
                3 => Relationship,
                4 => Address,
                5 => PhoneNumber,
                _ => null
            };
        }
    }
}
