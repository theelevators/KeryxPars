using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Financial Transaction
    /// </summary>
    public class FT1 : ISegment
    {
        public string SegmentId => nameof(FT1);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// FT1.1 - Set ID - FT1
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// FT1.2 - Transaction ID
        /// </summary>
        public ST TransactionID { get; set; }

        /// <summary>
        /// FT1.3 - Transaction Batch ID
        /// </summary>
        public ST TransactionBatchID { get; set; }

        /// <summary>
        /// FT1.4 - Transaction Date
        /// </summary>
        public DR TransactionDate { get; set; }

        /// <summary>
        /// FT1.5 - Transaction Posting Date
        /// </summary>
        public DTM TransactionPostingDate { get; set; }

        /// <summary>
        /// FT1.6 - Transaction Type
        /// </summary>
        public IS TransactionType { get; set; }

        /// <summary>
        /// FT1.7 - Transaction Code
        /// </summary>
        public CE TransactionCode { get; set; }

        /// <summary>
        /// FT1.8 - Transaction Description
        /// </summary>
        public ST TransactionDescription { get; set; }

        /// <summary>
        /// FT1.9 - Transaction Description - Alt
        /// </summary>
        public ST TransactionDescriptionAlt { get; set; }

        /// <summary>
        /// FT1.10 - Transaction Quantity
        /// </summary>
        public NM TransactionQuantity { get; set; }

        /// <summary>
        /// FT1.11 - Transaction Amount - Extended
        /// </summary>
        public NM TransactionAmountExtended { get; set; }

        /// <summary>
        /// FT1.12 - Transaction Amount - Unit
        /// </summary>
        public NM TransactionAmountUnit { get; set; }

        /// <summary>
        /// FT1.13 - Department Code
        /// </summary>
        public CE DepartmentCode { get; set; }

        /// <summary>
        /// FT1.14 - Insurance Plan ID
        /// </summary>
        public CE InsurancePlanID { get; set; }

        /// <summary>
        /// FT1.15 - Insurance Amount
        /// </summary>
        public NM InsuranceAmount { get; set; }

        /// <summary>
        /// FT1.16 - Assigned Patient Location
        /// </summary>
        public PL AssignedPatientLocation { get; set; }

        /// <summary>
        /// FT1.17 - Fee Schedule
        /// </summary>
        public IS FeeSchedule { get; set; }

        /// <summary>
        /// FT1.18 - Patient Type
        /// </summary>
        public IS PatientType { get; set; }

        /// <summary>
        /// FT1.19 - Diagnosis Code - FT1 (repeating)
        /// </summary>
        public CE[] DiagnosisCodeFT1 { get; set; }

        /// <summary>
        /// FT1.20 - Performed By Code (repeating)
        /// </summary>
        public XCN[] PerformedByCode { get; set; }

        /// <summary>
        /// FT1.21 - Ordered By Code (repeating)
        /// </summary>
        public XCN[] OrderedByCode { get; set; }

        /// <summary>
        /// FT1.22 - Unit Cost
        /// </summary>
        public NM UnitCost { get; set; }

        /// <summary>
        /// FT1.23 - Filler Order Number
        /// </summary>
        public EI FillerOrderNumber { get; set; }

        /// <summary>
        /// FT1.24 - Entered By Code (repeating)
        /// </summary>
        public XCN[] EnteredByCode { get; set; }

        /// <summary>
        /// FT1.25 - Procedure Code
        /// </summary>
        public CE ProcedureCode { get; set; }

        /// <summary>
        /// FT1.26 - Procedure Code Modifier (repeating)
        /// </summary>
        public CE[] ProcedureCodeModifier { get; set; }

        public FT1()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            TransactionID = default;
            TransactionBatchID = default;
            TransactionDate = default;
            TransactionPostingDate = default;
            TransactionType = default;
            TransactionCode = default;
            TransactionDescription = default;
            TransactionDescriptionAlt = default;
            TransactionQuantity = default;
            TransactionAmountExtended = default;
            TransactionAmountUnit = default;
            DepartmentCode = default;
            InsurancePlanID = default;
            InsuranceAmount = default;
            AssignedPatientLocation = default;
            FeeSchedule = default;
            PatientType = default;
            DiagnosisCodeFT1 = [];
            PerformedByCode = [];
            OrderedByCode = [];
            UnitCost = default;
            FillerOrderNumber = default;
            EnteredByCode = [];
            ProcedureCode = default;
            ProcedureCodeModifier = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: TransactionID = new ST(value); break;
                case 3: TransactionBatchID = new ST(value); break;
                case 4:
                    var dr4 = new DR();
                    dr4.Parse(value.AsSpan(), delimiters);
                    TransactionDate = dr4;
                    break;
                case 5: TransactionPostingDate = new DTM(value); break;
                case 6: TransactionType = new IS(value); break;
                case 7:
                    var ce7 = new CE();
                    ce7.Parse(value.AsSpan(), delimiters);
                    TransactionCode = ce7;
                    break;
                case 8: TransactionDescription = new ST(value); break;
                case 9: TransactionDescriptionAlt = new ST(value); break;
                case 10: TransactionQuantity = new NM(value); break;
                case 11: TransactionAmountExtended = new NM(value); break;
                case 12: TransactionAmountUnit = new NM(value); break;
                case 13:
                    var ce13 = new CE();
                    ce13.Parse(value.AsSpan(), delimiters);
                    DepartmentCode = ce13;
                    break;
                case 14:
                    var ce14 = new CE();
                    ce14.Parse(value.AsSpan(), delimiters);
                    InsurancePlanID = ce14;
                    break;
                case 15: InsuranceAmount = new NM(value); break;
                case 16:
                    var pl16 = new PL();
                    pl16.Parse(value.AsSpan(), delimiters);
                    AssignedPatientLocation = pl16;
                    break;
                case 17: FeeSchedule = new IS(value); break;
                case 18: PatientType = new IS(value); break;
                case 19: DiagnosisCodeFT1 = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 20: PerformedByCode = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 21: OrderedByCode = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 22: UnitCost = new NM(value); break;
                case 23:
                    var ei23 = new EI();
                    ei23.Parse(value.AsSpan(), delimiters);
                    FillerOrderNumber = ei23;
                    break;
                case 24: EnteredByCode = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 25:
                    var ce25 = new CE();
                    ce25.Parse(value.AsSpan(), delimiters);
                    ProcedureCode = ce25;
                    break;
                case 26: ProcedureCodeModifier = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                TransactionID.ToHL7String(delimiters),
                TransactionBatchID.ToHL7String(delimiters),
                TransactionDate.ToHL7String(delimiters),
                TransactionPostingDate.ToHL7String(delimiters),
                TransactionType.ToHL7String(delimiters),
                TransactionCode.ToHL7String(delimiters),
                TransactionDescription.ToHL7String(delimiters),
                TransactionDescriptionAlt.ToHL7String(delimiters),
                TransactionQuantity.ToHL7String(delimiters),
                TransactionAmountExtended.ToHL7String(delimiters),
                TransactionAmountUnit.ToHL7String(delimiters),
                DepartmentCode.ToHL7String(delimiters),
                InsurancePlanID.ToHL7String(delimiters),
                InsuranceAmount.ToHL7String(delimiters),
                AssignedPatientLocation.ToHL7String(delimiters),
                FeeSchedule.ToHL7String(delimiters),
                PatientType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(DiagnosisCodeFT1, delimiters),
                SegmentFieldHelper.JoinRepeatingField(PerformedByCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OrderedByCode, delimiters),
                UnitCost.ToHL7String(delimiters),
                FillerOrderNumber.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(EnteredByCode, delimiters),
                ProcedureCode.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ProcedureCodeModifier, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => TransactionID.Value,
                3 => TransactionBatchID.Value,
                4 => TransactionDate.ToHL7String(delimiters),
                5 => TransactionPostingDate.Value,
                6 => TransactionType.Value,
                7 => TransactionCode.ToHL7String(delimiters),
                8 => TransactionDescription.Value,
                9 => TransactionDescriptionAlt.Value,
                10 => TransactionQuantity.Value,
                11 => TransactionAmountExtended.Value,
                12 => TransactionAmountUnit.Value,
                13 => DepartmentCode.ToHL7String(delimiters),
                14 => InsurancePlanID.ToHL7String(delimiters),
                15 => InsuranceAmount.Value,
                16 => AssignedPatientLocation.ToHL7String(delimiters),
                17 => FeeSchedule.Value,
                18 => PatientType.Value,
                19 => SegmentFieldHelper.JoinRepeatingField(DiagnosisCodeFT1, delimiters),
                20 => SegmentFieldHelper.JoinRepeatingField(PerformedByCode, delimiters),
                21 => SegmentFieldHelper.JoinRepeatingField(OrderedByCode, delimiters),
                22 => UnitCost.Value,
                23 => FillerOrderNumber.ToHL7String(delimiters),
                24 => SegmentFieldHelper.JoinRepeatingField(EnteredByCode, delimiters),
                25 => ProcedureCode.ToHL7String(delimiters),
                26 => SegmentFieldHelper.JoinRepeatingField(ProcedureCodeModifier, delimiters),
                _ => null
            };
        }
    }
}
