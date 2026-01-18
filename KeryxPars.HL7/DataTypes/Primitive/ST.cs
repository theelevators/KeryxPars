using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// ST - String Data Type
/// A string data type is used to represent displayable text.
/// Maximum length: 199 characters.
/// </summary>
public readonly struct ST : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the ST data type.
    /// </summary>
    /// <param name="value">The string value.</param>
    public ST(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public string TypeCode => "ST";

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
        
        if (_value != null && _value.Length > 199)
        {
            errors.Add($"ST data type exceeds maximum length of 199 characters (actual: {_value.Length})");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to ST.
    /// </summary>
    public static implicit operator ST(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from ST to string.
    /// </summary>
    public static implicit operator string(ST st) => st._value ?? string.Empty;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is ST other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
