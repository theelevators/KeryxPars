using KeryxPars.HL7.Mapping.Core;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

public class FieldNotationTests
{
    [Fact]
    public void Parse_SegmentOnly_ShouldWork()
    {
        // Arrange & Act
        var notation = FieldNotation.Parse("PID");

        // Assert
        notation.SegmentId.ShouldBe("PID");
        notation.FieldIndex.ShouldBeNull();
        notation.Level.ShouldBe(AccessLevel.Segment);
    }

    [Fact]
    public void Parse_SegmentAndField_ShouldWork()
    {
        // Arrange & Act
        var notation = FieldNotation.Parse("PID.3");

        // Assert
        notation.SegmentId.ShouldBe("PID");
        notation.FieldIndex.ShouldBe(3);
        notation.ComponentIndex.ShouldBeNull();
        notation.Level.ShouldBe(AccessLevel.Field);
    }

    [Fact]
    public void Parse_ThreeLetterSegment_ShouldWork()
    {
        // Arrange & Act
        var notation = FieldNotation.Parse("PV1.2");

        // Assert
        notation.SegmentId.ShouldBe("PV1");
        notation.FieldIndex.ShouldBe(2);
        notation.ComponentIndex.ShouldBeNull();
        notation.Level.ShouldBe(AccessLevel.Field);
    }

    [Fact]
    public void Parse_Component_ShouldWork()
    {
        // Arrange & Act
        var notation = FieldNotation.Parse("PID.5.1");

        // Assert
        notation.SegmentId.ShouldBe("PID");
        notation.FieldIndex.ShouldBe(5);
        notation.ComponentIndex.ShouldBe(1);
        notation.Level.ShouldBe(AccessLevel.Component);
    }

    [Fact]
    public void Parse_Subcomponent_ShouldWork()
    {
        // Arrange & Act
        var notation = FieldNotation.Parse("PID.5.1.1");

        // Assert
        notation.SegmentId.ShouldBe("PID");
        notation.FieldIndex.ShouldBe(5);
        notation.ComponentIndex.ShouldBe(1);
        notation.SubcomponentIndex.ShouldBe(1);
        notation.Level.ShouldBe(AccessLevel.Subcomponent);
    }

    [Fact]
    public void Parse_WithRepetition_ShouldWork()
    {
        // Arrange & Act
        var notation = FieldNotation.Parse("PID.3[1]");

        // Assert
        notation.SegmentId.ShouldBe("PID");
        notation.FieldIndex.ShouldBe(3);
        notation.RepetitionIndex.ShouldBe(1);
        notation.Level.ShouldBe(AccessLevel.Field);
    }
}
