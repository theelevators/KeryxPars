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
