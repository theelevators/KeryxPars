namespace KeryxPars.HL7.Serialization;


/// <summary>
/// Enumerator for iterating fields without allocation.
/// </summary>
public ref struct FieldEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private readonly char _delimiter;
    private ReadOnlySpan<char> _current;

    public FieldEnumerator(ReadOnlySpan<char> span, char delimiter)
    {
        _remaining = span;
        _delimiter = delimiter;
        _current = default;
    }

    public ReadOnlySpan<char> Current => _current;

    public bool MoveNext()
    {
        if (_remaining.IsEmpty)
            return false;

        int delimiterIndex = _remaining.IndexOf(_delimiter);

        if (delimiterIndex == -1)
        {
            _current = _remaining;
            _remaining = ReadOnlySpan<char>.Empty;
            return true;
        }

        _current = _remaining[..delimiterIndex];
        _remaining = _remaining[(delimiterIndex + 1)..];
        return true;
    }
}
