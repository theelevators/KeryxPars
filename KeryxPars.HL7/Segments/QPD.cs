using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Query Parameter Definition
    /// </summary>
    public class QPD : ISegment
    {
        public string SegmentId => nameof(QPD);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// QPD.1 - Message Query Name
        /// </summary>
        public CE MessageQueryName { get; set; }

        /// <summary>
        /// QPD.2 - Query Tag
        /// </summary>
        public ST QueryTag { get; set; }

        /// <summary>
        /// QPD.3 - User Parameters (in successive fields)
        /// This is a variable field that contains query-specific parameters
        /// </summary>
        private List<ST> _userParameters = new();
        public ST[] UserParameters
        {
            get => _userParameters.ToArray();
            set => _userParameters = new List<ST>(value);
        }

        public QPD()
        {
            SegmentType = SegmentType.Universal;
            MessageQueryName = default;
            QueryTag = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ce1 = new CE();
                    ce1.Parse(value.AsSpan(), delimiters);
                    MessageQueryName = ce1;
                    break;
                case 2: QueryTag = new ST(value); break;
                default:
                    // QPD.3+ are user-defined parameters
                    if (element >= 3)
                    {
                        var paramIndex = element - 3;
                        while (_userParameters.Count <= paramIndex)
                        {
                            _userParameters.Add(default);
                        }
                        _userParameters[paramIndex] = new ST(value);
                    }
                    break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            var values = new List<string>
            {
                SegmentId,
                MessageQueryName.ToHL7String(delimiters),
                QueryTag.ToHL7String(delimiters)
            };
            
            foreach (var param in _userParameters)
            {
                values.Add(param.ToHL7String(delimiters));
            }
            
            return values.ToArray();
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => MessageQueryName.ToHL7String(delimiters),
                2 => QueryTag.Value,
                _ when index >= 3 && index - 3 < _userParameters.Count => _userParameters[index - 3].Value,
                _ => null
            };
        }
    }
}
