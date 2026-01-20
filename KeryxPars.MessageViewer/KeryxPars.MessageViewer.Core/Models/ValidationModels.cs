using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Validation;

namespace KeryxPars.MessageViewer.Core.Models;

/// <summary>
/// UI representation of a validation rule.
/// </summary>
public class RuleDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SegmentId { get; set; } = "";
    public int? FieldIndex { get; set; }
    public RuleType Type { get; set; }
    public Dictionary<string, object> Config { get; set; } = new();
    public bool IsEnabled { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CustomMessage { get; set; }
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;

    /// <summary>
    /// Converts this UI rule to a library FieldRule.
    /// </summary>
    public FieldRule ToFieldRule()
    {
        var fieldRule = new FieldRule { Severity = Severity };

        return Type switch
        {
            RuleType.Required => new FieldRule 
            { 
                Required = true, 
                CustomMessage = CustomMessage,
                Severity = Severity
            },
            RuleType.MaxLength => new FieldRule 
            { 
                MaxLength = GetConfigValue<int>("maxLength"),
                CustomMessage = CustomMessage,
                Severity = Severity
            },
            RuleType.MinLength => new FieldRule 
            { 
                MinLength = GetConfigValue<int>("minLength"),
                CustomMessage = CustomMessage,
                Severity = Severity
            },
            RuleType.Pattern => new FieldRule 
            { 
                Pattern = GetConfigValue<string>("pattern"),
                CustomMessage = CustomMessage,
                Severity = Severity
            },
            RuleType.AllowedValues => new FieldRule 
            { 
                AllowedValues = GetConfigValue<string[]>("allowedValues"),
                CustomMessage = CustomMessage,
                Severity = Severity
            },
            RuleType.NumericRange => new FieldRule 
            { 
                MinValue = GetConfigValue<decimal?>("minValue"),
                MaxValue = GetConfigValue<decimal?>("maxValue"),
                CustomMessage = CustomMessage,
                Severity = Severity
            },
            _ => new FieldRule { Severity = Severity }
        };
    }

    private T? GetConfigValue<T>(string key)
    {
        if (Config.TryGetValue(key, out var value) && value is T typedValue)
            return typedValue;
        return default;
    }
}

/// <summary>
/// Named collection of validation rules.
/// </summary>
public class ValidationProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Untitled";
    public string? Description { get; set; }
    public List<RuleDefinition> Rules { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsBuiltIn { get; set; } = false;

    /// <summary>
    /// Converts this profile to library ValidationRules.
    /// </summary>
    public ValidationRules ToValidationRules()
    {
        var enabledRules = Rules.Where(r => r.IsEnabled).ToList();

        // Group field rules by field path and merge them
        var fieldRules = enabledRules
            .Where(r => r.Type != RuleType.RequiredSegment && r.FieldIndex.HasValue)
            .GroupBy(r => $"{r.SegmentId}.{r.FieldIndex}")
            .ToDictionary(
                g => g.Key,
                g => MergeFieldRules(g.ToList())
            );

        return new ValidationRules
        {
            RequiredSegments = enabledRules
                .Where(r => r.Type == RuleType.RequiredSegment)
                .Select(r => r.SegmentId)
                .Distinct()
                .ToList(),
            Fields = fieldRules
        };
    }

    /// <summary>
    /// Merges multiple rules for the same field into a single FieldRule.
    /// </summary>
    private static FieldRule MergeFieldRules(List<RuleDefinition> rules)
    {
        var highestSeverity = rules.Max(r => r.Severity);
        var customMessage = rules.FirstOrDefault(r => !string.IsNullOrEmpty(r.CustomMessage))?.CustomMessage;

        bool required = false;
        int? maxLength = null;
        int? minLength = null;
        string? pattern = null;
        string[]? allowedValues = null;
        decimal? minValue = null;
        decimal? maxValue = null;

        foreach (var rule in rules)
        {
            switch (rule.Type)
            {
                case RuleType.Required:
                    required = true;
                    customMessage ??= rule.CustomMessage;
                    break;

                case RuleType.MaxLength:
                    if (rule.Config.TryGetValue("maxLength", out var maxLen))
                    {
                        maxLength = maxLen switch
                        {
                            int i => i,
                            long l => (int)l,
                            string s when int.TryParse(s, out var parsed) => parsed,
                            _ => maxLength
                        };
                        customMessage ??= rule.CustomMessage;
                    }
                    break;

                case RuleType.MinLength:
                    if (rule.Config.TryGetValue("minLength", out var minLen))
                    {
                        minLength = minLen switch
                        {
                            int i => i,
                            long l => (int)l,
                            string s when int.TryParse(s, out var parsed) => parsed,
                            _ => minLength
                        };
                        customMessage ??= rule.CustomMessage;
                    }
                    break;

                case RuleType.Pattern:
                    if (rule.Config.TryGetValue("pattern", out var pat))
                    {
                        pattern = pat?.ToString();
                        customMessage ??= rule.CustomMessage;
                    }
                    break;

                case RuleType.AllowedValues:
                    if (rule.Config.TryGetValue("allowedValues", out var allowed))
                    {
                        allowedValues = allowed as string[];
                        customMessage ??= rule.CustomMessage;
                    }
                    break;

                case RuleType.NumericRange:
                    if (rule.Config.TryGetValue("minValue", out var mnv))
                    {
                        minValue = mnv switch
                        {
                            decimal d => d,
                            int i => i,
                            long l => l,
                            double db => (decimal)db,
                            float f => (decimal)f,
                            string s when decimal.TryParse(s, out var parsed) => parsed,
                            _ => minValue
                        };
                    }
                    if (rule.Config.TryGetValue("maxValue", out var mxv))
                    {
                        maxValue = mxv switch
                        {
                            decimal d => d,
                            int i => i,
                            long l => l,
                            double db => (decimal)db,
                            float f => (decimal)f,
                            string s when decimal.TryParse(s, out var parsed) => parsed,
                            _ => maxValue
                        };
                    }
                    customMessage ??= rule.CustomMessage;
                    break;
            }
        }

        // Use object initializer syntax (required for init-only properties)
        return new FieldRule
        {
            Required = required,
            MaxLength = maxLength,
            MinLength = minLength,
            Pattern = pattern,
            AllowedValues = allowedValues,
            MinValue = minValue,
            MaxValue = maxValue,
            CustomMessage = customMessage,
            Severity = highestSeverity
        };
    }
}

/// <summary>
/// Type of validation rule.
/// </summary>
public enum RuleType
{
    None,
    RequiredSegment,
    Required,
    MaxLength,
    MinLength,
    Pattern,
    AllowedValues,
    DateFormat,
    NumericRange,
    Conditional,
    Custom
}
