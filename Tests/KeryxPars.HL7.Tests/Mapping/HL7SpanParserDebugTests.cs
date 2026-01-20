using KeryxPars.HL7.Mapping.Parsers;
using KeryxPars.HL7.Mapping.Core;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

public class HL7SpanParserDebugTests
{
    [Fact]
    public void FindSegment_SimplePID_ShouldWork()
    {
        // Arrange
        var message = "MSH|test|\rPID|1||MRN123||";
        var span = message.AsSpan();

        // Act
        var pid = HL7SpanParser.FindSegment(span, "PID");

        // Assert
        pid.IsEmpty.ShouldBeFalse();
        pid.ToString().ShouldStartWith("PID");
    }

    [Fact]
    public void GetField_SimplePID_ShouldWork()
    {
        // Arrange
        var segment = "PID|1||MRN123||".AsSpan();

        // Act
        var field = HL7SpanParser.GetField(segment, 3);

        // Assert
        field.ToString().ShouldBe("MRN123");
    }

    [Fact]
    public void GetValue_PID3_ShouldWork()
    {
        // Arrange
        var message = "MSH|test|\rPID|1||MRN123||";
        var notation = FieldNotation.Parse("PID.3");

        // Act
        var value = HL7SpanParser.GetValue(message.AsSpan(), notation);

        // Assert
        value.ToString().ShouldBe("MRN123");
    }
}
