using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Insurance Additional Information
    /// </summary>
    public class IN2 : ISegment
    {
        public string SegmentId => nameof(IN2);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// IN2.1 - Insured's Employee ID (repeating)
        /// </summary>
        public CX[] InsuredEmployeeID { get; set; }

        /// <summary>
        /// IN2.2 - Insured's Social Security Number
        /// </summary>
        public ST InsuredSocialSecurityNumber { get; set; }

        /// <summary>
        /// IN2.3 - Insured's Employer's Name and ID (repeating)
        /// </summary>
        public XCN[] InsuredEmployerNameAndID { get; set; }

        /// <summary>
        /// IN2.4 - Employer Information Data
        /// </summary>
        public IS EmployerInformationData { get; set; }

        /// <summary>
        /// IN2.5 - Mail Claim Party (repeating)
        /// </summary>
        public IS[] MailClaimParty { get; set; }

        /// <summary>
        /// IN2.6 - Medicare Health Ins Card Number
        /// </summary>
        public ST MedicareHealthInsCardNumber { get; set; }

        /// <summary>
        /// IN2.7 - Medicaid Case Name (repeating)
        /// </summary>
        public XPN[] MedicaidCaseName { get; set; }

        /// <summary>
        /// IN2.8 - Medicaid Case Number
        /// </summary>
        public ST MedicaidCaseNumber { get; set; }

        /// <summary>
        /// IN2.9 - Military Sponsor Name (repeating)
        /// </summary>
        public XPN[] MilitarySponsorName { get; set; }

        /// <summary>
        /// IN2.10 - Military ID Number
        /// </summary>
        public ST MilitaryIDNumber { get; set; }

        /// <summary>
        /// IN2.11 - Dependent Of Military Recipient
        /// </summary>
        public CE DependentOfMilitaryRecipient { get; set; }

        /// <summary>
        /// IN2.12 - Military Organization
        /// </summary>
        public ST MilitaryOrganization { get; set; }

        /// <summary>
        /// IN2.13 - Military Station
        /// </summary>
        public ST MilitaryStation { get; set; }

        /// <summary>
        /// IN2.14 - Military Service
        /// </summary>
        public IS MilitaryService { get; set; }

        /// <summary>
        /// IN2.15 - Military Rank/Grade
        /// </summary>
        public IS MilitaryRankGrade { get; set; }

        /// <summary>
        /// IN2.16 - Military Status
        /// </summary>
        public IS MilitaryStatus { get; set; }

        /// <summary>
        /// IN2.17 - Military Retire Date
        /// </summary>
        public DT MilitaryRetireDate { get; set; }

        /// <summary>
        /// IN2.18 - Military Non-Avail Cert On File
        /// </summary>
        public ID MilitaryNonAvailCertOnFile { get; set; }

        /// <summary>
        /// IN2.19 - Baby Coverage
        /// </summary>
        public ID BabyCoverage { get; set; }

        /// <summary>
        /// IN2.20 - Combine Baby Bill
        /// </summary>
        public ID CombineBabyBill { get; set; }

        public IN2()
        {
            SegmentType = SegmentType.Universal;
            InsuredEmployeeID = [];
            InsuredSocialSecurityNumber = default;
            InsuredEmployerNameAndID = [];
            EmployerInformationData = default;
            MailClaimParty = [];
            MedicareHealthInsCardNumber = default;
            MedicaidCaseName = [];
            MedicaidCaseNumber = default;
            MilitarySponsorName = [];
            MilitaryIDNumber = default;
            DependentOfMilitaryRecipient = default;
            MilitaryOrganization = default;
            MilitaryStation = default;
            MilitaryService = default;
            MilitaryRankGrade = default;
            MilitaryStatus = default;
            MilitaryRetireDate = default;
            MilitaryNonAvailCertOnFile = default;
            BabyCoverage = default;
            CombineBabyBill = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: InsuredEmployeeID = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 2: InsuredSocialSecurityNumber = new ST(value); break;
                case 3: InsuredEmployerNameAndID = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 4: EmployerInformationData = new IS(value); break;
                case 5: MailClaimParty = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters); break;
                case 6: MedicareHealthInsCardNumber = new ST(value); break;
                case 7: MedicaidCaseName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 8: MedicaidCaseNumber = new ST(value); break;
                case 9: MilitarySponsorName = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters); break;
                case 10: MilitaryIDNumber = new ST(value); break;
                case 11:
                    var ce11 = new CE();
                    ce11.Parse(value.AsSpan(), delimiters);
                    DependentOfMilitaryRecipient = ce11;
                    break;
                case 12: MilitaryOrganization = new ST(value); break;
                case 13: MilitaryStation = new ST(value); break;
                case 14: MilitaryService = new IS(value); break;
                case 15: MilitaryRankGrade = new IS(value); break;
                case 16: MilitaryStatus = new IS(value); break;
                case 17: MilitaryRetireDate = new DT(value); break;
                case 18: MilitaryNonAvailCertOnFile = new ID(value); break;
                case 19: BabyCoverage = new ID(value); break;
                case 20: CombineBabyBill = new ID(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SegmentFieldHelper.JoinRepeatingField(InsuredEmployeeID, delimiters),
                InsuredSocialSecurityNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuredEmployerNameAndID, delimiters),
                EmployerInformationData.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(MailClaimParty, delimiters),
                MedicareHealthInsCardNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(MedicaidCaseName, delimiters),
                MedicaidCaseNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(MilitarySponsorName, delimiters),
                MilitaryIDNumber.ToHL7String(delimiters),
                DependentOfMilitaryRecipient.ToHL7String(delimiters),
                MilitaryOrganization.ToHL7String(delimiters),
                MilitaryStation.ToHL7String(delimiters),
                MilitaryService.ToHL7String(delimiters),
                MilitaryRankGrade.ToHL7String(delimiters),
                MilitaryStatus.ToHL7String(delimiters),
                MilitaryRetireDate.ToHL7String(delimiters),
                MilitaryNonAvailCertOnFile.ToHL7String(delimiters),
                BabyCoverage.ToHL7String(delimiters),
                CombineBabyBill.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SegmentFieldHelper.JoinRepeatingField(InsuredEmployeeID, delimiters),
                2 => InsuredSocialSecurityNumber.Value,
                3 => SegmentFieldHelper.JoinRepeatingField(InsuredEmployerNameAndID, delimiters),
                4 => EmployerInformationData.Value,
                5 => SegmentFieldHelper.JoinRepeatingField(MailClaimParty, delimiters),
                6 => MedicareHealthInsCardNumber.Value,
                7 => SegmentFieldHelper.JoinRepeatingField(MedicaidCaseName, delimiters),
                8 => MedicaidCaseNumber.Value,
                9 => SegmentFieldHelper.JoinRepeatingField(MilitarySponsorName, delimiters),
                10 => MilitaryIDNumber.Value,
                11 => DependentOfMilitaryRecipient.ToHL7String(delimiters),
                12 => MilitaryOrganization.Value,
                13 => MilitaryStation.Value,
                14 => MilitaryService.Value,
                15 => MilitaryRankGrade.Value,
                16 => MilitaryStatus.Value,
                17 => MilitaryRetireDate.Value,
                18 => MilitaryNonAvailCertOnFile.Value,
                19 => BabyCoverage.Value,
                20 => CombineBabyBill.Value,
                _ => null
            };
        }
    }
}
