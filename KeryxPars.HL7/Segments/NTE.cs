using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Notes and Comments
    /// </summary>
    public class NTE : ISegment
    {
        public string SegmentId => nameof(NTE);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// NTE.1 - Set ID - NTE
        /// </summary>
        public SI SetID { get; set; }

        /// <summary>
        /// NTE.2 - Source of Comment
        /// </summary>
        public ID SourceOfComment { get; set; }

        /// <summary>
        /// NTE.3 - Comment (repeating)
        /// </summary>
        public FT[] Comment { get; set; }

        /// <summary>
        /// NTE.4 - Comment Type
        /// </summary>
        public CE CommentType { get; set; }

        public NTE()
        {
            SegmentType = SegmentType.Universal;
            SetID = default;
            SourceOfComment = default;
            Comment = [];
            CommentType = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: SetID = new SI(value); break;
                case 2: SourceOfComment = new ID(value); break;
                case 3: Comment = SegmentFieldHelper.ParseRepeatingField<FT>(value, delimiters); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    CommentType = ce4;
                    break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                SetID.ToHL7String(delimiters),
                SourceOfComment.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(Comment, delimiters),
                CommentType.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => SetID.Value,
                2 => SourceOfComment.Value,
                3 => SegmentFieldHelper.JoinRepeatingField(Comment, delimiters),
                4 => CommentType.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
