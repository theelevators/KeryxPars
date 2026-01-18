namespace KeryxPars.HL7.Serialization;

/// <summary>
/// Zero-allocation line enumerator for parsing messages.
/// </summary>
public ref struct LineEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private ReadOnlySpan<char> _current;

    public LineEnumerator(ReadOnlySpan<char> text)
    {
        _remaining = text;
        _current = default;
    }

    public ReadOnlySpan<char> Current => _current;

    public bool MoveNext()
    {
        if (_remaining.IsEmpty)
            return false;

        // Find next line break (\r, \n, or \r\n)
        int lineBreakIndex = _remaining.IndexOfAny('\r', '\n');

        if (lineBreakIndex == -1)
        {
            _current = _remaining;
            _remaining = [];
            return true;
        }

        _current = _remaining[..lineBreakIndex];

        // Skip line break character(s)
        int skipLength = 1;
        if (lineBreakIndex + 1 < _remaining.Length &&
            _remaining[lineBreakIndex] == '\r' &&
            _remaining[lineBreakIndex + 1] == '\n')
        {
            skipLength = 2;
        }

        _remaining = _remaining[(lineBreakIndex + skipLength)..];
        return true;
    }
}