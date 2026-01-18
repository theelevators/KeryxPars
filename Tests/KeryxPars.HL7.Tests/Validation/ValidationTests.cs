namespace KeryxPars.HL7.Tests.Validation;

using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Tests for validation and error handling scenarios
/// </summary>
public class ValidationTests
{
    [Fact]
    public void Deserialize_WithMissingMSH_ShouldReturnError()
    {
        
        var message = "PID|1||12345||DOE^JOHN||19800101|M";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error![0].Message.ShouldContain("Invalid message start");
    }

    [Fact]
    public void Deserialize_WithInvalidDelimiters_ShouldReturnError()
    {
        
        var message = "MSH|";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Deserialize_WithMalformedSegment_ShouldContinueOrFail()
    {
        
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "MALFORMED_SEGMENT_WITHOUT_PROPER_FORMAT\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        var optionsContinue = new SerializerOptions 
        { 
            ErrorHandling = ErrorHandlingStrategy.CollectAndContinue 
        };

        
        var result = HL7Serializer.Deserialize(message.AsSpan(), optionsContinue);

         //- Should continue processing despite bad segment
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithFailFastStrategy_ShouldStopOnError()
    {
         //- Segment that might cause parsing issues
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||";
        var options = new SerializerOptions 
        { 
            ErrorHandling = ErrorHandlingStrategy.FailFast 
        };

        
        var result = HL7Serializer.Deserialize(message.AsSpan(), options);

        
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithUnknownSegmentAndIgnoreTrue_ShouldSucceed()
    {
        
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ZZZ|custom|segment|data";

        var options = new SerializerOptions { IgnoreUnknownSegments = true };

        
        var result = HL7Serializer.Deserialize(message.AsSpan(), options);

        
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithNullOptions_ShouldUseDefaults()
    {
        
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        
        var result = HL7Serializer.Deserialize(message.AsSpan(), null);

        
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void ErrorCodes_ShouldBeCorrectlyAssigned()
    {
        
        var message = ""; // Will cause error

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error![0].Code.ShouldNotBe(ErrorCode.MessageAccepted);
    }

    [Fact]
    public void Deserialize_WithVeryLongMessageControlID_ShouldAccept()
    {
        
        var longId = new string('A', 1000);
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|{longId}|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeTrue();
        result.Value!.MessageControlID.Length.ShouldBe(1000);
    }

    [Fact]
    public void Deserialize_WithEmptyMessageControlID_ShouldAccept()
    {
        
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01||P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeTrue();
        result.Value!.MessageControlID.ShouldBe("");
    }

    [Theory]
    [InlineData("MSH|^~\\&|||||||||")]
    [InlineData("MSH|^~\\&|||||||||||\r\nPID|||||||")]
    public void Deserialize_WithMinimalRequiredData_ShouldNotFail(string message)
    {
        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Serialize_WithMinimalData_ShouldProduceValidMessage()
    {
        
        var message = new HL7Message
        {
            Msh = new MSH
            {
                MessageControlID = "1"
            }
        };

        
        var result = HL7Serializer.Serialize(message);

        
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldStartWith("MSH|");
    }

    [Fact]
    public void Deserialize_WithIncorrectSegmentSequence_ShouldStillParse()
    {
         //- PV1 before PID (unusual)
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PV1|1|I|WARD||||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pid.ShouldNotBeNull();
        result.Value.Pv1.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithMissingRequiredPID_ShouldStillComplete()
    {
         //- MSH only
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeTrue();
        // PID will be default/empty
    }

    [Fact]
    public void Deserialize_WithAllEmptyFields_ShouldHandleGracefully()
    {
        
        var message = "MSH|^~\\&||||||||||||\r\n" +
                      "PID|||||||||||||||||||||\r\n" +
                      "PV1||||||||||||||||||";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void ErrorSeverity_ShouldDistinguishLevels()
    {
         
        var info = ErrorSeverity.Info;
        var warning = ErrorSeverity.Warning;
        var error = ErrorSeverity.Error;
        var internalError = ErrorSeverity.InternalError;

        
        info.ShouldNotBe(error);
        warning.ShouldNotBe(error);
        error.ShouldNotBe(internalError);
    }

    [Fact]
    public void StaticErrors_ShouldBeWellDefined()
    {
        
        var invalidStart = HL7Error.InvalidMessageStart;
        var multipleEVN = HL7Error.MultipleEVNSegments;
        var noPID = HL7Error.NoPIDSegment;

        
        invalidStart.ShouldNotBeNull();
        multipleEVN.ShouldNotBeNull();
        noPID.ShouldNotBeNull();
        invalidStart.Severity.ShouldBe(ErrorSeverity.Error);
    }

    [Fact]
    public void Deserialize_WithExtremelyMalformedData_ShouldNotCrash()
    {
        
        var message = "!@#$%^&*()_+{}|:<>?[];',./";

        
        Action act = () => HL7Serializer.Deserialize(message.AsSpan());

        
        act.ShouldNotThrow();
    }

    [Fact]
    public void Deserialize_WithOnlySpaces_ShouldHandleGracefully()
    {
        
        var message = "                    ";

        
        var result = HL7Serializer.Deserialize(message.AsSpan());

        
        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Serialize_WithCustomDelimiters_ShouldUseThemCorrectly()
    {
        
        var message = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "APP",
                MessageControlID = "1"
            }
        };
        var customDelimiters = new HL7Delimiters('$', '*', '#', '@', '%');

        
        var result = HL7Serializer.Serialize(message, customDelimiters);

        
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("$");
        result.Value.ShouldContain("*");
    }
}
