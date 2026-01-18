namespace KeryxPars.HL7.Parsing;

/// <summary>
/// Zero-allocation enumerator for parsing HL7 components using Span&lt;char&gt;.
/// Components are separated by the component separator (default '^').
/// </summary>
public ref struct ComponentEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private readonly char _componentSeparator;
    private ReadOnlySpan<char> _current;

    /// <summary>
    /// Initializes a new instance of the ComponentEnumerator.
    /// </summary>
    /// <param name="span">The span to enumerate.</param>
    /// <param name="componentSeparator">The component separator character (default '^').</param>
    public ComponentEnumerator(ReadOnlySpan<char> span, char componentSeparator)
    {
        _remaining = span;
        _componentSeparator = componentSeparator;
        _current = default;
    }

    /// <summary>
    /// Gets the current component.
    /// </summary>
    public ReadOnlySpan<char> Current => _current;

    /// <summary>
    /// Advances the enumerator to the next component.
    /// </summary>
    /// <returns>true if the enumerator successfully advanced; false if the enumerator has passed the end.</returns>
    public bool MoveNext()
    {
        if (_remaining.IsEmpty)
            return false;

        int separatorIndex = _remaining.IndexOf(_componentSeparator);

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
    public ComponentEnumerator GetEnumerator() => this;
}
