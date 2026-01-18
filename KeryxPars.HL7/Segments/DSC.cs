using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Continuation Pointer
    /// </summary>
    public class DSC : ISegment
    {
        public string SegmentId => nameof(DSC);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// DSC.1 - Continuation Pointer
        /// </summary>
        public ST ContinuationPointer { get; set; }

        /// <summary>
        /// DSC.2 - Continuation Style
        /// </summary>
        public ID ContinuationStyle { get; set; }

        public DSC()
        {
            SegmentType = SegmentType.Universal;
            ContinuationPointer = default;
            ContinuationStyle = default;
        }

        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: ContinuationPointer = new ST(value); break;
                case 2: ContinuationStyle = new ID(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                ContinuationPointer.ToHL7String(delimiters),
                ContinuationStyle.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => ContinuationPointer.Value,
                2 => ContinuationStyle.Value,
                _ => null
            };
        }
    }
}
