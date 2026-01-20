using System;

namespace KeryxPars.HL7.Mapping.Converters;

/// <summary>
/// Converts DateTime to HL7 string format.
/// </summary>
public static class DateTimeReverseConverter
{
    /// <summary>
    /// Converts DateTime to HL7 format string.
    /// </summary>
    public static string ToHL7(DateTime value, string format = "yyyyMMdd")
    {
        return value.ToString(format);
    }

    /// <summary>
    /// Converts nullable DateTime to HL7 format string.
    /// </summary>
    public static string? ToHL7(DateTime? value, string format = "yyyyMMdd")
    {
        return value?.ToString(format);
    }
}

/// <summary>
/// Converts Enum to HL7 string code.
/// </summary>
public static class EnumReverseConverter
{
    /// <summary>
    /// Converts enum to HL7 code string.
    /// </summary>
    public static string ToHL7<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        return value.ToString();
    }

    /// <summary>
    /// Converts nullable enum to HL7 code string.
    /// </summary>
    public static string? ToHL7<TEnum>(TEnum? value) where TEnum : struct, Enum
    {
        return value?.ToString();
    }
}
