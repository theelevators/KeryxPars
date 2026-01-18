using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Contact Data
    /// </summary>
    public class CTD : ISegment
    {
        public string SegmentId => nameof(CTD);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// CTD.1 - Contact Role (repeating)
        /// </summary>
        public CE[] ContactRole { get; set; }

        /// <summary>
        /// CTD.2 - Contact Name (repeating)
        /// </summary>
        public XPN[] ContactName { get; set; }

        /// <summary>
        /// CTD.3 - Contact Address (repeating)
        /// </summary>
        public XAD[] ContactAddress { get; set; }

        /// <summary>
        /// CTD.4 - Contact Location
        /// </summary>
        public PL ContactLocation { get; set; }

        /// <summary>
        /// CTD.5 - Contact Communication Information (repeating)
        /// </summary>
        public XTN[] ContactCommunicationInformation { get; set; }

        /// <summary>
        /// CTD.6 - Preferred Method of Contact
        /// </summary>
        public CE PreferredMethodOfContact { get; set; }

        /// <summary>
        /// CTD.7 - Contact Identifiers (repeating)
        /// </summary>
        public CX[] ContactIdentifiers { get; set; }

        public CTD()
        {
            SegmentType = SegmentType.Universal;
            ContactRole = [];
            ContactName = [];
            ContactAddress = [];
            ContactLocation = default;
            ContactCommunicationInformation = [];
            PreferredMethodOfContact = default;
            ContactIdentifiers = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: ContactRole = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 2: ContactName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 3: ContactAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 4:
                    var pl4 = new PL();
                    pl4.Parse(value.AsSpan(), delimiters);
                    ContactLocation = pl4;
                    break;
                case 5: ContactCommunicationInformation = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 6:
                    var ce6 = new CE();
                    ce6.Parse(value.AsSpan(), delimiters);
                    PreferredMethodOfContact = ce6;
                    break;
                case 7: ContactIdentifiers = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SegmentFieldHelper.JoinRepeatingField(ContactRole, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContactName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContactAddress, delimiters),
                ContactLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContactCommunicationInformation, delimiters),
                PreferredMethodOfContact.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ContactIdentifiers, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SegmentFieldHelper.JoinRepeatingField(ContactRole, delimiters),
                2 => SegmentFieldHelper.JoinRepeatingField(ContactName, delimiters),
                3 => SegmentFieldHelper.JoinRepeatingField(ContactAddress, delimiters),
                4 => ContactLocation.ToHL7String(delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(ContactCommunicationInformation, delimiters),
                6 => PreferredMethodOfContact.ToHL7String(delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(ContactIdentifiers, delimiters),
                _ => null
            };
        }
    }
}
