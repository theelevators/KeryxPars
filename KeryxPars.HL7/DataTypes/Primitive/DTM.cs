using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// DTM - Date/Time Data Type
/// Represents a date and time in the format YYYY[MM[DD[HH[MM[SS[.S[S[S[S]]]]]]]]][+/-ZZZZ].
/// </summary>
public readonly struct DTM : IPrimitiveDataType, IEquatable<string>
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the DTM data type.
    /// </summary>
    /// <param name="value">The date/time value in HL7 format.</param>
    public DTM(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the DTM data type from a DateTime.
    /// </summary>
    /// <param name="dateTime">The date/time value.</param>
    public DTM(DateTime dateTime)
    {
        _value = dateTime.ToString("yyyyMMddHHmmss");
    }

    /// <inheritdoc/>
    public string TypeCode => "DTM";

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool IsEmpty => string.IsNullOrWhiteSpace(_value);

    /// <summary>
    /// Converts the date/time value to a DateTime.
    /// </summary>
    /// <returns>The DateTime value, or null if the value is empty or invalid.</returns>
    public DateTime? ToDateTime()
    {
        if (IsEmpty) return null;

        // Remove timezone if present
        var dtValue = Regex.Replace(_value, @"[+\-]\d{4}$", "");

        // Try different formats
        string[] formats = 
        { 
            "yyyyMMddHHmmss.ffff", "yyyyMMddHHmmss.fff", "yyyyMMddHHmmss.ff", "yyyyMMddHHmmss.f",
            "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddHH", "yyyyMMdd", "yyyyMM", "yyyy"
        };
        
        if (DateTime.TryParseExact(dtValue, formats, CultureInfo.InvariantCulture, 
            DateTimeStyles.None, out var result))
            return result;
        
        return null;
    }

    /// <summary>
    /// Extracts the timezone offset if present.
    /// </summary>
    /// <returns>The timezone offset as a TimeSpan, or null if not specified.</returns>
    public TimeSpan? GetTimezoneOffset()
    {
        if (IsEmpty) return null;

        var match = Regex.Match(_value, @"([+\-])(\d{2})(\d{2})$");
        if (!match.Success) return null;

        var sign = match.Groups[1].Value == "+" ? 1 : -1;
        var hours = int.Parse(match.Groups[2].Value);
        var minutes = int.Parse(match.Groups[3].Value);

        return TimeSpan.FromHours(sign * hours) + TimeSpan.FromMinutes(sign * minutes);
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

        // Check format: YYYY[MM[DD[HH[MM[SS[.S[S[S[S]]]]]]]]][+/-ZZZZ]
        if (!Regex.IsMatch(_value, @"^\d{4}(\d{2}(\d{2}(\d{2}(\d{2}(\d{2}(\.\d{1,4})?)?)?)?)?)?([+\-]\d{4})?$"))
        {
            errors.Add($"DTM data type has invalid format: '{_value}'. Expected YYYY[MM[DD[HH[MM[SS[.SSSS]]]]]][+/-ZZZZ]");
            return false;
        }

        // Verify it's a valid date/time
        if (ToDateTime() == null)
        {
            errors.Add($"DTM data type contains invalid date/time: '{_value}'");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to DTM.
    /// </summary>
    public static implicit operator DTM(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from DTM to string.
    /// </summary>
    public static implicit operator string(DTM dtm) => dtm._value ?? string.Empty;

    /// <summary>
    /// Implicit conversion from DateTime to DTM.
    /// </summary>
    public static implicit operator DTM(DateTime value) => new(value);

    /// <summary>
    /// Determines whether the specified string is equal to the current DTM value.
    /// </summary>
    public bool Equals(string? other) => _value == other;

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            DTM other => _value == other._value,
            string str => _value == str,
            _ => false
        };
    }

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    public static bool operator ==(DTM left, DTM right) => left.Equals(right);
    public static bool operator !=(DTM left, DTM right) => !left.Equals(right);
    public static bool operator ==(DTM left, string? right) => left.Equals(right);
    public static bool operator !=(DTM left, string? right) => !left.Equals(right);
    public static bool operator ==(string? left, DTM right) => right.Equals(left);
    public static bool operator !=(string? left, DTM right) => !right.Equals(left);
}
