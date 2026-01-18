using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Observation/Result
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class OBX : ISegment
    {
        public string SegmentId => nameof(OBX);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// OBX.1 - Set ID - OBX
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// OBX.2 - Value Type
        /// </summary>
        public ID ValueType { get; set; }

        /// <summary>
        /// OBX.3 - Observation Identifier
        /// </summary>
        public CE ObservationIdentifier { get; set; }

        /// <summary>
        /// OBX.4 - Observation Sub-ID
        /// </summary>
        public ST ObservationSubID { get; set; }

        /// <summary>
        /// OBX.5 - Observation Value
        /// Note: Type varies based on ValueType field (polymorphic)
        /// </summary>
        public ST[] ObservationValue { get; set; }

        /// <summary>
        /// OBX.6 - Units
        /// </summary>
        public CE Units { get; set; }

        /// <summary>
        /// OBX.7 - References Range
        /// </summary>
        public ST ReferencesRange { get; set; }

        /// <summary>
        /// OBX.8 - Interpretation Codes
        /// </summary>
        public IS[] InterpretationCodes { get; set; }

        /// <summary>
        /// OBX.9 - Probability
        /// </summary>
        public NM Probability { get; set; }

        /// <summary>
        /// OBX.10 - Nature of Abnormal Test
        /// </summary>
        public ID[] NatureOfAbnormalTest { get; set; }

        /// <summary>
        /// OBX.11 - Observation Result Status
        /// </summary>
        public ID ObservationResultStatus { get; set; }

        /// <summary>
        /// OBX.12 - Effective Date of Reference Range
        /// </summary>
        public DTM EffectiveDateOfReferenceRange { get; set; }

        /// <summary>
        /// OBX.13 - User Defined Access Checks
        /// </summary>
        public ST UserDefinedAccessChecks { get; set; }

        /// <summary>
        /// OBX.14 - Date/Time of Observation
        /// </summary>
        public DTM DateTimeOfObservation { get; set; }

        /// <summary>
        /// OBX.15 - Producer's ID
        /// </summary>
        public CE ProducerID { get; set; }

        /// <summary>
        /// OBX.16 - Responsible Observer
        /// </summary>
        public XCN[] ResponsibleObserver { get; set; }

        /// <summary>
        /// OBX.17 - Observation Method
        /// </summary>
        public CE[] OvservationMethod { get; set; }

        /// <summary>
        /// OBX.18 - Equipment Instance Identifier
        /// </summary>
        public EI[] EquipmentInstanceIdentifier { get; set; }

        /// <summary>
        /// OBX.19 - Date/Time of Analysis
        /// </summary>
        public DTM DateTimeOfAnalysis { get; set; }

        /// <summary>
        /// OBX.20 - Observation Site
        /// </summary>
        public CWE[] ObservationSite { get; set; }

        /// <summary>
        /// OBX.21 - Observation Instance Identifier
        /// </summary>
        public EI ObservationInstanceIdentifier { get; set; }

        /// <summary>
        /// OBX.22 - Mood Code
        /// </summary>
        public ID MoodCode { get; set; }

        /// <summary>
        /// OBX.23 - Performing Organization Name
        /// </summary>
        public XON PerformingOrganizationName { get; set; }

        /// <summary>
        /// OBX.24 - Performing Organization Address
        /// </summary>
        public XAD PerformingOrganizationAddress { get; set; }

        /// <summary>
        /// OBX.25 - Performing Organization Medical Director
        /// </summary>
        public XCN PerformingOrganizationMedicalDirector { get; set; }

        /// <summary>
        /// OBX.26 - Patient Results Release Category
        /// </summary>
        public ID PatientResultsReleaseCategory { get; set; }

        public OBX()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = default;
            ValueType = default;
            ObservationIdentifier = default;
            ObservationSubID = default;
            ObservationValue = [];
            Units = default;
            ReferencesRange = default;
            InterpretationCodes = [];
            Probability = default;
            NatureOfAbnormalTest = [];
            ObservationResultStatus = default;
            EffectiveDateOfReferenceRange = default;
            UserDefinedAccessChecks = default;
            DateTimeOfObservation = default;
            ProducerID = default;
            ResponsibleObserver = [];
            OvservationMethod = [];
            EquipmentInstanceIdentifier = [];
            DateTimeOfAnalysis = default;
            ObservationSite = [];
            ObservationInstanceIdentifier = default;
            MoodCode = default;
            PerformingOrganizationName = default;
            PerformingOrganizationAddress = default;
            PerformingOrganizationMedicalDirector = default;
            PatientResultsReleaseCategory = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: ValueType = new ID(value); break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    ObservationIdentifier = ce3;
                    break;
                case 4: ObservationSubID = new ST(value); break;
                case 5:
                    ObservationValue = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters);
                    break;
                case 6:
                    var ce6 = new CE();
                    ce6.Parse(value.AsSpan(), delimiters);
                    Units = ce6;
                    break;
                case 7: ReferencesRange = new ST(value); break;
                case 8:
                    InterpretationCodes = SegmentFieldHelper.ParseRepeatingField<IS>(value, delimiters);
                    break;
                case 9: Probability = new NM(value); break;
                case 10:
                    NatureOfAbnormalTest = SegmentFieldHelper.ParseRepeatingField<ID>(value, delimiters);
                    break;
                case 11: ObservationResultStatus = new ID(value); break;
                case 12: EffectiveDateOfReferenceRange = new DTM(value); break;
                case 13: UserDefinedAccessChecks = new ST(value); break;
                case 14: DateTimeOfObservation = new DTM(value); break;
                case 15:
                    var ce15 = new CE();
                    ce15.Parse(value.AsSpan(), delimiters);
                    ProducerID = ce15;
                    break;
                case 16:
                    ResponsibleObserver = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters);
                    break;
                case 17:
                    OvservationMethod = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 18:
                    EquipmentInstanceIdentifier = SegmentFieldHelper.ParseRepeatingField<EI>(value, delimiters);
                    break;
                case 19: DateTimeOfAnalysis = new DTM(value); break;
                case 20:
                    ObservationSite = SegmentFieldHelper.ParseRepeatingField<CWE>(value, delimiters);
                    break;
                case 21:
                    var ei21 = new EI();
                    ei21.Parse(value.AsSpan(), delimiters);
                    ObservationInstanceIdentifier = ei21;
                    break;
                case 22: MoodCode = new ID(value); break;
                case 23:
                    var xon23 = new XON();
                    xon23.Parse(value.AsSpan(), delimiters);
                    PerformingOrganizationName = xon23;
                    break;
                case 24:
                    var xad24 = new XAD();
                    xad24.Parse(value.AsSpan(), delimiters);
                    PerformingOrganizationAddress = xad24;
                    break;
                case 25:
                    var xcn25 = new XCN();
                    xcn25.Parse(value.AsSpan(), delimiters);
                    PerformingOrganizationMedicalDirector = xcn25;
                    break;
                case 26: PatientResultsReleaseCategory = new ID(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                ValueType.ToHL7String(delimiters),
                ObservationIdentifier.ToHL7String(delimiters),
                ObservationSubID.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ObservationValue, delimiters),
                Units.ToHL7String(delimiters),
                ReferencesRange.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(InterpretationCodes, delimiters),
                Probability.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(NatureOfAbnormalTest, delimiters),
                ObservationResultStatus.ToHL7String(delimiters),
                EffectiveDateOfReferenceRange.ToHL7String(delimiters),
                UserDefinedAccessChecks.ToHL7String(delimiters),
                DateTimeOfObservation.ToHL7String(delimiters),
                ProducerID.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ResponsibleObserver, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OvservationMethod, delimiters),
                SegmentFieldHelper.JoinRepeatingField(EquipmentInstanceIdentifier, delimiters),
                DateTimeOfAnalysis.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ObservationSite, delimiters),
                ObservationInstanceIdentifier.ToHL7String(delimiters),
                MoodCode.ToHL7String(delimiters),
                PerformingOrganizationName.ToHL7String(delimiters),
                PerformingOrganizationAddress.ToHL7String(delimiters),
                PerformingOrganizationMedicalDirector.ToHL7String(delimiters),
                PatientResultsReleaseCategory.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => ValueType.Value,
                3 => ObservationIdentifier.ToHL7String(delimiters),
                4 => ObservationSubID.Value,
                5 => SegmentFieldHelper.JoinRepeatingField(ObservationValue, delimiters),
                6 => Units.ToHL7String(delimiters),
                7 => ReferencesRange.Value,
                8 => SegmentFieldHelper.JoinRepeatingField(InterpretationCodes, delimiters),
                9 => Probability.Value,
                10 => SegmentFieldHelper.JoinRepeatingField(NatureOfAbnormalTest, delimiters),
                11 => ObservationResultStatus.Value,
                12 => EffectiveDateOfReferenceRange.Value,
                13 => UserDefinedAccessChecks.Value,
                14 => DateTimeOfObservation.Value,
                15 => ProducerID.ToHL7String(delimiters),
                16 => SegmentFieldHelper.JoinRepeatingField(ResponsibleObserver, delimiters),
                17 => SegmentFieldHelper.JoinRepeatingField(OvservationMethod, delimiters),
                18 => SegmentFieldHelper.JoinRepeatingField(EquipmentInstanceIdentifier, delimiters),
                19 => DateTimeOfAnalysis.Value,
                20 => SegmentFieldHelper.JoinRepeatingField(ObservationSite, delimiters),
                21 => ObservationInstanceIdentifier.ToHL7String(delimiters),
                22 => MoodCode.Value,
                23 => PerformingOrganizationName.ToHL7String(delimiters),
                24 => PerformingOrganizationAddress.ToHL7String(delimiters),
                25 => PerformingOrganizationMedicalDirector.ToHL7String(delimiters),
                26 => PatientResultsReleaseCategory.Value,
                _ => null
            };
        }
    }
}
