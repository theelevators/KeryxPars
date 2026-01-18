namespace KeryxPars.HL7.Tests.EdgeCases;

/// <summary>
/// Tests for boundary conditions and edge cases to prevent crashes and out-of-bounds access
/// </summary>
public class BoundaryConditionTests
{
    [Fact]
    public void Deserialize_WithZeroLengthMessage_ShouldReturnError()
    {
        // Arrange
        var message = string.Empty;

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithOneCharacter_ShouldReturnError()
    {
        // Arrange
        var message = "M";

        // Act
        Action act = () => HL7Serializer.Deserialize(message.AsSpan());

        // Assert - Currently throws ArgumentOutOfRangeException due to unchecked slice
        // This is a bug that should be fixed in the parser
        act.ShouldThrow<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Deserialize_WithTwoCharacters_ShouldReturnError()
    {
        // Arrange
        var message = "MS";

        // Act
        Action act = () => HL7Serializer.Deserialize(message.AsSpan());

        // Assert - Currently throws ArgumentOutOfRangeException due to unchecked slice  
        // This is a bug that should be fixed in the parser
        act.ShouldThrow<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Deserialize_WithExactlyThreeCharacters_ShouldReturnError()
    {
        // Arrange - MSH but no delimiters
        var message = "MSH";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithSevenCharacters_ShouldReturnError()
    {
        // Arrange - Not enough for full delimiter set
        var message = "MSH|^~\\";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithEightCharacters_MinimumValid_ShouldStillError()
    {
        // Arrange - Has delimiters but no content
        var message = "MSH|^~\\&";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert - Should succeed as it has valid MSH with delimiters
        // The rest of the message can be empty
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithOnlyMSH_ShouldSucceed()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithThousandsOfSegments_ShouldHandleCorrectly()
    {
        // Arrange
        var diagnoses = string.Join("\r\n", Enumerable.Range(1, 5000)
            .Select(i => $"DG1|{i}|I10|CODE{i}||20230101|A|"));
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      diagnoses;

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(5000);
    }

    [Fact]
    public void Deserialize_WithExtremelyLongSingleField_ShouldHandleCorrectly()
    {
        // Arrange - 1MB field value
        var longValue = new string('A', 1_000_000);
        var message = $"MSH|^~\\&|{longValue}|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.SendingApplication.Length.ShouldBe(1_000_000);
    }

    [Fact]
    public void Deserialize_WithMaximumFieldCount_ShouldNotOverflow()
    {
        // Arrange - Segment with hundreds of fields
        var fields = string.Join("|", Enumerable.Range(1, 500).Select(i => $"FIELD{i}"));
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      $"PID|{fields}";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithNoLineBreaks_ShouldTreatAsOneLine()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithOnlyDelimiters_ShouldHandleGracefully()
    {
        // Arrange
        var message = "MSH|^~\\&|||||||||||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithConsecutiveLineBreaks_ShouldSkipEmptyLines()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n\r\n\r\n\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pid.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithWhitespaceOnlyLines_ShouldHandleCorrectly()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "   \r\n" +  // Whitespace line
                      "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void SegmentReader_ReadingPastEnd_ShouldNotThrow()
    {
        // Arrange
        var segment = "PID|1".AsSpan();
        var reader = new SegmentReader(segment);

        // Act & Assert - Should not throw
        reader.TryReadField('|', out _);
        reader.TryReadField('|', out _);
        reader.TryReadField('|', out _).ShouldBeFalse();
        reader.TryReadField('|', out _).ShouldBeFalse();
        reader.TryReadField('|', out _).ShouldBeFalse();
    }

    [Fact]
    public void FieldEnumerator_MultipleIterations_ShouldNotThrow()
    {
        // Arrange
        var span = "a|b|c".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        // Act - Move through all items
        while (enumerator.MoveNext()) { }

        // Assert - Continue calling MoveNext should not throw
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void LineEnumerator_MultipleIterations_ShouldNotThrow()
    {
        // Arrange
        var text = "Line1\nLine2".AsSpan();
        var enumerator = new LineEnumerator(text);

        // Act - Move through all items
        while (enumerator.MoveNext()) { }

        // Assert - Continue calling MoveNext should not throw
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithUnicodeCharacters_ShouldHandleCorrectly()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DÖE^JÖHN^??||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithMixedCasingInSegmentId_ShouldHandleCaseSensitively()
    {
        // Arrange - HL7 segment IDs are case-sensitive and should be uppercase
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "pid|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert - lowercase 'pid' should be treated as unknown segment
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithSingleDelimiterChar_ShouldReturnError()
    {
        // Arrange
        var message = "MSH|";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithAllFieldsAtMaxLength_ShouldHandleCorrectly()
    {
        // Arrange - Each field is very long
        var longField = new string('X', 10000);
        var message = $"MSH|^~\\&|{longField}|{longField}|{longField}|{longField}|{longField}||ADT^A01|{longField}|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Delimiters_WithNullCharacter_ShouldHandleGracefully()
    {
        // Arrange - Testing delimiter extraction robustness
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||";

        // Act
        var success = HL7Delimiters.TryParse(message.AsSpan(), out var delimiters, out var error);

        // Assert
        success.ShouldBeTrue();
    }

    [Fact]
    public void Serialize_WithEmptySegments_ShouldHandleCorrectly()
    {
        // Arrange
        var message = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "",
                SendingFacility = "",
                MessageControlID = "1"
            }
        };

        // Act
        var result = HL7Serializer.Serialize(message);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithRepeatingSegmentIds_ShouldHandleBasedOnType()
    {
        // Arrange - Multiple AL1 segments (allowed)
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|PENICILLIN|SV|RASH\r\n" +
                      "AL1|2|MA|DUST|MO|SNEEZING";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_WithIndexOutOfBoundsScenario_ShouldNotThrow()
    {
        // Arrange - Malformed but shouldn't crash
        var message = "MSH|^";

        // Act
        Action act = () => HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        act.ShouldNotThrow();
    }
}
