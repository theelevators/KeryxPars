
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;

namespace KeryxPars.HL7.Extensions;

public static class GroupingExtensions
{
    public static MedicationOrder AsMedicationOrder(this OrderGroup group)
    {

        var medicationOrder = new MedicationOrder();


        if (group.TryGetSegment<ORC>("ORC", out var orc))
        {
            medicationOrder.Orc = orc;
        }

        if (group.TryGetSegment<RXE>("RXE", out var rxe))
        {
            medicationOrder.Rxe = rxe;
        }

        if (group.TryGetSegment<RXO>("RXO", out var rxo))
        {
            medicationOrder.Rxo = rxo;
        }

        if (group.TryGetRepeatableSegments<RXC>("RXC", out var rxc))
        {
            medicationOrder.RXC.AddRange(rxc);
        }

        if (group.TryGetRepeatableSegments<RXR>("RXR", out var rxr))
        {
            medicationOrder.RXR.AddRange(rxr);
        }

        if (group.TryGetSegment<TQ1>("TQ1", out var tq1))
        {
            medicationOrder.TQ1.Add(tq1);
        }

        return medicationOrder;
    }
}



