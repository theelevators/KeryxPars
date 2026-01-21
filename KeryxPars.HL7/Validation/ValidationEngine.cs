using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.DataTypes.Contracts;

namespace KeryxPars.HL7.Validation;

/// <summary>
/// Executes validation rules against HL7 messages - optimized for performance.
/// </summary>
public sealed class ValidationEngine
{
    private readonly ValidationRules _rules;

    public ValidationEngine(ValidationRules rules)
    {
        _rules = rules;
    }

    /// <summary>
    /// Validates a message against the configured rules.
    /// </summary>
    public ValidationResult Validate(object message)
    {
        List<ValidationError>? errors = null; // Lazy allocation
        var context = new ValidationContext(message);

        // Validate required segments
        ValidateRequiredSegments(message, ref errors);

        // Validate field rules
        ValidateFieldRules(message, ref errors);

        // Validate conditional rules
        ValidateConditionalRules(message, context, ref errors);

        // Execute custom rules
        if (_rules.Custom.Count > 0)
        {
            foreach (var customRule in _rules.Custom)
            {
                var result = customRule.Validate(context);
                if (!result.IsValid)
                {
                    errors ??= [];
                    errors.AddRange(result.Errors);
                }
            }
        }

        return errors is null or { Count: 0 }
            ? ValidationResult.Success()
            : new ValidationResult { IsValid = false, Errors = errors };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateRequiredSegments(object message, ref List<ValidationError>? errors)
    {
        if (_rules.RequiredSegments.Count == 0) return;

        foreach (var segmentId in _rules.RequiredSegments)
        {
            if (!HasSegment(message, segmentId))
            {
                errors ??= [];
                errors.Add(new ValidationError(
                    Field: segmentId,
                    Message: $"{segmentId} segment is required",
                    Severity: ValidationSeverity.Error
                ));
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateFieldRules(object message, ref List<ValidationError>? errors)
    {
        if (_rules.Fields.Count == 0) return;

        foreach (var (fieldPath, rule) in _rules.Fields)
        {
            ReadOnlySpan<char> pathSpan = fieldPath.AsSpan();
            var dotIndex = pathSpan.IndexOf('.');
            if (dotIndex < 0) continue;

            var segmentId = pathSpan[..dotIndex].ToString();
            var fieldIndexSpan = pathSpan[(dotIndex + 1)..];
            
            if (!int.TryParse(fieldIndexSpan, out var fieldIndex))
                continue;

            var segment = GetSegment(message, segmentId);
            if (segment == null) continue;

            var fieldValue = GetFieldValue(segment, fieldIndex);
            ValidateField(fieldPath, fieldValue, rule, ref errors);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateConditionalRules(object message, ValidationContext context, ref List<ValidationError>? errors)
    {
        if (_rules.Conditional.Count == 0) return;

        foreach (var conditionalRule in _rules.Conditional)
        {
            if (EvaluateCondition(message, conditionalRule.When))
            {
                foreach (var (fieldPath, rule) in conditionalRule.Then)
                {
                    ReadOnlySpan<char> pathSpan = fieldPath.AsSpan();
                    var dotIndex = pathSpan.IndexOf('.');
                    if (dotIndex < 0) continue;

                    var segmentId = pathSpan[..dotIndex].ToString();
                    var fieldIndexSpan = pathSpan[(dotIndex + 1)..];
                    
                    if (!int.TryParse(fieldIndexSpan, out var fieldIndex))
                        continue;

                    var segment = GetSegment(message, segmentId);
                    if (segment == null) continue;

                    var fieldValue = GetFieldValue(segment, fieldIndex);
                    ValidateField(fieldPath, fieldValue, rule, ref errors);
                }
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ValidateField(string fieldPath, object? fieldValue, FieldRule rule, ref List<ValidationError>? errors)
    {
        // Required validation
        if (rule.Required && IsNullOrEmpty(fieldValue))
        {
            errors ??= [];
            errors.Add(new ValidationError(
                Field: fieldPath,
                Message: rule.CustomMessage ?? $"{fieldPath} is required",
                Severity: rule.Severity
            ));
            return; // No point checking other rules if required and empty
        }

        if (IsNullOrEmpty(fieldValue))
            return;

        var stringValue = GetStringValue(fieldValue);

        // Length validation
        if (rule.MaxLength.HasValue && stringValue.Length > rule.MaxLength.Value)
        {
            errors ??= [];
            errors.Add(new ValidationError(
                Field: fieldPath,
                Message: rule.CustomMessage ?? $"{fieldPath} exceeds maximum length of {rule.MaxLength.Value}",
                Severity: rule.Severity
            ));
        }

        if (rule.MinLength.HasValue && stringValue.Length < rule.MinLength.Value)
        {
            errors ??= [];
            errors.Add(new ValidationError(
                Field: fieldPath,
                Message: rule.CustomMessage ?? $"{fieldPath} is shorter than minimum length of {rule.MinLength.Value}",
                Severity: rule.Severity
            ));
        }

        // Pattern validation (use compiled regex for better performance)
        if (!string.IsNullOrEmpty(rule.Pattern))
        {
            try
            {
                if (!Regex.IsMatch(stringValue, rule.Pattern, RegexOptions.Compiled))
                {
                    errors ??= [];
                    errors.Add(new ValidationError(
                        Field: fieldPath,
                        Message: rule.CustomMessage ?? $"{fieldPath} does not match required pattern: {rule.Pattern}",
                        Severity: rule.Severity
                    ));
                }
            }
            catch
            {
                // Invalid regex pattern - skip validation
            }
        }

        // Allowed values validation
        if (rule.AllowedValues is { Length: > 0 } && !rule.AllowedValues.Contains(stringValue))
        {
            errors ??= [];
            errors.Add(new ValidationError(
                Field: fieldPath,
                Message: rule.CustomMessage ?? $"{fieldPath} must be one of: {string.Join(", ", rule.AllowedValues)}",
                Severity: rule.Severity
            ));
        }

        // Numeric range validation
        if ((rule.MinValue.HasValue || rule.MaxValue.HasValue) && decimal.TryParse(stringValue, out var numericValue))
        {
            if (rule.MinValue.HasValue && numericValue < rule.MinValue.Value)
            {
                errors ??= [];
                errors.Add(new ValidationError(
                    Field: fieldPath,
                    Message: rule.CustomMessage ?? $"{fieldPath} must be at least {rule.MinValue.Value}",
                    Severity: rule.Severity
                ));
            }

            if (rule.MaxValue.HasValue && numericValue > rule.MaxValue.Value)
            {
                errors ??= [];
                errors.Add(new ValidationError(
                    Field: fieldPath,
                    Message: rule.CustomMessage ?? $"{fieldPath} must be at most {rule.MaxValue.Value}",
                    Severity: rule.Severity
                ));
            }
        }
    }


    private bool EvaluateCondition(object message, string condition)
    {
        // Simple condition parser: "SegmentId.FieldIndex operator value"
        ReadOnlySpan<char> conditionSpan = condition.AsSpan();
        
        // Find first space (operator separator)
        var firstSpace = conditionSpan.IndexOf(' ');
        if (firstSpace < 0) return false;

        var fieldPath = conditionSpan[..firstSpace];
        var rest = conditionSpan[(firstSpace + 1)..].Trim();
        
        var secondSpace = rest.IndexOf(' ');
        var op = secondSpace > 0 ? rest[..secondSpace] : rest;
        var expectedValue = secondSpace > 0 ? rest[(secondSpace + 1)..].Trim() : ReadOnlySpan<char>.Empty;

        // Parse field path
        var dotIndex = fieldPath.IndexOf('.');
        if (dotIndex < 0) return false;

        var segmentId = fieldPath[..dotIndex].ToString();
        if (!int.TryParse(fieldPath[(dotIndex + 1)..], out var fieldIndex))
            return false;

        var segment = SegmentAccessor.GetSegment(message, segmentId);
        if (segment == null) return false;

        var fieldValue = SegmentAccessor.GetFieldValue(segment, fieldIndex);
        var actualValue = GetStringValue(fieldValue).AsSpan();

        // Evaluate operator
        return op switch
        {
            "=" or "==" or "equals" => actualValue.Equals(expectedValue, StringComparison.OrdinalIgnoreCase),
            "!=" or "notEquals" => !actualValue.Equals(expectedValue, StringComparison.OrdinalIgnoreCase),
            "contains" => actualValue.Contains(expectedValue, StringComparison.OrdinalIgnoreCase),
            "isEmpty" => actualValue.IsWhiteSpace(),
            "isNotEmpty" => !actualValue.IsWhiteSpace(),
            _ => false
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool HasSegment(object message, string segmentId)
    {
        return SegmentAccessor.HasSegment(message, segmentId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ISegment? GetSegment(object message, string segmentId)
    {
        return SegmentAccessor.GetSegment(message, segmentId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static object? GetFieldValue(ISegment segment, int fieldIndex)
    {
        return SegmentAccessor.GetFieldValue(segment, fieldIndex);
    }

    private static bool IsNullOrEmpty(object? value)
    {
        return value switch
        {
            null => true,
            string s => string.IsNullOrWhiteSpace(s),
            IPrimitiveDataType primitive => string.IsNullOrWhiteSpace(primitive.Value),
            Array arr => arr.Length == 0,
            _ => false
        };
    }

    private static string GetStringValue(object? value)
    {
        return value switch
        {
            null => string.Empty,
            string s => s,
            IPrimitiveDataType primitive => primitive.Value ?? string.Empty,
            _ => value.ToString() ?? string.Empty
        };
    }
}
