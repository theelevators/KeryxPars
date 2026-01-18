using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// SI - Sequence ID Data Type
/// A non-negative integer in the form of a NM field.
/// Used to represent a sequence or set ID.
/// </summary>
public readonly struct SI : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the SI data type.
    /// </summary>
    /// <param name="value">The sequence ID value as a string.</param>
    public SI(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the SI data type from an integer.
    /// </summary>
    /// <param name="value">The sequence ID value.</param>
    public SI(int value)
    {
        _value = value.ToString();
    }

    /// <inheritdoc/>
    public string TypeCode => "SI";

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool IsEmpty => string.IsNullOrWhiteSpace(_value);

    /// <summary>
    /// Converts the sequence ID to an integer.
    /// </summary>
    /// <returns>The integer value, or null if the value is empty or invalid.</returns>
    public int? ToInt32()
    {
        if (IsEmpty) return null;
        if (int.TryParse(_value, out var result) && result >= 0)
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

        if (!int.TryParse(_value, out var intValue))
        {
            errors.Add($"SI data type contains invalid integer value: '{_value}'");
            return false;
        }

        if (intValue < 0)
        {
            errors.Add($"SI data type must be non-negative (actual: {intValue})");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to SI.
    /// </summary>
    public static implicit operator SI(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from SI to string.
    /// </summary>
    public static implicit operator string(SI si) => si._value ?? string.Empty;

    /// <summary>
    /// Implicit conversion from int to SI.
    /// </summary>
    public static implicit operator SI(int value) => new(value);

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is SI other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
