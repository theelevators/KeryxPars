using System;
using System.Runtime.CompilerServices;
using KeryxPars.HL7.Mapping.Core;

namespace KeryxPars.HL7.Mapping.Parsers;

/// <summary>
/// Ultra-fast, zero-allocation HL7 message parser using Span&lt;char&gt;.
/// Supports all HL7 hierarchy levels: Segment ? Field ? Component ? Subcomponent.
/// </summary>
public static class HL7SpanParser
{
    // HL7 Delimiters
    private const char SegmentDelimiter = '\r';
    private const char FieldDelimiter = '|';
    private const char ComponentDelimiter = '^';
    private const char SubcomponentDelimiter = '&';
    private const char RepetitionDelimiter = '~';
    private const char EscapeCharacter = '\\';

    #region Universal Accessor

    /// <summary>
    /// Universal accessor - gets any field value using FieldNotation.
    /// Zero allocation when possible.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> GetValue(ReadOnlySpan<char> message, FieldNotation notation)
    {
        // Find the segment
        var segment = FindSegment(message, notation.SegmentId);
        if (segment.IsEmpty)
            return ReadOnlySpan<char>.Empty;

        // If segment-level access, return entire segment
        if (!notation.FieldIndex.HasValue)
            return segment;

        // Get field
        var field = GetField(segment, notation.FieldIndex.Value);
        if (field.IsEmpty)
            return ReadOnlySpan<char>.Empty;

        // Handle repetition if specified
        if (notation.RepetitionIndex.HasValue)
        {
            field = GetRepetition(field, notation.RepetitionIndex.Value);
            if (field.IsEmpty)
                return ReadOnlySpan<char>.Empty;
        }

        // If field-level access, return field
        if (!notation.ComponentIndex.HasValue)
            return field;

        // Get component
        var component = GetComponent(field, notation.ComponentIndex.Value);
        if (component.IsEmpty)
            return ReadOnlySpan<char>.Empty;

        // If component-level access, return component
        if (!notation.SubcomponentIndex.HasValue)
            return component;

        // Get subcomponent
        return GetSubcomponent(component, notation.SubcomponentIndex.Value);
    }

    /// <summary>
    /// Overload that parses notation string inline.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> GetValue(ReadOnlySpan<char> message, string notationString)
    {
        var notation = FieldNotation.Parse(notationString);
        return GetValue(message, notation);
    }

    #endregion

    #region Segment Operations

    /// <summary>
    /// Finds a segment by ID in the message.
    /// Returns the entire segment including the segment ID.
    /// </summary>
    public static ReadOnlySpan<char> FindSegment(ReadOnlySpan<char> message, string segmentId)
    {
        if (message.IsEmpty || string.IsNullOrEmpty(segmentId))
            return ReadOnlySpan<char>.Empty;

        var searchSpan = message;
        var segmentIdSpan = segmentId.AsSpan();

        while (!searchSpan.IsEmpty)
        {
            // Find start of segment
            var segmentStart = searchSpan;
            
            // Check if this segment matches
            if (segmentStart.Length >= segmentIdSpan.Length &&
                segmentStart.Slice(0, segmentIdSpan.Length).Equals(segmentIdSpan, StringComparison.OrdinalIgnoreCase))
            {
                // Find end of segment (next \r or end of message)
                var segmentEndIndex = searchSpan.IndexOf(SegmentDelimiter);
                if (segmentEndIndex == -1)
                {
                    // Last segment in message
                    return searchSpan;
                }
                
                return searchSpan.Slice(0, segmentEndIndex);
            }

            // Move to next segment
            var nextSegmentIndex = searchSpan.IndexOf(SegmentDelimiter);
            if (nextSegmentIndex == -1)
                break;

            searchSpan = searchSpan.Slice(nextSegmentIndex + 1);
        }

        return ReadOnlySpan<char>.Empty;
    }

    /// <summary>
    /// Gets all segments with the specified ID.
    /// </summary>
    public static SegmentEnumerator GetSegments(ReadOnlySpan<char> message, string segmentId)
    {
        return new SegmentEnumerator(message, segmentId);
    }

    /// <summary>
    /// Finds all segments with the specified ID.
    /// Alias for GetSegments for use in generated code.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SegmentEnumerator FindAllSegments(ReadOnlySpan<char> message, string segmentId)
    {
        return GetSegments(message, segmentId);
    }

    #endregion

    #region Field Operations

    /// <summary>
    /// Extracts a field from a segment by index (1-based).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> GetField(ReadOnlySpan<char> segment, int fieldIndex)
    {
        if (segment.IsEmpty || fieldIndex < 1)
            return ReadOnlySpan<char>.Empty;

        // Skip segment ID (first field before |)
        var fieldStart = segment.IndexOf(FieldDelimiter);
        if (fieldStart == -1)
            return ReadOnlySpan<char>.Empty;

        fieldStart++; // Move past the delimiter

        // Special case: MSH.1 is the field delimiter itself
        if (segment.Length >= 3 && segment.Slice(0, 3).Equals("MSH".AsSpan(), StringComparison.OrdinalIgnoreCase))
        {
            if (fieldIndex == 1)
                return FieldDelimiter.ToString().AsSpan();
            
            // MSH.2 is the encoding characters
            if (fieldIndex == 2)
            {
                var encodingEnd = segment.Slice(fieldStart).IndexOf(FieldDelimiter);
                if (encodingEnd == -1)
                    return segment.Slice(fieldStart);
                return segment.Slice(fieldStart, encodingEnd);
            }
            
            // Adjust index for MSH quirks
            fieldIndex -= 2;
        }
        else
        {
            fieldIndex--; // Adjust to 0-based
        }

        // Find the nth field
        var currentField = segment.Slice(fieldStart);
        for (int i = 0; i < fieldIndex; i++)
        {
            var nextDelimiter = currentField.IndexOf(FieldDelimiter);
            if (nextDelimiter == -1)
                return ReadOnlySpan<char>.Empty; // Field doesn't exist

            currentField = currentField.Slice(nextDelimiter + 1);
        }

        // Extract this field
        var fieldEnd = currentField.IndexOf(FieldDelimiter);
        return fieldEnd == -1 
            ? currentField 
            : currentField.Slice(0, fieldEnd);
    }

    #endregion

    #region Component Operations

    /// <summary>
    /// Extracts a component from a field by index (1-based).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> GetComponent(ReadOnlySpan<char> field, int componentIndex)
    {
        if (field.IsEmpty || componentIndex < 1)
            return ReadOnlySpan<char>.Empty;

        componentIndex--; // Convert to 0-based

        // Find the nth component
        var currentComponent = field;
        for (int i = 0; i < componentIndex; i++)
        {
            var nextDelimiter = currentComponent.IndexOf(ComponentDelimiter);
            if (nextDelimiter == -1)
                return ReadOnlySpan<char>.Empty; // Component doesn't exist

            currentComponent = currentComponent.Slice(nextDelimiter + 1);
        }

        // Extract this component
        var componentEnd = currentComponent.IndexOf(ComponentDelimiter);
        return componentEnd == -1 
            ? currentComponent 
            : currentComponent.Slice(0, componentEnd);
    }

    #endregion

    #region Subcomponent Operations

    /// <summary>
    /// Extracts a subcomponent from a component by index (1-based).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> GetSubcomponent(ReadOnlySpan<char> component, int subcomponentIndex)
    {
        if (component.IsEmpty || subcomponentIndex < 1)
            return ReadOnlySpan<char>.Empty;

        subcomponentIndex--; // Convert to 0-based

        // Find the nth subcomponent
        var currentSubcomponent = component;
        for (int i = 0; i < subcomponentIndex; i++)
        {
            var nextDelimiter = currentSubcomponent.IndexOf(SubcomponentDelimiter);
            if (nextDelimiter == -1)
                return ReadOnlySpan<char>.Empty; // Subcomponent doesn't exist

            currentSubcomponent = currentSubcomponent.Slice(nextDelimiter + 1);
        }

        // Extract this subcomponent
        var subcomponentEnd = currentSubcomponent.IndexOf(SubcomponentDelimiter);
        return subcomponentEnd == -1 
            ? currentSubcomponent 
            : currentSubcomponent.Slice(0, subcomponentEnd);
    }

    #endregion

    #region Repetition Operations

    /// <summary>
    /// Gets a specific repetition from a field (0-based index).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> GetRepetition(ReadOnlySpan<char> field, int repetitionIndex)
    {
        if (field.IsEmpty || repetitionIndex < 0)
            return ReadOnlySpan<char>.Empty;

        // Find the nth repetition
        var currentRepetition = field;
        for (int i = 0; i < repetitionIndex; i++)
        {
            var nextDelimiter = currentRepetition.IndexOf(RepetitionDelimiter);
            if (nextDelimiter == -1)
                return ReadOnlySpan<char>.Empty; // Repetition doesn't exist

            currentRepetition = currentRepetition.Slice(nextDelimiter + 1);
        }

        // Extract this repetition
        var repetitionEnd = currentRepetition.IndexOf(RepetitionDelimiter);
        return repetitionEnd == -1 
            ? currentRepetition 
            : currentRepetition.Slice(0, repetitionEnd);
    }

    /// <summary>
    /// Gets all repetitions of a field.
    /// </summary>
    public static RepetitionEnumerator GetRepetitions(ReadOnlySpan<char> field)
    {
        return new RepetitionEnumerator(field);
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Checks if a span is empty or whitespace.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
            return true;

        for (int i = 0; i < value.Length; i++)
        {
            if (!char.IsWhiteSpace(value[i]))
                return false;
        }

        return true;
    }

    #endregion

    #region Enumerators (Stack-allocated for collections)

    /// <summary>
    /// Zero-allocation enumerator for segments.
    /// </summary>
    public ref struct SegmentEnumerator
    {
        private ReadOnlySpan<char> _remaining;
        private readonly string _segmentId;
        private ReadOnlySpan<char> _current;

        internal SegmentEnumerator(ReadOnlySpan<char> message, string segmentId)
        {
            _remaining = message;
            _segmentId = segmentId;
            _current = ReadOnlySpan<char>.Empty;
        }

        public ReadOnlySpan<char> Current => _current;

        public bool MoveNext()
        {
            while (!_remaining.IsEmpty)
            {
                var segment = FindSegment(_remaining, _segmentId);
                if (segment.IsEmpty)
                    return false;

                _current = segment;

                // Move past this segment
                var segmentEnd = _remaining.IndexOf(segment);
                if (segmentEnd >= 0)
                {
                    var nextStart = segmentEnd + segment.Length;
                    if (nextStart < _remaining.Length && _remaining[nextStart] == SegmentDelimiter)
                        nextStart++;
                    
                    _remaining = nextStart < _remaining.Length 
                        ? _remaining.Slice(nextStart) 
                        : ReadOnlySpan<char>.Empty;
                }
                else
                {
                    _remaining = ReadOnlySpan<char>.Empty;
                }

                return true;
            }

            return false;
        }

        public SegmentEnumerator GetEnumerator() => this;
    }

    /// <summary>
    /// Zero-allocation enumerator for field repetitions.
    /// </summary>
    public ref struct RepetitionEnumerator
    {
        private ReadOnlySpan<char> _remaining;
        private ReadOnlySpan<char> _current;

        internal RepetitionEnumerator(ReadOnlySpan<char> field)
        {
            _remaining = field;
            _current = ReadOnlySpan<char>.Empty;
        }

        public ReadOnlySpan<char> Current => _current;

        public bool MoveNext()
        {
            if (_remaining.IsEmpty)
                return false;

            var nextDelimiter = _remaining.IndexOf(RepetitionDelimiter);
            if (nextDelimiter == -1)
            {
                _current = _remaining;
                _remaining = ReadOnlySpan<char>.Empty;
                return true;
            }

            _current = _remaining.Slice(0, nextDelimiter);
            _remaining = _remaining.Slice(nextDelimiter + 1);
            return true;
        }

        public RepetitionEnumerator GetEnumerator() => this;
    }

    #endregion
}
