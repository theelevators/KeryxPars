using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Patient Diagnosis Information
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class DG1 : ISegment
    {
        public string SegmentId => nameof(DG1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// DG1.1 - Set ID - DG1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// DG1.2 - Diagnosis Coding Method
        /// </summary>
        public ID DiagnosisCodingMethod { get; set; }

        /// <summary>
        /// DG1.3 - Diagnosis Code
        /// </summary>
        public CE DiagnosisCode { get; set; }

        /// <summary>
        /// DG1.4 - Diagnosis Description
        /// </summary>
        public ST DiagnosisDescription { get; set; }

        /// <summary>
        /// DG1.5 - Diagnosis Date/Time
        /// </summary>
        public DTM DiagnosisDateTime { get; set; }

        /// <summary>
        /// DG1.6 - Diagnosis Type
        /// </summary>
        public IS DiagnosisType { get; set; }

        /// <summary>
        /// DG1.7 - Major Diagnostic Category
        /// </summary>
        public CE MajorDiagnosticCategory { get; set; }

        /// <summary>
        /// DG1.8 - Diagnostic Related Group
        /// </summary>
        public CE DiagnosticRelatedGroup { get; set; }

        /// <summary>
        /// DG1.9 - DRG Approval Indicator
        /// </summary>
        public ID DrgApprovalIndicator { get; set; }

        /// <summary>
        /// DG1.10 - DRG Grouper Review Code
        /// </summary>
        public IS DrgGrouperReviewCode { get; set; }

        /// <summary>
        /// DG1.11 - Outlier Type
        /// </summary>
        public CE OutlierType { get; set; }

        /// <summary>
        /// DG1.12 - Outlier Days
        /// </summary>
        public NM OutlierDays { get; set; }

        /// <summary>
        /// DG1.13 - Outlier Cost
        /// </summary>
        public NM OutlierCost { get; set; }

        /// <summary>
        /// DG1.14 - Grouper Version And Type
        /// </summary>
        public ST GrouperVersionAndType { get; set; }

        /// <summary>
        /// DG1.15 - Diagnosis Priority
        /// </summary>
        public ID DiagnosisPriority { get; set; }

        /// <summary>
        /// DG1.16 - Diagnosing Clinician
        /// </summary>
        public XCN[] DiagnosingClinician { get; set; }

        /// <summary>
        /// DG1.17 - Diagnosis Classification
        /// </summary>
        public IS DiagnosisClassification { get; set; }

        /// <summary>
        /// DG1.18 - Confidential Indicator
        /// </summary>
        public ID ConfidentialIndicator { get; set; }

        /// <summary>
        /// DG1.19 - Attestation Date/Time
        /// </summary>
        public DTM AttestationDateTime { get; set; }

        /// <summary>
        /// DG1.20 - Diagnosis Identifier
        /// </summary>
        public EI DiagnosisIdentifier { get; set; }

        /// <summary>
        /// DG1.21 - Diagnosis Action Code
        /// </summary>
        public ID DiagnosisActionCode { get; set; }

        /// <summary>
        /// DG1.22 - Parent Diagnosis
        /// </summary>
        public EI ParentDiagnosis { get; set; }

        /// <summary>
        /// DG1.23 - DRG CCL Value Code
        /// </summary>
        public CE DrgCCLValueCode { get; set; }

        /// <summary>
        /// DG1.24 - DRG Grouping Usage
        /// </summary>
        public ID DrgGroupingUsage { get; set; }

        /// <summary>
        /// DG1.25 - DRG Diagnosis Determination Status
        /// </summary>
        public IS DrgDiagnosisDeterminationStatus { get; set; }

        /// <summary>
        /// DG1.26 - Present On Admission Indicator
        /// </summary>
        public IS PresentOnAdmissionIndicator { get; set; }

        public DG1()
        {
            SegmentType = SegmentType.ADT;
            SetID = default;
            DiagnosisCodingMethod = default;
            DiagnosisCode = default;
            DiagnosisDescription = default;
            DiagnosisDateTime = default;
            DiagnosisType = default;
            MajorDiagnosticCategory = default;
            DiagnosticRelatedGroup = default;
            DrgApprovalIndicator = default;
            DrgGrouperReviewCode = default;
            OutlierType = default;
            OutlierDays = default;
            OutlierCost = default;
            GrouperVersionAndType = default;
            DiagnosisPriority = default;
            DiagnosingClinician = [];
            DiagnosisClassification = default;
            ConfidentialIndicator = default;
            AttestationDateTime = default;
            DiagnosisIdentifier = default;
            DiagnosisActionCode = default;
            ParentDiagnosis = default;
            DrgCCLValueCode = default;
            DrgGroupingUsage = default;
            DrgDiagnosisDeterminationStatus = default;
            PresentOnAdmissionIndicator = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: DiagnosisCodingMethod = new ID(value); break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    DiagnosisCode = ce3;
                    break;
                case 4: DiagnosisDescription = new ST(value); break;
                case 5: DiagnosisDateTime = new DTM(value); break;
                case 6: DiagnosisType = new IS(value); break;
                case 7:
                    var ce7 = new CE();
                    ce7.Parse(value.AsSpan(), delimiters);
                    MajorDiagnosticCategory = ce7;
                    break;
                case 8:
                    var ce8 = new CE();
                    ce8.Parse(value.AsSpan(), delimiters);
                    DiagnosticRelatedGroup = ce8;
                    break;
                case 9: DrgApprovalIndicator = new ID(value); break;
                case 10: DrgGrouperReviewCode = new IS(value); break;
                case 11:
                    var ce11 = new CE();
                    ce11.Parse(value.AsSpan(), delimiters);
                    OutlierType = ce11;
                    break;
                case 12: OutlierDays = new NM(value); break;
                case 13: OutlierCost = new NM(value); break;
                case 14: GrouperVersionAndType = new ST(value); break;
                case 15: DiagnosisPriority = new ID(value); break;
                case 16:
                    DiagnosingClinician = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters);
                    break;
                case 17: DiagnosisClassification = new IS(value); break;
                case 18: ConfidentialIndicator = new ID(value); break;
                case 19: AttestationDateTime = new DTM(value); break;
                case 20:
                    var ei20 = new EI();
                    ei20.Parse(value.AsSpan(), delimiters);
                    DiagnosisIdentifier = ei20;
                    break;
                case 21: DiagnosisActionCode = new ID(value); break;
                case 22:
                    var ei22 = new EI();
                    ei22.Parse(value.AsSpan(), delimiters);
                    ParentDiagnosis = ei22;
                    break;
                case 23:
                    var ce23 = new CE();
                    ce23.Parse(value.AsSpan(), delimiters);
                    DrgCCLValueCode = ce23;
                    break;
                case 24: DrgGroupingUsage = new ID(value); break;
                case 25: DrgDiagnosisDeterminationStatus = new IS(value); break;
                case 26: PresentOnAdmissionIndicator = new IS(value); break;
                default: break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                DiagnosisCodingMethod.ToHL7String(delimiters),
                DiagnosisCode.ToHL7String(delimiters),
                DiagnosisDescription.ToHL7String(delimiters),
                DiagnosisDateTime.ToHL7String(delimiters),
                DiagnosisType.ToHL7String(delimiters),
                MajorDiagnosticCategory.ToHL7String(delimiters),
                DiagnosticRelatedGroup.ToHL7String(delimiters),
                DrgApprovalIndicator.ToHL7String(delimiters),
                DrgGrouperReviewCode.ToHL7String(delimiters),
                OutlierType.ToHL7String(delimiters),
                OutlierDays.ToHL7String(delimiters),
                OutlierCost.ToHL7String(delimiters),
                GrouperVersionAndType.ToHL7String(delimiters),
                DiagnosisPriority.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(DiagnosingClinician, delimiters),
                DiagnosisClassification.ToHL7String(delimiters),
                ConfidentialIndicator.ToHL7String(delimiters),
                AttestationDateTime.ToHL7String(delimiters),
                DiagnosisIdentifier.ToHL7String(delimiters),
                DiagnosisActionCode.ToHL7String(delimiters),
                ParentDiagnosis.ToHL7String(delimiters),
                DrgCCLValueCode.ToHL7String(delimiters),
                DrgGroupingUsage.ToHL7String(delimiters),
                DrgDiagnosisDeterminationStatus.ToHL7String(delimiters),
                PresentOnAdmissionIndicator.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => DiagnosisCodingMethod.Value,
                3 => DiagnosisCode.ToHL7String(delimiters),
                4 => DiagnosisDescription.Value,
                5 => DiagnosisDateTime.Value,
                6 => DiagnosisType.Value,
                7 => MajorDiagnosticCategory.ToHL7String(delimiters),
                8 => DiagnosticRelatedGroup.ToHL7String(delimiters),
                9 => DrgApprovalIndicator.Value,
                10 => DrgGrouperReviewCode.Value,
                11 => OutlierType.ToHL7String(delimiters),
                12 => OutlierDays.Value,
                13 => OutlierCost.Value,
                14 => GrouperVersionAndType.Value,
                15 => DiagnosisPriority.Value,
                16 => SegmentFieldHelper.JoinRepeatingField(DiagnosingClinician, delimiters),
                17 => DiagnosisClassification.Value,
                18 => ConfidentialIndicator.Value,
                19 => AttestationDateTime.Value,
                20 => DiagnosisIdentifier.ToHL7String(delimiters),
                21 => DiagnosisActionCode.Value,
                22 => ParentDiagnosis.ToHL7String(delimiters),
                23 => DrgCCLValueCode.ToHL7String(delimiters),
                24 => DrgGroupingUsage.Value,
                25 => DrgDiagnosisDeterminationStatus.Value,
                26 => PresentOnAdmissionIndicator.Value,
                _ => null
            };
        }
    }
}
