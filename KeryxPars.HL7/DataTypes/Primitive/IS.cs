using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// IS - Coded Value for User-Defined Tables
/// The value of the IS data type is coded from a user-defined table.
/// Maximum length: 20 characters.
/// </summary>
public readonly struct IS : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the IS data type.
    /// </summary>
    /// <param name="value">The coded value.</param>
    public IS(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public string TypeCode => "IS";

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
        
        if (_value != null && _value.Length > 20)
        {
            errors.Add($"IS data type exceeds maximum length of 20 characters (actual: {_value.Length})");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to IS.
    /// </summary>
    public static implicit operator IS(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from IS to string.
    /// </summary>
    public static implicit operator string(IS isValue) => isValue._value ?? string.Empty;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is IS other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
