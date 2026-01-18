using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// IN1 Segment: Insurrance Information
    /// </summary>
    public partial class IN1 : ISegment
    {
        public string SegmentId => nameof(IN1);

        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// IN1.1
        /// </summary>
        public string SetID { get; set; }
        /// <summary>
        /// IN1.2 
        /// </summary>
        public string InsurancePlanId { get; set; }
        /// <summary>
        /// IN1.3
        /// </summary>
        public string InsuranceCompanyID { get; set; }
        /// <summary>
        /// IN1.4
        /// </summary>
        public string InsuranceCompanyName { get; set; }
        /// <summary>
        /// IN1.5
        /// </summary>
        public string InsuranceCompanyAddress { get; set; }
        /// <summary>
        /// IN1.6
        /// </summary>
        public string InsuranceCoContactPerson { get; set; }
        /// <summary>
        /// IN1.7
        /// </summary>
        public string InsuranceCoPhoneNumber { get; set; }
        /// <summary>
        /// IN1.8
        /// </summary>
        public string GroupNumber { get; set; }
        /// <summary>
        /// IN1.9
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// IN1.10
        /// </summary>
        public string InsuredsGroupEmpID { get; set; }
        /// <summary>
        /// IN1.11
        /// </summary>
        public string InsuredsGroupEmpName { get; set; }
        /// <summary>
        /// IN1.12
        /// </summary>
        public string PlanEffectiveDate { get; set; }
        /// <summary>
        /// IN1.13
        /// </summary>
        public string PlanExpirationDate { get; set; }
        public string AuthorizationInformation { get; set; }
        /// <summary>
        /// IN1.15
        /// </summary>
        public string PlanType { get; set; }
        /// <summary>
        /// IN1.16
        /// </summary>
        public string NameOfInsured { get; set; }
        /// <summary>
        /// IN1.17
        /// </summary>
        public string InsuredsRelationshipToPatient { get; set; }
        /// <summary>
        /// IN1.18
        /// </summary>
        public string InsuredsDateOfBirth { get; set; }
        /// <summary>
        /// IN1.19
        /// </summary>
        public string InsuredsAddress { get; set; }
        /// <summary>
        /// IN1.20
        /// </summary>
        public string AssignmentOfBenefits { get; set; }
        public string CoordinationOfBenefits { get; set; }
        /// <summary>
        /// IN1.22
        /// </summary>
        public string CoordOfBenPriority { get; set; }
        /// <summary>
        /// IN1.23
        /// </summary>
        public string NoticeOfAdmissionFlag { get; set; }
        /// <summary>
        /// IN1.24
        /// </summary>
        public string NoticeOfAdmissionDate { get; set; }
        /// <summary>
        /// IN1.25
        /// </summary>
        public string ReportOfEligibityFlag { get; set; }
        /// <summary>
        /// IN1.26
        /// </summary>
        public string ReportOfEligibilityDate { get; set; }
        /// <summary>
        /// IN1.27
        /// </summary>
        public string ReleaseInformationCode { get; set; }
        /// <summary>
        /// IN1.28
        /// </summary>
        public string PreAdmitCert { get; set; }
        /// <summary>
        /// IN1.29
        /// </summary>
        public string VerificationDateTime { get; set; }
        /// <summary>
        /// IN1.30
        /// </summary>
        public string VertificationBy { get; set; }
        /// <summary>
        /// IN1.31
        /// </summary>
        public string TypeOfAgreement { get; set; }
        /// <summary>
        /// IN1.32
        /// </summary>
        public string BillingStatus { get; set; }
        /// <summary>
        /// IN1.33
        /// </summary>
        public string LifeTimeReserveDays { get; set; }
        /// <summary>
        /// IN1.34
        /// </summary>
        public string DelayBeforeLRDay { get; set; }
        /// <summary>
        /// IN1.35
        /// </summary>
        public string CompanyPlanCode { get; set; }
        /// <summary>
        /// IN1.36
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// IN1.37
        /// </summary>
        public string PolicyDeductible { get; set; }
        /// <summary>
        /// IN1.43
        /// </summary>
        public string InsuredsAdministrativeSex { get; set; }
        /// <summary>
        /// IN1.49
        /// </summary>
        public string InsuredIdNumber { get; set; }

        public IN1()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = string.Empty;
            InsurancePlanId = string.Empty;
            InsuranceCompanyID = string.Empty;
            InsuranceCompanyName = string.Empty;
            InsuranceCompanyAddress = string.Empty;
            InsuranceCoContactPerson = string.Empty;
            InsuranceCoPhoneNumber = string.Empty;
            GroupNumber = string.Empty;
            GroupName = string.Empty;
            InsuredsGroupEmpID = string.Empty;
            InsuredsGroupEmpName = string.Empty;
            PlanEffectiveDate = string.Empty;
            PlanExpirationDate = string.Empty;
            AuthorizationInformation = string.Empty;
            PlanType = string.Empty;
            NameOfInsured = string.Empty;
            InsuredsRelationshipToPatient = string.Empty;
            InsuredsDateOfBirth = string.Empty;
            InsuredsAddress = string.Empty;
            AssignmentOfBenefits = string.Empty;
            CoordinationOfBenefits = string.Empty;
            CoordOfBenPriority = string.Empty;
            NoticeOfAdmissionFlag = string.Empty;
            NoticeOfAdmissionDate = string.Empty;
            ReportOfEligibityFlag = string.Empty;
            ReportOfEligibilityDate = string.Empty;
            ReleaseInformationCode = string.Empty;
            PreAdmitCert = string.Empty;
            VerificationDateTime = string.Empty;
            VertificationBy = string.Empty;
            TypeOfAgreement = string.Empty;
            BillingStatus = string.Empty;
            LifeTimeReserveDays = string.Empty;
            DelayBeforeLRDay = string.Empty;
            CompanyPlanCode = string.Empty;
            PolicyNumber = string.Empty;
            PolicyDeductible = string.Empty;
            InsuredsAdministrativeSex = string.Empty;
            InsuredIdNumber = string.Empty;
        }


        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: InsurancePlanId = value; break;
                case 3: InsuranceCompanyID = value; break;
                case 4: InsuranceCompanyName = value; break;
                case 5: InsuranceCompanyAddress = value; break;
                case 6: InsuranceCoContactPerson = value; break;
                case 7: InsuranceCoPhoneNumber = value; break;
                case 8: GroupNumber = value; break;
                case 9: GroupName = value; break;
                case 10: InsuredsGroupEmpID = value; break;
                case 11: InsuredsGroupEmpName = value; break;
                case 12: PlanEffectiveDate = value; break;
                case 13: PlanExpirationDate = value; break;
                case 14: AuthorizationInformation = value; break;
                case 15: PlanType = value; break;
                case 16: NameOfInsured = value; break;
                case 17: InsuredsRelationshipToPatient = value; break;
                case 18: InsuredsDateOfBirth = value; break;
                case 19: InsuredsAddress = value; break;
                case 20: AssignmentOfBenefits = value; break;
                case 21: CoordinationOfBenefits = value; break;
                case 22: CoordOfBenPriority = value; break;
                case 23: NoticeOfAdmissionFlag = value; break;
                case 24: NoticeOfAdmissionDate = value; break;
                case 25: ReportOfEligibityFlag = value; break;
                case 26: ReportOfEligibilityDate = value; break;
                case 27: ReleaseInformationCode = value; break;
                case 28: PreAdmitCert = value; break;
                case 29: VerificationDateTime = value; break;
                case 30: VertificationBy = value; break;
                case 31: TypeOfAgreement = value; break;
                case 32: BillingStatus = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return new string[]
            {
                SegmentId,
                SetID,
                InsurancePlanId,
                InsuranceCompanyID,
                InsuranceCompanyName,
                InsuranceCompanyAddress,
                InsuranceCoContactPerson,
                InsuranceCoPhoneNumber,
                GroupNumber,
                GroupName,
                InsuredsGroupEmpID,
                InsuredsGroupEmpName,
                PlanEffectiveDate,
                PlanExpirationDate,
                AuthorizationInformation,
                PlanType,
                NameOfInsured,
                InsuredsRelationshipToPatient,
                InsuredsDateOfBirth,
                InsuredsAddress,
                AssignmentOfBenefits,
                CoordinationOfBenefits,
                CoordOfBenPriority,
                NoticeOfAdmissionFlag,
                NoticeOfAdmissionDate,
                ReportOfEligibityFlag,
                ReportOfEligibilityDate,
                ReleaseInformationCode,
                PreAdmitCert,
                VerificationDateTime,
                VertificationBy,
                TypeOfAgreement,
                BillingStatus
            };
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => InsurancePlanId,
                3 => InsuranceCompanyID,
                4 => InsuranceCompanyName,
                5 => InsuranceCompanyAddress,
                6 => InsuranceCoContactPerson,
                7 => InsuranceCoPhoneNumber,
                8 => GroupNumber,
                9 => GroupName,
                10 => InsuredsGroupEmpID,
                11 => InsuredsGroupEmpName,
                12 => PlanEffectiveDate,
                13 => PlanExpirationDate,
                14 => AuthorizationInformation,
                15 => PlanType,
                16 => NameOfInsured,
                17 => InsuredsRelationshipToPatient,
                18 => InsuredsDateOfBirth,
                19 => InsuredsAddress,
                20 => AssignmentOfBenefits,
                21 => CoordinationOfBenefits,
                22 => CoordOfBenPriority,
                23 => NoticeOfAdmissionFlag,
                24 => NoticeOfAdmissionDate,
                25 => ReportOfEligibityFlag,
                26 => ReportOfEligibilityDate,
                27 => ReleaseInformationCode,
                28 => PreAdmitCert,
                29 => VerificationDateTime,
                30 => VertificationBy,
                31 => TypeOfAgreement,
                32 => BillingStatus,
                _ => null
            };
        }
    }
}
