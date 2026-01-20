using System;

namespace KeryxPars.HL7.Mapping.Converters;

/// <summary>
/// Converts HL7 string values to enum types.
/// Supports both string and numeric HL7 enum values.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert to</typeparam>
public class EnumConverter<TEnum> : IHL7TypeConverter<TEnum> where TEnum : struct, Enum
{
    public TEnum Convert(ReadOnlySpan<char> value, string? format = null)
    {
        if (value.IsEmpty)
            throw new ArgumentException($"Cannot convert empty value to {typeof(TEnum).Name}");

        var stringValue = value.ToString();

        // Try parsing as string (e.g., "M" or "Male")
        if (Enum.TryParse<TEnum>(stringValue, ignoreCase: true, out var result))
        {
            return result;
        }

        // Try parsing as number (e.g., "1" or "2")
        if (int.TryParse(stringValue, out var numericValue))
        {
            if (Enum.IsDefined(typeof(TEnum), numericValue))
            {
                return (TEnum)(object)numericValue;
            }
        }

        throw new FormatException(
            $"Could not convert '{stringValue}' to {typeof(TEnum).Name}. " +
            $"Valid values: {string.Join(", ", Enum.GetNames(typeof(TEnum)))}");
    }

    public string? ConvertBack(TEnum value, string? format = null)
    {
        return value.ToString();
    }
}

/// <summary>
/// Converts HL7 string values to nullable enum types.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert to</typeparam>
public class NullableEnumConverter<TEnum> : IHL7TypeConverter<TEnum?> where TEnum : struct, Enum
{
    private readonly EnumConverter<TEnum> _innerConverter = new();

    public TEnum? Convert(ReadOnlySpan<char> value, string? format = null)
    {
        if (value.IsEmpty || value.ToString().Trim() == "\"\"")
            return null;

        return _innerConverter.Convert(value, format);
    }

    public string? ConvertBack(TEnum? value, string? format = null)
    {
        return value.HasValue ? _innerConverter.ConvertBack(value.Value, format) : null;
    }
}
