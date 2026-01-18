using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Diagnosis Information
    /// </summary>
    public class DG1 : ISegment
    {
        public string SegmentId => nameof(DG1);
        
        public SegmentType SegmentType { get; private set; }

        // Auto-Implemented Properties

        /// <summary>
        /// DG1.1
        /// </summary>
        public string SetID { get; set; }

        /// <summary>
        /// DG1.2
        /// </summary>
        public string DiagnosisCodingMethod { get; set; }

        /// <summary>
        /// DG1.3
        /// </summary>
        public string DiagnosisCode { get; set; }

        /// <summary>
        /// DG1.4
        /// </summary>
        public string DiagnosisDescription { get; set; }

        /// <summary>
        /// DG1.5
        /// </summary>
        public string DiagnosisDateTime { get; set; }

        /// <summary>
        /// DG1.6
        /// </summary>
        public string DiagnosisType { get; set; }

        /// <summary>
        /// DG1.7
        /// </summary>
        public string MajorDiagnosticCategory { get; set; }

        /// <summary>
        /// DG1.8
        /// </summary>
        public string DiagnosticRelatedGroup { get; set; }

        /// <summary>
        /// DG1.9
        /// </summary>
        public string DrgApprovalIndicator { get; set; }

        /// <summary>
        /// DG1.10
        /// </summary>
        public string DrgGrouperReviewCode { get; set; }

        /// <summary>
        /// DG1.11
        /// </summary>
        public string OutlierType { get; set; }

        /// <summary>
        /// DG1.12
        /// </summary>
        public string OutlierDays { get; set; }

        /// <summary>
        /// DG1.13
        /// </summary>
        public string OutlierCost { get; set; }

        /// <summary>
        /// DG1.14
        /// </summary>
        public string GrouperVersionAndType { get; set; }

        /// <summary>
        /// DG1.15
        /// </summary>
        public string DiagnosisPriority { get; set; }

        /// <summary>
        /// DG1.16
        /// </summary>
        public string DiagnosingClinician { get; set; }

        /// <summary>
        /// DG1.17
        /// </summary>
        public string DiagnosisClassification { get; set; }

        /// <summary>
        /// DG1.18
        /// </summary>
        public string ConfidentialIndicator { get; set; }

        /// <summary>
        /// DG1.19
        /// </summary>
        public string AttestationDateTime { get; set; }

        /// <summary>
        /// DG1.20
        /// </summary>
        public string DiagnosisIdentifier { get; set; }

        /// <summary>
        /// DG1.21
        /// </summary>
        public string DiagnosisActionCode { get; set; }

        /// <summary>
        /// DG1.22
        /// </summary>
        public string ParentDiagnosis { get; set; }

        /// <summary>
        /// DG1.23
        /// </summary>
        public string DrgCCLValueCode { get; set; }

        /// <summary>
        /// DG1.24
        /// </summary>
        public string DrgGroupingUsage { get; set; }

        /// <summary>
        /// DG1.25
        /// </summary>
        public string DrgDiagnosisDeterminationStatus { get; set; }

        /// <summary>
        /// DG1.26
        /// </summary>
        public string PresentOnAdmissionIndicator { get; set; }


        // Constructors
        public DG1()
        {
            SegmentType = SegmentType.ADT;

            ClearValues();
        }

        // Methods
        public void ClearValues()
        {
            SetID = string.Empty;
            DiagnosisCodingMethod = string.Empty;
            DiagnosisCode = string.Empty;
            DiagnosisDescription = string.Empty;
            DiagnosisDateTime = string.Empty;
            DiagnosisType = string.Empty;
            MajorDiagnosticCategory = string.Empty;
            DiagnosticRelatedGroup = string.Empty;
            DrgApprovalIndicator = string.Empty;
            DrgGrouperReviewCode = string.Empty;
            OutlierType = string.Empty;
            OutlierDays = string.Empty;
            OutlierCost = string.Empty;
            GrouperVersionAndType = string.Empty;
            DiagnosisPriority = string.Empty;
            DiagnosingClinician = string.Empty;
            DiagnosisClassification = string.Empty;
            ConfidentialIndicator = string.Empty;
            AttestationDateTime = string.Empty;
            DiagnosisIdentifier = string.Empty;
            DiagnosisActionCode = string.Empty;
            ParentDiagnosis = string.Empty;
            DrgCCLValueCode = string.Empty;
            DrgGroupingUsage = string.Empty;
            DrgDiagnosisDeterminationStatus = string.Empty;
            PresentOnAdmissionIndicator = string.Empty;
        }

        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: SetID = value; break;
                case 2: DiagnosisCodingMethod = value; break;
                case 3: DiagnosisCode = value; break;
                case 4: DiagnosisDescription = value; break;
                case 5: DiagnosisDateTime = value; break;
                case 6: DiagnosisType = value; break;
                case 7: MajorDiagnosticCategory = value; break;
                case 8: DiagnosticRelatedGroup = value; break;
                case 9: DrgApprovalIndicator = value; break;
                case 10: DrgGrouperReviewCode = value; break;
                case 11: OutlierType = value; break;
                case 12: OutlierDays = value; break;
                case 13: OutlierCost = value; break;
                case 14: GrouperVersionAndType = value; break;
                case 15: DiagnosisPriority = value; break;
                case 16: DiagnosingClinician = value; break;
                case 17: DiagnosisClassification = value; break;
                case 18: ConfidentialIndicator = value; break;
                case 19: AttestationDateTime = value; break;
                case 20: DiagnosisIdentifier = value; break;
                case 21: DiagnosisActionCode = value; break;
                case 22: ParentDiagnosis = value; break;
                case 23: DrgCCLValueCode = value; break;
                case 24: DrgGroupingUsage = value; break;
                case 25: DrgDiagnosisDeterminationStatus = value; break;
                case 26: PresentOnAdmissionIndicator = value; break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                SetID,
                DiagnosisCodingMethod,
                DiagnosisCode,
                DiagnosisDescription,
                DiagnosisDateTime,
                DiagnosisType,
                MajorDiagnosticCategory,
                DiagnosticRelatedGroup,
                DrgApprovalIndicator,
                DrgGrouperReviewCode,
                OutlierType,
                OutlierDays,
                OutlierCost,
                GrouperVersionAndType,
                DiagnosisPriority,
                DiagnosingClinician,
                DiagnosisClassification,
                ConfidentialIndicator,
                AttestationDateTime,
                DiagnosisIdentifier,
                DiagnosisActionCode,
                ParentDiagnosis,
                DrgCCLValueCode,
                DrgGroupingUsage,
                DrgDiagnosisDeterminationStatus,
                PresentOnAdmissionIndicator
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => SetID,
                2 => DiagnosisCodingMethod,
                3 => DiagnosisCode,
                4 => DiagnosisDescription,
                5 => DiagnosisDateTime,
                6 => DiagnosisType,
                7 => MajorDiagnosticCategory,
                8 => DiagnosticRelatedGroup,
                9 => DrgApprovalIndicator,
                10 => DrgGrouperReviewCode,
                11 => OutlierType,
                12 => OutlierDays,
                13 => OutlierCost,
                14 => GrouperVersionAndType,
                15 => DiagnosisPriority,
                16 => DiagnosingClinician,
                17 => DiagnosisClassification,
                18 => ConfidentialIndicator,
                19 => AttestationDateTime,
                20 => DiagnosisIdentifier,
                21 => DiagnosisActionCode,
                22 => ParentDiagnosis,
                23 => DrgCCLValueCode,
                24 => DrgGroupingUsage,
                25 => DrgDiagnosisDeterminationStatus,
                26 => PresentOnAdmissionIndicator,
                _ => null
            };
        }
    }
}
