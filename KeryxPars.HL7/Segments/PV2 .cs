using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Visit - Additional Information
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class PV2 : ISegment
    {
        public string SegmentId => nameof(PV2);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// PV2.1 - Prior Pending Location
        /// </summary>
        public PL PriorPendingLocation { get; set; }

        /// <summary>
        /// PV2.2 - Accommodation Code
        /// </summary>
        public CE AccomodationCode { get; set; }

        /// <summary>
        /// PV2.3 - Admit Reason
        /// </summary>
        public CE AdmitReason { get; set; }

        /// <summary>
        /// PV2.4 - Transfer Reason
        /// </summary>
        public CE TransferReason { get; set; }

        /// <summary>
        /// PV2.5 - Patient Valuables
        /// </summary>
        public ST[] PatientValuables { get; set; }

        /// <summary>
        /// PV2.6 - Patient Valuables Location
        /// </summary>
        public ST PatientValuablesLocation { get; set; }

        /// <summary>
        /// PV2.7 - Visit User Code
        /// </summary>
        public IS[] VisitUserCode { get; set; }

        /// <summary>
        /// PV2.8 - Expected Admit Date/Time
        /// </summary>
        public DTM ExpectedAdmitDateTime { get; set; }

        /// <summary>
        /// PV2.9 - Expected Discharge Date/Time
        /// </summary>
        public DTM ExpectedDischargeDateTime { get; set; }

        /// <summary>
        /// PV2.10 - Estimated Length of Inpatient Stay
        /// </summary>
        public NM EstimatedLengthOfInpatientStay { get; set; }

        /// <summary>
        /// PV2.11 - Actual Length of Inpatient Stay
        /// </summary>
        public NM ActualLengthOfInpatientStay { get; set; }

        /// <summary>
        /// PV2.12 - Visit Description
        /// </summary>
        public ST VisitDescription { get; set; }

        /// <summary>
        /// PV2.13 - Referral Source Code
        /// </summary>
        public XCN[] ReferralSourceCode { get; set; }

        /// <summary>
        /// PV2.14 - Previous Service Date
        /// </summary>
        public DT PreviousServiceDate { get; set; }

        /// <summary>
        /// PV2.15 - Employment Illness Related Indicator
        /// </summary>
        public ID EmploymentIllnessRelatedIndicator { get; set; }

        /// <summary>
        /// PV2.16 - Purge Status Code
        /// </summary>
        public IS PurgeStatusCode { get; set; }

        /// <summary>
        /// PV2.17 - Purge Status Date
        /// </summary>
        public DT PurgeStatusDate { get; set; }

        /// <summary>
        /// PV2.18 - Special Program Code
        /// </summary>
        public IS SpecialProgramCode { get; set; }

        /// <summary>
        /// PV2.19 - Retention Indicator
        /// </summary>
        public ID RetentionIndicator { get; set; }

        /// <summary>
        /// PV2.20 - Expected Number of Insurance Plans
        /// </summary>
        public NM ExpectedNumberOfInsurancePlans { get; set; }

        /// <summary>
        /// PV2.21 - Visit Publicity Code
        /// </summary>
        public IS VisitPublicityCode { get; set; }

        /// <summary>
        /// PV2.22 - Visit Protection Indicator
        /// </summary>
        public ID VisitProtectionIndicator { get; set; }

        /// <summary>
        /// PV2.23 - Clinic Organization Name
        /// </summary>
        public XON[] ClinicOrganizationName { get; set; }

        /// <summary>
        /// PV2.24 - Patient Status Code
        /// </summary>
        public IS PatientStatusCode { get; set; }

        /// <summary>
        /// PV2.25 - Visit Priority Code
        /// </summary>
        public IS VisitPriorityCode { get; set; }

        /// <summary>
        /// PV2.26 - Previous Treatment Date
        /// </summary>
        public DT PreviousTreatmentDate { get; set; }

        /// <summary>
        /// PV2.27 - Expected Discharge Disposition
        /// </summary>
        public IS ExpectedDischargeDisposition { get; set; }

        /// <summary>
        /// PV2.28 - Signature on File Date
        /// </summary>
        public DT SignatureOnFileDate { get; set; }

        /// <summary>
        /// PV2.29 - First Similar Illness Date
        /// </summary>
        public DT FirstSimilarIllnessDate { get; set; }

        /// <summary>
        /// PV2.30 - Patient Charge Adjustment Code
        /// </summary>
        public CE PatientChargeAdjustmentCode { get; set; }

        /// <summary>
        /// PV2.31 - Recurring Service Code
        /// </summary>
        public IS RecurringServiceCode { get; set; }

        /// <summary>
        /// PV2.32 - Billing Media Code
        /// </summary>
        public ID BillingMediaCode { get; set; }

        /// <summary>
        /// PV2.33 - Expected Surgery Date and Time
        /// </summary>
        public DTM ExpectedSurgeryDateAndTime { get; set; }

        /// <summary>
        /// PV2.34 - Military Partnership Code
        /// </summary>
        public ID MilitaryPartnershipCode { get; set; }

        /// <summary>
        /// PV2.35 - Military Non-Availability Code
        /// </summary>
        public ID MilitaryNonAvailabilityCode { get; set; }

        /// <summary>
        /// PV2.36 - Newborn Baby Indicator
        /// </summary>
        public ID NewbornBabyIndicator { get; set; }

        /// <summary>
        /// PV2.37 - Baby Detained Indicator
        /// </summary>
        public ID BabyDetainedIndicator { get; set; }

        /// <summary>
        /// PV2.38 - Mode of Arrival Code
        /// </summary>
        public CE ModeOfArrivalCode { get; set; }

        /// <summary>
        /// PV2.39 - Recreational Drug Use Code
        /// </summary>
        public CE[] RecreationDrugUseCode { get; set; }

        /// <summary>
        /// PV2.40 - Admission Level of Care Code
        /// </summary>
        public CE AdmissionLevelOfCareCode { get; set; }

        /// <summary>
        /// PV2.41 - Precaution Code
        /// </summary>
        public CE[] PrecautionCode { get; set; }

        /// <summary>
        /// PV2.42 - Patient Condition Code
        /// </summary>
        public CE PatientConditionCode { get; set; }

        /// <summary>
        /// PV2.43 - Living Will Code
        /// </summary>
        public IS LivingWillCode { get; set; }

        /// <summary>
        /// PV2.44 - Organ Donor Code
        /// </summary>
        public IS OrganDonorCode { get; set; }

        /// <summary>
        /// PV2.45 - Advance Directive Code
        /// </summary>
        public CE[] AdvanceDirectiveCode { get; set; }

        /// <summary>
        /// PV2.46 - Patient Status Effective Date
        /// </summary>
        public DT PatientStatusEffectiveDate { get; set; }

        /// <summary>
        /// PV2.47 - Expected LOA Return Date/Time
        /// </summary>
        public DTM ExpectedLOAReturnDateTime { get; set; }

        /// <summary>
        /// PV2.48 - Expected Pre-admission Testing Date/Time
        /// </summary>
        public DTM ExpectedPreAdmissionTestingDateTime { get; set; }

        /// <summary>
        /// PV2.49 - Notify Clergy Code
        /// </summary>
        public IS[] NotifyClergyCode { get; set; }

        /// <summary>
        /// PV2.50 - Advance Directive Last Verified Date
        /// </summary>
        public DT AdvanceDirectiveLastVerifiedDate { get; set; }

        public PV2()
        {
            SegmentType = SegmentType.ADT;
            PriorPendingLocation = default;
            AccomodationCode = default;
            AdmitReason = default;
            TransferReason = default;
            PatientValuables = [];
            PatientValuablesLocation = default;
            VisitUserCode = [];
            ExpectedAdmitDateTime = default;
            ExpectedDischargeDateTime = default;
            EstimatedLengthOfInpatientStay = default;
            ActualLengthOfInpatientStay = default;
            VisitDescription = default;
            ReferralSourceCode = [];
            PreviousServiceDate = default;
            EmploymentIllnessRelatedIndicator = default;
            PurgeStatusCode = default;
            PurgeStatusDate = default;
            SpecialProgramCode = default;
            RetentionIndicator = default;
            ExpectedNumberOfInsurancePlans = default;
            VisitPublicityCode = default;
            VisitProtectionIndicator = default;
            ClinicOrganizationName = [];
            PatientStatusCode = default;
            VisitPriorityCode = default;
            PreviousTreatmentDate = default;
            ExpectedDischargeDisposition = default;
            SignatureOnFileDate = default;
            FirstSimilarIllnessDate = default;
            PatientChargeAdjustmentCode = default;
            RecurringServiceCode = default;
            BillingMediaCode = default;
            ExpectedSurgeryDateAndTime = default;
            MilitaryPartnershipCode = default;
            MilitaryNonAvailabilityCode = default;
            NewbornBabyIndicator = default;
            BabyDetainedIndicator = default;
            ModeOfArrivalCode = default;
            RecreationDrugUseCode = [];
            AdmissionLevelOfCareCode = default;
            PrecautionCode = [];
            PatientConditionCode = default;
            LivingWillCode = default;
            OrganDonorCode = default;
            AdvanceDirectiveCode = [];
            PatientStatusEffectiveDate = default;
            ExpectedLOAReturnDateTime = default;
            ExpectedPreAdmissionTestingDateTime = default;
            NotifyClergyCode = [];
            AdvanceDirectiveLastVerifiedDate = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var pl1 = new PL();
                    pl1.Parse(value.AsSpan(), delimiters);
                    PriorPendingLocation = pl1;
                    break;
                case 2:
                    var ce2 = new CE();
                    ce2.Parse(value.AsSpan(), delimiters);
                    AccomodationCode = ce2;
                    break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    AdmitReason = ce3;
                    break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    TransferReason = ce4;
                    break;
                case 5:
                    PatientValuables = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters);
                    break;
                case 6: PatientValuablesLocation = new ST(value); break;
                case 7:
                    VisitUserCode = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters);
                    break;
                case 8: ExpectedAdmitDateTime = new DTM(value); break;
                case 9: ExpectedDischargeDateTime = new DTM(value); break;
                case 10: EstimatedLengthOfInpatientStay = new NM(value); break;
                case 11: ActualLengthOfInpatientStay = new NM(value); break;
                case 12: VisitDescription = new ST(value); break;
                case 13:
                    ReferralSourceCode = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters);
                    break;
                case 14: PreviousServiceDate = new DT(value); break;
                case 15: EmploymentIllnessRelatedIndicator = new ID(value); break;
                case 16: PurgeStatusCode = new IS(value); break;
                case 17: PurgeStatusDate = new DT(value); break;
                case 18: SpecialProgramCode = new IS(value); break;
                case 19: RetentionIndicator = new ID(value); break;
                case 20: ExpectedNumberOfInsurancePlans = new NM(value); break;
                case 21: VisitPublicityCode = new IS(value); break;
                case 22: VisitProtectionIndicator = new ID(value); break;
                case 23:
                    ClinicOrganizationName = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters);
                    break;
                case 24: PatientStatusCode = new IS(value); break;
                case 25: VisitPriorityCode = new IS(value); break;
                case 26: PreviousTreatmentDate = new DT(value); break;
                case 27: ExpectedDischargeDisposition = new IS(value); break;
                case 28: SignatureOnFileDate = new DT(value); break;
                case 29: FirstSimilarIllnessDate = new DT(value); break;
                case 30:
                    var ce30 = new CE();
                    ce30.Parse(value.AsSpan(), delimiters);
                    PatientChargeAdjustmentCode = ce30;
                    break;
                case 31: RecurringServiceCode = new IS(value); break;
                case 32: BillingMediaCode = new ID(value); break;
                case 33: ExpectedSurgeryDateAndTime = new DTM(value); break;
                case 34: MilitaryPartnershipCode = new ID(value); break;
                case 35: MilitaryNonAvailabilityCode = new ID(value); break;
                case 36: NewbornBabyIndicator = new ID(value); break;
                case 37: BabyDetainedIndicator = new ID(value); break;
                case 38:
                    var ce38 = new CE();
                    ce38.Parse(value.AsSpan(), delimiters);
                    ModeOfArrivalCode = ce38;
                    break;
                case 39:
                    RecreationDrugUseCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 40:
                    var ce40 = new CE();
                    ce40.Parse(value.AsSpan(), delimiters);
                    AdmissionLevelOfCareCode = ce40;
                    break;
                case 41:
                    PrecautionCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 42:
                    var ce42 = new CE();
                    ce42.Parse(value.AsSpan(), delimiters);
                    PatientConditionCode = ce42;
                    break;
                case 43: LivingWillCode = new IS(value); break;
                case 44: OrganDonorCode = new IS(value); break;
                case 45:
                    AdvanceDirectiveCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 46: PatientStatusEffectiveDate = new DT(value); break;
                case 47: ExpectedLOAReturnDateTime = new DTM(value); break;
                case 48: ExpectedPreAdmissionTestingDateTime = new DTM(value); break;
                case 49:
                    NotifyClergyCode = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters);
                    break;
                case 50: AdvanceDirectiveLastVerifiedDate = new DT(value); break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                PriorPendingLocation.ToHL7String(delimiters),
                AccomodationCode.ToHL7String(delimiters),
                AdmitReason.ToHL7String(delimiters),
                TransferReason.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientValuables, delimiters),
                PatientValuablesLocation.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(VisitUserCode, delimiters),
                ExpectedAdmitDateTime.ToHL7String(delimiters),
                ExpectedDischargeDateTime.ToHL7String(delimiters),
                EstimatedLengthOfInpatientStay.ToHL7String(delimiters),
                ActualLengthOfInpatientStay.ToHL7String(delimiters),
                VisitDescription.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ReferralSourceCode, delimiters),
                PreviousServiceDate.ToHL7String(delimiters),
                EmploymentIllnessRelatedIndicator.ToHL7String(delimiters),
                PurgeStatusCode.ToHL7String(delimiters),
                PurgeStatusDate.ToHL7String(delimiters),
                SpecialProgramCode.ToHL7String(delimiters),
                RetentionIndicator.ToHL7String(delimiters),
                ExpectedNumberOfInsurancePlans.ToHL7String(delimiters),
                VisitPublicityCode.ToHL7String(delimiters),
                VisitProtectionIndicator.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ClinicOrganizationName, delimiters),
                PatientStatusCode.ToHL7String(delimiters),
                VisitPriorityCode.ToHL7String(delimiters),
                PreviousTreatmentDate.ToHL7String(delimiters),
                ExpectedDischargeDisposition.ToHL7String(delimiters),
                SignatureOnFileDate.ToHL7String(delimiters),
                FirstSimilarIllnessDate.ToHL7String(delimiters),
                PatientChargeAdjustmentCode.ToHL7String(delimiters),
                RecurringServiceCode.ToHL7String(delimiters),
                BillingMediaCode.ToHL7String(delimiters),
                ExpectedSurgeryDateAndTime.ToHL7String(delimiters),
                MilitaryPartnershipCode.ToHL7String(delimiters),
                MilitaryNonAvailabilityCode.ToHL7String(delimiters),
                NewbornBabyIndicator.ToHL7String(delimiters),
                BabyDetainedIndicator.ToHL7String(delimiters),
                ModeOfArrivalCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(RecreationDrugUseCode, delimiters),
                AdmissionLevelOfCareCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PrecautionCode, delimiters),
                PatientConditionCode.ToHL7String(delimiters),
                LivingWillCode.ToHL7String(delimiters),
                OrganDonorCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(AdvanceDirectiveCode, delimiters),
                PatientStatusEffectiveDate.ToHL7String(delimiters),
                ExpectedLOAReturnDateTime.ToHL7String(delimiters),
                ExpectedPreAdmissionTestingDateTime.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(NotifyClergyCode, delimiters),
                AdvanceDirectiveLastVerifiedDate.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => PriorPendingLocation.ToHL7String(delimiters),
                2 => AccomodationCode.ToHL7String(delimiters),
                3 => AdmitReason.ToHL7String(delimiters),
                4 => TransferReason.ToHL7String(delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(PatientValuables, delimiters),
                6 => PatientValuablesLocation.Value,
                7 => SegmentFieldHelper.JoinRepeatingField(VisitUserCode, delimiters),
                8 => ExpectedAdmitDateTime.Value,
                9 => ExpectedDischargeDateTime.Value,
                10 => EstimatedLengthOfInpatientStay.Value,
                11 => ActualLengthOfInpatientStay.Value,
                12 => VisitDescription.Value,
                13 => SegmentFieldHelper.JoinRepeatingField(ReferralSourceCode, delimiters),
                14 => PreviousServiceDate.Value,
                15 => EmploymentIllnessRelatedIndicator.Value,
                16 => PurgeStatusCode.Value,
                17 => PurgeStatusDate.Value,
                18 => SpecialProgramCode.Value,
                19 => RetentionIndicator.Value,
                20 => ExpectedNumberOfInsurancePlans.Value,
                21 => VisitPublicityCode.Value,
                22 => VisitProtectionIndicator.Value,
                23 => SegmentFieldHelper.JoinRepeatingField(ClinicOrganizationName, delimiters),
                24 => PatientStatusCode.Value,
                25 => VisitPriorityCode.Value,
                26 => PreviousTreatmentDate.Value,
                27 => ExpectedDischargeDisposition.Value,
                28 => SignatureOnFileDate.Value,
                29 => FirstSimilarIllnessDate.Value,
                30 => PatientChargeAdjustmentCode.ToHL7String(delimiters),
                31 => RecurringServiceCode.Value,
                32 => BillingMediaCode.Value,
                33 => ExpectedSurgeryDateAndTime.Value,
                34 => MilitaryPartnershipCode.Value,
                35 => MilitaryNonAvailabilityCode.Value,
                36 => NewbornBabyIndicator.Value,
                37 => BabyDetainedIndicator.Value,
                38 => ModeOfArrivalCode.ToHL7String(delimiters),
                39 => SegmentFieldHelper.JoinRepeatingField(RecreationDrugUseCode, delimiters),
                40 => AdmissionLevelOfCareCode.ToHL7String(delimiters),
                41 => SegmentFieldHelper.JoinRepeatingField(PrecautionCode, delimiters),
                42 => PatientConditionCode.ToHL7String(delimiters),
                43 => LivingWillCode.Value,
                44 => OrganDonorCode.Value,
                45 => SegmentFieldHelper.JoinRepeatingField(AdvanceDirectiveCode, delimiters),
                46 => PatientStatusEffectiveDate.Value,
                47 => ExpectedLOAReturnDateTime.Value,
                48 => ExpectedPreAdmissionTestingDateTime.Value,
                49 => SegmentFieldHelper.JoinRepeatingField(NotifyClergyCode, delimiters),
                50 => AdvanceDirectiveLastVerifiedDate.Value,
                _ => null
            };
        }
    }
}
