using KeryxPars.HL7.Validation;
using KeryxPars.HL7.Serialization;
using Shouldly;
using System.Diagnostics;

namespace KeryxPars.HL7.Tests.Validation;

/// <summary>
/// Tests for the high-performance SegmentAccessor
/// </summary>
public class SegmentAccessorTests
{
    private const string SampleMessage = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

    #region GetSegment Tests

    [Fact]
    public void GetSegment_ValidSegment_ShouldReturnSegment()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var segment = SegmentAccessor.GetSegment(message, "PID");

        // Assert
        segment.ShouldNotBeNull();
        segment.SegmentId.ShouldBe("PID");
    }

    [Fact]
    public void GetSegment_NonExistentSegment_ShouldReturnNull()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var segment = SegmentAccessor.GetSegment(message, "ORC");

        // Assert
        segment.ShouldBeNull();
    }

    [Fact]
    public void GetSegment_CaseInsensitive_ShouldWork()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var segment1 = SegmentAccessor.GetSegment(message, "PID");
        var segment2 = SegmentAccessor.GetSegment(message, "pid");
        var segment3 = SegmentAccessor.GetSegment(message, "Pid");

        // Assert
        segment1.ShouldNotBeNull();
        segment2.ShouldNotBeNull();
        segment3.ShouldNotBeNull();
    }

    [Fact]
    public void GetSegment_MSH_ShouldWork()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var segment = SegmentAccessor.GetSegment(message, "MSH");

        // Assert
        segment.ShouldNotBeNull();
        segment.SegmentId.ShouldBe("MSH");
    }

    #endregion

    #region HasSegment Tests

    [Fact]
    public void HasSegment_ExistingSegment_ShouldReturnTrue()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var hasSegment = SegmentAccessor.HasSegment(message, "PID");

        // Assert
        hasSegment.ShouldBeTrue();
    }

    [Fact]
    public void HasSegment_NonExistentSegment_ShouldReturnFalse()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var hasSegment = SegmentAccessor.HasSegment(message, "ORC");

        // Assert
        hasSegment.ShouldBeFalse();
    }

    #endregion

    #region GetFieldValue Tests

    [Fact]
    public void GetFieldValue_ValidField_ShouldReturnValue()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var segment = SegmentAccessor.GetSegment(message, "MSH")!;

        // Act
        var messageType = SegmentAccessor.GetFieldValue(segment, 9); // MSH.9 = Message Type
        var controlId = SegmentAccessor.GetFieldValue(segment, 10); // MSH.10 = Control ID

        // Assert
        messageType.ShouldNotBeNull();
        controlId.ShouldNotBeNull();
        controlId.ToString().ShouldContain("MSG001");
    }

    [Fact]
    public void GetFieldValue_InvalidFieldIndex_ShouldReturnNull()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var segment = SegmentAccessor.GetSegment(message, "MSH")!;

        // Act
        var field = SegmentAccessor.GetFieldValue(segment, 999); // Way out of bounds

        // Assert
        field.ShouldBeNull();
    }

    [Fact]
    public void GetFieldValue_ZeroIndex_ShouldReturnNull()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var segment = SegmentAccessor.GetSegment(message, "MSH")!;

        // Act
        var field = SegmentAccessor.GetFieldValue(segment, 0); // Invalid: fields are 1-based

        // Assert
        field.ShouldBeNull();
    }

    [Fact]
    public void GetFieldValue_PIDFields_ShouldWork()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var segment = SegmentAccessor.GetSegment(message, "PID")!;

        // Act
        var patientId = SegmentAccessor.GetFieldValue(segment, 3); // PID.3
        var name = SegmentAccessor.GetFieldValue(segment, 5); // PID.5
        var dob = SegmentAccessor.GetFieldValue(segment, 7); // PID.7
        var gender = SegmentAccessor.GetFieldValue(segment, 8); // PID.8

        // Assert
        patientId.ShouldNotBeNull();
        name.ShouldNotBeNull();
        dob.ShouldNotBeNull();
        gender.ShouldNotBeNull();
        gender.ToString().ShouldBe("M");
    }

    #endregion

    #region Performance Tests

    [Fact]
    public void GetSegment_Cached_ShouldBeFast()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        
        // Warm up cache
        SegmentAccessor.GetSegment(message, "PID");

        // Act - Time 1000 cached accesses
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            SegmentAccessor.GetSegment(message, "PID");
        }
        sw.Stop();

        // Assert - Should be blazing fast (< 1ms for 1000 calls)
        sw.ElapsedMilliseconds.ShouldBeLessThan(1);
    }

    [Fact]
    public void GetFieldValue_Cached_ShouldBeFast()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var segment = SegmentAccessor.GetSegment(message, "PID")!;
        
        // Warm up cache
        SegmentAccessor.GetFieldValue(segment, 3);

        // Act - Time 1000 cached accesses
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            SegmentAccessor.GetFieldValue(segment, 3);
        }
        sw.Stop();

        // Assert - Should be blazing fast (< 1ms for 1000 calls)
        sw.ElapsedMilliseconds.ShouldBeLessThan(1);
    }

    [Fact]
    public void GetSegment_DifferentMessageTypes_ShouldCache()
    {
        // Arrange
        var adtMessage = HL7Serializer.Deserialize(SampleMessage).Value!;
        var evnMessage = @"MSH|^~\&|LAB|LAB_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG002|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";
        var lab = HL7Serializer.Deserialize(evnMessage).Value!;

        // Act
        var adtPid = SegmentAccessor.GetSegment(adtMessage, "PID");
        var labPid = SegmentAccessor.GetSegment(lab, "PID");
        var labEvn = SegmentAccessor.GetSegment(lab, "EVN");

        // Assert
        adtPid.ShouldNotBeNull();
        labPid.ShouldNotBeNull();
        labEvn.ShouldNotBeNull();
        
        // Verify caching worked for both message types
        var stats = SegmentAccessor.GetCacheStats();
        stats.SegmentAccessors.ShouldBeGreaterThan(0);
    }

    #endregion

    #region Cache Management Tests

    [Fact]
    public void ClearCache_ShouldResetCacheCounters()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        SegmentAccessor.GetSegment(message, "PID");
        var segment = SegmentAccessor.GetSegment(message, "PID")!;
        SegmentAccessor.GetFieldValue(segment, 3);

        // Act
        SegmentAccessor.ClearCache();
        var stats = SegmentAccessor.GetCacheStats();

        // Assert
        stats.SegmentAccessors.ShouldBe(0);
        stats.FieldAccessors.ShouldBe(0);
    }

    [Fact]
    public void GetCacheStats_AfterMultipleAccesses_ShouldShowCachedItems()
    {
        // Arrange
        SegmentAccessor.ClearCache();
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        SegmentAccessor.GetSegment(message, "MSH");
        SegmentAccessor.GetSegment(message, "PID");
        SegmentAccessor.GetSegment(message, "EVN");
        
        var msh = SegmentAccessor.GetSegment(message, "MSH")!;
        var pid = SegmentAccessor.GetSegment(message, "PID")!;
        SegmentAccessor.GetFieldValue(msh, 9);
        SegmentAccessor.GetFieldValue(msh, 10);
        SegmentAccessor.GetFieldValue(pid, 3);
        SegmentAccessor.GetFieldValue(pid, 5);

        var stats = SegmentAccessor.GetCacheStats();

        // Assert
        stats.SegmentAccessors.ShouldBeGreaterThanOrEqualTo(3); // MSH, PID, EVN
        stats.FieldAccessors.ShouldBeGreaterThanOrEqualTo(4); // MSH.9, MSH.10, PID.3, PID.5
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void GetSegment_NullSegmentId_ShouldReturnNull()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var segment = SegmentAccessor.GetSegment(message, null!);

        // Assert
        segment.ShouldBeNull();
    }

    [Fact]
    public void GetSegment_EmptySegmentId_ShouldReturnNull()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;

        // Act
        var segment = SegmentAccessor.GetSegment(message, "");

        // Assert
        segment.ShouldBeNull();
    }

    [Fact]
    public void GetFieldValue_NegativeIndex_ShouldReturnNull()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var segment = SegmentAccessor.GetSegment(message, "MSH")!;

        // Act
        var field = SegmentAccessor.GetFieldValue(segment, -1);

        // Assert
        field.ShouldBeNull();
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public void SegmentAccessor_ConcurrentAccess_ShouldBeThreadSafe()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var exceptions = new System.Collections.Concurrent.ConcurrentBag<Exception>();

        // Act - Multiple threads accessing simultaneously
        Parallel.For(0, 100, i =>
        {
            try
            {
                var segment = SegmentAccessor.GetSegment(message, "PID");
                segment.ShouldNotBeNull();
                
                var field = SegmentAccessor.GetFieldValue(segment, 3);
                field.ShouldNotBeNull();
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        });

        // Assert - No exceptions should occur
        exceptions.ShouldBeEmpty();
    }

    #endregion
}
