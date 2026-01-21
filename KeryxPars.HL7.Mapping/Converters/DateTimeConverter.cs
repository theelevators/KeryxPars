using System;
using System.Globalization;

namespace KeryxPars.HL7.Mapping.Converters;

/// <summary>
/// Converts HL7 date/time strings to DateTime objects.
/// Supports common HL7 date/time formats.
/// </summary>
public class DateTimeConverter : IHL7TypeConverter<DateTime>
{
    /// <summary>
    /// Common HL7 date/time formats in order of specificity.
    /// </summary>
    private static readonly string[] HL7DateTimeFormats = new[]
    {
        "yyyyMMddHHmmss.ffff",  // Full precision
        "yyyyMMddHHmmss.fff",   // Milliseconds
        "yyyyMMddHHmmss.ff",    // Centiseconds
        "yyyyMMddHHmmss.f",     // Deciseconds
        "yyyyMMddHHmmss",       // Seconds
        "yyyyMMddHHmm",         // Minutes
        "yyyyMMddHH",           // Hours
        "yyyyMMdd",             // Date only
        "yyyyMM",               // Month only
        "yyyy"                  // Year only
    };

    public DateTime Convert(ReadOnlySpan<char> value, string? format = null)
    {
        if (value.IsEmpty)
            throw new ArgumentException("DateTime value cannot be empty");

        // If a specific format is provided, use it
        if (!string.IsNullOrEmpty(format))
        {
            if (DateTime.TryParseExact(
                value.ToString(),
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
            {
                return result;
            }

            throw new FormatException($"Could not parse '{value.ToString()}' using format '{format}'");
        }

        // Try each HL7 format in order
        foreach (var hl7Format in HL7DateTimeFormats)
        {
            if (DateTime.TryParseExact(
                value.ToString(),
                hl7Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
            {
                return result;
            }
        }

        // Fallback to general parsing
        if (DateTime.TryParse(value.ToString(), out var fallbackResult))
        {
            return fallbackResult;
        }

        throw new FormatException($"Could not parse '{value.ToString()}' as a DateTime");
    }

    public string? ConvertBack(DateTime value, string? format = null)
    {
        format ??= "yyyyMMddHHmmss";
        return value.ToString(format, CultureInfo.InvariantCulture);
    }
}

/// <summary>
/// Converts HL7 date/time strings to nullable DateTime objects.
/// </summary>
public class NullableDateTimeConverter : IHL7TypeConverter<DateTime?>
{
    private readonly DateTimeConverter _innerConverter = new();

    public DateTime? Convert(ReadOnlySpan<char> value, string? format = null)
    {
        if (value.IsEmpty || value.ToString().Trim() == "\"\"")
            return null;

        return _innerConverter.Convert(value, format);
    }

    public string? ConvertBack(DateTime? value, string? format = null)
    {
        return value.HasValue ? _innerConverter.ConvertBack(value.Value, format) : null;
    }
}
