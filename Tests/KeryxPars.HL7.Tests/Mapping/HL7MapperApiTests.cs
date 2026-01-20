using System;
using KeryxPars.HL7.Mapping;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for directly using the generated mappers.
/// This is the recommended approach for maximum performance.
/// </summary>
public class HL7MapperApiTests
{
    private const string EnhancedAdtMessage = 
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230615143022||ADT^A01|MSG12345|P|2.5||\r" +
        "EVN|A01|20230615143020||\r" +
        "PID|1||MRN123456||DOE^JOHN^ROBERT||19800115|M|||123 MAIN ST^APT 4B^CITYVILLE^CA^90210|||||||\r" +
        "PV1|1|I|ICU^201^A||||||||||||||||VISIT123||||||||||||||||||||||||||||||||||||||20230615140000|20230616100000||";

    [Fact]
    public void GeneratedMapper_DirectCall_ShouldWork()
    {
        // Act - Call the generated mapper directly (recommended)
        var admission = PatientAdmissionEnhancedMapper.MapFromSpan(EnhancedAdtMessage.AsSpan());

        // Assert
        admission.ShouldNotBeNull();
        admission.PatientId.ShouldBe("MRN123456");
        admission.LastName.ShouldBe("DOE");
        admission.FirstName.ShouldBe("JOHN");
    }

    [Fact]
    public void GeneratedMapper_DateTime_ShouldConvert()
    {
        // Act
        var admission = PatientAdmissionEnhancedMapper.MapFromSpan(EnhancedAdtMessage.AsSpan());

        // Assert
        admission.DateOfBirth.ShouldBe(new DateTime(1980, 1, 15));
        admission.MessageDateTime.ShouldBe(new DateTime(2023, 6, 15, 14, 30, 22));
        admission.EventDateTime.ShouldBe(new DateTime(2023, 6, 15, 14, 30, 20));
    }

    [Fact]
    public void GeneratedMapper_Enum_ShouldConvert()
    {
        // Act
        var admission = PatientAdmissionEnhancedMapper.MapFromSpan(EnhancedAdtMessage.AsSpan());

        // Assert
        admission.Gender.ShouldBe(Gender.M);
        admission.PatientClass.ShouldBe(Examples.PatientClass.I);
    }

    [Fact]
    public void GeneratedMapper_AllFields_ShouldMap()
    {
        // Act
        var admission = PatientAdmissionEnhancedMapper.MapFromSpan(EnhancedAdtMessage.AsSpan());

        // Assert - Basic info
        admission.PatientId.ShouldBe("MRN123456");
        admission.LastName.ShouldBe("DOE");
        admission.FirstName.ShouldBe("JOHN");

        // DateTime fields
        admission.DateOfBirth.ShouldBe(new DateTime(1980, 1, 15));
        admission.MessageDateTime.ShouldNotBeNull();
        admission.MessageDateTime.Value.ShouldBe(new DateTime(2023, 6, 15, 14, 30, 22));

        // Enum fields
        admission.Gender.ShouldBe(Gender.M);
        admission.PatientClass.ShouldBe(Examples.PatientClass.I);

        // Address
        admission.StreetAddress.ShouldBe("123 MAIN ST");
        admission.City.ShouldBe("CITYVILLE");
        admission.State.ShouldBe("CA");
        admission.ZipCode.ShouldBe("90210");

        // Visit info
        admission.AssignedLocation.ShouldBe("ICU");
        admission.VisitNumber.ShouldBe("VISIT123");
    }

    [Fact]
    public void GeneratedMapper_Performance_ShouldBeFast()
    {
        // Arrange
        var iterations = 1000;
        var span = EnhancedAdtMessage.AsSpan();

        // Act
        var sw = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            _ = PatientAdmissionEnhancedMapper.MapFromSpan(span);
        }
        sw.Stop();

        // Assert - should still be blazing fast even with type conversions
        sw.ElapsedMilliseconds.ShouldBeLessThan(100);

        Console.WriteLine($"Mapped {iterations} enhanced messages in {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Average: {sw.Elapsed.TotalMilliseconds / iterations:F4}ms per message");
        Console.WriteLine($"With DateTime & Enum conversions!");
    }
}
