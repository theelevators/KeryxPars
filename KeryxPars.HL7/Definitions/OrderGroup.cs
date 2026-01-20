using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace KeryxPars.HL7.Definitions;

/// <summary>
/// Generic container for grouped order-related segments.
/// Supports dynamic grouping based on trigger segments defined in SerializerOptions.
/// </summary>
public class OrderGroup
{
    /// <summary>
    /// The type of order this group represents (e.g., "Medication", "Lab", "Imaging")
    /// </summary>
    public string OrderType { get; set; } = "Medication";

    /// <summary>
    /// Configuration that defines which segments are repeatable vs detail
    /// </summary>
    internal OrderGroupingConfiguration? Configuration { get; set; }

    private Dictionary<string, bool>? _segmentStatus;
    private Dictionary<string, ISegment>? _detailSegments;
    private Dictionary<string, List<ISegment>>? _repeatableSegments;


    /// <summary>
    /// Indicates if specific segments have been processed
    /// </summary>
    public Dictionary<string, bool> SegmentStatus
    {
        get => _segmentStatus ??= [];
        set => _segmentStatus = value;
    }

    /// <summary>
    /// Primary order control segment (typically ORC, but configurable)
    /// </summary>
    public ISegment? PrimarySegment { get; set; }

    /// <summary>
    /// Secondary order detail segments (RXE, RXO, OBR, etc.)
    /// </summary>
    public Dictionary<string, ISegment> DetailSegments
    {
        get => _detailSegments ??= [];
        set => _detailSegments = value;
    }

    /// <summary>
    /// Repeatable segments associated with this order
    /// </summary>
    public Dictionary<string, List<ISegment>> RepeatableSegments
    {
        get => _repeatableSegments ??= [];
        set => _repeatableSegments = value;
    }

    /// <summary>
    /// Adds a segment to the appropriate collection based on its type
    /// </summary>
    public void AddSegment(ISegment segment)
    {
        var segmentId = segment.SegmentId.ToString();
        SegmentStatus[segmentId] = true;

        // Determine if this segment is repeatable based on configuration
        bool isRepeatable = Configuration?.RepeatableSegmentIds.Contains(segmentId) ?? false;
        bool isDetail = Configuration?.DetailSegmentIds.Contains(segmentId) ?? false;

        if (isRepeatable)
        {
            // Add to repeatable segments list
            if (!RepeatableSegments.TryGetValue(segmentId, out var list))
            {
                list = [];
                RepeatableSegments[segmentId] = list;
            }
            list.Add(segment);
        }
        else if (isDetail)
        {
            // Add as single detail segment (will overwrite if multiple exist)
            DetailSegments[segmentId] = segment;
        }
        else
        {
            // Unknown segment type - treat as detail
            DetailSegments[segmentId] = segment;
        }
    }


    /// <summary>
    /// Gets all segments of a specific type
    /// </summary>
    public bool TryGetRepeatableSegments<T>(string segmentId, [NotNullWhen(true)] out List<T>? segments) where T : ISegment
    {
        if (!RepeatableSegments.TryGetValue(segmentId, out var segs))
        {
            segs = [];
            RepeatableSegments[segmentId] = segs;
        }
        segments = [.. segs.OfType<T>()];
        return segments.Count > 0;
    }

    /// <summary>
    /// Checks if a specific segment type has been processed
    /// </summary>
    public bool HasSegment(string segmentId) => _segmentStatus?.GetValueOrDefault(segmentId, false) ?? false;

    public bool TryGetSegment<T>(string segmentId, [NotNullWhen(true)] out T? segment)
        where T : ISegment
    {
        DetailSegments.TryGetValue(segmentId, out ISegment? seg);
        segment = (T?)seg;
        return segment != null;
    }
}