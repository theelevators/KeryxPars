using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Next of Kin / Associated Parties
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class NK1 : ISegment
    {
        public string SegmentId => nameof(NK1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// NK1.1 - Set ID - NK1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// NK1.2 - Name
        /// </summary>
        public XPN[] Name { get; set; }

        /// <summary>
        /// NK1.3 - Relationship
        /// </summary>
        public CE Relationship { get; set; }

        /// <summary>
        /// NK1.4 - Address
        /// </summary>
        public XAD[] Address { get; set; }

        /// <summary>
        /// NK1.5 - Phone Number
        /// </summary>
        public XTN[] PhoneNumber { get; set; }

        public NK1()
        {
            SegmentType = SegmentType.ADT;
            SetID = default;
            Name = [];
            Relationship = default;
            Address = [];
            PhoneNumber = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2:
                    Name = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters);
                    break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    Relationship = ce3;
                    break;
                case 4:
                    Address = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters);
                    break;
                case 5:
                    PhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters);
                    break;
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
                SegmentFieldHelper.JoinRepeatingField(Name, delimiters),
                Relationship.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Address, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PhoneNumber, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => SegmentFieldHelper.JoinRepeatingField(Name, delimiters),
                3 => Relationship.ToHL7String(delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(Address, delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(PhoneNumber, delimiters),
                _ => null
            };
        }
    }
}
