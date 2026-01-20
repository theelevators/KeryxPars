using System;

namespace KeryxPars.HL7.Mapping.Core;

/// <summary>
/// Evaluates simple HL7 field conditions for conditional mapping.
/// Supports: ==, !=, &&, ||
/// Examples: "PID.8 == M", "PV1.2 != O", "PID.8 == F && PV1.2 == I"
/// </summary>
public static class ConditionEvaluator
{
    /// <summary>
    /// Evaluates a condition against an HL7 message.
    /// </summary>
    /// <param name="message">HL7 message span</param>
    /// <param name="condition">Condition expression (e.g., "PID.8 == M")</param>
    /// <returns>True if condition is met, false otherwise</returns>
    public static bool Evaluate(ReadOnlySpan<char> message, string condition)
    {
        if (string.IsNullOrWhiteSpace(condition))
            return true; // No condition = always true

        // Handle AND conditions first (lower precedence)
        var andIndex = condition.IndexOf("&&");
        if (andIndex >= 0)
        {
            var left = condition.Substring(0, andIndex).Trim();
            var right = condition.Substring(andIndex + 2).Trim();
            return Evaluate(message, left) && Evaluate(message, right);
        }

        // Handle OR conditions
        var orIndex = condition.IndexOf("||");
        if (orIndex >= 0)
        {
            var left = condition.Substring(0, orIndex).Trim();
            var right = condition.Substring(orIndex + 2).Trim();
            return Evaluate(message, left) || Evaluate(message, right);
        }

        // Parse simple comparison: "FIELD_PATH OP VALUE"
        var eqIndex = condition.IndexOf("==");
        var neIndex = condition.IndexOf("!=");

        if (eqIndex >= 0)
        {
            // Equality check
            var fieldPath = condition.Substring(0, eqIndex).Trim();
            var expectedValue = condition.Substring(eqIndex + 2).Trim().Trim('\'', '"', ' ');

            var actualValue = Parsers.HL7SpanParser.GetValue(message, fieldPath);
            
            // Empty check - both must be empty or both must match
            if (Parsers.HL7SpanParser.IsNullOrWhiteSpace(actualValue))
                return string.IsNullOrEmpty(expectedValue);

            return actualValue.Equals(expectedValue.AsSpan(), StringComparison.OrdinalIgnoreCase);
        }
        else if (neIndex >= 0)
        {
            // Inequality check
            var fieldPath = condition.Substring(0, neIndex).Trim();
            var expectedValue = condition.Substring(neIndex + 2).Trim().Trim('\'', '"', ' ');

            var actualValue = Parsers.HL7SpanParser.GetValue(message, fieldPath);
            
            if (Parsers.HL7SpanParser.IsNullOrWhiteSpace(actualValue))
                return !string.IsNullOrEmpty(expectedValue);

            return !actualValue.Equals(expectedValue.AsSpan(), StringComparison.OrdinalIgnoreCase);
        }

        // Default: unrecognized condition = true (fail open)
        return true;
    }

    /// <summary>
    /// Checks if a field value is not null or empty.
    /// </summary>
    public static bool IsNotEmpty(ReadOnlySpan<char> value)
    {
        return !Parsers.HL7SpanParser.IsNullOrWhiteSpace(value);
    }
}
