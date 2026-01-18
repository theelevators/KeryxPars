namespace KeryxPars.HL7.Serialization;

/// <summary>
/// Zero-allocation enumerator for iterating over components within a field.
/// Components are separated by the component separator (^).
/// </summary>
public ref struct ComponentEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private readonly char _componentSeparator;
    private ReadOnlySpan<char> _current;

    /// <summary>
    /// Initializes a new instance of the ComponentEnumerator.
    /// </summary>
    /// <param name="span">The span containing the field value.</param>
    /// <param name="componentSeparator">The component separator character (typically ^).</param>
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
    /// <returns>True if there is a next component; otherwise, false.</returns>
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
    /// Gets the enumerator.
    /// </summary>
    public ComponentEnumerator GetEnumerator() => this;
}
