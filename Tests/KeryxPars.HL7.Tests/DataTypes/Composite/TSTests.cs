using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class TSTests
{
    [Fact]
    public void Parse_SimpleTimestamp_ShouldParseCorrectly()
    {
        
        var input = "20230101120000";
        var delimiters = HL7Delimiters.Default;
        
        
        var ts = new TS();
        ts.Parse(input.AsSpan(), delimiters);
        
        
        ts.Time.Value.ShouldBe("20230101120000");
        ts.DegreeOfPrecision.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Parse_TimestampWithPrecision_ShouldParseAllComponents()
    {
        
        var input = "20230101120000^S";
        var delimiters = HL7Delimiters.Default;
        
        
        var ts = new TS();
        ts.Parse(input.AsSpan(), delimiters);
        
        
        ts.Time.Value.ShouldBe("20230101120000");
        ts.DegreeOfPrecision.Value.ShouldBe("S");
    }

    [Fact]
    public void ToHL7String_SimpleTimestamp_ShouldFormatCorrectly()
    {
        
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe("20230101120000");
    }

    [Fact]
    public void ToHL7String_TimestampWithPrecision_ShouldFormatCorrectly()
    {
        
        var ts = new TS();
        ts.Parse("20230101120000^S".AsSpan(), HL7Delimiters.Default);
        
        
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe("20230101120000^S");
    }

    [Fact]
    public void RoundTrip_SimpleTimestamp_ShouldMaintainData()
    {
        
        var original = "20230101120000";
        
        
        var ts = new TS();
        ts.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe(original);
    }

    [Fact]
    public void RoundTrip_TimestampWithPrecision_ShouldMaintainData()
    {
        
        var original = "20230101120000^S";
        
        
        var ts = new TS();
        ts.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = ts.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe(original);
    }

    [Fact]
    public void ToDateTime_ValidTimestamp_ShouldConvert()
    {
        
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        
        var dateTime = ts.ToDateTime();
        
        
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
        
        var dateTime = new DateTime(2023, 1, 1, 12, 0, 0);
        
        
        var ts = new TS(dateTime);
        
        
        ts.Time.Value.ShouldBe("20230101120000");
    }

    [Fact]
    public void ImplicitConversion_FromDateTime_ShouldConvert()
    {
        
        var dateTime = new DateTime(2023, 1, 1, 12, 0, 0);
        
        
        TS ts = dateTime;
        
        
        ts.Time.Value.ShouldBe("20230101120000");
    }

    [Fact]
    public void ExplicitConversion_ToDateTime_ShouldConvert()
    {
        
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        
        var dateTime = (DateTime?)ts;
        
        
        dateTime.ShouldNotBeNull();
        dateTime.Value.Year.ShouldBe(2023);
    }

    [Fact]
    public void Validate_ValidTimestamp_ShouldReturnTrue()
    {
        
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        
        var isValid = ts.Validate(out var errors);
        
        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyTimestamp_ShouldReturnTrue()
    {
        
        var ts = new TS();
        
         
        ts.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        
        TS ts = "20230101120000";
        
        
        ts.Time.Value.ShouldBe("20230101120000");
    }

    [Fact]
    public void ToString_ShouldReturnTimeValue()
    {
        
        var ts = new TS();
        ts.Parse("20230101120000".AsSpan(), HL7Delimiters.Default);
        
        
        var result = ts.ToString();
        
        
        result.ShouldBe("20230101120000");
    }
}
