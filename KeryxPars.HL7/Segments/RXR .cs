using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy/Treatment Route
    /// </summary>
    public class RXR : ISegment
    {
        public string SegmentId => nameof(RXR);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXR.1
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// RXR.2
        /// </summary>
        public string AdministrationSite { get; set; }

        /// <summary>
        /// RXR.3
        /// </summary>
        public string AdministrationDevice { get; set; }

        /// <summary>
        /// RXR.4
        /// </summary>
        public string AdministrationMethod { get; set; }

        /// <summary>
        /// RXR.5
        /// </summary>
        public string RoutingInstruction { get; set; }

        /// <summary>
        /// RXR.6
        /// </summary>
        public string AdministrationSiteModifier { get; set; }

        // Constructors
        public RXR()
        {
            SegmentType = SegmentType.MedOrder;
            Route = string.Empty;
            AdministrationSite = string.Empty;
            AdministrationDevice = string.Empty;
            AdministrationMethod = string.Empty;
            RoutingInstruction = string.Empty;
            AdministrationSiteModifier = string.Empty;
        }

        // Methods
        public void SetValue(string value, int element)
        {
            switch (element)
            {
                case 1: Route = value; break;
                case 2: AdministrationSite = value; break;
                case 3: AdministrationDevice = value; break;
                case 4: AdministrationMethod = value; break;
                case 5: RoutingInstruction = value; break;
                case 6: AdministrationSiteModifier = value; break;
            }
        }
        
        public string[] GetValues()
        {
            return
            [
                SegmentId,
                Route,
                AdministrationSite,
                AdministrationDevice,
                AdministrationMethod,
                RoutingInstruction,
                AdministrationSiteModifier
            ];
        }

        public string? GetField(int index)
        {
            return index switch
            {
                0 => SegmentId,
                1 => Route,
                2 => AdministrationSite,
                3 => AdministrationDevice,
                4 => AdministrationMethod,
                5 => RoutingInstruction,
                6 => AdministrationSiteModifier,
                _ => null
            };
        }
    }
}
