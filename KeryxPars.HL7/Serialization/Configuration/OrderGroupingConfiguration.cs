namespace KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Defines how segments should be grouped into order structures
/// </summary>
public sealed class OrderGroupingConfiguration
{
    /// <summary>
    /// Segment ID that triggers creation of a new order group (e.g., "ORC", "OBR")
    /// </summary>
    public string TriggerSegmentId { get; init; } = "ORC";

    /// <summary>
    /// Segment IDs that should be added as single detail segments
    /// </summary>
    public HashSet<string> DetailSegmentIds { get; init; } = new() { "RXE", "RXO", "OBR" };

    /// <summary>
    /// Segment IDs that can repeat within an order group
    /// </summary>
    public HashSet<string> RepeatableSegmentIds { get; init; } = new() { "RXR", "RXC", "TQ1", "NTE", "OBX" };

    /// <summary>
    /// Order type label for this configuration
    /// </summary>
    public string OrderType { get; init; } = "Medication";

    /// <summary>
    /// Predefined medication order configuration
    /// </summary>
    public static OrderGroupingConfiguration Medication { get; } = new()
    {
        TriggerSegmentId = "ORC",
        DetailSegmentIds = new HashSet<string> { "RXE", "RXO" },
        RepeatableSegmentIds = new HashSet<string> { "RXR", "RXC", "TQ1", "NTE" },
        OrderType = "Medication"
    };

    /// <summary>
    /// Predefined lab/observation order configuration
    /// </summary>
    public static OrderGroupingConfiguration Lab { get; } = new()
    {
        TriggerSegmentId = "ORC",
        DetailSegmentIds = new HashSet<string> { "OBR" },
        RepeatableSegmentIds = new HashSet<string> { "OBX", "NTE", "TQ1" },
        OrderType = "Lab"
    };

    /// <summary>
    /// Predefined imaging order configuration
    /// </summary>
    public static OrderGroupingConfiguration Imaging { get; } = new()
    {
        TriggerSegmentId = "ORC",
        DetailSegmentIds = new HashSet<string> { "OBR", "IPC" },
        RepeatableSegmentIds = new HashSet<string> { "OBX", "NTE" },
        OrderType = "Imaging"
    };

    /// <summary>
    /// Checks if the segment should trigger a new order group
    /// </summary>
    public bool IsTriggerSegment(ReadOnlySpan<char> segmentId) 
        => segmentId.SequenceEqual(TriggerSegmentId.AsSpan());

    /// <summary>
    /// Checks if the segment belongs to an order group
    /// </summary>
    public bool BelongsToOrderGroup(ReadOnlySpan<char> segmentId)
    {
        var segId = segmentId.ToString();
        return segId == TriggerSegmentId 
            || DetailSegmentIds.Contains(segId) 
            || RepeatableSegmentIds.Contains(segId);
    }
}