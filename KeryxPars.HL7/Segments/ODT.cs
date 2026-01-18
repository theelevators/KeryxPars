using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Diet Tray Instructions
    /// </summary>
    public class ODT : ISegment
    {
        public string SegmentId => nameof(ODT);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// ODT.1 - Tray Type
        /// </summary>
        public CE TrayType { get; set; }

        /// <summary>
        /// ODT.2 - Service Period (repeating)
        /// </summary>
        public CE[] ServicePeriod { get; set; }

        /// <summary>
        /// ODT.3 - Text Instruction
        /// </summary>
        public ST TextInstruction { get; set; }

        public ODT()
        {
            SegmentType = SegmentType.Universal;
            TrayType = default;
            ServicePeriod = [];
            TextInstruction = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ce1 = new CE();
                    ce1.Parse(value.AsSpan(), delimiters);
                    TrayType = ce1;
                    break;
                case 2: ServicePeriod = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters); break;
                case 3: TextInstruction = new ST(value); break;
            }
        }

        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                TrayType.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(ServicePeriod, delimiters),
                TextInstruction.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => TrayType.ToHL7String(delimiters),
                2 => SegmentFieldHelper.JoinRepeatingField(ServicePeriod, delimiters),
                3 => TextInstruction.Value,
                _ => null
            };
        }
    }
}
