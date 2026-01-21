using KeryxPars.HL7.Contracts;

namespace KeryxPars.HL7.Validation;

/// <summary>
/// Simple POCO-based validation rules - optimized for performance.
/// </summary>
public sealed class ValidationRules
{
    /// <summary>
    /// List of segment IDs that are required in the message.
    /// </summary>
    public List<string> RequiredSegments { get; init; } = [];

    /// <summary>
    /// Field-level validation rules.
    /// Key format: "SegmentId.FieldIndex" (e.g., "MSH.9", "PID.3")
    /// </summary>
    public Dictionary<string, FieldRule> Fields { get; init; } = [];

    /// <summary>
    /// Conditional validation rules.
    /// </summary>
    public List<ConditionalRule> Conditional { get; init; } = [];

    /// <summary>
    /// Custom validation rules.
    /// </summary>
    public List<IValidationRule> Custom { get; init; } = [];

    /// <summary>
    /// Validates a message against these rules.
    /// </summary>
    public ValidationResult Validate(object message)
    {
        var engine = new ValidationEngine(this);
        return engine.Validate(message);
    }
}


/// <summary>
/// Validation rule for a specific field.
/// </summary>
public sealed class FieldRule
{
    public bool Required { get; init; }
    public int? MaxLength { get; init; }
    public int? MinLength { get; init; }
    public string? Pattern { get; init; }
    public string[]? AllowedValues { get; init; }
    public decimal? MinValue { get; init; }
    public decimal? MaxValue { get; init; }
    public string? CustomMessage { get; init; }
    public ValidationSeverity Severity { get; init; } = ValidationSeverity.Error;
}

/// <summary>
/// Conditional validation rule.
/// Example: When MSH.11 = "P", then RXE.2 is required.
/// </summary>
public sealed class ConditionalRule
{
    /// <summary>
    /// Condition expression (simple format: "SegmentId.FieldIndex operator value")
    /// Examples: "MSH.11 = P", "ORC.1 != CA"
    /// </summary>
    public string When { get; init; } = "";

    /// <summary>
    /// Rules to apply when condition is true.
    /// </summary>
    public Dictionary<string, FieldRule> Then { get; init; } = [];
}

/// <summary>
/// Interface for custom validation rules.
/// </summary>
public interface IValidationRule
{
    string RuleId { get; }
    string Description { get; }
    ValidationResult Validate(ValidationContext context);
}
