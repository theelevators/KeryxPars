namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Error information for HL7 mapping failures.
/// Used with Result&lt;T, E&gt; pattern instead of throwing exceptions.
/// </summary>
public record HL7MappingError
{
    /// <summary>
    /// Error message describing what went wrong.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// The HL7 field path that caused the error (e.g., "PID.7").
    /// </summary>
    public string FieldPath { get; init; }

    /// <summary>
    /// The target type being mapped to.
    /// </summary>
    public string TargetType { get; init; }

    /// <summary>
    /// Creates a new HL7 mapping error.
    /// </summary>
    public HL7MappingError(string message, string fieldPath, string targetType)
    {
        Message = message ?? "Unknown error";
        FieldPath = fieldPath ?? "N/A";
        TargetType = targetType ?? "N/A";
    }

    /// <summary>
    /// Returns a user-friendly error message.
    /// </summary>
    public override string ToString()
    {
        return $"HL7 Mapping Error: {Message} (Field: {FieldPath}, Target: {TargetType})";
    }
}
