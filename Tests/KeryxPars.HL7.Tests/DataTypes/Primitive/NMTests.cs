using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Tests.DataTypes.Primitive;

public class NMTests
{
    [Fact]
    public void Constructor_WithString_ShouldSetValue()
    {
        // Arrange & Act
        var nm = new NM("123.45");

        // Assert
        nm.Value.ShouldBe("123.45");
        nm.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithDecimal_ShouldFormatCorrectly()
    {
        // Arrange & Act
        var nm = new NM(123.45m);

        // Assert
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void Constructor_WithDouble_ShouldFormatCorrectly()
    {
        // Arrange & Act
        var nm = new NM(123.45);

        // Assert
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void ToDecimal_WithValidValue_ShouldConvert()
    {
        // Arrange
        var nm = new NM("123.45");

        // Act
        var result = nm.ToDecimal();

        // Assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(123.45m);
    }

    [Fact]
    public void ToDouble_WithValidValue_ShouldConvert()
    {
        // Arrange
        var nm = new NM("123.45");

        // Act
        var result = nm.ToDouble();

        // Assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(123.45, 0.001);
    }

    [Fact]
    public void ToInt32_WithWholeNumber_ShouldConvert()
    {
        // Arrange
        var nm = new NM("123");

        // Act
        var result = nm.ToInt32();

        // Assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(123);
    }

    [Fact]
    public void ToInt32_WithDecimal_ShouldReturnNull()
    {
        // Arrange
        var nm = new NM("123.45");

        // Act
        var result = nm.ToInt32();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ToDecimal_WithEmptyValue_ShouldReturnNull()
    {
        // Arrange
        var nm = new NM("");

        // Act
        var result = nm.ToDecimal();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void Validate_WithValidNumber_ShouldReturnTrue()
    {
        // Arrange
        var nm = new NM("123.45");

        // Act
        var isValid = nm.Validate(out var errors);

        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithNegativeNumber_ShouldReturnTrue()
    {
        // Arrange
        var nm = new NM("-123.45");

        // Act
        var isValid = nm.Validate(out var errors);

        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithInvalidNumber_ShouldReturnFalse()
    {
        // Arrange
        var nm = new NM("abc");

        // Act
        var isValid = nm.Validate(out var errors);

        // Assert
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("invalid numeric value");
    }

    [Fact]
    public void Validate_WithTooLongValue_ShouldWarn()
    {
        // Arrange
        var nm = new NM("12345678901234567"); // 17 characters

        // Act
        var isValid = nm.Validate(out var errors);

        // Assert
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("exceeds recommended maximum length");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldWork()
    {
        // Arrange & Act
        NM nm = "123.45";

        // Assert
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        // Arrange
        var nm = new NM("123.45");

        // Act
        string value = nm;

        // Assert
        value.ShouldBe("123.45");
    }

    [Fact]
    public void ImplicitConversion_FromDecimal_ShouldWork()
    {
        // Arrange & Act
        NM nm = 123.45m;

        // Assert
        nm.Value.ShouldBe("123.45");
    }

    [Fact]
    public void TypeCode_ShouldReturnNM()
    {
        // Arrange
        var nm = new NM("123");

        // Assert
        nm.TypeCode.ShouldBe("NM");
    }
}
