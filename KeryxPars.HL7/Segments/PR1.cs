using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Procedures
    /// </summary>
    public class PR1 : ISegment
    {
        public string SegmentId => nameof(PR1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// PR1.1 - Set ID - PR1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// PR1.2 - Procedure Coding Method
        /// </summary>
        public IS ProcedureCodingMethod { get; set; }

        /// <summary>
        /// PR1.3 - Procedure Code
        /// </summary>
        public CE ProcedureCode { get; set; }

        /// <summary>
        /// PR1.4 - Procedure Description
        /// </summary>
        public ST ProcedureDescription { get; set; }

        /// <summary>
        /// PR1.5 - Procedure Date/Time
        /// </summary>
        public DTM ProcedureDateTime { get; set; }

        /// <summary>
        /// PR1.6 - Procedure Functional Type
        /// </summary>
        public IS ProcedureFunctionalType { get; set; }

        /// <summary>
        /// PR1.7 - Procedure Minutes
        /// </summary>
        public NM ProcedureMinutes { get; set; }

        /// <summary>
        /// PR1.8 - Anesthesiologist (repeating)
        /// </summary>
        public XCN[] Anesthesiologist { get; set; }

        /// <summary>
        /// PR1.9 - Anesthesia Code
        /// </summary>
        public IS AnesthesiaCode { get; set; }

        /// <summary>
        /// PR1.10 - Anesthesia Minutes
        /// </summary>
        public NM AnesthesiaMinutes { get; set; }

        /// <summary>
        /// PR1.11 - Surgeon (repeating)
        /// </summary>
        public XCN[] Surgeon { get; set; }

        /// <summary>
        /// PR1.12 - Procedure Practitioner (repeating)
        /// </summary>
        public XCN[] ProcedurePractitioner { get; set; }

        /// <summary>
        /// PR1.13 - Consent Code
        /// </summary>
        public CE ConsentCode { get; set; }

        /// <summary>
        /// PR1.14 - Procedure Priority
        /// </summary>
        public NM ProcedurePriority { get; set; }

        /// <summary>
        /// PR1.15 - Associated Diagnosis Code
        /// </summary>
        public CE AssociatedDiagnosisCode { get; set; }

        /// <summary>
        /// PR1.16 - Procedure Code Modifier (repeating)
        /// </summary>
        public CE[] ProcedureCodeModifier { get; set; }

        /// <summary>
        /// PR1.17 - Procedure DRG Type
        /// </summary>
        public IS ProcedureDRGType { get; set; }

        /// <summary>
        /// PR1.18 - Tissue Type Code (repeating)
        /// </summary>
        public CE[] TissueTypeCode { get; set; }

        public PR1()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            ProcedureCodingMethod = default;
            ProcedureCode = default;
            ProcedureDescription = default;
            ProcedureDateTime = default;
            ProcedureFunctionalType = default;
            ProcedureMinutes = default;
            Anesthesiologist = [];
            AnesthesiaCode = default;
            AnesthesiaMinutes = default;
            Surgeon = [];
            ProcedurePractitioner = [];
            ConsentCode = default;
            ProcedurePriority = default;
            AssociatedDiagnosisCode = default;
            ProcedureCodeModifier = [];
            ProcedureDRGType = default;
            TissueTypeCode = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: ProcedureCodingMethod = new IS(value); break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    ProcedureCode = ce3;
                    break;
                case 4: ProcedureDescription = new ST(value); break;
                case 5: ProcedureDateTime = new DTM(value); break;
                case 6: ProcedureFunctionalType = new IS(value); break;
                case 7: ProcedureMinutes = new NM(value); break;
                case 8: Anesthesiologist = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 9: AnesthesiaCode = new IS(value); break;
                case 10: AnesthesiaMinutes = new NM(value); break;
                case 11: Surgeon = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 12: ProcedurePractitioner = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 13:
                    var ce13 = new CE();
                    ce13.Parse(value.AsSpan(), delimiters);
                    ConsentCode = ce13;
                    break;
                case 14: ProcedurePriority = new NM(value); break;
                case 15:
                    var ce15 = new CE();
                    ce15.Parse(value.AsSpan(), delimiters);
                    AssociatedDiagnosisCode = ce15;
                    break;
                case 16: ProcedureCodeModifier = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 17: ProcedureDRGType = new IS(value); break;
                case 18: TissueTypeCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                ProcedureCodingMethod.ToHL7String(delimiters),
                ProcedureCode.ToHL7String(delimiters),
                ProcedureDescription.ToHL7String(delimiters),
                ProcedureDateTime.ToHL7String(delimiters),
                ProcedureFunctionalType.ToHL7String(delimiters),
                ProcedureMinutes.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Anesthesiologist, delimiters),
                AnesthesiaCode.ToHL7String(delimiters),
                AnesthesiaMinutes.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Surgeon, delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProcedurePractitioner, delimiters),
                ConsentCode.ToHL7String(delimiters),
                ProcedurePriority.ToHL7String(delimiters),
                AssociatedDiagnosisCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProcedureCodeModifier, delimiters),
                ProcedureDRGType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(TissueTypeCode, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => ProcedureCodingMethod.Value,
                3 => ProcedureCode.ToHL7String(delimiters),
                4 => ProcedureDescription.Value,
                5 => ProcedureDateTime.Value,
                6 => ProcedureFunctionalType.Value,
                7 => ProcedureMinutes.Value,
                8 => SegmentFieldHelper.JoinRepeatingField(Anesthesiologist, delimiters),
                9 => AnesthesiaCode.Value,
                10 => AnesthesiaMinutes.Value,
                11 => SegmentFieldHelper.JoinRepeatingField(Surgeon, delimiters),
                12 => SegmentFieldHelper.JoinRepeatingField(ProcedurePractitioner, delimiters),
                13 => ConsentCode.ToHL7String(delimiters),
                14 => ProcedurePriority.Value,
                15 => AssociatedDiagnosisCode.ToHL7String(delimiters),
                16 => SegmentFieldHelper.JoinRepeatingField(ProcedureCodeModifier, delimiters),
                17 => ProcedureDRGType.Value,
                18 => SegmentFieldHelper.JoinRepeatingField(TissueTypeCode, delimiters),
                _ => null
            };
        }
    }
}
