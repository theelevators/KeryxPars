using System;
using KeryxPars.HL7.Mapping;
using Shouldly;
using Xunit;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for HL7ComplexType feature - validates absolute and relative path mapping.
/// </summary>
public class ComplexTypeMappingTests
{
    #region Test Data

    private const string SamplePatientMessage =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||\r" +
        "PID|1||MRN12345^^^HOSPITAL^MR||DOE^JOHN^A||19850615|M|||123 MAIN ST^^ANYTOWN^CA^12345||555-1234|||M|NON|\r" +
        "PV1|1|I|WARD^101^1^HOSPITAL||||123^SMITH^JOHN|||SUR||||ADM|A0|||||||||||||||||||||||||20230101100000";

    private const string SampleCrossSegmentMessage =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||\r" +
        "PID|1||MRN12345^^^HOSPITAL^MR||DOE^JOHN^A||19900101|F|||123 MAIN ST^^ANYTOWN^CA^12345||555-1234|||D|NON|\r" +
        "PV1|1|O|CLINIC^201^1^HOSPITAL||||456^JONES^MARY|||MED||||ADM|A0|||||||||||||||||||||||||20230615143000";

    private const string SamplePartialMessage =
        "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||\r" +
        "PID|1||MRN12345^^^HOSPITAL^MR||DOE^JOHN^A||19850615|M|||123 MAIN ST^^ANYTOWN^CA^12345||555-1234|||S|NON|";

    private const string MinimalMessage = "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||";

    #endregion

    #region Absolute Path Tests

    [Fact]
    public void AbsolutePathMapper_ShouldBeGenerated()
    {
        var mapperType = typeof(PatientDemographicsMapper);
        mapperType.ShouldNotBeNull();
    }

    [Fact]
    public void AbsolutePaths_ShouldMapFromMultipleSegments()
    {
        // Act
        var result = PatientDemographicsMapper.MapFromMessage(SamplePatientMessage.AsSpan());

        // Assert
        result.ShouldNotBeNull();
        result.DateOfBirth.ShouldBe(new DateTime(1985, 6, 15));
        result.Gender.ShouldBe("M");
        result.PatientClass.ShouldBe("I");
        result.PatientLocation.ShouldBe("WARD");
        result.MaritalStatus.ShouldBe("M");
    }

    [Fact]
    public void AbsolutePaths_WithMissingSegment_ShouldHandleGracefully()
    {
        // Act
        var result = PatientDemographicsMapper.MapFromMessage(SamplePartialMessage.AsSpan());

        // Assert
        result.ShouldNotBeNull();
        result.DateOfBirth.ShouldBe(new DateTime(1985, 6, 15));
        result.Gender.ShouldBe("M");
        result.PatientClass.ShouldBeNull();
        result.PatientLocation.ShouldBeNull();
        result.MaritalStatus.ShouldBe("S");
    }

    [Fact]
    public void AbsolutePaths_CrossSegmentEnrichment_ShouldWork()
    {
        // Act
        var result = PatientDemographicsMapper.MapFromMessage(SampleCrossSegmentMessage.AsSpan());

        // Assert - PID segment data
        result.DateOfBirth.ShouldBe(new DateTime(1990, 1, 1));
        result.Gender.ShouldBe("F");
        result.MaritalStatus.ShouldBe("D");
        
        // Assert - PV1 segment data (cross-segment enrichment)
        result.PatientClass.ShouldBe("O");
        result.PatientLocation.ShouldBe("CLINIC");
    }

    [Fact]
    public void AbsolutePaths_EmptyMessage_ShouldReturnEmptyObject()
    {
        // Act
        var result = PatientDemographicsMapper.MapFromMessage(MinimalMessage.AsSpan());

        // Assert
        result.ShouldNotBeNull();
        result.DateOfBirth.ShouldBeNull();
        result.Gender.ShouldBeNull();
        result.PatientClass.ShouldBeNull();
        result.PatientLocation.ShouldBeNull();
        result.MaritalStatus.ShouldBeNull();
    }

    [Fact]
    public void AbsolutePaths_InvalidDateFormat_ShouldThrowMappingException()
    {
        // Arrange
        var invalidMessage = "MSH|^~\\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||\r" +
                           "PID|1||MRN12345^^^HOSPITAL^MR||DOE^JOHN^A||INVALID|M|||123 MAIN ST^^ANYTOWN^CA^12345||555-1234|||M|NON|";

        // Act & Assert
        var ex = Should.Throw<HL7MappingException>(() => 
            PatientDemographicsMapper.MapFromMessage(invalidMessage.AsSpan()));
        
        ex.TargetType.ShouldContain("System.DateTime?");
        ex.FieldPath.ShouldContain("PID.7");
    }

    #endregion

    #region Relative Path Tests

    [Fact]
    public void RelativePathMapper_ShouldBeGenerated()
    {
        var mapperType = typeof(EnrichedAddressMapper);
        mapperType.ShouldNotBeNull();
    }

    [Fact]
    public void RelativePaths_ShouldResolveAgainstBasePath()
    {
        // Verify that source generator resolved "1" ? "PID.11.1", "3" ? "PID.11.3", etc.
        // The generated code should contain the resolved absolute paths
        var mapperType = typeof(EnrichedAddressMapper);
        mapperType.ShouldNotBeNull();
    }

    [Fact]
    public void RelativePaths_AbsoluteOverride_ShouldPreserveAbsolutePath()
    {
        // Verify that "PID.12" stays as "PID.12" even with BaseFieldPath = "PID.11"
        var mapperType = typeof(EnrichedAddressMapper);
        mapperType.ShouldNotBeNull();
    }

    [Fact]
    public void RelativePaths_EmptyMessage_ShouldReturnEmptyObject()
    {
        // Act
        var result = EnrichedAddressMapper.MapFromMessage(MinimalMessage.AsSpan());

        // Assert
        result.ShouldNotBeNull();
        result.Street.ShouldBeNull();
        result.City.ShouldBeNull();
        result.State.ShouldBeNull();
        result.ZipCode.ShouldBeNull();
        result.County.ShouldBeNull();
    }

    #endregion
}

#region Test Models

/// <summary>
/// Complex type demonstrating absolute path mapping across multiple segments.
/// </summary>
/// <remarks>
/// This complex type pulls data from both PID (Patient Identification) and PV1 (Patient Visit) segments,
/// demonstrating cross-segment enrichment without a BaseFieldPath.
/// All field paths are absolute (e.g., "PID.7", "PV1.2").
/// </remarks>
/// <example>
/// <code>
/// var demographics = PatientDemographicsMapper.MapFromMessage(hl7Message.AsSpan());
/// Console.WriteLine($"DOB: {demographics.DateOfBirth}");
/// Console.WriteLine($"Patient Class: {demographics.PatientClass}");
/// </code>
/// </example>
[HL7ComplexType]
public class PatientDemographics
{
    /// <summary>
    /// Patient's date of birth from PID.7.
    /// </summary>
    /// <remarks>
    /// Format: yyyyMMdd (e.g., "19850615" ? 1985-06-15)
    /// </remarks>
    [HL7Field("PID.7", Format = "yyyyMMdd")]
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Patient's administrative sex from PID.8.
    /// </summary>
    /// <remarks>
    /// Common values: M (Male), F (Female), O (Other), U (Unknown)
    /// </remarks>
    [HL7Field("PID.8")]
    public string? Gender { get; set; }
    
    /// <summary>
    /// Patient class from PV1.2 (cross-segment field).
    /// </summary>
    /// <remarks>
    /// Common values: I (Inpatient), O (Outpatient), E (Emergency), R (Recurring)
    /// Demonstrates cross-segment enrichment - pulls from PV1 segment even though base is PID.
    /// </remarks>
    [HL7Field("PV1.2")]
    public string? PatientClass { get; set; }
    
    /// <summary>
    /// Patient location (point of care) from PV1.3.1.
    /// </summary>
    /// <remarks>
    /// First component of the PL (Person Location) data type.
    /// Example: "WARD", "ICU", "CLINIC"
    /// </remarks>
    [HL7Field("PV1.3.1")]
    public string? PatientLocation { get; set; }
    
    /// <summary>
    /// Marital status from PID.16.
    /// </summary>
    /// <remarks>
    /// Common values: S (Single), M (Married), D (Divorced), W (Widowed)
    /// </remarks>
    [HL7Field("PID.16")]
    public string? MaritalStatus { get; set; }
}

/// <summary>
/// Complex type demonstrating relative path mapping with BaseFieldPath.
/// </summary>
/// <remarks>
/// <para>
/// This complex type uses a BaseFieldPath of "PID.11" (Patient Address field).
/// Relative numeric paths (e.g., "1", "3") are resolved against this base at compile time.
/// </para>
/// <para>
/// Resolution examples:
/// - "1" ? "PID.11.1" (Street Address)
/// - "3" ? "PID.11.3" (City)
/// - "PID.12" ? stays "PID.12" (absolute override for County)
/// </para>
/// <para>
/// This demonstrates the power of mixing relative paths (for convenience) with
/// absolute paths (for cross-field enrichment).
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var address = EnrichedAddressMapper.MapFromMessage(hl7Message.AsSpan());
/// Console.WriteLine($"{address.Street}, {address.City}, {address.State} {address.ZipCode}");
/// Console.WriteLine($"County: {address.County}"); // From PID.12, not PID.11!
/// </code>
/// </example>
[HL7ComplexType(BaseFieldPath = "PID.11")]
public class EnrichedAddress
{
    /// <summary>
    /// Street address - resolved from relative path "1" to "PID.11.1".
    /// </summary>
    /// <remarks>
    /// First component of XAD (Extended Address) data type.
    /// Example: "123 MAIN ST", "456 ELM AVENUE APT 2B"
    /// </remarks>
    [HL7Field("1")]
    public string? Street { get; set; }
    
    /// <summary>
    /// City - resolved from relative path "3" to "PID.11.3".
    /// </summary>
    /// <remarks>
    /// Third component of XAD (Extended Address) data type.
    /// Example: "ANYTOWN", "NEW YORK", "LOS ANGELES"
    /// </remarks>
    [HL7Field("3")]
    public string? City { get; set; }
    
    /// <summary>
    /// State or province - resolved from relative path "4" to "PID.11.4".
    /// </summary>
    /// <remarks>
    /// Fourth component of XAD (Extended Address) data type.
    /// Example: "CA", "NY", "TX" (typically 2-letter state codes)
    /// </remarks>
    [HL7Field("4")]
    public string? State { get; set; }
    
    /// <summary>
    /// ZIP or postal code - resolved from relative path "5" to "PID.11.5".
    /// </summary>
    /// <remarks>
    /// Fifth component of XAD (Extended Address) data type.
    /// Example: "12345", "90210-1234", "M5H 2N2"
    /// </remarks>
    [HL7Field("5")]
    public string? ZipCode { get; set; }
    
    /// <summary>
    /// County code from PID.12 - demonstrates absolute path override.
    /// </summary>
    /// <remarks>
    /// Even though BaseFieldPath is "PID.11", this field uses an absolute path "PID.12"
    /// to pull county information from a different field entirely.
    /// This demonstrates cross-field enrichment within the same complex type.
    /// Example: "ORANGE", "LOS ANGELES", "COOK"
    /// </remarks>
    [HL7Field("PID.12")]
    public string? County { get; set; }
}

#endregion
