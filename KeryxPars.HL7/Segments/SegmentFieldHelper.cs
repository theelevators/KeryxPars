using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.Definitions;
using System.Text;

namespace KeryxPars.HL7.Segments;

/// <summary>
/// Helper methods for working with HL7 segment fields and datatypes.
/// Provides utilities for parsing repeating fields and serializing typed data.
/// </summary>
internal static class SegmentFieldHelper
{
    /// <summary>
    /// Parses a repeating field from an HL7 string into an array of typed datatypes.
    /// </summary>
    /// <typeparam name="T">The datatype implementing IHL7DataType</typeparam>
    /// <param name="value">The HL7 field value containing repetitions</param>
    /// <param name="delimiters">The HL7 delimiters to use for parsing</param>
    /// <returns>Array of parsed datatypes, or empty array if value is null/empty</returns>
    public static T[] ParseRepeatingField<T>(string value, in HL7Delimiters delimiters) where T : IHL7DataType, new()
    {
        if (string.IsNullOrWhiteSpace(value))
            return Array.Empty<T>();
        
        var parts = value.Split(delimiters.FieldRepeatSeparator);
        var result = new T[parts.Length];
        
        for (int i = 0; i < parts.Length; i++)
        {
            var item = new T();
            item.Parse(parts[i].AsSpan(), delimiters);
            result[i] = item;
        }
        
        return result;
    }

    /// <summary>
    /// Serializes an array of typed datatypes into an HL7 repeating field string.
    /// </summary>
    /// <typeparam name="T">The datatype implementing IHL7DataType</typeparam>
    /// <param name="items">The array of datatypes to serialize</param>
    /// <param name="delimiters">The HL7 delimiters to use for serialization</param>
    /// <returns>HL7 formatted string with repetitions, or empty string if array is null/empty</returns>
    public static string JoinRepeatingField<T>(T[] items, in HL7Delimiters delimiters) where T : IHL7DataType
    {
        if (items == null || items.Length == 0)
            return string.Empty;
        
        // Copy the delimiter value to avoid ref struct in loop
        var delimitersCopy = delimiters;
        char separator = delimitersCopy.FieldRepeatSeparator;
        
        var sb = new StringBuilder();
        for (int i = 0; i < items.Length; i++)
        {
            if (i > 0)
                sb.Append(separator);
            sb.Append(items[i].ToHL7String(delimitersCopy));
        }
        
        return sb.ToString();
    }
}
