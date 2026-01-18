namespace KeryxPars.HL7.Tests.Serialization;

/// <summary>
/// Tests for LineEnumerator to ensure proper line parsing and no out-of-bounds access
/// </summary>
public class LineEnumeratorTests
{
    [Fact]
    public void MoveNext_WithCRLF_ShouldEnumerateLines()
    {
        // Arrange
        var text = "Line1\r\nLine2\r\nLine3".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line3");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithLFOnly_ShouldEnumerateLines()
    {
        // Arrange
        var text = "Line1\nLine2\nLine3".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line3");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithCROnly_ShouldEnumerateLines()
    {
        // Arrange
        var text = "Line1\rLine2\rLine3".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line3");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithMixedLineEndings_ShouldHandleCorrectly()
    {
        // Arrange
        var text = "Line1\r\nLine2\nLine3\rLine4".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line3");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line4");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithEmptyText_ShouldReturnFalse()
    {
        // Arrange
        var text = "".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithSingleLine_ShouldEnumerateOnce()
    {
        // Arrange
        var text = "SingleLine".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("SingleLine");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithEmptyLines_ShouldEnumerateEmptySpans()
    {
        // Arrange
        var text = "Line1\n\nLine3".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line3");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithTrailingNewline_ShouldHandleCorrectly()
    {
        // Arrange
        var text = "Line1\nLine2\n".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithLeadingNewline_ShouldHandleCorrectly()
    {
        // Arrange
        var text = "\nLine1\nLine2".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Line2");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithVeryLongLine_ShouldHandleCorrectly()
    {
        // Arrange
        var longLine = new string('A', 100000);
        var text = $"Short\n{longLine}\nEnd".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("Short");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.Length.ShouldBe(100000);

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("End");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithManyLines_ShouldHandleCorrectly()
    {
        // Arrange
        var lines = string.Join("\n", Enumerable.Range(1, 1000));
        var text = lines.AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act
        var count = 0;
        while (enumerator.MoveNext())
        {
            count++;
            int.Parse(enumerator.Current).ShouldBe(count);
        }

        // Assert
        count.ShouldBe(1000);
    }

    [Fact]
    public void Current_BeforeFirstMoveNext_ShouldBeEmpty()
    {
        // Arrange
        var text = "Line1\nLine2".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Assert
        enumerator.Current.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void MoveNext_AfterExhaustion_ShouldKeepReturningFalse()
    {
        // Arrange
        var text = "Line1".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithHL7Message_ShouldParseSegments()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||\r\nPID|1||12345||DOE^JOHN||19800101|M\r\nPV1|1|I|WARD|||".AsSpan();
        var enumerator = new LineEnumerator(message);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldStartWith("MSH");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldStartWith("PID");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldStartWith("PV1");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithOnlyNewlines_ShouldEnumerateEmptyLines()
    {
        // Arrange
        var text = "\n\n\n".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeFalse();
    }
}
