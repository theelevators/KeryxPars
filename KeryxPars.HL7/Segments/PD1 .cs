using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Additional Demographics
    /// </summary>
    public class PD1 : ISegment
    {
        public string SegmentId => nameof(PD1);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// PD1.1
        /// </summary>
        public string LivingDependency { get; set; }

        /// <summary>
        /// PD1.2
        /// </summary>
        public string LivingArrangement { get; set; }

        /// <summary>
        /// PD1.3
        /// </summary>
        public string PatientPrimaryFacility { get; set; }

        /// <summary>
        /// PD1.4
        /// </summary>
        public string PatientPrimaryCareProviderNameAndIDNo { get; set; }

        /// <summary>
        /// PD1.5
        /// </summary>
        public string StudentIndicator { get; set; }

        /// <summary>
        /// PD1.6
        /// </summary>
        public string Handicap { get; set; }

        /// <summary>
        /// PD1.7
        /// </summary>
        public string LivingWillCode { get; set; }

        /// <summary>
        /// PD1.8
        /// </summary>
        public string OrganDonorCode { get; set; }

        /// <summary>
        /// PD1.9
        /// </summary>
        public string SeparateBill { get; set; }

        /// <summary>
        /// PD1.10
        /// </summary>
        public string DuplicatePatient { get; set; }

        /// <summary>
        /// PD1.11
        /// </summary>
        public string PublicityCode { get; set; }

        /// <summary>
        /// PD1.12
        /// </summary>
        public string ProtectionIndicator { get; set; }

        /// <summary>
        /// PD1.13
        /// </summary>
        public string ProtectionIndicatorEffectiveDate { get; set; }

        /// <summary>
        /// PD1.14
        /// </summary>
        public string PlaceOfWorship { get; set; }

        /// <summary>
        /// PD1.15
        /// </summary>
        public string AdvanceDirectiveCode { get; set; }

        /// <summary>
        /// PD1.16
        /// </summary>
        public string ImmunizationRegistryStatus { get; set; }

        /// <summary>
        /// PD1.17
        /// </summary>
        public string ImmunizationRegistryStatusEffectiveDate { get; set; }

        /// <summary>
        /// PD1.18
        /// </summary>
        public string PublicityCodeEffectiveDate { get; set; }

        /// <summary>
        /// PD1.19
        /// </summary>
        public string MilitaryBranch { get; set; }

        /// <summary>
        /// PD1.20
        /// </summary>
        public string MilitaryRankGrade { get; set; }

        /// <summary>
        /// PD1.21
        /// </summary>
        public string MilitaryStatus { get; set; }

        /// <summary>
        /// PD1.22
        /// </summary>
        public string AdvanceDirectiveLastVerifiedDate { get; set; }

        // Constructors
        public PD1()
        {
            SegmentType = SegmentType.ADT;
            LivingDependency = string.Empty;
            LivingArrangement = string.Empty;
            PatientPrimaryFacility = string.Empty;
            PatientPrimaryCareProviderNameAndIDNo = string.Empty;
            StudentIndicator = string.Empty;
            Handicap = string.Empty;
            LivingWillCode = string.Empty;
            OrganDonorCode = string.Empty;
            SeparateBill = string.Empty;
            DuplicatePatient = string.Empty;
            PublicityCode = string.Empty;
            ProtectionIndicator = string.Empty;
            ProtectionIndicatorEffectiveDate = string.Empty;
            PlaceOfWorship = string.Empty;
            AdvanceDirectiveCode = string.Empty;
            ImmunizationRegistryStatus = string.Empty;
            ImmunizationRegistryStatusEffectiveDate = string.Empty;
            PublicityCodeEffectiveDate = string.Empty;
            MilitaryBranch = string.Empty;
            MilitaryRankGrade = string.Empty;
            MilitaryStatus = string.Empty;
            AdvanceDirectiveLastVerifiedDate = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: LivingDependency = value; break;
                case 2: LivingArrangement = value; break;
                case 3: PatientPrimaryFacility = value; break;
                case 4: PatientPrimaryCareProviderNameAndIDNo = value; break;
                case 5: StudentIndicator = value; break;
                case 6: Handicap = value; break;
                case 7: LivingWillCode = value; break;
                case 8: OrganDonorCode = value; break;
                case 9: SeparateBill = value; break;
                case 10: DuplicatePatient = value; break;
                case 11: PublicityCode = value; break;
                case 12: ProtectionIndicator = value; break;
                case 13: ProtectionIndicatorEffectiveDate = value; break;
                case 14: PlaceOfWorship = value; break;
                case 15: AdvanceDirectiveCode = value; break;
                case 16: ImmunizationRegistryStatus = value; break;
                case 17: ImmunizationRegistryStatusEffectiveDate = value; break;
                case 18: PublicityCodeEffectiveDate = value; break;
                case 19: MilitaryBranch = value; break;
                case 20: MilitaryRankGrade = value; break;
                case 21: MilitaryStatus = value; break;
                case 22: AdvanceDirectiveLastVerifiedDate = value; break;
                default: break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                LivingDependency,
                LivingArrangement,
                PatientPrimaryFacility,
                PatientPrimaryCareProviderNameAndIDNo,
                StudentIndicator,
                Handicap,
                LivingWillCode,
                OrganDonorCode,
                SeparateBill,
                DuplicatePatient,
                PublicityCode,
                ProtectionIndicator,
                ProtectionIndicatorEffectiveDate,
                PlaceOfWorship,
                AdvanceDirectiveCode,
                ImmunizationRegistryStatus,
                ImmunizationRegistryStatusEffectiveDate,
                PublicityCodeEffectiveDate,
                MilitaryBranch,
                MilitaryRankGrade,
                MilitaryStatus,
                AdvanceDirectiveLastVerifiedDate
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => LivingDependency,
                2 => LivingArrangement,
                3 => PatientPrimaryFacility,
                4 => PatientPrimaryCareProviderNameAndIDNo,
                5 => StudentIndicator,
                6 => Handicap,
                7 => LivingWillCode,
                8 => OrganDonorCode,
                9 => SeparateBill,
                10 => DuplicatePatient,
                11 => PublicityCode,
                12 => ProtectionIndicator,
                13 => ProtectionIndicatorEffectiveDate,
                14 => PlaceOfWorship,
                15 => AdvanceDirectiveCode,
                16 => ImmunizationRegistryStatus,
                17 => ImmunizationRegistryStatusEffectiveDate,
                18 => PublicityCodeEffectiveDate,
                19 => MilitaryBranch,
                20 => MilitaryRankGrade,
                21 => MilitaryStatus,
                22 => AdvanceDirectiveLastVerifiedDate,
                _ => null
            };
        }
    }
}
