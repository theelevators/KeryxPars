using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Tests.DataTypes.Primitive;

public class NMTests
{
    [Fact]
    public void Constructor_WithString_ShouldSetValue()
    {
         
        var nm = new NM("123.45");

        
        nm.Value.ShouldBe("123.45");
        nm.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithDecimal_ShouldFormatCorrectly()
    {
         
        var nm = new NM(123.45m);

        
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void Constructor_WithDouble_ShouldFormatCorrectly()
    {
         
        var nm = new NM(123.45);

        
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void ToDecimal_WithValidValue_ShouldConvert()
    {
        
        var nm = new NM("123.45");

        
        var result = nm.ToDecimal();

        
        result.ShouldNotBeNull();
        result.Value.ShouldBe(123.45m);
    }

    [Fact]
    public void ToDouble_WithValidValue_ShouldConvert()
    {
        
        var nm = new NM("123.45");

        
        var result = nm.ToDouble();

        
        result.ShouldNotBeNull();
        result.Value.ShouldBe(123.45, 0.001);
    }

    [Fact]
    public void ToInt32_WithWholeNumber_ShouldConvert()
    {
        
        var nm = new NM("123");

        
        var result = nm.ToInt32();

        
        result.ShouldNotBeNull();
        result.Value.ShouldBe(123);
    }

    [Fact]
    public void ToInt32_WithDecimal_ShouldReturnNull()
    {
        
        var nm = new NM("123.45");

        
        var result = nm.ToInt32();

        
        result.ShouldBeNull();
    }

    [Fact]
    public void ToDecimal_WithEmptyValue_ShouldReturnNull()
    {
        
        var nm = new NM("");

        
        var result = nm.ToDecimal();

        
        result.ShouldBeNull();
    }

    [Fact]
    public void Validate_WithValidNumber_ShouldReturnTrue()
    {
        
        var nm = new NM("123.45");

        
        var isValid = nm.Validate(out var errors);

        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithNegativeNumber_ShouldReturnTrue()
    {
        
        var nm = new NM("-123.45");

        
        var isValid = nm.Validate(out var errors);

        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithInvalidNumber_ShouldReturnFalse()
    {
        
        var nm = new NM("abc");

        
        var isValid = nm.Validate(out var errors);

        
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("invalid numeric value");
    }

    [Fact]
    public void Validate_WithTooLongValue_ShouldWarn()
    {
        
        var nm = new NM("12345678901234567"); // 17 characters

        
        var isValid = nm.Validate(out var errors);

        
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("exceeds recommended maximum length");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldWork()
    {
         
        NM nm = "123.45";

        
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        
        var nm = new NM("123.45");

        
        string value = nm;

        
        value.ShouldBe("123.45");
    }

    [Fact]
    public void ImplicitConversion_FromDecimal_ShouldWork()
    {
         
        NM nm = 123.45m;

        
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void TypeCode_ShouldReturnNM()
    {
        
        var nm = new NM("123");

        
        nm.TypeCode.ShouldBe("NM");
    }
}
