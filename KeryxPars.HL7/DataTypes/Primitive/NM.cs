using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;
using System.Globalization;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// NM - Numeric Data Type
/// Represents a numeric value (recommended maximum 16 characters).
/// </summary>
public readonly struct NM : IPrimitiveDataType, IEquatable<string>
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

    /// <summary>
    /// Initializes a new instance of the NM data type from an int.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public NM(int value)
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
        
        if (decimal.TryParse(_value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
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
        
        if (double.TryParse(_value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            return result;
        
        return null;
    }

    /// <summary>
    /// Converts the numeric value to an int (only if it's a whole number).
    /// </summary>
    /// <returns>The int value, or null if the value is empty, invalid, or not a whole number.</returns>
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

        // Verify it's a valid number
        if (ToDecimal() == null)
        {
            errors.Add($"NM data type contains invalid numeric value: '{_value}'");
            return false;
        }

        // Check recommended maximum length
        if (_value.Length > 16)
        {
            errors.Add($"NM data type exceeds recommended maximum length of 16 characters: '{_value.Length}' characters");
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

    /// <summary>
    /// Implicit conversion from int to NM.
    /// </summary>
    public static implicit operator NM(int value) => new(value);

    /// <summary>
    /// Determines whether the specified string is equal to the current NM value.
    /// </summary>
    public bool Equals(string? other) => _value == other;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            NM other => _value == other._value,
            string str => _value == str,
            decimal dec => ToDecimal() == dec,
            double dbl => ToDouble() == dbl,
            int i => ToInt32() == i,
            _ => false
        };
    }

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    public static bool operator ==(NM left, NM right) => left.Equals(right);
    public static bool operator !=(NM left, NM right) => !left.Equals(right);
    public static bool operator ==(NM left, string? right) => left.Equals(right);
    public static bool operator !=(NM left, string? right) => !left.Equals(right);
    public static bool operator ==(string? left, NM right) => right.Equals(left);
    public static bool operator !=(string? left, NM right) => !right.Equals(left);
}
