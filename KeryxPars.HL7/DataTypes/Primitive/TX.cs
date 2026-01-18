using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// TX - Text Data Type
/// Text data is used to represent displayable text of unlimited length.
/// </summary>
public readonly struct TX : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the TX data type.
    /// </summary>
    /// <param name="value">The text value.</param>
    public TX(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public string TypeCode => "TX";

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool IsEmpty => string.IsNullOrWhiteSpace(_value);

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters) => _value ?? string.Empty;

    /// <inheritdoc/>
    public void Parse(ReadOnlySpan<char> value, in HL7Delimiters delimiters)
    {
        System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value.ToString();
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();
        // TX has no length restriction
        return true;
    }

    /// <summary>
    /// Implicit conversion from string to TX.
    /// </summary>
    public static implicit operator TX(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from TX to string.
    /// </summary>
    public static implicit operator string(TX tx) => tx._value ?? string.Empty;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is TX other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
