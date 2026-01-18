using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Query Filter (Deprecated in v2.5, but still used for backward compatibility)
    /// </summary>
    public class QRF : ISegment
    {
        public string SegmentId => nameof(QRF);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// QRF.1 - Where Subject Filter (repeating)
        /// </summary>
        public ST[] WhereSubjectFilter { get; set; }

        /// <summary>
        /// QRF.2 - When Data Start Date/Time
        /// </summary>
        public DTM WhenDataStartDateTime { get; set; }

        /// <summary>
        /// QRF.3 - When Data End Date/Time
        /// </summary>
        public DTM WhenDataEndDateTime { get; set; }

        /// <summary>
        /// QRF.4 - What User Qualifier (repeating)
        /// </summary>
        public ST[] WhatUserQualifier { get; set; }

        /// <summary>
        /// QRF.5 - Other QRY Subject Filter (repeating)
        /// </summary>
        public ST[] OtherQRYSubjectFilter { get; set; }

        /// <summary>
        /// QRF.6 - Which Date/Time Qualifier (repeating)
        /// </summary>
        public ID[] WhichDateTimeQualifier { get; set; }

        /// <summary>
        /// QRF.7 - Which Date/Time Status Qualifier (repeating)
        /// </summary>
        public ID[] WhichDateTimeStatusQualifier { get; set; }

        /// <summary>
        /// QRF.8 - Date/Time Selection Qualifier (repeating)
        /// </summary>
        public ID[] DateTimeSelectionQualifier { get; set; }

        /// <summary>
        /// QRF.9 - When Quantity/Timing Qualifier
        /// </summary>
        public ST WhenQuantityTimingQualifier { get; set; }

        public QRF()
        {
            SegmentType = SegmentType.Universal;
            WhereSubjectFilter = [];
            WhenDataStartDateTime = default;
            WhenDataEndDateTime = default;
            WhatUserQualifier = [];
            OtherQRYSubjectFilter = [];
            WhichDateTimeQualifier = [];
            WhichDateTimeStatusQualifier = [];
            DateTimeSelectionQualifier = [];
            WhenQuantityTimingQualifier = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: WhereSubjectFilter = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 2: WhenDataStartDateTime = new DTM(value); break;
                case 3: WhenDataEndDateTime = new DTM(value); break;
                case 4: WhatUserQualifier = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 5: OtherQRYSubjectFilter = SegmentFieldHelper.ParseRepeatingField<ST>(value, delimiters); break;
                case 6: WhichDateTimeQualifier = SegmentFieldHelper.ParseRepeatingField<ID>(value, delimiters); break;
                case 7: WhichDateTimeStatusQualifier = SegmentFieldHelper.ParseRepeatingField<ID>(value, delimiters); break;
                case 8: DateTimeSelectionQualifier = SegmentFieldHelper.ParseRepeatingField<ID>(value, delimiters); break;
                case 9: WhenQuantityTimingQualifier = new ST(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SegmentFieldHelper.JoinRepeatingField(WhereSubjectFilter, delimiters),
                WhenDataStartDateTime.ToHL7String(delimiters),
                WhenDataEndDateTime.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhatUserQualifier, delimiters),
                SegmentFieldHelper.JoinRepeatingField(OtherQRYSubjectFilter, delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhichDateTimeQualifier, delimiters),
                SegmentFieldHelper.JoinRepeatingField(WhichDateTimeStatusQualifier, delimiters),
                SegmentFieldHelper.JoinRepeatingField(DateTimeSelectionQualifier, delimiters),
                WhenQuantityTimingQualifier.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SegmentFieldHelper.JoinRepeatingField(WhereSubjectFilter, delimiters),
                2 => WhenDataStartDateTime.Value,
                3 => WhenDataEndDateTime.Value,
                4 => SegmentFieldHelper.JoinRepeatingField(WhatUserQualifier, delimiters),
                5 => SegmentFieldHelper.JoinRepeatingField(OtherQRYSubjectFilter, delimiters),
                6 => SegmentFieldHelper.JoinRepeatingField(WhichDateTimeQualifier, delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(WhichDateTimeStatusQualifier, delimiters),
                8 => SegmentFieldHelper.JoinRepeatingField(DateTimeSelectionQualifier, delimiters),
                9 => WhenQuantityTimingQualifier.Value,
                _ => null
            };
        }
    }
}
