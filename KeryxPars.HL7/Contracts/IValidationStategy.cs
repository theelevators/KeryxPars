namespace KeryxPars.HL7.Contracts;

/// <summary>
/// Strategy for validating messages and segments.
/// </summary>
public interface IValidationStrategy
{
    /// <summary>
    /// Validates a complete message.
    /// </summary>
    ValidationResult ValidateMessage(object message);

    /// <summary>
    /// Validates an individual segment.
    /// </summary>
    ValidationResult ValidateSegment(ISegment segment, SegmentMetadata? metadata);
}

/// <summary>
/// Result of a validation operation.
/// </summary>
public sealed class ValidationResult
{
    public bool IsValid { get; init; }
    public List<ValidationError> Errors { get; init; } = [];

    public static ValidationResult Success() => new() { IsValid = true };

    public static ValidationResult Failure(params ValidationError[] errors) =>
        new() { IsValid = false, Errors = [.. errors] };
}

/// <summary>
/// Represents a validation error.
/// </summary>
public sealed record ValidationError(
    string Field,
    string Message,
    ValidationSeverity Severity = ValidationSeverity.Error);

public enum ValidationSeverity
{
    Information,
    Warning,
    Error,
    Critical
}