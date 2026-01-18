using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy Component (Compounds)
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class RXC : ISegment
    {
        public string SegmentId => nameof(RXC);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXC.1 - RX Component Type
        /// </summary>
        public ID RxComponentType { get; set; }

        /// <summary>
        /// RXC.2 - Component Code
        /// </summary>
        public CE ComponentCode { get; set; }

        /// <summary>
        /// RXC.3 - Component Amount
        /// </summary>
        public NM ComponentAmount { get; set; }

        /// <summary>
        /// RXC.4 - Component Units
        /// </summary>
        public CE ComponentUnits { get; set; }

        /// <summary>
        /// RXC.5 - Component Strength
        /// </summary>
        public NM ComponentStrength { get; set; }

        /// <summary>
        /// RXC.6 - Component Strength Units
        /// </summary>
        public CE ComponentStrengthUnits { get; set; }

        /// <summary>
        /// RXC.7 - Supplementary Code
        /// </summary>
        public CE[] SupplementaryCode { get; set; }

        /// <summary>
        /// RXC.8 - Component Drug Strength Volume
        /// </summary>
        public NM ComponentDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXC.9 - Component Drug Strength Volume Units
        /// </summary>
        public CWE ComponentDrugStrengthVolumeUnits { get; set; }

        public RXC()
        {
            SegmentType = SegmentType.MedOrder;
            RxComponentType = default;
            ComponentCode = default;
            ComponentAmount = default;
            ComponentUnits = default;
            ComponentStrength = default;
            ComponentStrengthUnits = default;
            SupplementaryCode = [];
            ComponentDrugStrengthVolume = default;
            ComponentDrugStrengthVolumeUnits = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1: RxComponentType = new ID(value); break;
                case 2:
                    var ce2 = new CE();
                    ce2.Parse(value.AsSpan(), delimiters);
                    ComponentCode = ce2;
                    break;
                case 3: ComponentAmount = new NM(value); break;
                case 4:
                    var ce4 = new CE();
                    ce4.Parse(value.AsSpan(), delimiters);
                    ComponentUnits = ce4;
                    break;
                case 5: ComponentStrength = new NM(value); break;
                case 6:
                    var ce6 = new CE();
                    ce6.Parse(value.AsSpan(), delimiters);
                    ComponentStrengthUnits = ce6;
                    break;
                case 7:
                    SupplementaryCode = SegmentFieldHelper.ParseRepeatingField<CE>(value, delimiters);
                    break;
                case 8: ComponentDrugStrengthVolume = new NM(value); break;
                case 9:
                    var cwe9 = new CWE();
                    cwe9.Parse(value.AsSpan(), delimiters);
                    ComponentDrugStrengthVolumeUnits = cwe9;
                    break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                RxComponentType.ToHL7String(delimiters),
                ComponentCode.ToHL7String(delimiters),
                ComponentAmount.ToHL7String(delimiters),
                ComponentUnits.ToHL7String(delimiters),
                ComponentStrength.ToHL7String(delimiters),
                ComponentStrengthUnits.ToHL7String(delimiters),
                SegmentFieldHelper.JoinRepeatingField(SupplementaryCode, delimiters),
                ComponentDrugStrengthVolume.ToHL7String(delimiters),
                ComponentDrugStrengthVolumeUnits.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => RxComponentType.Value,
                2 => ComponentCode.ToHL7String(delimiters),
                3 => ComponentAmount.Value,
                4 => ComponentUnits.ToHL7String(delimiters),
                5 => ComponentStrength.Value,
                6 => ComponentStrengthUnits.ToHL7String(delimiters),
                7 => SegmentFieldHelper.JoinRepeatingField(SupplementaryCode, delimiters),
                8 => ComponentDrugStrengthVolume.Value,
                9 => ComponentDrugStrengthVolumeUnits.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
