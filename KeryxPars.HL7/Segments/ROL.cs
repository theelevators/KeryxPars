using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Role
    /// </summary>
    public class ROL : ISegment
    {
        public string SegmentId => nameof(ROL);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// ROL.1 - Role Instance ID
        /// </summary>
        public EI RoleInstanceID { get; set; }

        /// <summary>
        /// ROL.2 - Action Code
        /// </summary>
        public ID ActionCode { get; set; }

        /// <summary>
        /// ROL.3 - Role-ROL
        /// </summary>
        public CE RoleROL { get; set; }

        /// <summary>
        /// ROL.4 - Role Person (repeating)
        /// </summary>
        public XCN[] RolePerson { get; set; }

        /// <summary>
        /// ROL.5 - Role Begin Date/Time
        /// </summary>
        public DTM RoleBeginDateTime { get; set; }

        /// <summary>
        /// ROL.6 - Role End Date/Time
        /// </summary>
        public DTM RoleEndDateTime { get; set; }

        /// <summary>
        /// ROL.7 - Role Duration
        /// </summary>
        public CE RoleDuration { get; set; }

        /// <summary>
        /// ROL.8 - Role Action Reason
        /// </summary>
        public CE RoleActionReason { get; set; }

        /// <summary>
        /// ROL.9 - Provider Type (repeating)
        /// </summary>
        public CE[] ProviderType { get; set; }

        /// <summary>
        /// ROL.10 - Organization Unit Type
        /// </summary>
        public CE OrganizationUnitType { get; set; }

        /// <summary>
        /// ROL.11 - Office/Home Address/Birthplace (repeating)
        /// </summary>
        public XAD[] OfficeHomeAddressBirthplace { get; set; }

        /// <summary>
        /// ROL.12 - Phone (repeating)
        /// </summary>
        public XTN[] Phone { get; set; }

        public ROL()
        {
            SegmentType = SegmentType.Universal;
            RoleInstanceID = default;
            ActionCode = default;
            RoleROL = default;
            RolePerson = [];
            RoleBeginDateTime = default;
            RoleEndDateTime = default;
            RoleDuration = default;
            RoleActionReason = default;
            ProviderType = [];
            OrganizationUnitType = default;
            OfficeHomeAddressBirthplace = [];
            Phone = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ei1 = new EI();
                    ei1.Parse(value.AsSpan(), delimiters);
                    RoleInstanceID = ei1;
                    break;
                case 2: ActionCode = new ID(value); break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    RoleROL = ce3;
                    break;
                case 4: RolePerson = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 5: RoleBeginDateTime = new DTM(value); break;
                case 6: RoleEndDateTime = new DTM(value); break;
                case 7:
                    var ce7 = new CE();
                    ce7.Parse(value.AsSpan(), delimiters);
                    RoleDuration = ce7;
                    break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    RoleActionReason = ce8;
                    break;
                case 9: ProviderType = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 10:
                    var ce10 = new CE();
                    ce10.Parse(value.AsSpan(), delimiters);
                    OrganizationUnitType = ce10;
                    break;
                case 11: OfficeHomeAddressBirthplace = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 12: Phone = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                RoleInstanceID.ToHL7String(delimiters),
                ActionCode.ToHL7String(delimiters),
                RoleROL.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(RolePerson, delimiters),
                RoleBeginDateTime.ToHL7String(delimiters),
                RoleEndDateTime.ToHL7String(delimiters),
                RoleDuration.ToHL7String(delimiters),
                RoleActionReason.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProviderType, delimiters),
                OrganizationUnitType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(OfficeHomeAddressBirthplace, delimiters),
                SegmentFieldHelper.JoinRepeatingField(Phone, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => RoleInstanceID.ToHL7String(delimiters),
                2 => ActionCode.Value,
                3 => RoleROL.ToHL7String(delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(RolePerson, delimiters),
                5 => RoleBeginDateTime.Value,
                6 => RoleEndDateTime.Value,
                7 => RoleDuration.ToHL7String(delimiters),
                8 => RoleActionReason.ToHL7String(delimiters),
                9 => SegmentFieldHelper.JoinRepeatingField(ProviderType, delimiters),
                10 => OrganizationUnitType.ToHL7String(delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(OfficeHomeAddressBirthplace, delimiters),
                12 => SegmentFieldHelper.JoinRepeatingField(Phone, delimiters),
                _ => null
            };
        }
    }
}
