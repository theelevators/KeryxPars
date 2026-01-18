namespace KeryxPars.HL7.Contracts;

/// <summary>
/// Registry for managing segment converters and metadata.
/// </summary>
public interface ISegmentRegistry
{
    /// <summary>
    /// Registers a converter for a specific segment type.
    /// </summary>
    void Register(ISegmentConverter converter);

    /// <summary>
    /// Gets a converter for the specified segment ID.
    /// </summary>
    /// <param name="segmentId">The segment identifier (e.g., "MSH", "PID").</param>
    /// <returns>The converter if found; otherwise, null.</returns>
    ISegmentConverter? GetConverter(ReadOnlySpan<char> segmentId);

    /// <summary>
    /// Checks if a converter is registered for the specified segment ID.
    /// </summary>
    bool IsRegistered(ReadOnlySpan<char> segmentId);

    /// <summary>
    /// Gets metadata for the specified segment ID.
    /// </summary>
    SegmentMetadata? GetMetadata(ReadOnlySpan<char> segmentId);
}

/// <summary>
/// Metadata describing segment characteristics.
/// </summary>
public sealed record SegmentMetadata(
    string SegmentId,
    bool IsRepeatable,
    bool IsRequired,
    int? MaxOccurrences = null,
    string? Description = null);