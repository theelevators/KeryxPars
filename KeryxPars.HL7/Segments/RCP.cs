using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Response Control Parameter
    /// </summary>
    public class RCP : ISegment
    {
        public string SegmentId => nameof(RCP);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RCP.1 - Query Priority
        /// </summary>
        public ID QueryPriority { get; set; }

        /// <summary>
        /// RCP.2 - Quantity Limited Request
        /// </summary>
        public CQ QuantityLimitedRequest { get; set; }

        /// <summary>
        /// RCP.3 - Response Modality
        /// </summary>
        public CE ResponseModality { get; set; }

        /// <summary>
        /// RCP.4 - Execution and Delivery Time
        /// </summary>
        public DTM ExecutionAndDeliveryTime { get; set; }

        /// <summary>
        /// RCP.5 - Modify Indicator
        /// </summary>
        public ID ModifyIndicator { get; set; }

        /// <summary>
        /// RCP.6 - Sort-by Field (repeating)
        /// </summary>
        public ST[] SortByField { get; set; }

        /// <summary>
        /// RCP.7 - Segment group inclusion (repeating)
        /// </summary>
        public ID[] SegmentGroupInclusion { get; set; }

        public RCP()
        {
            SegmentType = SegmentType.Universal;
            QueryPriority = default;
            QuantityLimitedRequest = default;
            ResponseModality = default;
            ExecutionAndDeliveryTime = default;
            ModifyIndicator = default;
            SortByField = [];
            SegmentGroupInclusion = [];
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: QueryPriority = new ID(value); break;
                case 2:
                    var cq2 = new CQ();
                    cq2.Parse(value.AsSpan(), delimiters);
                    QuantityLimitedRequest = cq2;
                    break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    ResponseModality = ce3;
                    break;
                case 4: ExecutionAndDeliveryTime = new DTM(value); break;
                case 5: ModifyIndicator = new ID(value); break;
                case 6: SortByField = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 7: SegmentGroupInclusion = SegmentFieldHelper.ParseRepeatingField<ID>(value, delimiters); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                QueryPriority.ToHL7String(delimiters),
                QuantityLimitedRequest.ToHL7String(delimiters),
                ResponseModality.ToHL7String(delimiters),
                ExecutionAndDeliveryTime.ToHL7String(delimiters),
                ModifyIndicator.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SortByField, delimiters),
                SegmentFieldHelper.JoinRepeatingField(SegmentGroupInclusion, delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => QueryPriority.Value,
                2 => QuantityLimitedRequest.ToHL7String(delimiters),
                3 => ResponseModality.ToHL7String(delimiters),
                4 => ExecutionAndDeliveryTime.Value,
                5 => ModifyIndicator.Value,
                6 => SegmentFieldHelper.JoinRepeatingField(SortByField, delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(SegmentGroupInclusion, delimiters),
                _ => null
            };
        }
    }
}
