using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Tests.DataTypes.Primitive;

public class DTTests
{
    [Fact]
    public void Constructor_WithValidDate_ShouldSetValue()
    {
         
        var dt = new DT("20230115");

        
        dt.Value.ShouldBe("20230115");
        dt.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithDateTime_ShouldFormatCorrectly()
    {
         
        var dt = new DT(new DateTime(2023, 1, 15));

        
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void Constructor_WithDateOnly_ShouldFormatCorrectly()
    {
         
        var dt = new DT(new DateOnly(2023, 1, 15));

        
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void ToDateTime_WithFullDate_ShouldParseCorrectly()
    {
        
        var dt = new DT("20230115");

        
        var result = dt.ToDateTime();

        
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
        result.Value.Month.ShouldBe(1);
        result.Value.Day.ShouldBe(15);
    }

    [Fact]
    public void ToDateTime_WithYearMonthOnly_ShouldParseCorrectly()
    {
        
        var dt = new DT("202301");

        
        var result = dt.ToDateTime();

        
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
        result.Value.Month.ShouldBe(1);
    }

    [Fact]
    public void ToDateTime_WithYearOnly_ShouldParseCorrectly()
    {
        
        var dt = new DT("2023");

        
        var result = dt.ToDateTime();

        
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
    }

    [Fact]
    public void ToDateTime_WithEmptyValue_ShouldReturnNull()
    {
        
        var dt = new DT("");

        
        var result = dt.ToDateTime();

        
        result.ShouldBeNull();
    }

    [Fact]
    public void ToDateOnly_ShouldConvertCorrectly()
    {
        
        var dt = new DT("20230115");

        
        var result = dt.ToDateOnly();

        
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
        result.Value.Month.ShouldBe(1);
        result.Value.Day.ShouldBe(15);
    }

    [Fact]
    public void Validate_WithValidFullDate_ShouldReturnTrue()
    {
        
        var dt = new DT("20230115");

        
        var isValid = dt.Validate(out var errors);

        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithValidYearMonth_ShouldReturnTrue()
    {
        
        var dt = new DT("202301");

        
        var isValid = dt.Validate(out var errors);

        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithInvalidFormat_ShouldReturnFalse()
    {
        
        var dt = new DT("2023-01-15");

        
        var isValid = dt.Validate(out var errors);

        
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("invalid format");
    }

    [Fact]
    public void Validate_WithInvalidDate_ShouldReturnFalse()
    {
        
        var dt = new DT("20231332"); // Month 13, day 32

        
        var isValid = dt.Validate(out var errors);

        
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldWork()
    {
         
        DT dt = "20230115";

        
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        
        var dt = new DT("20230115");

        
        string value = dt;

        
        value.ShouldBe("20230115");
    }

    [Fact]
    public void ImplicitConversion_FromDateTime_ShouldWork()
    {
         
        DT dt = new DateTime(2023, 1, 15);

        
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void TypeCode_ShouldReturnDT()
    {
        
        var dt = new DT("20230115");

        
        dt.TypeCode.ShouldBe("DT");
    }
}
