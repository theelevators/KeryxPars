using System.Text.Json;
using KeryxPars.HL7.Contracts;

namespace KeryxPars.HL7.Validation;

/// <summary>
/// Simple static helpers for validation.
/// </summary>
public static class Validator
{
    /// <summary>
    /// Quick validation for required segments.
    /// </summary>
    public static ValidationResult Required(object message, params string[] segments)
    {
        var rules = new ValidationRules 
        { 
            RequiredSegments = [.. segments] 
        };
        return rules.Validate(message);
    }

    /// <summary>
    /// Validates a message from JSON rules.
    /// </summary>
    public static ValidationResult FromJson(object message, string jsonRules)
    {
        var rules = JsonSerializer.Deserialize<ValidationRules>(jsonRules);
        if (rules == null)
            return ValidationResult.Success();
            
        return rules.Validate(message);
    }

    /// <summary>
    /// Validates a message from a JSON file.
    /// </summary>
    public static ValidationResult FromFile(object message, string path)
    {
        if (!File.Exists(path))
            return ValidationResult.Success();
            
        var json = File.ReadAllText(path);
        return FromJson(message, json);
    }

    /// <summary>
    /// Creates a new validation rules builder.
    /// </summary>
    public static ValidationRules CreateRules(Action<ValidationRules>? configure = null)
    {
        var rules = new ValidationRules();
        configure?.Invoke(rules);
        return rules;
    }
}
