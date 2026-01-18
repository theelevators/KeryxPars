using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KeryxPars.HL7.DataTypes.Primitive;

/// <summary>
/// DT - Date Data Type
/// Represents a date in the format YYYY[MM[DD]].
/// </summary>
public readonly struct DT : IPrimitiveDataType
{
    private readonly string _value;

    /// <summary>
    /// Initializes a new instance of the DT data type.
    /// </summary>
    /// <param name="value">The date value in HL7 format (YYYYMMDD).</param>
    public DT(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the DT data type from a DateTime.
    /// </summary>
    /// <param name="date">The date value.</param>
    public DT(DateTime date)
    {
        _value = date.ToString("yyyyMMdd");
    }

    /// <summary>
    /// Initializes a new instance of the DT data type from a DateOnly.
    /// </summary>
    /// <param name="date">The date value.</param>
    public DT(DateOnly date)
    {
        _value = date.ToString("yyyyMMdd");
    }

    /// <inheritdoc/>
    public string TypeCode => "DT";

    /// <inheritdoc/>
    public string Value
    {
        get => _value;
        set => System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool IsEmpty => string.IsNullOrWhiteSpace(_value);

    /// <summary>
    /// Converts the date value to a DateTime.
    /// </summary>
    /// <returns>The DateTime value, or null if the value is empty or invalid.</returns>
    public DateTime? ToDateTime()
    {
        if (IsEmpty) return null;

        // Try different formats (YYYYMMDD, YYYYMM, YYYY)
        string[] formats = { "yyyyMMdd", "yyyyMM", "yyyy" };
        
        if (DateTime.TryParseExact(_value, formats, CultureInfo.InvariantCulture, 
            DateTimeStyles.None, out var result))
            return result;
        
        return null;
    }

    /// <summary>
    /// Converts the date value to a DateOnly.
    /// </summary>
    /// <returns>The DateOnly value, or null if the value is empty or invalid.</returns>
    public DateOnly? ToDateOnly()
    {
        var dateTime = ToDateTime();
        return dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : null;
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

        // Check format: YYYY[MM[DD]]
        if (!Regex.IsMatch(_value, @"^\d{4}(\d{2}(\d{2})?)?$"))
        {
            errors.Add($"DT data type has invalid format: '{_value}'. Expected YYYY[MM[DD]]");
            return false;
        }

        // Verify it's a valid date
        if (ToDateTime() == null)
        {
            errors.Add($"DT data type contains invalid date: '{_value}'");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Implicit conversion from string to DT.
    /// </summary>
    public static implicit operator DT(string? value) => new(value);

    /// <summary>
    /// Implicit conversion from DT to string.
    /// </summary>
    public static implicit operator string(DT dt) => dt._value ?? string.Empty;

    /// <summary>
    /// Implicit conversion from DateTime to DT.
    /// </summary>
    public static implicit operator DT(DateTime value) => new(value);

    /// <summary>
    /// Implicit conversion from DateOnly to DT.
    /// </summary>
    public static implicit operator DT(DateOnly value) => new(value);

    /// <inheritdoc/>
    public override string ToString() => _value ?? string.Empty;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is DT other && _value == other._value;

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    public static bool operator ==(DT left, DT right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DT left, DT right)
    {
        return !(left == right);
    }
}
