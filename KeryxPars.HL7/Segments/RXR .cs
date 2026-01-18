using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.HL7.Segments
{
    /// <summary>
    /// HL7 Segment: Pharmacy/Treatment Route
    /// Refactored to use strongly-typed HL7 datatypes.
    /// </summary>
    public class RXR : ISegment
    {
        public string SegmentId => nameof(RXR);
        
        public SegmentType SegmentType { get; private set; }

        /// <summary>
        /// RXR.1 - Route
        /// </summary>
        public CE Route { get; set; }

        /// <summary>
        /// RXR.2 - Administration Site
        /// </summary>
        public CWE AdministrationSite { get; set; }

        /// <summary>
        /// RXR.3 - Administration Device
        /// </summary>
        public CE AdministrationDevice { get; set; }

        /// <summary>
        /// RXR.4 - Administration Method
        /// </summary>
        public CWE AdministrationMethod { get; set; }

        /// <summary>
        /// RXR.5 - Routing Instruction
        /// </summary>
        public CE RoutingInstruction { get; set; }

        /// <summary>
        /// RXR.6 - Administration Site Modifier
        /// </summary>
        public CWE AdministrationSiteModifier { get; set; }

        public RXR()
        {
            SegmentType = SegmentType.MedOrder;
            Route = default;
            AdministrationSite = default;
            AdministrationDevice = default;
            AdministrationMethod = default;
            RoutingInstruction = default;
            AdministrationSiteModifier = default;
        }

        public void SetValue(string value, int element)
        {
            var delimiters = HL7Delimiters.Default;
            
            switch (element)
            {
                case 1:
                    var ce1 = new CE();
                    ce1.Parse(value.AsSpan(), delimiters);
                    Route = ce1;
                    break;
                case 2:
                    var cwe2 = new CWE();
                    cwe2.Parse(value.AsSpan(), delimiters);
                    AdministrationSite = cwe2;
                    break;
                case 3:
                    var ce3 = new CE();
                    ce3.Parse(value.AsSpan(), delimiters);
                    AdministrationDevice = ce3;
                    break;
                case 4:
                    var cwe4 = new CWE();
                    cwe4.Parse(value.AsSpan(), delimiters);
                    AdministrationMethod = cwe4;
                    break;
                case 5:
                    var ce5 = new CE();
                    ce5.Parse(value.AsSpan(), delimiters);
                    RoutingInstruction = ce5;
                    break;
                case 6:
                    var cwe6 = new CWE();
                    cwe6.Parse(value.AsSpan(), delimiters);
                    AdministrationSiteModifier = cwe6;
                    break;
            }
        }
        
        public string[] GetValues()
        {
            var delimiters = HL7Delimiters.Default;
            
            return
            [
                SegmentId,
                Route.ToHL7String(delimiters),
                AdministrationSite.ToHL7String(delimiters),
                AdministrationDevice.ToHL7String(delimiters),
                AdministrationMethod.ToHL7String(delimiters),
                RoutingInstruction.ToHL7String(delimiters),
                AdministrationSiteModifier.ToHL7String(delimiters)
            ];
        }

        public string? GetField(int index)
        {
            var delimiters = HL7Delimiters.Default;
            
            return index switch
            {
                0 => SegmentId,
                1 => Route.ToHL7String(delimiters),
                2 => AdministrationSite.ToHL7String(delimiters),
                3 => AdministrationDevice.ToHL7String(delimiters),
                4 => AdministrationMethod.ToHL7String(delimiters),
                5 => RoutingInstruction.ToHL7String(delimiters),
                6 => AdministrationSiteModifier.ToHL7String(delimiters),
                _ => null
            };
        }
    }
}
