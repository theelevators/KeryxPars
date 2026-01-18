using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Specimen
    /// </summary>
    public class SPM : ISegment
    {
        public string SegmentId => nameof(SPM);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// SPM.1 - Set ID - SPM
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// SPM.2 - Specimen ID
        /// </summary>
        public EI SpecimenID { get; set; }

        /// <summary>
        /// SPM.3 - Specimen Parent IDs (repeating)
        /// </summary>
        public EI[] SpecimenParentIDs { get; set; }

        /// <summary>
        /// SPM.4 - Specimen Type
        /// </summary>
        public CWE SpecimenType { get; set; }

        /// <summary>
        /// SPM.5 - Specimen Type Modifier (repeating)
        /// </summary>
        public CWE[] SpecimenTypeModifier { get; set; }

        /// <summary>
        /// SPM.6 - Specimen Additives (repeating)
        /// </summary>
        public CWE[] SpecimenAdditives { get; set; }

        /// <summary>
        /// SPM.7 - Specimen Collection Method
        /// </summary>
        public CWE SpecimenCollectionMethod { get; set; }

        /// <summary>
        /// SPM.8 - Specimen Source Site
        /// </summary>
        public CWE SpecimenSourceSite { get; set; }

        /// <summary>
        /// SPM.9 - Specimen Source Site Modifier (repeating)
        /// </summary>
        public CWE[] SpecimenSourceSiteModifier { get; set; }

        /// <summary>
        /// SPM.10 - Specimen Collection Site
        /// </summary>
        public CWE SpecimenCollectionSite { get; set; }

        /// <summary>
        /// SPM.11 - Specimen Role (repeating)
        /// </summary>
        public CWE[] SpecimenRole { get; set; }

        /// <summary>
        /// SPM.12 - Specimen Collection Amount
        /// </summary>
        public CQ SpecimenCollectionAmount { get; set; }

        /// <summary>
        /// SPM.13 - Grouped Specimen Count
        /// </summary>
        public NM GroupedSpecimenCount { get; set; }

        /// <summary>
        /// SPM.14 - Specimen Description (repeating)
        /// </summary>
        public ST[] SpecimenDescription { get; set; }

        /// <summary>
        /// SPM.15 - Specimen Handling Code (repeating)
        /// </summary>
        public CWE[] SpecimenHandlingCode { get; set; }

        /// <summary>
        /// SPM.16 - Specimen Risk Code (repeating)
        /// </summary>
        public CWE[] SpecimenRiskCode { get; set; }

        /// <summary>
        /// SPM.17 - Specimen Collection Date/Time
        /// </summary>
        public DR SpecimenCollectionDateTime { get; set; }

        /// <summary>
        /// SPM.18 - Specimen Received Date/Time
        /// </summary>
        public DTM SpecimenReceivedDateTime { get; set; }

        /// <summary>
        /// SPM.19 - Specimen Expiration Date/Time
        /// </summary>
        public DTM SpecimenExpirationDateTime { get; set; }

        /// <summary>
        /// SPM.20 - Specimen Availability
        /// </summary>
        public ID SpecimenAvailability { get; set; }

        /// <summary>
        /// SPM.21 - Specimen Reject Reason (repeating)
        /// </summary>
        public CWE[] SpecimenRejectReason { get; set; }

        /// <summary>
        /// SPM.22 - Specimen Quality
        /// </summary>
        public CWE SpecimenQuality { get; set; }

        /// <summary>
        /// SPM.23 - Specimen Appropriateness
        /// </summary>
        public CWE SpecimenAppropriateness { get; set; }

        /// <summary>
        /// SPM.24 - Specimen Condition (repeating)
        /// </summary>
        public CWE[] SpecimenCondition { get; set; }

        /// <summary>
        /// SPM.25 - Specimen Current Quantity
        /// </summary>
        public CQ SpecimenCurrentQuantity { get; set; }

        /// <summary>
        /// SPM.26 - Number of Specimen Containers
        /// </summary>
        public NM NumberOfSpecimenContainers { get; set; }

        /// <summary>
        /// SPM.27 - Container Type
        /// </summary>
        public CWE ContainerType { get; set; }

        /// <summary>
        /// SPM.28 - Container Condition
        /// </summary>
        public CWE ContainerCondition { get; set; }

        /// <summary>
        /// SPM.29 - Specimen Child Role
        /// </summary>
        public CWE SpecimenChildRole { get; set; }

        public SPM()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            SpecimenID = default;
            SpecimenParentIDs = [];
            SpecimenType = default;
            SpecimenTypeModifier = [];
            SpecimenAdditives = [];
            SpecimenCollectionMethod = default;
            SpecimenSourceSite = default;
            SpecimenSourceSiteModifier = [];
            SpecimenCollectionSite = default;
            SpecimenRole = [];
            SpecimenCollectionAmount = default;
            GroupedSpecimenCount = default;
            SpecimenDescription = [];
            SpecimenHandlingCode = [];
            SpecimenRiskCode = [];
            SpecimenCollectionDateTime = default;
            SpecimenReceivedDateTime = default;
            SpecimenExpirationDateTime = default;
            SpecimenAvailability = default;
            SpecimenRejectReason = [];
            SpecimenQuality = default;
            SpecimenAppropriateness = default;
            SpecimenCondition = [];
            SpecimenCurrentQuantity = default;
            NumberOfSpecimenContainers = default;
            ContainerType = default;
            ContainerCondition = default;
            SpecimenChildRole = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2:
                    var ei2 = new EI();
                    ei2.Parse(value.AsSpan(), delimiters);
                    SpecimenID = ei2;
                    break;
                case 3: SpecimenParentIDs = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters); break;
                case 4:
                    var cwe4 = new CWE();
                    cwe4.Parse(value.AsSpan(), delimiters);
                    SpecimenType = cwe4;
                    break;
                case 5: SpecimenTypeModifier = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 6: SpecimenAdditives = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 7:
                    var cwe7 = new CWE();
                    cwe7.Parse(value.AsSpan(), delimiters);
                    SpecimenCollectionMethod = cwe7;
                    break;
                case 8:
                    var cwe8 = new CWE();
                    cwe8.Parse(value.AsSpan(), delimiters);
                    SpecimenSourceSite = cwe8;
                    break;
                case 9: SpecimenSourceSiteModifier = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 10:
                    var cwe10 = new CWE();
                    cwe10.Parse(value.AsSpan(), delimiters);
                    SpecimenCollectionSite = cwe10;
                    break;
                case 11: SpecimenRole = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 12:
                    var cq12 = new CQ();
                    cq12.Parse(value.AsSpan(), delimiters);
                    SpecimenCollectionAmount = cq12;
                    break;
                case 13: GroupedSpecimenCount = new NM(value); break;
                case 14: SpecimenDescription = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 15: SpecimenHandlingCode = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 16: SpecimenRiskCode = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 17:
                    var dr17 = new DR();
                    dr17.Parse(value.AsSpan(), delimiters);
                    SpecimenCollectionDateTime = dr17;
                    break;
                case 18: SpecimenReceivedDateTime = new DTM(value); break;
                case 19: SpecimenExpirationDateTime = new DTM(value); break;
                case 20: SpecimenAvailability = new ID(value); break;
                case 21: SpecimenRejectReason = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 22:
                    var cwe22 = new CWE();
                    cwe22.Parse(value.AsSpan(), delimiters);
                    SpecimenQuality = cwe22;
                    break;
                case 23:
                    var cwe23 = new CWE();
                    cwe23.Parse(value.AsSpan(), delimiters);
                    SpecimenAppropriateness = cwe23;
                    break;
                case 24: SpecimenCondition = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters); break;
                case 25:
                    var cq25 = new CQ();
                    cq25.Parse(value.AsSpan(), delimiters);
                    SpecimenCurrentQuantity = cq25;
                    break;
                case 26: NumberOfSpecimenContainers = new NM(value); break;
                case 27:
                    var cwe27 = new CWE();
                    cwe27.Parse(value.AsSpan(), delimiters);
                    ContainerType = cwe27;
                    break;
                case 28:
                    var cwe28 = new CWE();
                    cwe28.Parse(value.AsSpan(), delimiters);
                    ContainerCondition = cwe28;
                    break;
                case 29:
                    var cwe29 = new CWE();
                    cwe29.Parse(value.AsSpan(), delimiters);
                    SpecimenChildRole = cwe29;
                    break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                SpecimenID.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenParentIDs, delimiters),
                SpecimenType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenTypeModifier, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenAdditives, delimiters),
                SpecimenCollectionMethod.ToHL7String(delimiters),
                SpecimenSourceSite.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenSourceSiteModifier, delimiters),
                SpecimenCollectionSite.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenRole, delimiters),
                SpecimenCollectionAmount.ToHL7String(delimiters),
                GroupedSpecimenCount.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenDescription, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenHandlingCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenRiskCode, delimiters),
                SpecimenCollectionDateTime.ToHL7String(delimiters),
                SpecimenReceivedDateTime.ToHL7String(delimiters),
                SpecimenExpirationDateTime.ToHL7String(delimiters),
                SpecimenAvailability.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenRejectReason, delimiters),
                SpecimenQuality.ToHL7String(delimiters),
                SpecimenAppropriateness.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SpecimenCondition, delimiters),
                SpecimenCurrentQuantity.ToHL7String(delimiters),
                NumberOfSpecimenContainers.ToHL7String(delimiters),
                ContainerType.ToHL7String(delimiters),
                ContainerCondition.ToHL7String(delimiters),
                SpecimenChildRole.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => SpecimenID.ToHL7String(delimiters),
                3 => SegmentFieldHelper.JoinRepeatingField(SpecimenParentIDs, delimiters),
                4 => SpecimenType.ToHL7String(delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(SpecimenTypeModifier, delimiters),
                6 => SegmentFieldHelper.JoinRepeatingField(SpecimenAdditives, delimiters),
                7 => SpecimenCollectionMethod.ToHL7String(delimiters),
                8 => SpecimenSourceSite.ToHL7String(delimiters),
                9 => SegmentFieldHelper.JoinRepeatingField(SpecimenSourceSiteModifier, delimiters),
                10 => SpecimenCollectionSite.ToHL7String(delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(SpecimenRole, delimiters),
                12 => SpecimenCollectionAmount.ToHL7String(delimiters),
                13 => GroupedSpecimenCount.Value,
                14 => SegmentFieldHelper.JoinRepeatingField(SpecimenDescription, delimiters),
                15 => SegmentFieldHelper.JoinRepeatingField(SpecimenHandlingCode, delimiters),
                16 => SegmentFieldHelper.JoinRepeatingField(SpecimenRiskCode, delimiters),
                17 => SpecimenCollectionDateTime.ToHL7String(delimiters),
                18 => SpecimenReceivedDateTime.Value,
                19 => SpecimenExpirationDateTime.Value,
                20 => SpecimenAvailability.Value,
                21 => SegmentFieldHelper.JoinRepeatingField(SpecimenRejectReason, delimiters),
                22 => SpecimenQuality.ToHL7String(delimiters),
                23 => SpecimenAppropriateness.ToHL7String(delimiters),
                24 => SegmentFieldHelper.JoinRepeatingField(SpecimenCondition, delimiters),
                25 => SpecimenCurrentQuantity.ToHL7String(delimiters),
                26 => NumberOfSpecimenContainers.Value,
                27 => ContainerType.ToHL7String(delimiters),
                28 => ContainerCondition.ToHL7String(delimiters),
                29 => SpecimenChildRole.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
