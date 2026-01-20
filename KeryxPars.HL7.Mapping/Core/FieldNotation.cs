using System;
using System.Text.RegularExpressions;

namespace KeryxPars.HL7.Mapping.Core;

/// <summary>
/// Parses and represents HL7 field notation.
/// Supports: SEG.F, SEG.F.C, SEG.F.C.S, SEG.F[R], SEG.F[R].C, etc.
/// </summary>
public readonly struct FieldNotation : IEquatable<FieldNotation>
{
    private static readonly Regex NotationPattern = new(
        @"^(?<seg>[A-Z][A-Z0-9]{1,2})(?:\.(?<field>\d+))?(?:\[(?<rep>\d+)\])?(?:\.(?<comp>\d+))?(?:\.(?<sub>\d+))?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Segment ID (e.g., "PID", "MSH").
    /// </summary>
    public string SegmentId { get; init; }

    /// <summary>
    /// Field index (1-based), or null for segment-only notation.
    /// </summary>
    public int? FieldIndex { get; init; }

    /// <summary>
    /// Component index (1-based), or null if not specified.
    /// </summary>
    public int? ComponentIndex { get; init; }

    /// <summary>
    /// Subcomponent index (1-based), or null if not specified.
    /// </summary>
    public int? SubcomponentIndex { get; init; }

    /// <summary>
    /// Repetition index (0-based), or null if not specified.
    /// </summary>
    public int? RepetitionIndex { get; init; }

    /// <summary>
    /// Level of access: Segment, Field, Component, or Subcomponent.
    /// </summary>
    public AccessLevel Level { get; init; }

    /// <summary>
    /// Original notation string.
    /// </summary>
    public string OriginalNotation { get; init; }

    /// <summary>
    /// Parses an HL7 field notation string.
    /// </summary>
    /// <param name="notation">Notation string (e.g., "PID.5.1")</param>
    /// <returns>Parsed field notation</returns>
    /// <exception cref="ArgumentException">If notation is invalid</exception>
    public static FieldNotation Parse(string notation)
    {
        if (string.IsNullOrWhiteSpace(notation))
        {
            throw new ArgumentException("Notation cannot be null or empty", nameof(notation));
        }

        var match = NotationPattern.Match(notation.Trim());
        if (!match.Success)
        {
            throw new ArgumentException($"Invalid HL7 field notation: {notation}", nameof(notation));
        }

        var segmentId = match.Groups["seg"].Value.ToUpperInvariant();
        var field = match.Groups["field"];
        var rep = match.Groups["rep"];
        var comp = match.Groups["comp"];
        var sub = match.Groups["sub"];

        int? fieldIndex = field.Success ? int.Parse(field.Value) : null;
        int? repIndex = rep.Success ? int.Parse(rep.Value) : null;
        int? compIndex = comp.Success ? int.Parse(comp.Value) : null;
        int? subIndex = sub.Success ? int.Parse(sub.Value) : null;

        // Validate indices
        if (fieldIndex.HasValue && fieldIndex.Value < 1)
            throw new ArgumentException($"Field index must be >= 1: {notation}", nameof(notation));
        
        if (repIndex.HasValue && repIndex.Value < 0)
            throw new ArgumentException($"Repetition index must be >= 0: {notation}", nameof(notation));
        
        if (compIndex.HasValue && compIndex.Value < 1)
            throw new ArgumentException($"Component index must be >= 1: {notation}", nameof(notation));
        
        if (subIndex.HasValue && subIndex.Value < 1)
            throw new ArgumentException($"Subcomponent index must be >= 1: {notation}", nameof(notation));

        // Validate hierarchy (can't have component without field, etc.)
        if (compIndex.HasValue && !fieldIndex.HasValue)
            throw new ArgumentException($"Component notation requires field: {notation}", nameof(notation));
        
        if (subIndex.HasValue && !compIndex.HasValue)
            throw new ArgumentException($"Subcomponent notation requires component: {notation}", nameof(notation));

        // Determine access level
        var level = DetermineLevel(fieldIndex, compIndex, subIndex);

        return new FieldNotation
        {
            SegmentId = segmentId,
            FieldIndex = fieldIndex,
            ComponentIndex = compIndex,
            SubcomponentIndex = subIndex,
            RepetitionIndex = repIndex,
            Level = level,
            OriginalNotation = notation
        };
    }

    /// <summary>
    /// Tries to parse notation without throwing exceptions.
    /// </summary>
    public static bool TryParse(string notation, out FieldNotation result)
    {
        try
        {
            result = Parse(notation);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    private static AccessLevel DetermineLevel(int? field, int? component, int? subcomponent)
    {
        if (subcomponent.HasValue) return AccessLevel.Subcomponent;
        if (component.HasValue) return AccessLevel.Component;
        if (field.HasValue) return AccessLevel.Field;
        return AccessLevel.Segment;
    }

    public bool Equals(FieldNotation other) =>
        SegmentId == other.SegmentId &&
        FieldIndex == other.FieldIndex &&
        ComponentIndex == other.ComponentIndex &&
        SubcomponentIndex == other.SubcomponentIndex &&
        RepetitionIndex == other.RepetitionIndex;

    public override bool Equals(object? obj) => obj is FieldNotation other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(
        SegmentId, FieldIndex, ComponentIndex, SubcomponentIndex, RepetitionIndex);

    public override string ToString() => OriginalNotation;

    public static bool operator ==(FieldNotation left, FieldNotation right) => left.Equals(right);
    public static bool operator !=(FieldNotation left, FieldNotation right) => !left.Equals(right);
}

/// <summary>
/// Level of HL7 field access.
/// </summary>
public enum AccessLevel
{
    Segment,
    Field,
    Component,
    Subcomponent
}
