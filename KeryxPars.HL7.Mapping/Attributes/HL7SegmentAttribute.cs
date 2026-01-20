using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Maps a property to an entire HL7 segment.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class HL7SegmentAttribute : Attribute
{
    /// <summary>
    /// Segment ID (e.g., "PID", "MSH", "OBX").
    /// </summary>
    public string SegmentId { get; }

    /// <summary>
    /// Optional converter for the entire segment.
    /// </summary>
    public Type? Converter { get; set; }

    /// <summary>
    /// Maps a property to an HL7 segment.
    /// </summary>
    /// <param name="segmentId">Segment identifier (e.g., "PID")</param>
    public HL7SegmentAttribute(string segmentId)
    {
        if (string.IsNullOrWhiteSpace(segmentId))
        {
            throw new ArgumentException("Segment ID cannot be null or empty", nameof(segmentId));
        }

        SegmentId = segmentId.ToUpperInvariant();
    }
}
