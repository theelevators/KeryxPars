using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// ST - String Data Type
/// Represents a string data value (maximum 199 characters).
/// </summary>
public readonly struct ST : IPrimitiveDataType, IEquatable<string>
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
            errors.Add($"ST data type exceeds maximum length of 199 characters: '{_value.Length}' characters");
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

    /// <summary>
    /// Determines whether the specified string is equal to the current ST value.
    /// </summary>
    public bool Equals(string? other) => _value == other;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            ST other => _value == other._value,
            string str => _value == str,
            _ => false
        };
    }

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    public static bool operator ==(ST left, ST right) => left.Equals(right);
    public static bool operator !=(ST left, ST right) => !left.Equals(right);
    public static bool operator ==(ST left, string? right) => left.Equals(right);
    public static bool operator !=(ST left, string? right) => !left.Equals(right);
    public static bool operator ==(string? left, ST right) => right.Equals(left);
    public static bool operator !=(string? left, ST right) => !right.Equals(left);
}
