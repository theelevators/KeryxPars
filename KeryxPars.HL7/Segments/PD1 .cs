using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Additional Demographics
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class PD1 : ISegment
    {
        public string SegmentId => nameof(PD1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// PD1.1 - Living Dependency (repeating)
        /// </summary>
        public IS[] LivingDependency { get; set; }

        /// <summary>
        /// PD1.2 - Living Arrangement
        /// </summary>
        public IS LivingArrangement { get; set; }

        /// <summary>
        /// PD1.3 - Patient Primary Facility (repeating)
        /// </summary>
        public XON[] PatientPrimaryFacility { get; set; }

        /// <summary>
        /// PD1.4 - Patient Primary Care Provider Name & ID No. (repeating)
        /// </summary>
        public XCN[] PatientPrimaryCareProviderNameAndIDNo { get; set; }

        /// <summary>
        /// PD1.5 - Student Indicator
        /// </summary>
        public IS StudentIndicator { get; set; }

        /// <summary>
        /// PD1.6 - Handicap
        /// </summary>
        public IS Handicap { get; set; }

        /// <summary>
        /// PD1.7 - Living Will Code
        /// </summary>
        public IS LivingWillCode { get; set; }

        /// <summary>
        /// PD1.8 - Organ Donor Code
        /// </summary>
        public IS OrganDonorCode { get; set; }

        /// <summary>
        /// PD1.9 - Separate Bill
        /// </summary>
        public ID SeparateBill { get; set; }

        /// <summary>
        /// PD1.10 - Duplicate Patient (repeating)
        /// </summary>
        public CX[] DuplicatePatient { get; set; }

        /// <summary>
        /// PD1.11 - Publicity Code
        /// </summary>
        public CE PublicityCode { get; set; }

        /// <summary>
        /// PD1.12 - Protection Indicator
        /// </summary>
        public ID ProtectionIndicator { get; set; }

        /// <summary>
        /// PD1.13 - Protection Indicator Effective Date
        /// </summary>
        public DT ProtectionIndicatorEffectiveDate { get; set; }

        /// <summary>
        /// PD1.14 - Place of Worship (repeating)
        /// </summary>
        public XON[] PlaceOfWorship { get; set; }

        /// <summary>
        /// PD1.15 - Advance Directive Code (repeating)
        /// </summary>
        public CE[] AdvanceDirectiveCode { get; set; }

        /// <summary>
        /// PD1.16 - Immunization Registry Status
        /// </summary>
        public IS ImmunizationRegistryStatus { get; set; }

        /// <summary>
        /// PD1.17 - Immunization Registry Status Effective Date
        /// </summary>
        public DT ImmunizationRegistryStatusEffectiveDate { get; set; }

        /// <summary>
        /// PD1.18 - Publicity Code Effective Date
        /// </summary>
        public DT PublicityCodeEffectiveDate { get; set; }

        /// <summary>
        /// PD1.19 - Military Branch
        /// </summary>
        public IS MilitaryBranch { get; set; }

        /// <summary>
        /// PD1.20 - Military Rank/Grade
        /// </summary>
        public IS MilitaryRankGrade { get; set; }

        /// <summary>
        /// PD1.21 - Military Status
        /// </summary>
        public IS MilitaryStatus { get; set; }

        /// <summary>
        /// PD1.22 - Advance Directive Last Verified Date
        /// </summary>
        public DT AdvanceDirectiveLastVerifiedDate { get; set; }

        public PD1()
        {
            SegmentType = SegmentType.ADT;
            
            LivingArrangement = default;
            StudentIndicator = default;
            Handicap = default;
            LivingWillCode = default;
            OrganDonorCode = default;
            SeparateBill = default;
            PublicityCode = default;
            ProtectionIndicator = default;
            ProtectionIndicatorEffectiveDate = default;
            ImmunizationRegistryStatus = default;
            ImmunizationRegistryStatusEffectiveDate = default;
            PublicityCodeEffectiveDate = default;
            MilitaryBranch = default;
            MilitaryRankGrade = default;
            MilitaryStatus = default;
            AdvanceDirectiveLastVerifiedDate = default;
            
            LivingDependency = Array.Empty<IS>();
            PatientPrimaryFacility = Array.Empty<XON>();
            PatientPrimaryCareProviderNameAndIDNo = Array.Empty<XCN>();
            DuplicatePatient = Array.Empty<CX>();
            PlaceOfWorship = Array.Empty<XON>();
            AdvanceDirectiveCode = Array.Empty<CE>();
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: LivingDependency = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters); break;
                case 2: LivingArrangement = new IS(value); break;
                case 3: PatientPrimaryFacility = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters); break;
                case 4: PatientPrimaryCareProviderNameAndIDNo = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 5: StudentIndicator = new IS(value); break;
                case 6: Handicap = new IS(value); break;
                case 7: LivingWillCode = new IS(value); break;
                case 8: OrganDonorCode = new IS(value); break;
                case 9: SeparateBill = new ID(value); break;
                case 10: DuplicatePatient = SegmentFieldHelper.ParseRepeatingField<CX>(value, delimiters); break;
                case 11: PublicityCode = value; break;
                case 12: ProtectionIndicator = new ID(value); break;
                case 13: ProtectionIndicatorEffectiveDate = new DT(value); break;
                case 14: PlaceOfWorship = SegmentFieldHelper.ParseRepeatingField<XON>(value, delimiters); break;
                case 15: AdvanceDirectiveCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 16: ImmunizationRegistryStatus = new IS(value); break;
                case 17: ImmunizationRegistryStatusEffectiveDate = new DT(value); break;
                case 18: PublicityCodeEffectiveDate = new DT(value); break;
                case 19: MilitaryBranch = new IS(value); break;
                case 20: MilitaryRankGrade = new IS(value); break;
                case 21: MilitaryStatus = new IS(value); break;
                case 22: AdvanceDirectiveLastVerifiedDate = new DT(value); break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SegmentFieldHelper.JoinRepeatingField(LivingDependency, delimiters),
                LivingArrangement.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientPrimaryFacility, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PatientPrimaryCareProviderNameAndIDNo, delimiters),
                StudentIndicator.ToHL7String(delimiters),
                Handicap.ToHL7String(delimiters),
                LivingWillCode.ToHL7String(delimiters),
                OrganDonorCode.ToHL7String(delimiters),
                SeparateBill.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(DuplicatePatient, delimiters),
                PublicityCode.ToHL7String(delimiters),
                ProtectionIndicator.ToHL7String(delimiters),
                ProtectionIndicatorEffectiveDate.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(PlaceOfWorship, delimiters),
                SegmentFieldHelper.JoinRepeatingField(AdvanceDirectiveCode, delimiters),
                ImmunizationRegistryStatus.ToHL7String(delimiters),
                ImmunizationRegistryStatusEffectiveDate.ToHL7String(delimiters),
                PublicityCodeEffectiveDate.ToHL7String(delimiters),
                MilitaryBranch.ToHL7String(delimiters),
                MilitaryRankGrade.ToHL7String(delimiters),
                MilitaryStatus.ToHL7String(delimiters),
                AdvanceDirectiveLastVerifiedDate.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var values = GetValues();
            return index >= 0 && index < values.Length ? values[index] : null;
        }
    }
}
