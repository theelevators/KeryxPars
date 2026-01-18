namespace KeryxPars.HL7.Tests.Serialization;

/// <summary>
/// Tests for SegmentReader to ensure no out-of-bounds access and correct parsing
/// </summary>
public class SegmentReaderTests
{
    [Fact]
    public void TryReadField_WithValidSegment_ShouldReadFieldsCorrectly()
    {
        // Arrange
        var segment = "PID|1||12345||DOE^JOHN||19800101|M".AsSpan();
        var reader = new SegmentReader(segment);

        // Act & Assert
        reader.TryReadField('|', out var field1).ShouldBeTrue();
        field1.ToString().ShouldBe("PID");

        reader.TryReadField('|', out var field2).ShouldBeTrue();
        field2.ToString().ShouldBe("1");

        reader.TryReadField('|', out var field3).ShouldBeTrue();
        field3.ToString().ShouldBe("");

        reader.TryReadField('|', out var field4).ShouldBeTrue();
        field4.ToString().ShouldBe("12345");
    }

    [Fact]
    public void TryReadField_WithEmptySegment_ShouldReturnFalse()
    {
        // Arrange
        var segment = "".AsSpan();
        var reader = new SegmentReader(segment);

        // Act
        var result = reader.TryReadField('|', out var field);

        // Assert
        result.ShouldBeFalse();
        field.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void TryReadField_ReadingBeyondEnd_ShouldReturnFalse()
    {
        // Arrange
        var segment = "PID|1".AsSpan();
        var reader = new SegmentReader(segment);

        // Act - Read all fields
        reader.TryReadField('|', out _); // PID
        reader.TryReadField('|', out _); // 1
        var result = reader.TryReadField('|', out var field);

        // Assert
        result.ShouldBeFalse();
        field.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void TryReadField_WithLastFieldNoDelimiter_ShouldReadCorrectly()
    {
        // Arrange
        var segment = "MSH|FIELD1|LASTFIELD".AsSpan();
        var reader = new SegmentReader(segment);

        // Act
        reader.TryReadField('|', out _); // MSH
        reader.TryReadField('|', out _); // FIELD1
        var result = reader.TryReadField('|', out var lastField);

        // Assert
        result.ShouldBeTrue();
        lastField.ToString().ShouldBe("LASTFIELD");
    }

    [Fact]
    public void TryReadField_WithConsecutiveDelimiters_ShouldReturnEmptyFields()
    {
        // Arrange
        var segment = "PID|||VALUE".AsSpan();
        var reader = new SegmentReader(segment);

        // Act
        reader.TryReadField('|', out var field1).ShouldBeTrue();
        field1.ToString().ShouldBe("PID");

        reader.TryReadField('|', out var field2).ShouldBeTrue();
        field2.IsEmpty.ShouldBeTrue();

        reader.TryReadField('|', out var field3).ShouldBeTrue();
        field3.IsEmpty.ShouldBeTrue();

        reader.TryReadField('|', out var field4).ShouldBeTrue();
        field4.ToString().ShouldBe("VALUE");
    }

    [Fact]
    public void IsAtEnd_InitiallyFalse_ThenTrueAfterReadingAll()
    {
        // Arrange
        var segment = "PID|1".AsSpan();
        var reader = new SegmentReader(segment);

        // Assert - Initial
        reader.IsAtEnd.ShouldBeFalse();

        // Act
        reader.TryReadField('|', out _);
        reader.TryReadField('|', out _);

        // Assert - After reading all
        reader.IsAtEnd.ShouldBeTrue();
    }

    [Fact]
    public void Remaining_ShouldReturnCorrectSpan()
    {
        // Arrange
        var segment = "PID|1|2|3".AsSpan();
        var reader = new SegmentReader(segment);

        // Act
        reader.TryReadField('|', out _); // PID
        var remaining = reader.Remaining;

        // Assert
        remaining.ToString().ShouldBe("1|2|3");
    }

    [Fact]
    public void EnumerateFields_ShouldEnumerateAllFields()
    {
        // Arrange
        var segment = "PID|1|2|3".AsSpan();
        var reader = new SegmentReader(segment);
        reader.TryReadField('|', out _); // Skip PID

        // Act
        var enumerator = reader.EnumerateFields('|');
        var fields = new List<string>();
        
        while (enumerator.MoveNext())
        {
            fields.Add(enumerator.Current.ToString());
        }

        // Assert
        fields.ShouldBe(new[] { "1", "2", "3" });
    }

    [Fact]
    public void TryReadField_WithVeryLongField_ShouldHandleCorrectly()
    {
        // Arrange
        var longValue = new string('A', 10000);
        var segment = $"PID|{longValue}|END".AsSpan();
        var reader = new SegmentReader(segment);

        // Act
        reader.TryReadField('|', out _); // PID
        var result = reader.TryReadField('|', out var field);

        // Assert
        result.ShouldBeTrue();
        field.Length.ShouldBe(10000);
        field.ToString().ShouldBe(longValue);
    }

    [Fact]
    public void TryReadField_WithSingleCharacterFields_ShouldReadCorrectly()
    {
        // Arrange
        var segment = "A|B|C|D|E".AsSpan();
        var reader = new SegmentReader(segment);

        // Act & Assert
        reader.TryReadField('|', out var f1).ShouldBeTrue();
        f1.ToString().ShouldBe("A");
        reader.TryReadField('|', out var f2).ShouldBeTrue();
        f2.ToString().ShouldBe("B");
        reader.TryReadField('|', out var f3).ShouldBeTrue();
        f3.ToString().ShouldBe("C");
    }

    [Fact]
    public void TryReadField_WithTrailingDelimiter_ShouldHandleCorrectly()
    {
        // Arrange
        var segment = "PID|1|2|".AsSpan();
        var reader = new SegmentReader(segment);

        // Act
        reader.TryReadField('|', out _); // PID
        reader.TryReadField('|', out _); // 1
        reader.TryReadField('|', out _); // 2
        var result = reader.TryReadField('|', out var field);

        // Assert
        result.ShouldBeTrue();
        field.IsEmpty.ShouldBeTrue();
    }
}
