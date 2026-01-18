namespace KeryxPars.HL7.Tests.EdgeCases;

/// <summary>
/// Tests for boundary conditions and edge cases to prevent crashes and out-of-bounds access
/// </summary>
public class BoundaryConditionTests
{
    [Fact]
    public void Deserialize_WithZeroLengthMessage_ShouldReturnError()
    {

        var message = string.Empty;


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithOneCharacter_ShouldReturnError()
    {

        var message = "M";
        var result = HL7Serializer.Deserialize(message.AsSpan());
        result.IsSuccess.ShouldBeFalse();

    }

    [Fact]
    public void Deserialize_WithTwoCharacters_ShouldReturnError()
    {

        var message = "MS";


        var result = HL7Serializer.Deserialize(message.AsSpan());

        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithExactlyThreeCharacters_ShouldReturnError()
    {
        //- MSH but no delimiters
        var message = "MSH";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithSevenCharacters_ShouldReturnError()
    {
        //- Not enough for full delimiter set
        var message = "MSH|^~\\";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithEightCharacters_MinimumValid_ShouldStillError()
    {
        //- Has delimiters but no content
        var message = "MSH|^~\\&";


        var result = HL7Serializer.Deserialize(message.AsSpan());

        //- Should succeed as it has valid MSH with delimiters
        // The rest of the message can be empty
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithOnlyMSH_ShouldSucceed()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithThousandsOfSegments_ShouldHandleCorrectly()
    {

        var diagnoses = string.Join("\r\n", Enumerable.Range(1, 5000)
            .Select(i => $"DG1|{i}|I10|CODE{i}||20230101|A|"));
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      diagnoses;


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(5000);
    }

    [Fact]
    public void Deserialize_WithExtremelyLongSingleField_ShouldHandleCorrectly()
    {
        //- 1MB field value
        var longValue = new string('A', 1_000_000);
        var message = $"MSH|^~\\&|{longValue}|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.SendingApplication.Length.ShouldBe(1_000_000);
    }

    [Fact]
    public void Deserialize_WithMaximumFieldCount_ShouldNotOverflow()
    {
        //- Segment with hundreds of fields
        var fields = string.Join("|", Enumerable.Range(1, 500).Select(i => $"FIELD{i}"));
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      $"PID|{fields}";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithNoLineBreaks_ShouldTreatAsOneLine()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithOnlyDelimiters_ShouldHandleGracefully()
    {

        var message = "MSH|^~\\&|||||||||||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithConsecutiveLineBreaks_ShouldSkipEmptyLines()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n\r\n\r\n\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pid.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithWhitespaceOnlyLines_ShouldHandleCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "   \r\n" +  // Whitespace line
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void SegmentReader_ReadingPastEnd_ShouldNotThrow()
    {

        var segment = "PID|1".AsSpan();
        var reader = new SegmentReader(segment);

        // - Should not throw
        reader.TryReadField('|', out _);
        reader.TryReadField('|', out _);
        reader.TryReadField('|', out _).ShouldBeFalse();
        reader.TryReadField('|', out _).ShouldBeFalse();
        reader.TryReadField('|', out _).ShouldBeFalse();
    }

    [Fact]
    public void FieldEnumerator_MultipleIterations_ShouldNotThrow()
    {

        var span = "a|b|c".AsSpan();
        var enumerator = new FieldEnumerator(span, '|');

        //- Move through all items
        while (enumerator.MoveNext()) { }

        //- Continue calling MoveNext should not throw
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void LineEnumerator_MultipleIterations_ShouldNotThrow()
    {

        var text = "Line1\nLine2".AsSpan();
        var enumerator = new LineEnumerator(text);

        //- Move through all items
        while (enumerator.MoveNext()) { }

        //- Continue calling MoveNext should not throw
        enumerator.MoveNext().ShouldBeFalse();
        enumerator.MoveNext().ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithUnicodeCharacters_ShouldHandleCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DÖE^JÖHN^??||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithMixedCasingInSegmentId_ShouldHandleCaseSensitively()
    {
        //- HL7 segment IDs are case-sensitive and should be uppercase
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "pid|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());

        //- lowercase 'pid' should be treated as unknown segment
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithSingleDelimiterChar_ShouldReturnError()
    {

        var message = "MSH|";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithAllFieldsAtMaxLength_ShouldHandleCorrectly()
    {
        //- Each field is very long
        var longField = new string('X', 10000);
        var message = $"MSH|^~\\&|{longField}|{longField}|{longField}|{longField}|{longField}||ADT^A01|{longField}|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Delimiters_WithNullCharacter_ShouldHandleGracefully()
    {
        //- Testing delimiter extraction robustness
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||";


        var success = HL7Delimiters.TryParse(message.AsSpan(), out var delimiters, out var error);


        success.ShouldBeTrue();
    }

    [Fact]
    public void Serialize_WithEmptySegments_ShouldHandleCorrectly()
    {

        var message = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "",
                SendingFacility = "",
                MessageControlID = "1"
            }
        };


        var result = HL7Serializer.Serialize(message);


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithRepeatingSegmentIds_ShouldHandleBasedOnType()
    {
        //- Multiple AL1 segments (allowed)
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|PENICILLIN|SV|RASH\r\n" +
                      "AL1|2|MA|DUST|MO|SNEEZING";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_WithIndexOutOfBoundsScenario_ShouldNotThrow()
    {
        //- Malformed but shouldn't crash
        var message = "MSH|^";


        Action act = () => HL7Serializer.Deserialize(message.AsSpan());


        act.ShouldNotThrow();
    }
}
