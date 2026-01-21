using System;

namespace KeryxPars.HL7.Mapping.Converters;

/// <summary>
/// Interface for custom HL7 type converters.
/// Implement this to create custom conversion logic for complex types.
/// </summary>
/// <typeparam name="T">The target type to convert to</typeparam>
public interface IHL7TypeConverter<T>
{
    /// <summary>
    /// Converts an HL7 field value to the target type.
    /// </summary>
    /// <param name="value">The HL7 field value as a span</param>
    /// <param name="format">Optional format string (e.g., date format)</param>
    /// <returns>Converted value</returns>
    T Convert(ReadOnlySpan<char> value, string? format = null);

    /// <summary>
    /// Converts the target type back to HL7 format (for reverse mapping).
    /// </summary>
    /// <param name="value">The domain model value</param>
    /// <param name="format">Optional format string</param>
    /// <returns>HL7 formatted string</returns>
    string? ConvertBack(T value, string? format = null);
}
