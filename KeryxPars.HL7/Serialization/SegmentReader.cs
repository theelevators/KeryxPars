namespace KeryxPars.HL7.Serialization;

/// <summary>
/// Zero-allocation segment reader using spans.
/// </summary>
public ref struct SegmentReader
{
    private ReadOnlySpan<char> _segment;
    private int _position;

    public SegmentReader(ReadOnlySpan<char> segment)
    {
        _segment = segment;
        _position = 0;
    }

    public ReadOnlySpan<char> Remaining => _segment[_position..];
    public bool IsAtEnd => _position >= _segment.Length;

    public bool TryReadField(char delimiter, out ReadOnlySpan<char> field)
    {
        if (IsAtEnd)
        {
            field = ReadOnlySpan<char>.Empty;
            return false;
        }

        int delimiterIndex = _segment[_position..].IndexOf(delimiter);

        if (delimiterIndex == -1)
        {
            // Last field
            field = _segment[_position..];
            _position = _segment.Length;
            return true;
        }

        field = _segment.Slice(_position, delimiterIndex);
        _position += delimiterIndex + 1;
        return true;
    }

    public FieldEnumerator EnumerateFields(char delimiter) => new(Remaining, delimiter);
}
