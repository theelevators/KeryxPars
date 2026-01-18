using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// IN1 Segment: Insurance Information
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public partial class IN1 : ISegment
    {
        public string SegmentId => nameof(IN1);

        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// IN1.1 - Set ID - IN1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// IN1.2 - Insurance Plan ID
        /// </summary>
        public CE InsurancePlanId { get; set; }

        /// <summary>
        /// IN1.3 - Insurance Company ID
        /// </summary>
        public CX[] InsuranceCompanyID { get; set; }

        /// <summary>
        /// IN1.4 - Insurance Company Name
        /// </summary>
        public XON[] InsuranceCompanyName { get; set; }

        /// <summary>
        /// IN1.5 - Insurance Company Address
        /// </summary>
        public XAD[] InsuranceCompanyAddress { get; set; }

        /// <summary>
        /// IN1.6 - Insurance Company Contact Person
        /// </summary>
        public XPN[] InsuranceCoContactPerson { get; set; }

        /// <summary>
        /// IN1.7 - Insurance Company Phone Number
        /// </summary>
        public XTN[] InsuranceCoPhoneNumber { get; set; }

        /// <summary>
        /// IN1.8 - Group Number
        /// </summary>
        public ST GroupNumber { get; set; }

        /// <summary>
        /// IN1.9 - Group Name
        /// </summary>
        public XON[] GroupName { get; set; }

        /// <summary>
        /// IN1.10 - Insured's Group Emp ID
        /// </summary>
        public CX[] InsuredsGroupEmpID { get; set; }

        /// <summary>
        /// IN1.11 - Insured's Group Emp Name
        /// </summary>
        public XON[] InsuredsGroupEmpName { get; set; }

        /// <summary>
        /// IN1.12 - Plan Effective Date
        /// </summary>
        public DT PlanEffectiveDate { get; set; }

        /// <summary>
        /// IN1.13 - Plan Expiration Date
        /// </summary>
        public DT PlanExpirationDate { get; set; }

        /// <summary>
        /// IN1.14 - Authorization Information
        /// </summary>
        public ST AuthorizationInformation { get; set; }

        /// <summary>
        /// IN1.15 - Plan Type
        /// </summary>
        public IS PlanType { get; set; }

        /// <summary>
        /// IN1.16 - Name Of Insured
        /// </summary>
        public XPN[] NameOfInsured { get; set; }

        /// <summary>
        /// IN1.17 - Insured's Relationship To Patient
        /// </summary>
        public CE InsuredsRelationshipToPatient { get; set; }

        /// <summary>
        /// IN1.18 - Insured's Date Of Birth
        /// </summary>
        public DTM InsuredsDateOfBirth { get; set; }

        /// <summary>
        /// IN1.19 - Insured's Address
        /// </summary>
        public XAD[] InsuredsAddress { get; set; }

        /// <summary>
        /// IN1.20 - Assignment Of Benefits
        /// </summary>
        public IS AssignmentOfBenefits { get; set; }

        /// <summary>
        /// IN1.21 - Coordination Of Benefits
        /// </summary>
        public IS CoordinationOfBenefits { get; set; }

        /// <summary>
        /// IN1.22 - Coord Of Ben. Priority
        /// </summary>
        public ST CoordOfBenPriority { get; set; }

        /// <summary>
        /// IN1.23 - Notice Of Admission Flag
        /// </summary>
        public ID NoticeOfAdmissionFlag { get; set; }

        /// <summary>
        /// IN1.24 - Notice Of Admission Date
        /// </summary>
        public DT NoticeOfAdmissionDate { get; set; }

        /// <summary>
        /// IN1.25 - Report Of Eligibility Flag
        /// </summary>
        public ID ReportOfEligibityFlag { get; set; }

        /// <summary>
        /// IN1.26 - Report Of Eligibility Date
        /// </summary>
        public DT ReportOfEligibilityDate { get; set; }

        /// <summary>
        /// IN1.27 - Release Information Code
        /// </summary>
        public IS ReleaseInformationCode { get; set; }

        /// <summary>
        /// IN1.28 - Pre-Admit Cert (PAC)
        /// </summary>
        public ST PreAdmitCert { get; set; }

        /// <summary>
        /// IN1.29 - Verification Date/Time
        /// </summary>
        public DTM VerificationDateTime { get; set; }

        /// <summary>
        /// IN1.30 - Verification By
        /// </summary>
        public XCN[] VertificationBy { get; set; }

        /// <summary>
        /// IN1.31 - Type Of Agreement Code
        /// </summary>
        public IS TypeOfAgreement { get; set; }

        /// <summary>
        /// IN1.32 - Billing Status
        /// </summary>
        public IS BillingStatus { get; set; }

        /// <summary>
        /// IN1.33 - Lifetime Reserve Days
        /// </summary>
        public NM LifeTimeReserveDays { get; set; }

        /// <summary>
        /// IN1.34 - Delay Before L.R. Day
        /// </summary>
        public NM DelayBeforeLRDay { get; set; }

        /// <summary>
        /// IN1.35 - Company Plan Code
        /// </summary>
        public IS CompanyPlanCode { get; set; }

        /// <summary>
        /// IN1.36 - Policy Number
        /// </summary>
        public ST PolicyNumber { get; set; }

        /// <summary>
        /// IN1.37 - Policy Deductible
        /// </summary>
        public NM PolicyDeductible { get; set; }

        /// <summary>
        /// IN1.43 - Insured's Administrative Sex
        /// </summary>
        public IS InsuredsAdministrativeSex { get; set; }

        /// <summary>
        /// IN1.49 - Insured's ID Number
        /// </summary>
        public CX[] InsuredIdNumber { get; set; }

        public IN1()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = default;
            InsurancePlanId = default;
            InsuranceCompanyID = [];
            InsuranceCompanyName = [];
            InsuranceCompanyAddress = [];
            InsuranceCoContactPerson = [];
            InsuranceCoPhoneNumber = [];
            GroupNumber = default;
            GroupName = [];
            InsuredsGroupEmpID = [];
            InsuredsGroupEmpName = [];
            PlanEffectiveDate = default;
            PlanExpirationDate = default;
            AuthorizationInformation = default;
            PlanType = default;
            NameOfInsured = [];
            InsuredsRelationshipToPatient = default;
            InsuredsDateOfBirth = default;
            InsuredsAddress = [];
            AssignmentOfBenefits = default;
            CoordinationOfBenefits = default;
            CoordOfBenPriority = default;
            NoticeOfAdmissionFlag = default;
            NoticeOfAdmissionDate = default;
            ReportOfEligibityFlag = default;
            ReportOfEligibilityDate = default;
            ReleaseInformationCode = default;
            PreAdmitCert = default;
            VerificationDateTime = default;
            VertificationBy = [];
            TypeOfAgreement = default;
            BillingStatus = default;
            LifeTimeReserveDays = default;
            DelayBeforeLRDay = default;
            CompanyPlanCode = default;
            PolicyNumber = default;
            PolicyDeductible = default;
            InsuredsAdministrativeSex = default;
            InsuredIdNumber = [];
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
                    InsurancePlanId = ce2;
                    break;
                case 3:
                    InsuranceCompanyID = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters);
                    break;
                case 4:
                    InsuranceCompanyName = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters);
                    break;
                case 5:
                    InsuranceCompanyAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters);
                    break;
                case 6:
                    InsuranceCoContactPerson = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters);
                    break;
                case 7:
                    InsuranceCoPhoneNumber = SegmentFieldHelper.ParseRepeatingField<XTN>(value, delimiters);
                    break;
                case 8: GroupNumber = new ST(value); break;
                case 9:
                    GroupName = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters);
                    break;
                case 10:
                    InsuredsGroupEmpID = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters);
                    break;
                case 11:
                    InsuredsGroupEmpName = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters);
                    break;
                case 12: PlanEffectiveDate = new DT(value); break;
                case 13: PlanExpirationDate = new DT(value); break;
                case 14: AuthorizationInformation = new ST(value); break;
                case 15: PlanType = new IS(value); break;
                case 16:
                    NameOfInsured = SegmentFieldHelper.ParseRepeatingField<XPN>(value, delimiters);
                    break;
                case 17:
                    var ce17 = new CE();
                    ce17.Parse(value.AsSpan(), delimiters);
                    InsuredsRelationshipToPatient = ce17;
                    break;
                case 18: InsuredsDateOfBirth = new DTM(value); break;
                case 19:
                    InsuredsAddress = SegmentFieldHelper.ParseRepeatingField<XAD>(value, delimiters);
                    break;
                case 20: AssignmentOfBenefits = new IS(value); break;
                case 21: CoordinationOfBenefits = new IS(value); break;
                case 22: CoordOfBenPriority = new ST(value); break;
                case 23: NoticeOfAdmissionFlag = new ID(value); break;
                case 24: NoticeOfAdmissionDate = new DT(value); break;
                case 25: ReportOfEligibityFlag = new ID(value); break;
                case 26: ReportOfEligibilityDate = new DT(value); break;
                case 27: ReleaseInformationCode = new IS(value); break;
                case 28: PreAdmitCert = new ST(value); break;
                case 29: VerificationDateTime = new DTM(value); break;
                case 30:
                    VertificationBy = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters);
                    break;
                case 31: TypeOfAgreement = new IS(value); break;
                case 32: BillingStatus = new IS(value); break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return new string[]
            {
                SegmentId,
                SetID.ToHL7String(delimiters),
                InsurancePlanId.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuranceCompanyID, delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuranceCompanyName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuranceCompanyAddress, delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuranceCoContactPerson, delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuranceCoPhoneNumber, delimiters),
                GroupNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(GroupName, delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuredsGroupEmpID, delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuredsGroupEmpName, delimiters),
                PlanEffectiveDate.ToHL7String(delimiters),
                PlanExpirationDate.ToHL7String(delimiters),
                AuthorizationInformation.ToHL7String(delimiters),
                PlanType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(NameOfInsured, delimiters),
                InsuredsRelationshipToPatient.ToHL7String(delimiters),
                InsuredsDateOfBirth.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(InsuredsAddress, delimiters),
                AssignmentOfBenefits.ToHL7String(delimiters),
                CoordinationOfBenefits.ToHL7String(delimiters),
                CoordOfBenPriority.ToHL7String(delimiters),
                NoticeOfAdmissionFlag.ToHL7String(delimiters),
                NoticeOfAdmissionDate.ToHL7String(delimiters),
                ReportOfEligibityFlag.ToHL7String(delimiters),
                ReportOfEligibilityDate.ToHL7String(delimiters),
                ReleaseInformationCode.ToHL7String(delimiters),
                PreAdmitCert.ToHL7String(delimiters),
                VerificationDateTime.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(VertificationBy, delimiters),
                TypeOfAgreement.ToHL7String(delimiters),
                BillingStatus.ToHL7String(delimiters)
            };
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => InsurancePlanId.ToHL7String(delimiters),
                3 => SegmentFieldHelper.JoinRepeatingField(InsuranceCompanyID, delimiters),
                4 => SegmentFieldHelper.JoinRepeatingField(InsuranceCompanyName, delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(InsuranceCompanyAddress, delimiters),
                6 => SegmentFieldHelper.JoinRepeatingField(InsuranceCoContactPerson, delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(InsuranceCoPhoneNumber, delimiters),
                8 => GroupNumber.Value,
                9 => SegmentFieldHelper.JoinRepeatingField(GroupName, delimiters),
                10 => SegmentFieldHelper.JoinRepeatingField(InsuredsGroupEmpID, delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(InsuredsGroupEmpName, delimiters),
                12 => PlanEffectiveDate.Value,
                13 => PlanExpirationDate.Value,
                14 => AuthorizationInformation.Value,
                15 => PlanType.Value,
                16 => SegmentFieldHelper.JoinRepeatingField(NameOfInsured, delimiters),
                17 => InsuredsRelationshipToPatient.ToHL7String(delimiters),
                18 => InsuredsDateOfBirth.Value,
                19 => SegmentFieldHelper.JoinRepeatingField(InsuredsAddress, delimiters),
                20 => AssignmentOfBenefits.Value,
                21 => CoordinationOfBenefits.Value,
                22 => CoordOfBenPriority.Value,
                23 => NoticeOfAdmissionFlag.Value,
                24 => NoticeOfAdmissionDate.Value,
                25 => ReportOfEligibityFlag.Value,
                26 => ReportOfEligibilityDate.Value,
                27 => ReleaseInformationCode.Value,
                28 => PreAdmitCert.Value,
                29 => VerificationDateTime.Value,
                30 => SegmentFieldHelper.JoinRepeatingField(VertificationBy, delimiters),
                31 => TypeOfAgreement.Value,
                32 => BillingStatus.Value,
                _ => null
            };
        }
    }
}
