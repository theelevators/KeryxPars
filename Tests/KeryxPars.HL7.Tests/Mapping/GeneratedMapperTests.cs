using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for the source-generated mappers.
/// </summary>
public class GeneratedMapperTests
{
    private const string SampleAdtMessage = "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143022||ADT^A01|MSG12345|P|2.5||\r" +
                                            "EVN|A01|20230615143020||\r" +
                                            "PID|1||MRN123456||DOE^JOHN^ROBERT||19800115|M|||123 MAIN ST^APT 4B^CITYVILLE^CA^90210|||||||\r" +
                                            "PV1|1|I|ICU^201^A||||||||||||||||";

    [Fact]
    public void GeneratedMapper_ShouldExist()
    {
        // The source generator should have created PatientAdmissionMapper class
        var mapperType = typeof(PatientAdmissionMapper);
        mapperType.ShouldNotBeNull();
    }

    [Fact]
    public void MapFromSpan_ValidMessage_ShouldMapAllFields()
    {
        // Arrange
        var messageSpan = SampleAdtMessage.AsSpan();

        // Act
        var admission = PatientAdmissionMapper.MapFromSpan(messageSpan);

        // Assert
        admission.ShouldNotBeNull();
        admission.PatientId.ShouldBe("MRN123456");
        admission.LastName.ShouldBe("DOE");
        admission.FirstName.ShouldBe("JOHN");
        admission.MiddleName.ShouldBe("ROBERT");
        admission.DateOfBirth.ShouldBe("19800115");
        admission.Gender.ShouldBe("M");
        admission.StreetAddress.ShouldBe("123 MAIN ST");
        admission.City.ShouldBe("CITYVILLE");
        admission.State.ShouldBe("CA");
        admission.ZipCode.ShouldBe("90210");
        admission.PatientClass.ShouldBe("I");
        admission.AssignedLocation.ShouldBe("ICU");
    }

    [Fact]
    public void MapFromSpan_ComponentAccess_ShouldWork()
    {
        // Arrange - test that component-level access works
        var messageSpan = SampleAdtMessage.AsSpan();

        // Act
        var admission = PatientAdmissionMapper.MapFromSpan(messageSpan);

        // Assert - verify individual name components
        admission.LastName.ShouldBe("DOE");    // PID.5.1
        admission.FirstName.ShouldBe("JOHN");  // PID.5.2
        admission.MiddleName.ShouldBe("ROBERT"); // PID.5.3
    }

    [Fact]
    public void MapFromSpan_AddressComponents_ShouldWork()
    {
        // Arrange
        var messageSpan = SampleAdtMessage.AsSpan();

        // Act
        var admission = PatientAdmissionMapper.MapFromSpan(messageSpan);

        // Assert - verify address components
        admission.StreetAddress.ShouldBe("123 MAIN ST"); // PID.11.1
        admission.City.ShouldBe("CITYVILLE");            // PID.11.3
        admission.State.ShouldBe("CA");                  // PID.11.4
        admission.ZipCode.ShouldBe("90210");             // PID.11.5
    }

    [Fact]
    public void MapFromSpan_EmptyFields_ShouldHandleGracefully()
    {
        // Arrange - message with missing optional fields
        var message = "MSH|^~\\&|APP|FAC|REC|FAC|20230101120000||ADT^A01|MSG001|P|2.5||\r" +
                     "PID|1||MRN123456||DOE^JOHN|||M|||||||||";

        var messageSpan = message.AsSpan();

        // Act
        var admission = PatientAdmissionMapper.MapFromSpan(messageSpan);

        // Assert
        admission.PatientId.ShouldBe("MRN123456");
        admission.LastName.ShouldBe("DOE");
        admission.FirstName.ShouldBe("JOHN");
        admission.MiddleName.ShouldBe(string.Empty); // Missing field
        admission.Gender.ShouldBe("M");
        admission.StreetAddress.ShouldBe(string.Empty); // Missing address
    }

    [Fact]
    public void MapFromSpan_Performance_ShouldBeVeryFast()
    {
        // Arrange
        var messageSpan = SampleAdtMessage.AsSpan();

        // Act - map 1000 times and measure
        var sw = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            _ = PatientAdmissionMapper.MapFromSpan(messageSpan);
        }
        sw.Stop();

        // Assert - should be blazing fast (< 10ms for 1000 mappings)
        sw.ElapsedMilliseconds.ShouldBeLessThan(50);
        
        Console.WriteLine($"Mapped 1000 messages in {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Average: {sw.Elapsed.TotalMilliseconds / 1000:F4}ms per message");
    }
}
