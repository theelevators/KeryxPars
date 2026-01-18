namespace KeryxPars.HL7.DataTypes.Contracts;

/// <summary>
/// Interface for primitive (atomic) HL7 data types that contain a single value.
/// Examples: ST (String), NM (Numeric), DT (Date), etc.
/// </summary>
public interface IPrimitiveDataType : IHL7DataType
{
    /// <summary>
    /// Gets or sets the string value of this primitive data type.
    /// </summary>
    string Value { get; set; }
}
