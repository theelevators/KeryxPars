using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class TSTests
{
    [Fact]
    public void Parse_SimpleTimestamp_ShouldParseCorrectly()
    {
        // Arrange
        var input = "20230101120000";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var ts = new TS();
        ts.Parse(input.AsSpan(), delimiters);
        
        // Assert
        ts.Time.Value.ShouldBe("20230101120000");
        ts.DegreeOfPrecision.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Parse_TimestampWithPrecision_ShouldParseAllComponents()
    {
        // Arrange
        var input = "20230101120000^S";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var ts = new TS();
        ts.Parse(input.AsSpan(), delimiters);
        
        // Assert
        ts.Time.Value.ShouldBe("20230101120000");
        ts.DegreeOfPrecision.Value.ShouldBe("S");
    }

    [Fact]
    public void ToHL7String_SimpleTimestamp_ShouldFormatCorrectly()
    {
        // Arrange
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("20230101120000");
    }

    [Fact]
    public void ToHL7String_TimestampWithPrecision_ShouldFormatCorrectly()
    {
        // Arrange
        var ts = new TS();
        ts.Parse("20230101120000^S".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("20230101120000^S");
    }

    [Fact]
    public void RoundTrip_SimpleTimestamp_ShouldMaintainData()
    {
        // Arrange
        var original = "20230101120000";
        
        // Act
        var ts = new TS();
        ts.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void RoundTrip_TimestampWithPrecision_ShouldMaintainData()
    {
        // Arrange
        var original = "20230101120000^S";
        
        // Act
        var ts = new TS();
        ts.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void ToDateTime_ValidTimestamp_ShouldConvert()
    {
        // Arrange
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var dateTime = ts.ToDateTime();
        
        // Assert
        dateTime.ShouldNotBeNull();
        dateTime.Value.Year.ShouldBe(2023);
        dateTime.Value.Month.ShouldBe(1);
        dateTime.Value.Day.ShouldBe(1);
        dateTime.Value.Hour.ShouldBe(12);
        dateTime.Value.Minute.ShouldBe(0);
        dateTime.Value.Second.ShouldBe(0);
    }

    [Fact]
    public void Constructor_FromDateTime_ShouldInitialize()
    {
        // Arrange
        var dateTime = new DateTime(2023, 1, 1, 12, 0, 0);
        
        // Act
        var ts = new TS(dateTime);
        
        // Assert
        ts.Time.Value.ShouldBe("20230101120000");
    }

    [Fact]
    public void ImplicitConversion_FromDateTime_ShouldConvert()
    {
        // Arrange
        var dateTime = new DateTime(2023, 1, 1, 12, 0, 0);
        
        // Act
        TS ts = dateTime;
        
        // Assert
        ts.Time.Value.ShouldBe("20230101120000");
    }

    [Fact]
    public void ExplicitConversion_ToDateTime_ShouldConvert()
    {
        // Arrange
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var dateTime = (DateTime?)ts;
        
        // Assert
        dateTime.ShouldNotBeNull();
        dateTime.Value.Year.ShouldBe(2023);
    }

    [Fact]
    public void Validate_ValidTimestamp_ShouldReturnTrue()
    {
        // Arrange
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var isValid = ts.Validate(out var errors);
        
        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyTimestamp_ShouldReturnTrue()
    {
        // Arrange
        var ts = new TS();
        
        // Act & Assert
        ts.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        TS ts = "20230101120000";
        
        // Assert
        ts.Time.Value.ShouldBe("20230101120000");
    }

    [Fact]
    public void ToString_ShouldReturnTimeValue()
    {
        // Arrange
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = ts.ToString();
        
        // Assert
        result.ShouldBe("20230101120000");
    }
}
