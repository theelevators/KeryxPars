using KeryxPars.HL7.Validation;
using KeryxPars.HL7.Serialization;
using Shouldly;

namespace KeryxPars.HL7.Tests.Validation;

/// <summary>
/// Tests for the static Validator helper methods
/// </summary>
public class ValidatorTests
{
    private const string SampleMessage = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

    [Fact]
    public void Required_AllSegmentsPresent_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var result = Validator.Required(message, "MSH", "PID", "EVN");

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Required_SegmentMissing_ShouldFail()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var result = Validator.Required(message, "MSH", "PID", "ORC");

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].Field.ShouldBe("ORC");
    }

    [Fact]
    public void Required_NoSegmentsSpecified_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var result = Validator.Required(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Required_MultipleSegmentsMissing_ShouldReportAll()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var result = Validator.Required(message, "MSH", "ORC", "RXA", "RXR");

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(3);
        result.Errors.ShouldContain(e => e.Field == "ORC");
        result.Errors.ShouldContain(e => e.Field == "RXA");
        result.Errors.ShouldContain(e => e.Field == "RXR");
    }

    [Fact]
    public void FromJson_ValidJson_ShouldDeserializeAndValidate()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var json = @"{
  ""RequiredSegments"": [""MSH"", ""PID""],
  ""Fields"": {
    ""MSH.9"": { ""Required"": true },
    ""PID.3"": { ""Required"": true }
  }
}";

        // Act
        var result = Validator.FromJson(message, json);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void FromJson_InvalidJson_ShouldThrowException()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var invalidJson = "{ invalid json }";

        // Act & Assert
        Should.Throw<Exception>(() => Validator.FromJson(message, invalidJson));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void FromJson_NullOrEmptyJson_ShouldThrowException(string? json)
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act & Assert
        Should.Throw<Exception>(() => Validator.FromJson(message, json!));
    }
}
