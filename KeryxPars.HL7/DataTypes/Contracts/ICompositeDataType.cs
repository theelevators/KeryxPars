namespace KeryxPars.HL7.DataTypes.Contracts;

/// <summary>
/// Interface for composite HL7 data types that contain multiple components.
/// Examples: XPN (Extended Person Name), XAD (Extended Address), CE (Coded Element), etc.
/// </summary>
public interface ICompositeDataType : IHL7DataType
{
    /// <summary>
    /// Gets the number of components in this composite data type.
    /// </summary>
    int ComponentCount { get; }

    /// <summary>
    /// Gets the component at the specified index (0-based).
    /// </summary>
    /// <param name="index">The component index (0-based).</param>
    /// <returns>The component data type, or null if empty or out of range.</returns>
    IHL7DataType? GetComponent(int index);

    /// <summary>
    /// Sets the component at the specified index (0-based).
    /// </summary>
    /// <param name="index">The component index (0-based).</param>
    /// <param name="value">The component value to set.</param>
    void SetComponent(int index, IHL7DataType? value);
}
