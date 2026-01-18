using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Diagnosis Related Group
    /// </summary>
    public class DRG : ISegment
    {
        public string SegmentId => nameof(DRG);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// DRG.1 - Diagnostic Related Group
        /// </summary>
        public CE DiagnosticRelatedGroup { get; set; }

        /// <summary>
        /// DRG.2 - DRG Assigned Date/Time
        /// </summary>
        public DTM DRGAssignedDateTime { get; set; }

        /// <summary>
        /// DRG.3 - DRG Approval Indicator
        /// </summary>
        public ID DRGApprovalIndicator { get; set; }

        /// <summary>
        /// DRG.4 - DRG Grouper Review Code
        /// </summary>
        public IS DRGGrouperReviewCode { get; set; }

        /// <summary>
        /// DRG.5 - Outlier Type
        /// </summary>
        public CE OutlierType { get; set; }

        /// <summary>
        /// DRG.6 - Outlier Days
        /// </summary>
        public NM OutlierDays { get; set; }

        /// <summary>
        /// DRG.7 - Outlier Cost
        /// </summary>
        public NM OutlierCost { get; set; }

        /// <summary>
        /// DRG.8 - DRG Payor
        /// </summary>
        public IS DRGPayor { get; set; }

        /// <summary>
        /// DRG.9 - Outlier Reimbursement
        /// </summary>
        public NM OutlierReimbursement { get; set; }

        /// <summary>
        /// DRG.10 - Confidential Indicator
        /// </summary>
        public ID ConfidentialIndicator { get; set; }

        /// <summary>
        /// DRG.11 - DRG Transfer Type
        /// </summary>
        public IS DRGTransferType { get; set; }

        public DRG()
        {
            SegmentType = SegmentType.Universal;
            DiagnosticRelatedGroup = default;
            DRGAssignedDateTime = default;
            DRGApprovalIndicator = default;
            DRGGrouperReviewCode = default;
            OutlierType = default;
            OutlierDays = default;
            OutlierCost = default;
            DRGPayor = default;
            OutlierReimbursement = default;
            ConfidentialIndicator = default;
            DRGTransferType = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ce1 = new CE();
                    ce1.Parse(value.AsSpan(), delimiters);
                    DiagnosticRelatedGroup = ce1;
                    break;
                case 2: DRGAssignedDateTime = new DTM(value); break;
                case 3: DRGApprovalIndicator = new ID(value); break;
                case 4: DRGGrouperReviewCode = new IS(value); break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    OutlierType = ce5;
                    break;
                case 6: OutlierDays = new NM(value); break;
                case 7: OutlierCost = new NM(value); break;
                case 8: DRGPayor = new IS(value); break;
                case 9: OutlierReimbursement = new NM(value); break;
                case 10: ConfidentialIndicator = new ID(value); break;
                case 11: DRGTransferType = new IS(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                DiagnosticRelatedGroup.ToHL7String(delimiters),
                DRGAssignedDateTime.ToHL7String(delimiters),
                DRGApprovalIndicator.ToHL7String(delimiters),
                DRGGrouperReviewCode.ToHL7String(delimiters),
                OutlierType.ToHL7String(delimiters),
                OutlierDays.ToHL7String(delimiters),
                OutlierCost.ToHL7String(delimiters),
                DRGPayor.ToHL7String(delimiters),
                OutlierReimbursement.ToHL7String(delimiters),
                ConfidentialIndicator.ToHL7String(delimiters),
                DRGTransferType.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => DiagnosticRelatedGroup.ToHL7String(delimiters),
                2 => DRGAssignedDateTime.Value,
                3 => DRGApprovalIndicator.Value,
                4 => DRGGrouperReviewCode.Value,
                5 => OutlierType.ToHL7String(delimiters),
                6 => OutlierDays.Value,
                7 => OutlierCost.Value,
                8 => DRGPayor.Value,
                9 => OutlierReimbursement.Value,
                10 => ConfidentialIndicator.Value,
                11 => DRGTransferType.Value,
                _ => null
            };
        }
    }
}
