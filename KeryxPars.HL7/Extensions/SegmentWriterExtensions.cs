namespace KeryxPars.HL7.Extensions;

using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Extension methods for SegmentWriter to write segments and collections.
/// Encapsulates the writing logic for different segment types.
/// </summary>
internal static class SegmentWriterExtensions
{
    /// <summary>
    /// Writes a single segment if it's present and has data.
    /// </summary>
    internal static void WriteSegmentIfPresent(
        this ref SegmentWriter writer,
        ISegment? segment,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        if (segment == null)
            return;

        var values = segment.GetValues();
        if (values.Length > 1 && values[1..].Any(v => !string.IsNullOrEmpty(v)))
        {
            var converter = options.SegmentRegistry.GetConverter(segment.SegmentId);
            converter?.Write(segment, ref writer, in delimiters, options);
            writer.Write("\r\n");
        }
    }

    /// <summary>
    /// Writes a collection of segments.
    /// </summary>
    internal static void WriteSegmentCollection<T>(
        this ref SegmentWriter writer,
        IEnumerable<T> segments,
        in HL7Delimiters delimiters,
        SerializerOptions options) where T : ISegment
    {
        foreach (var segment in segments)
            writer.WriteSegmentIfPresent(segment, in delimiters, options);
    }

    /// <summary>
    /// Writes a collection of order groups.
    /// </summary>
    internal static void WriteOrderGroups(
        this ref SegmentWriter writer,
        IEnumerable<OrderGroup> orderGroups,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        foreach (var orderGroup in orderGroups)
            writer.WriteOrderGroup(orderGroup, in delimiters, options);
    }

    /// <summary>
    /// Writes a single order group with all its segments.
    /// </summary>
    internal static void WriteOrderGroup(
        this ref SegmentWriter writer,
        OrderGroup orderGroup,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        // Write primary segment (e.g., ORC)
        writer.WriteSegmentIfPresent(orderGroup.PrimarySegment, in delimiters, options);

        // Write detail segments (RXE, RXO, OBR, etc.)
        foreach (var detailSegment in orderGroup.DetailSegments.Values)
            writer.WriteSegmentIfPresent(detailSegment, in delimiters, options);

        // Write repeatable segments (RXR, RXC, TQ1, OBX, NTE, etc.)
        foreach (var segmentList in orderGroup.RepeatableSegments.Values)
            writer.WriteSegmentCollection(segmentList, in delimiters, options);
    }
}
