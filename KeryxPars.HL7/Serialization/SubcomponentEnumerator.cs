namespace KeryxPars.HL7.Serialization;

/// <summary>
/// Zero-allocation enumerator for iterating over subcomponents within a component.
/// Subcomponents are separated by the subcomponent separator (&amp;).
/// </summary>
public ref struct SubcomponentEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private readonly char _subcomponentSeparator;
    private ReadOnlySpan<char> _current;

    /// <summary>
    /// Initializes a new instance of the SubcomponentEnumerator.
    /// </summary>
    /// <param name="span">The span containing the component value.</param>
    /// <param name="subcomponentSeparator">The subcomponent separator character (typically &amp;).</param>
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
    /// <returns>True if there is a next subcomponent; otherwise, false.</returns>
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
    /// Gets the enumerator.
    /// </summary>
    public SubcomponentEnumerator GetEnumerator() => this;
}
