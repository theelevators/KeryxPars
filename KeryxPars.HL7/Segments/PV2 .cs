using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Visit - Additional Information
    /// </summary>
    public class PV2 : ISegment
    {
        public string SegmentId => nameof(PV2);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// PV2.1
        /// </summary>
        public string PriorPendingLocation { get; set; }

        /// <summary>
        /// PV2.2
        /// </summary>
        public string AccomodationCode { get; set; }

        /// <summary>
        /// PV2.3
        /// </summary>
        public string AdmitReason { get; set; }

        /// <summary>
        /// PV2.4
        /// </summary>
        public string TransferReason { get; set; }

        /// <summary>
        /// PV2.5
        /// </summary>
        public string PatientValuables { get; set; }

        /// <summary>
        /// PV2.6
        /// </summary>
        public string PatientValuablesLocation { get; set; }

        /// <summary>
        /// PV2.7
        /// </summary>
        public string VisitUserCode { get; set; }

        /// <summary>
        /// PV2.8
        /// </summary>
        public string ExpectedAdmitDateTime { get; set; }

        /// <summary>
        /// PV2.9
        /// </summary>
        public string ExpectedDischargeDateTime { get; set; }

        /// <summary>
        /// PV2.10
        /// </summary>
        public string EstimatedLengthOfInpatientStay { get; set; }

        /// <summary>
        /// PV2.11
        /// </summary>
        public string ActualLengthOfInpatientStay { get; set; }

        /// <summary>
        /// PV2.12
        /// </summary>
        public string VisitDescription { get; set; }

        /// <summary>
        /// PV2.13
        /// </summary>
        public string ReferralSourceCode { get; set; }

        /// <summary>
        /// PV2.14
        /// </summary>
        public string PreviousServiceDate { get; set; }

        /// <summary>
        /// PV2.15
        /// </summary>
        public string EmploymentIllnessRelatedIndicator { get; set; }

        /// <summary>
        /// PV2.16
        /// </summary>
        public string PurgeStatusCode { get; set; }

        /// <summary>
        /// PV2.17
        /// </summary>
        public string PurgeStatusDate { get; set; }

        /// <summary>
        /// PV2.18
        /// </summary>
        public string SpecialProgramCode { get; set; }

        /// <summary>
        /// PV2.19
        /// </summary>
        public string RetentionIndicator { get; set; }

        /// <summary>
        /// PV2.20
        /// </summary>
        public string ExpectedNumberOfInsurancePlans { get; set; }

        /// <summary>
        /// PV2.21
        /// </summary>
        public string VisitPublicityCode { get; set; }

        /// <summary>
        /// PV2.22
        /// </summary>
        public string VisitProtectionIndicator { get; set; }

        /// <summary>
        /// PV2.23
        /// </summary>
        public string ClinicOrganizationName { get; set; }

        /// <summary>
        /// PV2.24
        /// </summary>
        public string PatientStatusCode { get; set; }

        /// <summary>
        /// PV2.25
        /// </summary>
        public string VisitPriorityCode { get; set; }

        /// <summary>
        /// PV2.26
        /// </summary>
        public string PreviousTreatmentDate { get; set; }

        /// <summary>
        /// PV2.27
        /// </summary>
        public string ExpectedDischargeDisposition { get; set; }

        /// <summary>
        /// PV2.28
        /// </summary>
        public string SignatureOnFileDate { get; set; }

        /// <summary>
        /// PV2.29
        /// </summary>
        public string FirstSimilarIllnessDate { get; set; }

        /// <summary>
        /// PV2.30
        /// </summary>
        public string PatientChargeAdjustmentCode { get; set; }

        /// <summary>
        /// PV2.31
        /// </summary>
        public string RecurringServiceCode { get; set; }

        /// <summary>
        /// PV2.32
        /// </summary>
        public string BillingMediaCode { get; set; }

        /// <summary>
        /// PV2.33
        /// </summary>
        public string ExpectedSurgeryDateAndTime { get; set; }

        /// <summary>
        /// PV2.34
        /// </summary>
        public string MilitaryPartnershipCode { get; set; }

        /// <summary>
        /// PV2.35
        /// </summary>
        public string MilitaryNonAvailabilityCode { get; set; }

        /// <summary>
        /// PV2.36
        /// </summary>
        public string NewbornBabyIndicator { get; set; }

        /// <summary>
        /// PV2.37
        /// </summary>
        public string BabyDetainedIndicator { get; set; }

        /// <summary>
        /// PV2.38
        /// </summary>
        public string ModeOfArrivalCode { get; set; }

        /// <summary>
        /// PV2.39
        /// </summary>
        public string RecreationDrugUseCode { get; set; }

        /// <summary>
        /// PV2.40
        /// </summary>
        public string AdmissionLevelOfCareCode { get; set; }

        /// <summary>
        /// PV2.41
        /// </summary>
        public string PrecautionCode { get; set; }

        /// <summary>
        /// PV2.42
        /// </summary>
        public string PatientConditionCode { get; set; }

        /// <summary>
        /// PV2.43
        /// </summary>
        public string LivingWillCode { get; set; }

        /// <summary>
        /// PV2.44
        /// </summary>
        public string OrganDonorCode { get; set; }

        /// <summary>
        /// PV2.45
        /// </summary>
        public string AdvanceDirectiveCode { get; set; }

        /// <summary>
        /// PV2.46
        /// </summary>
        public string PatientStatusEffectiveDate { get; set; }

        /// <summary>
        /// PV2.47
        /// </summary>
        public string ExpectedLOAReturnDateTime { get; set; }

        /// <summary>
        /// PV2.48
        /// </summary>
        public string ExpectedPreAdmissionTestingDateTime { get; set; }

        /// <summary>
        /// PV2.49
        /// </summary>
        public string NotifyClergyCode { get; set; }

        /// <summary>
        /// PV2.50
        /// </summary>
        public string AdvanceDirectiveLastVerifiedDate { get; set; }

        // Constructors
        public PV2()
        {
            SegmentType = SegmentType.ADT;
            PriorPendingLocation = string.Empty;
            AccomodationCode = string.Empty;
            AdmitReason = string.Empty;
            TransferReason = string.Empty;
            PatientValuables = string.Empty;
            PatientValuablesLocation = string.Empty;
            VisitUserCode = string.Empty;
            ExpectedAdmitDateTime = string.Empty;
            ExpectedDischargeDateTime = string.Empty;
            EstimatedLengthOfInpatientStay = string.Empty;
            ActualLengthOfInpatientStay = string.Empty;
            VisitDescription = string.Empty;
            ReferralSourceCode = string.Empty;
            PreviousServiceDate = string.Empty;
            EmploymentIllnessRelatedIndicator = string.Empty;
            PurgeStatusCode = string.Empty;
            PurgeStatusDate = string.Empty;
            SpecialProgramCode = string.Empty;
            RetentionIndicator = string.Empty;
            ExpectedNumberOfInsurancePlans = string.Empty;
            VisitPublicityCode = string.Empty;
            VisitProtectionIndicator = string.Empty;
            ClinicOrganizationName = string.Empty;
            PatientStatusCode = string.Empty;
            VisitPriorityCode = string.Empty;
            PreviousTreatmentDate = string.Empty;
            ExpectedDischargeDisposition = string.Empty;
            SignatureOnFileDate = string.Empty;
            FirstSimilarIllnessDate = string.Empty;
            PatientChargeAdjustmentCode = string.Empty;
            RecurringServiceCode = string.Empty;
            BillingMediaCode = string.Empty;
            ExpectedSurgeryDateAndTime = string.Empty;
            MilitaryPartnershipCode = string.Empty;
            MilitaryNonAvailabilityCode = string.Empty;
            NewbornBabyIndicator = string.Empty;
            BabyDetainedIndicator = string.Empty;
            ModeOfArrivalCode = string.Empty;
            RecreationDrugUseCode = string.Empty;
            AdmissionLevelOfCareCode = string.Empty;
            PrecautionCode = string.Empty;
            PatientConditionCode = string.Empty;
            LivingWillCode = string.Empty;
            OrganDonorCode = string.Empty;
            AdvanceDirectiveCode = string.Empty;
            PatientStatusEffectiveDate = string.Empty;
            ExpectedLOAReturnDateTime = string.Empty;
            ExpectedPreAdmissionTestingDateTime = string.Empty;
            NotifyClergyCode = string.Empty;
            AdvanceDirectiveLastVerifiedDate = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: PriorPendingLocation = value; break;
                case 2: AccomodationCode = value; break;
                case 3: AdmitReason = value; break;
                case 4: TransferReason = value; break;
                case 5: PatientValuables = value; break;
                case 6: PatientValuablesLocation = value; break;
                case 7: VisitUserCode = value; break;
                case 8: ExpectedAdmitDateTime = value; break;
                case 9: ExpectedDischargeDateTime = value; break;
                case 10: EstimatedLengthOfInpatientStay = value; break;
                case 11: ActualLengthOfInpatientStay = value; break;
                case 12: VisitDescription = value; break;
                case 13: ReferralSourceCode = value; break;
                case 14: PreviousServiceDate = value; break;
                case 15: EmploymentIllnessRelatedIndicator = value; break;
                case 16: PurgeStatusCode = value; break;
                case 17: PurgeStatusDate = value; break;
                case 18: SpecialProgramCode = value; break;
                case 19: RetentionIndicator = value; break;
                case 20: ExpectedNumberOfInsurancePlans = value; break;
                case 21: VisitPublicityCode = value; break;
                case 22: VisitProtectionIndicator = value; break;
                case 23: ClinicOrganizationName = value; break;
                case 24: PatientStatusCode = value; break;
                case 25: VisitPriorityCode = value; break;
                case 26: PreviousTreatmentDate = value; break;
                case 27: ExpectedDischargeDisposition = value; break;
                case 28: SignatureOnFileDate = value; break;
                case 29: FirstSimilarIllnessDate = value; break;
                case 30: PatientChargeAdjustmentCode = value; break;
                case 31: RecurringServiceCode = value; break;
                case 32: BillingMediaCode = value; break;
                case 33: ExpectedSurgeryDateAndTime = value; break;
                case 34: MilitaryPartnershipCode = value; break;
                case 35: MilitaryNonAvailabilityCode = value; break;
                case 36: NewbornBabyIndicator = value; break;
                case 37: BabyDetainedIndicator = value; break;
                case 38: ModeOfArrivalCode = value; break;
                case 39: RecreationDrugUseCode = value; break;
                case 40: AdmissionLevelOfCareCode = value; break;
                case 41: PrecautionCode = value; break;
                case 42: PatientConditionCode = value; break;
                case 43: LivingWillCode = value; break;
                case 44: OrganDonorCode = value; break;
                case 45: AdvanceDirectiveCode = value; break;
                case 46: PatientStatusEffectiveDate = value; break;
                case 47: ExpectedLOAReturnDateTime = value; break;
                case 48: ExpectedPreAdmissionTestingDateTime = value; break;
                case 49: NotifyClergyCode = value; break;
                case 50: AdvanceDirectiveLastVerifiedDate = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                PriorPendingLocation,
                AccomodationCode,
                AdmitReason,
                TransferReason,
                PatientValuables,
                PatientValuablesLocation,
                VisitUserCode,
                ExpectedAdmitDateTime,
                ExpectedDischargeDateTime,
                EstimatedLengthOfInpatientStay,
                ActualLengthOfInpatientStay,
                VisitDescription,
                ReferralSourceCode,
                PreviousServiceDate,
                EmploymentIllnessRelatedIndicator,
                PurgeStatusCode,
                PurgeStatusDate,
                SpecialProgramCode,
                RetentionIndicator,
                ExpectedNumberOfInsurancePlans,
                VisitPublicityCode,
                VisitProtectionIndicator,
                ClinicOrganizationName,
                PatientStatusCode,
                VisitPriorityCode,
                PreviousTreatmentDate,
                ExpectedDischargeDisposition,
                SignatureOnFileDate,
                FirstSimilarIllnessDate,
                PatientChargeAdjustmentCode,
                RecurringServiceCode,
                BillingMediaCode,
                ExpectedSurgeryDateAndTime,
                MilitaryPartnershipCode,
                MilitaryNonAvailabilityCode,
                NewbornBabyIndicator,
                BabyDetainedIndicator,
                ModeOfArrivalCode,
                RecreationDrugUseCode,
                AdmissionLevelOfCareCode,
                PrecautionCode,
                PatientConditionCode,
                LivingWillCode,
                OrganDonorCode,
                AdvanceDirectiveCode,
                PatientStatusEffectiveDate,
                ExpectedLOAReturnDateTime,
                ExpectedPreAdmissionTestingDateTime,
                NotifyClergyCode,
                AdvanceDirectiveLastVerifiedDate
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => PriorPendingLocation,
                2 => AccomodationCode,
                3 => AdmitReason,
                4 => TransferReason,
                5 => PatientValuables,
                6 => PatientValuablesLocation,
                7 => VisitUserCode,
                8 => ExpectedAdmitDateTime,
                9 => ExpectedDischargeDateTime,
                10 => EstimatedLengthOfInpatientStay,
                11 => ActualLengthOfInpatientStay,
                12 => VisitDescription,
                13 => ReferralSourceCode,
                14 => PreviousServiceDate,
                15 => EmploymentIllnessRelatedIndicator,
                16 => PurgeStatusCode,
                17 => PurgeStatusDate,
                18 => SpecialProgramCode,
                19 => RetentionIndicator,
                20 => ExpectedNumberOfInsurancePlans,
                21 => VisitPublicityCode,
                22 => VisitProtectionIndicator,
                23 => ClinicOrganizationName,
                24 => PatientStatusCode,
                25 => VisitPriorityCode,
                26 => PreviousTreatmentDate,
                27 => ExpectedDischargeDisposition,
                28 => SignatureOnFileDate,
                29 => FirstSimilarIllnessDate,
                30 => PatientChargeAdjustmentCode,
                31 => RecurringServiceCode,
                32 => BillingMediaCode,
                33 => ExpectedSurgeryDateAndTime,
                34 => MilitaryPartnershipCode,
                35 => MilitaryNonAvailabilityCode,
                36 => NewbornBabyIndicator,
                37 => BabyDetainedIndicator,
                38 => ModeOfArrivalCode,
                39 => RecreationDrugUseCode,
                40 => AdmissionLevelOfCareCode,
                41 => PrecautionCode,
                42 => PatientConditionCode,
                43 => LivingWillCode,
                44 => OrganDonorCode,
                45 => AdvanceDirectiveCode,
                46 => PatientStatusEffectiveDate,
                47 => ExpectedLOAReturnDateTime,
                48 => ExpectedPreAdmissionTestingDateTime,
                49 => NotifyClergyCode,
                50 => AdvanceDirectiveLastVerifiedDate,
                _ => null
            };
        }
    }
}
