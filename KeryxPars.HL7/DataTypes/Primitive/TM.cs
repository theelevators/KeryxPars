using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// TM - Time Data Type
/// Represents a time in the format HH[MM[SS[.S[S[S[S]]]]]][+/-ZZZZ].
/// </summary>
public readonly struct TM : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the TM data type.
    /// </summary>
    /// <param name="value">The time value in HL7 format.</param>
    public TM(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the TM data type from a DateTime.
    /// </summary>
    /// <param name="time">The time value.</param>
    public TM(DateTime time)
    {
        _value = time.ToString("HHmmss");
    }

    /// <summary>
    /// Initializes a new instance of the TM data type from a TimeOnly.
    /// </summary>
    /// <param name="time">The time value.</param>
    public TM(TimeOnly time)
    {
        _value = time.ToString("HHmmss");
    }

    /// <inheritdoc/>
    public string TypeCode => "TM";

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool IsEmpty => string.IsNullOrWhiteSpace(_value);

    /// <summary>
    /// Converts the time value to a TimeSpan.
    /// </summary>
    /// <returns>The TimeSpan value, or null if the value is empty or invalid.</returns>
    public TimeSpan? ToTimeSpan()
    {
        if (IsEmpty) return null;

        // Remove timezone if present
        var timeValue = Regex.Replace(_value, @"[+\-]\d{4}$", "");

        // Try different formats
        string[] formats = { "HHmmss.ffff", "HHmmss.fff", "HHmmss.ff", "HHmmss.f", "HHmmss", "HHmm", "HH" };
        
        if (DateTime.TryParseExact(timeValue, formats, CultureInfo.InvariantCulture, 
            DateTimeStyles.None, out var result))
            return result.TimeOfDay;
        
        return null;
    }

    /// <summary>
    /// Converts the time value to a TimeOnly.
    /// </summary>
    /// <returns>The TimeOnly value, or null if the value is empty or invalid.</returns>
    public TimeOnly? ToTimeOnly()
    {
        var timeSpan = ToTimeSpan();
        return timeSpan.HasValue ? TimeOnly.FromTimeSpan(timeSpan.Value) : null;
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

        // Check format: HH[MM[SS[.S[S[S[S]]]]]][+/-ZZZZ]
        if (!Regex.IsMatch(_value, @"^\d{2}(\d{2}(\d{2}(\.\d{1,4})?)?)?([+\-]\d{4})?$"))
        {
            errors.Add($"TM data type has invalid format: '{_value}'. Expected HH[MM[SS[.SSSS]]][+/-ZZZZ]");
            return false;
        }

        // Verify it's a valid time
        if (ToTimeSpan() == null)
        {
            errors.Add($"TM data type contains invalid time: '{_value}'");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to TM.
    /// </summary>
    public static implicit operator TM(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from TM to string.
    /// </summary>
    public static implicit operator string(TM tm) => tm._value ?? string.Empty;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is TM other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;
}
