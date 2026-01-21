using System;
using System.Linq;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

public class CollectionMappingTests
{
    private const string SampleLabResult = 
        "MSH|^~\\&|LAB|LAB_FAC|EMR|EMR_FAC|20230615100000||ORU^R01|MSG001|P|2.5||\r" +
        "PID|1||PAT123456||DOE^JOHN||19800115|M|||||||||||\r" +
        "OBR|1|ORD123|SPEC456|CBC^Complete Blood Count||20230615090000|||||||||||||||||||F||\r" +
        "OBX|1|NM|WBC^White Blood Cell Count|1|7.5|10*3/uL|4.5-11.0|N|||F|||\r" +
        "OBX|2|NM|RBC^Red Blood Cell Count|1|4.8|10*6/uL|4.2-5.4|N|||F|||\r" +
        "OBX|3|NM|HGB^Hemoglobin|1|14.5|g/dL|13.5-17.5|N|||F|||\r" +
        "OBX|4|NM|HCT^Hematocrit|1|42.1|%|38.8-50.0|N|||F|||";

    [Fact]
    public void ObservationMapper_ShouldBeGenerated()
    {
        // The generator should create ObservationMapper
        var mapperType = typeof(ObservationMapper);
        mapperType.ShouldNotBeNull();
    }

    [Fact]
    public void LabResultMapper_ShouldMapCollections()
    {
        // Act
        var result = LabResultMapper.MapFromSpan(SampleLabResult.AsSpan());

        // Assert
        result.ShouldNotBeNull();
        result.PatientId.ShouldBe("PAT123456");
        result.LastName.ShouldBe("DOE");
        result.FirstName.ShouldBe("JOHN");
        result.PlacerOrderNumber.ShouldBe("ORD123");
        result.TestName.ShouldBe("CBC");
        
        // Check observations collection
        result.Observations.ShouldNotBeNull();
        result.Observations.Count.ShouldBe(4);
    }

    [Fact]
    public void LabResultMapper_ShouldMapObservationDetails()
    {
        // Act
        var result = LabResultMapper.MapFromSpan(SampleLabResult.AsSpan());

        // Assert - First OBX (WBC)
        var wbc = result.Observations[0];
        wbc.SetId.ShouldBe("1");
        wbc.ValueType.ShouldBe("NM");
        wbc.ObservationIdentifier.ShouldBe("WBC");
        wbc.ObservationText.ShouldBe("White Blood Cell Count");
        wbc.ObservationValue.ShouldBe("7.5");
        wbc.Units.ShouldBe("10*3/uL");
        wbc.ReferenceRange.ShouldBe("4.5-11.0");
        wbc.AbnormalFlags.ShouldBe("N");
        wbc.ObservationResultStatus.ShouldBe("F");

        // Assert - Second OBX (RBC)
        var rbc = result.Observations[1];
        rbc.ObservationIdentifier.ShouldBe("RBC");
        rbc.ObservationText.ShouldBe("Red Blood Cell Count");
        rbc.ObservationValue.ShouldBe("4.8");
    }

    [Fact]
    public void CollectionMapping_Performance_ShouldBeFast()
    {
        // Arrange
        var iterations = 1000;
        var span = SampleLabResult.AsSpan();

        // Act
        var sw = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            _ = LabResultMapper.MapFromSpan(span);
        }
        sw.Stop();

        // Assert - should still be fast even with collections
        sw.ElapsedMilliseconds.ShouldBeLessThan(200);

        Console.WriteLine($"Mapped {iterations} lab results (4 OBX each) in {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Average: {sw.Elapsed.TotalMilliseconds / iterations:F4}ms per message");
        Console.WriteLine($"With collection mapping!");
    }
}

