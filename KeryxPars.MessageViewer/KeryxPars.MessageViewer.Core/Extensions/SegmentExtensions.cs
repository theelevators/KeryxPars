using KeryxPars.HL7.Contracts;

namespace KeryxPars.MessageViewer.Core.Extensions;

public static class SegmentExtensions
{
    public static bool IsEmpty(this ISegment? segment)
    {
        if (segment == null)
            return true;

        var values = segment.GetValues();
        
        // Skip the first element (segment ID) and check if all others are empty
        for (int i = 1; i < values.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(values[i]))
                return false;
        }

        return true;
    }
}
