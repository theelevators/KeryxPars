using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;
using System.Globalization;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// NM - Numeric Data Type
/// A number represented as a series of ASCII numeric characters.
/// Maximum length: 16 characters (or as defined in specification).
/// </summary>
public readonly struct NM : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the NM data type.
    /// </summary>
    /// <param name="value">The numeric value as a string.</param>
    public NM(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the NM data type from a decimal.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public NM(decimal value)
    {
        _value = value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Initializes a new instance of the NM data type from a double.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public NM(double value)
    {
        _value = value.ToString(CultureInfo.InvariantCulture);
    }

    /// <inheritdoc/>
    public string TypeCode => "NM";

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool IsEmpty => string.IsNullOrWhiteSpace(_value);

    /// <summary>
    /// Converts the numeric value to a decimal.
    /// </summary>
    /// <returns>The decimal value, or null if the value is empty or invalid.</returns>
    public decimal? ToDecimal()
    {
        if (IsEmpty) return null;
        if (decimal.TryParse(_value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
            return result;
        return null;
    }

    /// <summary>
    /// Converts the numeric value to a double.
    /// </summary>
    /// <returns>The double value, or null if the value is empty or invalid.</returns>
    public double? ToDouble()
    {
        if (IsEmpty) return null;
        if (double.TryParse(_value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
            return result;
        return null;
    }

    /// <summary>
    /// Converts the numeric value to an integer.
    /// </summary>
    /// <returns>The integer value, or null if the value is empty or invalid.</returns>
    public int? ToInt32()
    {
        if (IsEmpty) return null;
        if (int.TryParse(_value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
            return result;
        return null;
    }

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

        if (IsEmpty)
            return true;

        // Check if it's a valid number
        if (!decimal.TryParse(_value, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
        {
            errors.Add($"NM data type contains invalid numeric value: '{_value}'");
            return false;
        }

        if (_value.Length > 16)
        {
            errors.Add($"NM data type exceeds recommended maximum length of 16 characters (actual: {_value.Length})");
        }

        return errors.Count == 0;
    }

    /// <summary>
    /// Implicit conversion from string to NM.
    /// </summary>
    public static implicit operator NM(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from NM to string.
    /// </summary>
    public static implicit operator string(NM nm) => nm._value ?? string.Empty;

    /// <summary>
    /// Implicit conversion from decimal to NM.
    /// </summary>
    public static implicit operator NM(decimal value) => new(value);

    /// <summary>
    /// Implicit conversion from double to NM.
    /// </summary>
    public static implicit operator NM(double value) => new(value);

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is NM other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
