using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Guarantor
    /// </summary>
    public class GT1 : ISegment
    {
        public string SegmentId => nameof(GT1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// GT1.1 - Set ID - GT1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// GT1.2 - Guarantor Number (repeating)
        /// </summary>
        public CX[] GuarantorNumber { get; set; }

        /// <summary>
        /// GT1.3 - Guarantor Name (repeating)
        /// </summary>
        public XPN[] GuarantorName { get; set; }

        /// <summary>
        /// GT1.4 - Guarantor Spouse Name (repeating)
        /// </summary>
        public XPN[] GuarantorSpouseName { get; set; }

        /// <summary>
        /// GT1.5 - Guarantor Address (repeating)
        /// </summary>
        public XAD[] GuarantorAddress { get; set; }

        /// <summary>
        /// GT1.6 - Guarantor Phone Number - Home (repeating)
        /// </summary>
        public XTN[] GuarantorPhoneNumberHome { get; set; }

        /// <summary>
        /// GT1.7 - Guarantor Phone Number - Business (repeating)
        /// </summary>
        public XTN[] GuarantorPhoneNumberBusiness { get; set; }

        /// <summary>
        /// GT1.8 - Guarantor Date/Time of Birth
        /// </summary>
        public DTM GuarantorDateTimeOfBirth { get; set; }

        /// <summary>
        /// GT1.9 - Guarantor Administrative Sex
        /// </summary>
        public IS GuarantorAdministrativeSex { get; set; }

        /// <summary>
        /// GT1.10 - Guarantor Type
        /// </summary>
        public IS GuarantorType { get; set; }

        /// <summary>
        /// GT1.11 - Guarantor Relationship
        /// </summary>
        public CE GuarantorRelationship { get; set; }

        /// <summary>
        /// GT1.12 - Guarantor SSN
        /// </summary>
        public ST GuarantorSSN { get; set; }

        /// <summary>
        /// GT1.13 - Guarantor Date - Begin
        /// </summary>
        public DT GuarantorDateBegin { get; set; }

        /// <summary>
        /// GT1.14 - Guarantor Date - End
        /// </summary>
        public DT GuarantorDateEnd { get; set; }

        /// <summary>
        /// GT1.15 - Guarantor Priority
        /// </summary>
        public NM GuarantorPriority { get; set; }

        /// <summary>
        /// GT1.16 - Guarantor Employer Name (repeating)
        /// </summary>
        public XPN[] GuarantorEmployerName { get; set; }

        /// <summary>
        /// GT1.17 - Guarantor Employer Address (repeating)
        /// </summary>
        public XAD[] GuarantorEmployerAddress { get; set; }

        /// <summary>
        /// GT1.18 - Guarantor Employer Phone Number (repeating)
        /// </summary>
        public XTN[] GuarantorEmployerPhoneNumber { get; set; }

        /// <summary>
        /// GT1.19 - Guarantor Employee ID Number (repeating)
        /// </summary>
        public CX[] GuarantorEmployeeIDNumber { get; set; }

        /// <summary>
        /// GT1.20 - Guarantor Employment Status
        /// </summary>
        public IS GuarantorEmploymentStatus { get; set; }

        public GT1()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            GuarantorNumber = [];
            GuarantorName = [];
            GuarantorSpouseName = [];
            GuarantorAddress = [];
            GuarantorPhoneNumberHome = [];
            GuarantorPhoneNumberBusiness = [];
            GuarantorDateTimeOfBirth = default;
            GuarantorAdministrativeSex = default;
            GuarantorType = default;
            GuarantorRelationship = default;
            GuarantorSSN = default;
            GuarantorDateBegin = default;
            GuarantorDateEnd = default;
            GuarantorPriority = default;
            GuarantorEmployerName = [];
            GuarantorEmployerAddress = [];
            GuarantorEmployerPhoneNumber = [];
            GuarantorEmployeeIDNumber = [];
            GuarantorEmploymentStatus = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: GuarantorNumber = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 3: GuarantorName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 4: GuarantorSpouseName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 5: GuarantorAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 6: GuarantorPhoneNumberHome = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 7: GuarantorPhoneNumberBusiness = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 8: GuarantorDateTimeOfBirth = new DTM(value); break;
                case 9: GuarantorAdministrativeSex = new IS(value); break;
                case 10: GuarantorType = new IS(value); break;
                case 11:
                    var ce11 = new CE();
                    ce11.Parse(value.AsSpan(), delimiters);
                    GuarantorRelationship = ce11;
                    break;
                case 12: GuarantorSSN = new ST(value); break;
                case 13: GuarantorDateBegin = new DT(value); break;
                case 14: GuarantorDateEnd = new DT(value); break;
                case 15: GuarantorPriority = new NM(value); break;
                case 16: GuarantorEmployerName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 17: GuarantorEmployerAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters); break;
                case 18: GuarantorEmployerPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters); break;
                case 19: GuarantorEmployeeIDNumber = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 20: GuarantorEmploymentStatus = new IS(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorSpouseName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorAddress, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorPhoneNumberHome, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorPhoneNumberBusiness, delimiters),
                GuarantorDateTimeOfBirth.ToHL7String(delimiters),
                GuarantorAdministrativeSex.ToHL7String(delimiters),
                GuarantorType.ToHL7String(delimiters),
                GuarantorRelationship.ToHL7String(delimiters),
                GuarantorSSN.ToHL7String(delimiters),
                GuarantorDateBegin.ToHL7String(delimiters),
                GuarantorDateEnd.ToHL7String(delimiters),
                GuarantorPriority.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorEmployerName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorEmployerAddress, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorEmployerPhoneNumber, delimiters),
                SegmentFieldHelper.JoinRepeatingField(GuarantorEmployeeIDNumber, delimiters),
                GuarantorEmploymentStatus.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => SegmentFieldHelper.JoinRepeatingField(GuarantorNumber, delimiters),
                3 => SegmentFieldHelper.JoinRepeatingField(GuarantorName, delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(GuarantorSpouseName, delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(GuarantorAddress, delimiters),
                6 => SegmentFieldHelper.JoinRepeatingField(GuarantorPhoneNumberHome, delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(GuarantorPhoneNumberBusiness, delimiters),
                8 => GuarantorDateTimeOfBirth.Value,
                9 => GuarantorAdministrativeSex.Value,
                10 => GuarantorType.Value,
                11 => GuarantorRelationship.ToHL7String(delimiters),
                12 => GuarantorSSN.Value,
                13 => GuarantorDateBegin.Value,
                14 => GuarantorDateEnd.Value,
                15 => GuarantorPriority.Value,
                16 => SegmentFieldHelper.JoinRepeatingField(GuarantorEmployerName, delimiters),
                17 => SegmentFieldHelper.JoinRepeatingField(GuarantorEmployerAddress, delimiters),
                18 => SegmentFieldHelper.JoinRepeatingField(GuarantorEmployerPhoneNumber, delimiters),
                19 => SegmentFieldHelper.JoinRepeatingField(GuarantorEmployeeIDNumber, delimiters),
                20 => GuarantorEmploymentStatus.Value,
                _ => null
            };
        }
    }
}
