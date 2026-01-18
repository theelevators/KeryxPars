using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// ID - Coded Value for HL7 Defined Tables
/// The value of the ID data type is coded from an HL7 table.
/// Maximum length: 199 characters.
/// </summary>
public readonly struct ID : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the ID data type.
    /// </summary>
    /// <param name="value">The coded value.</param>
    public ID(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public string TypeCode => "ID";

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
            errors.Add($"ID data type exceeds maximum length of 199 characters (actual: {_value.Length})");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to ID.
    /// </summary>
    public static implicit operator ID(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from ID to string.
    /// </summary>
    public static implicit operator string(ID id) => id._value ?? string.Empty;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is ID other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
