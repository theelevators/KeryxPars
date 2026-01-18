using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.DataTypes.Contracts;

/// <summary>
/// Base interface for all HL7 data types (primitive and composite).
/// Provides common functionality for parsing, validation, and serialization.
/// </summary>
public interface IHL7DataType
{
    /// <summary>
    /// Gets the HL7 data type code (e.g., "ST", "XPN", "CE").
    /// </summary>
    string TypeCode { get; }

    /// <summary>
    /// Indicates whether this data type contains any non-empty values.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Converts this data type to its HL7 string representation.
    /// </summary>
    /// <param name="delimiters">The HL7 delimiters to use for formatting.</param>
    /// <returns>The HL7-formatted string.</returns>
    string ToHL7String(in HL7Delimiters delimiters);

    /// <summary>
    /// Parses an HL7 string value into this data type.
    /// </summary>
    /// <param name="value">The HL7 string value to parse.</param>
    /// <param name="delimiters">The HL7 delimiters used in the value.</param>
    void Parse(ReadOnlySpan<char> value, in HL7Delimiters delimiters);

    /// <summary>
    /// Validates this data type according to HL7 2.5 rules.
    /// </summary>
    /// <param name="errors">List of validation errors, if any.</param>
    /// <returns>True if valid, false otherwise.</returns>
    bool Validate(out List<string> errors);
}
