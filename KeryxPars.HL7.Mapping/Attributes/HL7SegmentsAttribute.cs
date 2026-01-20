using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Maps a collection property to repeating HL7 segments.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class HL7SegmentsAttribute : Attribute
{
    /// <summary>
    /// Segment ID for repeating segments (e.g., "OBX").
    /// </summary>
    public string SegmentId { get; }

    /// <summary>
    /// Optional converter for each segment instance.
    /// </summary>
    public Type? Converter { get; set; }

    /// <summary>
    /// Filter condition for segments. Only segments matching this condition are mapped.
    /// Example: "OBX.2 == NM" (only numeric observations)
    /// </summary>
    public string? Where { get; set; }

    /// <summary>
    /// Maximum number of segments to map. Useful for limiting collection size.
    /// </summary>
    public int? MaxCount { get; set; }

    /// <summary>
    /// Maps a collection property to repeating segments.
    /// </summary>
    /// <param name="segmentId">Segment identifier (e.g., "OBX")</param>
    public HL7SegmentsAttribute(string segmentId)
    {
        if (string.IsNullOrWhiteSpace(segmentId))
        {
            throw new ArgumentException("Segment ID cannot be null or empty", nameof(segmentId));
        }

        SegmentId = segmentId.ToUpperInvariant();
    }
}
