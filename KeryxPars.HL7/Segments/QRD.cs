using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Query Definition (Deprecated in v2.5, but still used for backward compatibility)
    /// </summary>
    public class QRD : ISegment
    {
        public string SegmentId => nameof(QRD);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// QRD.1 - Query Date/Time
        /// </summary>
        public DTM QueryDateTime { get; set; }

        /// <summary>
        /// QRD.2 - Query Format Code
        /// </summary>
        public ID QueryFormatCode { get; set; }

        /// <summary>
        /// QRD.3 - Query Priority
        /// </summary>
        public ID QueryPriority { get; set; }

        /// <summary>
        /// QRD.4 - Query ID
        /// </summary>
        public ST QueryID { get; set; }

        /// <summary>
        /// QRD.5 - Deferred Response Type
        /// </summary>
        public ID DeferredResponseType { get; set; }

        /// <summary>
        /// QRD.6 - Deferred Response Date/Time
        /// </summary>
        public DTM DeferredResponseDateTime { get; set; }

        /// <summary>
        /// QRD.7 - Quantity Limited Request
        /// </summary>
        public CQ QuantityLimitedRequest { get; set; }

        /// <summary>
        /// QRD.8 - Who Subject Filter (repeating)
        /// </summary>
        public XCN[] WhoSubjectFilter { get; set; }

        /// <summary>
        /// QRD.9 - What Subject Filter (repeating)
        /// </summary>
        public CE[] WhatSubjectFilter { get; set; }

        /// <summary>
        /// QRD.10 - What Department Data Code (repeating)
        /// </summary>
        public CE[] WhatDepartmentDataCode { get; set; }

        /// <summary>
        /// QRD.11 - What Data Code Value Qualifier (repeating)
        /// </summary>
        public ST[] WhatDataCodeValueQualifier { get; set; }

        /// <summary>
        /// QRD.12 - Query Results Level
        /// </summary>
        public ID QueryResultsLevel { get; set; }

        public QRD()
        {
            SegmentType = SegmentType.Universal;
            QueryDateTime = default;
            QueryFormatCode = default;
            QueryPriority = default;
            QueryID = default;
            DeferredResponseType = default;
            DeferredResponseDateTime = default;
            QuantityLimitedRequest = default;
            WhoSubjectFilter = [];
            WhatSubjectFilter = [];
            WhatDepartmentDataCode = [];
            WhatDataCodeValueQualifier = [];
            QueryResultsLevel = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: QueryDateTime = new DTM(value); break;
                case 2: QueryFormatCode = new ID(value); break;
                case 3: QueryPriority = new ID(value); break;
                case 4: QueryID = new ST(value); break;
                case 5: DeferredResponseType = new ID(value); break;
                case 6: DeferredResponseDateTime = new DTM(value); break;
                case 7:
                    var cq7 = new CQ();
                    cq7.Parse(value.AsSpan(), delimiters);
                    QuantityLimitedRequest = cq7;
                    break;
                case 8: WhoSubjectFilter = SegmentFieldHelper.ParseRepeatingField<XCN>(value, delimiters); break;
                case 9: WhatSubjectFilter = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 10: WhatDepartmentDataCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 11: WhatDataCodeValueQualifier = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 12: QueryResultsLevel = new ID(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                QueryDateTime.ToHL7String(delimiters),
                QueryFormatCode.ToHL7String(delimiters),
                QueryPriority.ToHL7String(delimiters),
                QueryID.ToHL7String(delimiters),
                DeferredResponseType.ToHL7String(delimiters),
                DeferredResponseDateTime.ToHL7String(delimiters),
                QuantityLimitedRequest.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhoSubjectFilter, delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhatSubjectFilter, delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhatDepartmentDataCode, delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhatDataCodeValueQualifier, delimiters),
                QueryResultsLevel.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => QueryDateTime.Value,
                2 => QueryFormatCode.Value,
                3 => QueryPriority.Value,
                4 => QueryID.Value,
                5 => DeferredResponseType.Value,
                6 => DeferredResponseDateTime.Value,
                7 => QuantityLimitedRequest.ToHL7String(delimiters),
                8 => SegmentFieldHelper.JoinRepeatingField(WhoSubjectFilter, delimiters),
                9 => SegmentFieldHelper.JoinRepeatingField(WhatSubjectFilter, delimiters),
                10 => SegmentFieldHelper.JoinRepeatingField(WhatDepartmentDataCode, delimiters),
                11 => SegmentFieldHelper.JoinRepeatingField(WhatDataCodeValueQualifier, delimiters),
                12 => QueryResultsLevel.Value,
                _ => null
            };
        }
    }
}
