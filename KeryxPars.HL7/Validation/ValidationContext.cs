using System.Runtime.CompilerServices;

namespace KeryxPars.HL7.Validation;

/// <summary>
/// Minimal context for validation operations - using readonly struct for zero allocation.
/// </summary>
public readonly record struct ValidationContext(
    object Message,
    string? SegmentId = null,
    int? FieldIndex = null,
    object? FieldValue = null,
    int SegmentIndex = 0
)
{
    /// <summary>
    /// Creates a context for segment-level validation.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationContext WithSegment(string segmentId, int index = 0) =>
        this with { SegmentId = segmentId, SegmentIndex = index };

    /// <summary>
    /// Creates a context for field-level validation.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationContext WithField(int fieldIndex, object? value) =>
        this with { FieldIndex = fieldIndex, FieldValue = value };
}

