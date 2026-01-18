using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Obervation/Result
    /// </summary>
    public class OBX : ISegment
    {
        public string SegmentId => nameof(OBX);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// OBX.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// OBX.2
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// OBX.3
        /// </summary>
        public string ObservationIdentifier { get; set; }

        /// <summary>
        /// OBX.4
        /// </summary>
        public string ObservationSubID { get; set; }

        /// <summary>
        /// OBX.5
        /// </summary>
        public string ObservationValue { get; set; }

        /// <summary>
        /// OBX.6
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// OBX.7
        /// </summary>
        public string ReferencesRange { get; set; }

        /// <summary>
        /// OBX.8
        /// </summary>
        public string InterpretationCodes { get; set; }

        /// <summary>
        /// OBX.9
        /// </summary>
        public string Probability { get; set; }

        /// <summary>
        /// OBX.10
        /// </summary>
        public string NatureOfAbnormalTest { get; set; }

        /// <summary>
        /// OBX.11
        /// </summary>
        public string ObservationResultStatus { get; set; }

        /// <summary>
        /// OBX.12
        /// </summary>
        public string EffectiveDateOfReferenceRange { get; set; }

        /// <summary>
        /// OBX.13
        /// </summary>
        public string UserDefinedAccessChecks { get; set; }

        /// <summary>
        /// OBX.14
        /// </summary>
        public string DateTimeOfObservation { get; set; }

        /// <summary>
        /// OBX.15
        /// </summary>
        public string ProducerID { get; set; }

        /// <summary>
        /// OBX.16
        /// </summary>
        public string ResponsibleObserver { get; set; }

        /// <summary>
        /// OBX.17
        /// </summary>
        public string OvservationMethod { get; set; }

        /// <summary>
        /// OBX.18
        /// </summary>
        public string EquipmentInstanceIdentifier { get; set; }

        /// <summary>
        /// OBX.19
        /// </summary>
        public string DateTimeOfAnalysis { get; set; }

        /// <summary>
        /// OBX.20
        /// </summary>
        public string ObservationSite { get; set; }

        /// <summary>
        /// OBX.21
        /// </summary>
        public string ObservationInstanceIdentifier { get; set; }

        /// <summary>
        /// OBX.22
        /// </summary>
        public string MoodCode { get; set; }

        /// <summary>
        /// OBX.23
        /// </summary>
        public string PerformingOrganizationName { get; set; }

        /// <summary>
        /// OBX.24
        /// </summary>
        public string PerformingOrganizationAddress { get; set; }

        /// <summary>
        /// OBX.25
        /// </summary>
        public string PerformingOrganizationMedicalDirector { get; set; }

        /// <summary>
        /// OBX.26
        /// </summary>
        public string PatientResultsReleaseCategory { get; set; }

        // Constructors
        public OBX()
        {
            SegmentType = SegmentType.MedOrder;
            SetID = string.Empty;
            ValueType = string.Empty;
            ObservationIdentifier = string.Empty;
            ObservationSubID = string.Empty;
            ObservationValue = string.Empty;
            Units = string.Empty;
            ReferencesRange = string.Empty;
            InterpretationCodes = string.Empty;
            Probability = string.Empty;
            NatureOfAbnormalTest = string.Empty;
            ObservationResultStatus = string.Empty;
            EffectiveDateOfReferenceRange = string.Empty;
            UserDefinedAccessChecks = string.Empty;
            DateTimeOfObservation = string.Empty;
            ProducerID = string.Empty;
            ResponsibleObserver = string.Empty;
            OvservationMethod = string.Empty;
            EquipmentInstanceIdentifier = string.Empty;
            DateTimeOfAnalysis = string.Empty;
            ObservationSite = string.Empty;
            ObservationInstanceIdentifier = string.Empty;
            MoodCode = string.Empty;
            PerformingOrganizationName = string.Empty;
            PerformingOrganizationAddress = string.Empty;
            PerformingOrganizationMedicalDirector = string.Empty;
            PatientResultsReleaseCategory = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: ValueType = value; break;
                case 3: ObservationIdentifier = value; break;
                case 4: ObservationSubID = value; break;
                case 5: ObservationValue = value; break;
                case 6: Units = value; break;
                case 7: ReferencesRange = value; break;
                case 8: InterpretationCodes = value; break;
                case 9: Probability = value; break;
                case 10: NatureOfAbnormalTest = value; break;
                case 11: ObservationResultStatus = value; break;
                case 12: EffectiveDateOfReferenceRange = value; break;
                case 13: UserDefinedAccessChecks = value; break;
                case 14: DateTimeOfObservation = value; break;
                case 15: ProducerID = value; break;
                case 16: ResponsibleObserver = value; break;
                case 17: OvservationMethod = value; break;
                case 18: EquipmentInstanceIdentifier = value; break;
                case 19: DateTimeOfAnalysis = value; break;
                case 20: ObservationSite = value; break;
                case 21: ObservationInstanceIdentifier = value; break;
                case 22: MoodCode = value; break;
                case 23: PerformingOrganizationName = value; break;
                case 24: PerformingOrganizationAddress = value; break;
                case 25: PerformingOrganizationMedicalDirector = value; break;
                case 26: PatientResultsReleaseCategory = value; break;
            }
        }

        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                ValueType,
                ObservationIdentifier,
                ObservationSubID,
                ObservationValue,
                Units,
                ReferencesRange,
                InterpretationCodes,
                Probability,
                NatureOfAbnormalTest,
                ObservationResultStatus,
                EffectiveDateOfReferenceRange,
                UserDefinedAccessChecks,
                DateTimeOfObservation,
                ProducerID,
                ResponsibleObserver,
                OvservationMethod,
                EquipmentInstanceIdentifier,
                DateTimeOfAnalysis,
                ObservationSite,
                ObservationInstanceIdentifier,
                MoodCode,
                PerformingOrganizationName,
                PerformingOrganizationAddress,
                PerformingOrganizationMedicalDirector,
                PatientResultsReleaseCategory
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => ValueType,
                3 => ObservationIdentifier,
                4 => ObservationSubID,
                5 => ObservationValue,
                6 => Units,
                7 => ReferencesRange,
                8 => InterpretationCodes,
                9 => Probability,
                10 => NatureOfAbnormalTest,
                11 => ObservationResultStatus,
                12 => EffectiveDateOfReferenceRange,
                13 => UserDefinedAccessChecks,
                14 => DateTimeOfObservation,
                15 => ProducerID,
                16 => ResponsibleObserver,
                17 => OvservationMethod,
                18 => EquipmentInstanceIdentifier,
                19 => DateTimeOfAnalysis,
                20 => ObservationSite,
                21 => ObservationInstanceIdentifier,
                22 => MoodCode,
                23 => PerformingOrganizationName,
                24 => PerformingOrganizationAddress,
                25 => PerformingOrganizationMedicalDirector,
                26 => PatientResultsReleaseCategory,
                _ => null
            };
        }
    }
}
