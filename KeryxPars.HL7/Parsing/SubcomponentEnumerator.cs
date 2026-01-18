namespace KeryxPars.HL7.Parsing;

/// <summary>
/// Zero-allocation enumerator for parsing HL7 subcomponents using Span&lt;char&gt;.
/// Subcomponents are separated by the subcomponent separator (default '&amp;').
/// </summary>
public ref struct SubcomponentEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private readonly char _subcomponentSeparator;
    private ReadOnlySpan<char> _current;

    /// <summary>
    /// Initializes a new instance of the SubcomponentEnumerator.
    /// </summary>
    /// <param name="span">The span to enumerate.</param>
    /// <param name="subcomponentSeparator">The subcomponent separator character (default '&amp;').</param>
    public SubcomponentEnumerator(ReadOnlySpan<char> span, char subcomponentSeparator)
    {
        _remaining = span;
        _subcomponentSeparator = subcomponentSeparator;
        _current = default;
    }

    /// <summary>
    /// Gets the current subcomponent.
    /// </summary>
    public ReadOnlySpan<char> Current => _current;

    /// <summary>
    /// Advances the enumerator to the next subcomponent.
    /// </summary>
    /// <returns>true if the enumerator successfully advanced; false if the enumerator has passed the end.</returns>
    public bool MoveNext()
    {
        if (_remaining.IsEmpty)
            return false;

        int separatorIndex = _remaining.IndexOf(_subcomponentSeparator);

        if (separatorIndex == -1)
        {
            _current = _remaining;
            _remaining = ReadOnlySpan<char>.Empty;
            return true;
        }

        _current = _remaining[..separatorIndex];
        _remaining = _remaining[(separatorIndex + 1)..];
        return true;
    }

    /// <summary>
    /// Returns this enumerator instance (for foreach support).
    /// </summary>
    public SubcomponentEnumerator GetEnumerator() => this;
}
