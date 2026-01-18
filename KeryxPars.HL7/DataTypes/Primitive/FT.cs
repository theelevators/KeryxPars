using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// FT - Formatted Text Data Type
/// Formatted text data is used to represent displayable text with embedded formatting instructions.
/// </summary>
public readonly struct FT : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the FT data type.
    /// </summary>
    /// <param name="value">The formatted text value.</param>
    public FT(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public string TypeCode => "FT";

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
        // FT has no specific validation rules beyond being a string
        return true;
    }

    /// <summary>
    /// Implicit conversion from string to FT.
    /// </summary>
    public static implicit operator FT(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from FT to string.
    /// </summary>
    public static implicit operator string(FT ft) => ft._value ?? string.Empty;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is FT other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
