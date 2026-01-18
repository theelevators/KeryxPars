using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy Component (Compounds)
    /// </summary>
    public class RXC : ISegment
    {
        public string SegmentId => nameof(RXC);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXC.1
        /// </summary>
        public string RxComponentType { get; set; } // used for medselect

        /// <summary>
        /// RXC.2
        /// </summary>
        public string ComponentCode { get; set; } // used for medselect

        /// <summary>
        /// RXC.3
        /// </summary>
        public string ComponentAmount { get; set; } // used for medselect

        /// <summary>
        /// RXC.4
        /// </summary>
        public string ComponentUnits { get; set; } // used for medselect

        /// <summary>
        /// RXC.5
        /// </summary>
        public string ComponentStrength { get; set; }

        /// <summary>
        /// RXC.6
        /// </summary>
        public string ComponentStrengthUnits { get; set; }

        /// <summary>
        /// RXC.7
        /// </summary>
        public string SupplementaryCode { get; set; }

        /// <summary>
        /// RXC.8
        /// </summary>
        public string ComponentDrugStrengthVolume { get; set; }

        /// <summary>
        /// RXC.9
        /// </summary>
        public string ComponentDrugStrengthVolumeUnits { get; set; }

        // Constructors
        public RXC()
        {
            SegmentType = SegmentType.MedOrder;
            RxComponentType = string.Empty;
            ComponentCode = string.Empty;
            ComponentAmount = string.Empty;
            ComponentUnits = string.Empty;
            ComponentStrength = string.Empty;
            ComponentStrengthUnits = string.Empty;
            SupplementaryCode = string.Empty;
            ComponentDrugStrengthVolume = string.Empty;
            ComponentDrugStrengthVolumeUnits = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: RxComponentType = value; break; // used for medselect
                case 2: ComponentCode = value; break;// used for medselect
                case 3: ComponentAmount = value; break;// used for medselect
                case 4: ComponentUnits = value; break;// used for medselect
                case 5: ComponentStrength = value; break;
                case 6: ComponentStrengthUnits = value; break;
                case 7: SupplementaryCode = value; break;
                case 8: ComponentDrugStrengthVolume = value; break;
                case 9: ComponentDrugStrengthVolumeUnits = value; break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                RxComponentType,
                ComponentCode,
                ComponentAmount,
                ComponentUnits,
                ComponentStrength,
                ComponentStrengthUnits,
                SupplementaryCode,
                ComponentDrugStrengthVolume,
                ComponentDrugStrengthVolumeUnits
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => RxComponentType,
                2 => ComponentCode,
                3 => ComponentAmount,
                4 => ComponentUnits,
                5 => ComponentStrength,
                6 => ComponentStrengthUnits,
                7 => SupplementaryCode,
                8 => ComponentDrugStrengthVolume,
                9 => ComponentDrugStrengthVolumeUnits,
                _ => null
            };
        }
    }
}
