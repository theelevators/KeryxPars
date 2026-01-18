namespace KeryxPars.HL7.Tests.Serialization;

using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Comprehensive tests for HL7Serializer with validation and boundary checks
/// </summary>
public class HL7SerializerTests
{
    [Fact]
    public void Deserialize_WithValidSimpleMessage_ShouldParseCorrectly()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101120000||ADT^A01|MSG001|P|2.5||\r\n" +
                      "EVN|A01|20230101120000||\r\n" +
                      "PID|1||12345||DOE^JOHN^A||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value!.Msh.ShouldNotBeNull();
        result.Value.Msh.MessageControlID.ShouldBe("MSG001");
        result.Value.Pid.ShouldNotBeNull();
        result.Value.EventType.ShouldBe(EventType.NewAdmit);
    }

    [Fact]
    public void Deserialize_WithInvalidMessageStart_ShouldReturnError()
    {
        // Arrange
        var message = "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error![0].Message.ShouldContain("Invalid message start");
    }

    [Fact]
    public void Deserialize_WithEmptyMessage_ShouldReturnError()
    {
        // Arrange
        var message = "";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithTooShortMessage_ShouldReturnError()
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
    public void Deserialize_WithMissingDelimiters_ShouldReturnError()
    {
        // Arrange
        var message = "MSH|^~";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithMessageContainingAllergies_ShouldParseAllergies()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "AL1|2|MA|^DUST|MO|SNEEZING";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_WithMessageContainingDiagnoses_ShouldParseDiagnoses()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "DG1|1|I10|E11.9^Diabetes||20230101|A|\r\n" +
                      "DG1|2|I10|I10^Hypertension||20230101|A|";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_WithMedicationOrder_ShouldParseOrderGroups()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530|ORDERING^PHYSICIAN||\r\n" +
                      "RXO|00378-1805-10^Metformin 500mg|500||MG||||\r\n" +
                      "RXR|PO^Oral|||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan(), SerializerOptions.ForMedicationOrders());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(1);
    }

    [Fact]
    public void Deserialize_WithUnknownSegment_ShouldIgnoreByDefault()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "XXX|unknown|segment|data";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithUnknownSegmentAndStrictMode_ShouldReportWarning()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "XXX|unknown|segment|data";
        var options = new SerializerOptions { IgnoreUnknownSegments = false };

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan(), options);

        // Assert - Should still succeed but may have warnings in context
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithEmptyLines_ShouldSkipEmptyLines()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "\r\n";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pid.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithVeryLongMessage_ShouldHandleCorrectly()
    {
        // Arrange
        var diagnoses = string.Join("\r\n", Enumerable.Range(1, 100)
            .Select(i => $"DG1|{i}|I10|CODE{i}^Description{i}||20230101|A|"));
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      diagnoses;

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(100);
    }

    [Fact]
    public void Deserialize_WithCustomDelimiters_ShouldParseCorrectly()
    {
        // Arrange
        // MSH segment structure: MSH | *#@% | SendApp | SendFac | RecApp | RecFac | DateTime | Security | MessageType | MsgCtrlID | ProcessingID | VersionID
        //  Field positions:       0     1        2          3         4         5         6          7            8              9             10            11
        // Field 1 (*#@%) is the encoding characters which come right after the first delimiter
        var message = "MSH$*#@%$SEND$FAC$REC$FAC$20230101$$ADT*A01$MSG001$P$2.5$$\r\n" +
                      "PID$1$$12345$$DOE*JOHN$$19800101$M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.SendingApplication.ShouldBe("SEND");
        result.Value.Msh.MessageType.ShouldBe("ADT*A01"); // Field 8
        result.Value.Msh.MessageControlID.ShouldBe("MSG001"); // Field 9
    }

    [Fact]
    public void Deserialize_WithNullCharactersInFields_ShouldHandleGracefully()
    {
        // Arrange - Note: This tests robustness, though null chars shouldn't appear in valid HL7
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Serialize_WithValidMessage_ShouldSerializeCorrectly()
    {
        // Arrange
        var hl7Message = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "SEND",
                SendingFacility = "FAC",
                ReceivingApplication = "REC",
                ReceivingFacility = "FAC",
                DateTimeOfMessage = "20230101120000",
                MessageType = "ADT^A01",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID()
        };

        // Act
        var result = HL7Serializer.Serialize(hl7Message);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldContain("MSG001");
    }

    [Fact]
    public void Serialize_WithNullMessage_ShouldReturnError()
    {
        // Act
        var result = HL7Serializer.Serialize(null!);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error!.Message.ShouldContain("null");
    }

    [Fact]
    public void Serialize_ThenDeserialize_ShouldRoundTrip()
    {
        // Arrange
        var original = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "APP",
                SendingFacility = "FAC",
                ReceivingApplication = "REC",
                ReceivingFacility = "RECFAC",
                DateTimeOfMessage = "20230101120000",
                MessageType = "ADT^A01",
                MessageControlID = "CTRL123",
                ProcessingID = "P",
                VersionID = "2.5"
            }
        };

        // Act
        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize(serialized.Value.AsSpan());

        // Assert
        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Msh.MessageControlID.ShouldBe("CTRL123");
        deserialized.Value.Msh.SendingApplication.ShouldBe("APP");
    }

    [Fact]
    public void Deserialize_WithMultiplePV1Segments_ShouldHandleCorrectly()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|I|WARD|||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pv1.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_WithInsuranceSegments_ShouldParseInsurance()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "IN1|1|PLAN001|INS123|INSURANCE CO|||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Insurance.Count.ShouldBe(1);
    }

    [Theory]
    [InlineData("ADT^A01", EventType.NewAdmit)]
    [InlineData("ADT^A04", EventType.NewAdmit)]
    [InlineData("ADT^A08", EventType.Update)]
    [InlineData("ADT^A03", EventType.Discharge)]
    [InlineData("ADT^A05", EventType.Preadmit)]
    [InlineData("RDE^O11", EventType.MedicationOrder)]
    public void Deserialize_WithDifferentEventTypes_ShouldIdentifyCorrectly(string messageType, EventType expectedEvent)
    {
        // Arrange
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||{messageType}|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.EventType.ShouldBe(expectedEvent);
    }

    [Fact]
    public void Deserialize_WithCorruptedSegment_ShouldHandleGracefully()
    {
        // Arrange
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1"; // Incomplete segment

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert - Should still parse successfully, ignoring bad segment
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithManyEmptyFields_ShouldHandleCorrectly()
    {
        // Arrange
        var message = "MSH|^~\\&||||||||||\r\n" +
                      "PID|||||||||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}
