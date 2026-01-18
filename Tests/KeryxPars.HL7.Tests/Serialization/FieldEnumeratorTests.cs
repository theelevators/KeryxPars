namespace KeryxPars.HL7.Tests.Serialization;

/// <summary>
/// Tests for FieldEnumerator to ensure proper iteration and no out-of-bounds access
/// </summary>
public class FieldEnumeratorTests
{
    [Fact]
    public void MoveNext_WithValidFields_ShouldEnumerateCorrectly()
    {
        // Arrange
        var span = "field1|field2|field3".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("field1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("field2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("field3");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithEmptySpan_ShouldReturnFalse()
    {
        // Arrange
        var span = "".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithSingleField_ShouldEnumerateOnce()
    {
        // Arrange
        var span = "singlefield".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("singlefield");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithEmptyFields_ShouldEnumerateEmptySpans()
    {
        // Arrange
        var span = "||value||".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("value");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithConsecutiveDelimiters_ShouldHandleCorrectly()
    {
        // Arrange
        var span = "a|||b".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("a");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.IsEmpty.ShouldBeTrue();

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("b");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithDifferentDelimiter_ShouldUseCorrectDelimiter()
    {
        // Arrange
        var span = "part1^part2^part3".AsSpan();
        var enumerator = new FieldEnumerator(span, '^');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("part1");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("part2");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("part3");

        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_AfterExhaustion_ShouldKeepReturningFalse()
    {
        // Arrange
        var span = "field".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void MoveNext_WithLargeNumberOfFields_ShouldHandleCorrectly()
    {
        // Arrange
        var fields = string.Join("|", Enumerable.Range(1, 1000));
        var span = fields.AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

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
    public void MoveNext_WithSpecialCharacters_ShouldHandleCorrectly()
    {
        // Arrange
        var span = "field~with~tildes|field^with^carets|normal".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("field~with~tildes");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("field^with^carets");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("normal");
    }

    [Fact]
    public void Current_BeforeFirstMoveNext_ShouldBeEmpty()
    {
        // Arrange
        var span = "field1|field2".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Assert
        enumerator.Current.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void MoveNext_WithVeryLongField_ShouldHandleCorrectly()
    {
        // Arrange
        var longField = new string('X', 50000);
        var span = $"short|{longField}|end".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act & Assert
        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("short");

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.Length.ShouldBe(50000);

        enumerator.MoveNext().ShouldBeTrue();
        enumerator.Current.ToString().ShouldBe("end");
    }
}
