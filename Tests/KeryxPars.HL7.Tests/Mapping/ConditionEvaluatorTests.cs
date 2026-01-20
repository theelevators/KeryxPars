using System;
using KeryxPars.HL7.Mapping.Core;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

public class ConditionEvaluatorTests
{
    private const string TestMessage = 
        "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
        "PID|1||MRN123||DOE^JANE||19850315|F|||123 MAIN ST|||||||||\r" +
        "PV1|1|I|ICU^201^A||||||||||||||||VISIT123|||||||||||||||||||||||||||||||||||||||";

    [Fact]
    public void ConditionEvaluator_EqualityCheck_ShouldWork()
    {
        // Act
        var result = ConditionEvaluator.Evaluate(TestMessage.AsSpan(), "PID.8 == F");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void ConditionEvaluator_InequalityCheck_ShouldWork()
    {
        // Act
        var result = ConditionEvaluator.Evaluate(TestMessage.AsSpan(), "PID.8 != M");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void ConditionEvaluator_AndCondition_ShouldWork()
    {
        // Act
        var result = ConditionEvaluator.Evaluate(TestMessage.AsSpan(), "PID.8 == F && PV1.2 == I");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void ConditionEvaluator_FalseCondition_ShouldReturnFalse()
    {
        // Act
        var result = ConditionEvaluator.Evaluate(TestMessage.AsSpan(), "PID.8 == M");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void ConditionEvaluator_EmptyCondition_ShouldReturnTrue()
    {
        // Act
        var result = ConditionEvaluator.Evaluate(TestMessage.AsSpan(), "");

        // Assert
        result.ShouldBeTrue();
    }
}
