using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Tests.DataTypes.Primitive;

public class DTTests
{
    [Fact]
    public void Constructor_WithValidDate_ShouldSetValue()
    {
        // Arrange & Act
        var dt = new DT("20230115");

        // Assert
        dt.Value.ShouldBe("20230115");
        dt.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithDateTime_ShouldFormatCorrectly()
    {
        // Arrange & Act
        var dt = new DT(new DateTime(2023, 1, 15));

        // Assert
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void Constructor_WithDateOnly_ShouldFormatCorrectly()
    {
        // Arrange & Act
        var dt = new DT(new DateOnly(2023, 1, 15));

        // Assert
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void ToDateTime_WithFullDate_ShouldParseCorrectly()
    {
        // Arrange
        var dt = new DT("20230115");

        // Act
        var result = dt.ToDateTime();

        // Assert
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
        result.Value.Month.ShouldBe(1);
        result.Value.Day.ShouldBe(15);
    }

    [Fact]
    public void ToDateTime_WithYearMonthOnly_ShouldParseCorrectly()
    {
        // Arrange
        var dt = new DT("202301");

        // Act
        var result = dt.ToDateTime();

        // Assert
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
        result.Value.Month.ShouldBe(1);
    }

    [Fact]
    public void ToDateTime_WithYearOnly_ShouldParseCorrectly()
    {
        // Arrange
        var dt = new DT("2023");

        // Act
        var result = dt.ToDateTime();

        // Assert
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
    }

    [Fact]
    public void ToDateTime_WithEmptyValue_ShouldReturnNull()
    {
        // Arrange
        var dt = new DT("");

        // Act
        var result = dt.ToDateTime();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void ToDateOnly_ShouldConvertCorrectly()
    {
        // Arrange
        var dt = new DT("20230115");

        // Act
        var result = dt.ToDateOnly();

        // Assert
        result.ShouldNotBeNull();
        result.Value.Year.ShouldBe(2023);
        result.Value.Month.ShouldBe(1);
        result.Value.Day.ShouldBe(15);
    }

    [Fact]
    public void Validate_WithValidFullDate_ShouldReturnTrue()
    {
        // Arrange
        var dt = new DT("20230115");

        // Act
        var isValid = dt.Validate(out var errors);

        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithValidYearMonth_ShouldReturnTrue()
    {
        // Arrange
        var dt = new DT("202301");

        // Act
        var isValid = dt.Validate(out var errors);

        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithInvalidFormat_ShouldReturnFalse()
    {
        // Arrange
        var dt = new DT("2023-01-15");

        // Act
        var isValid = dt.Validate(out var errors);

        // Assert
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("invalid format");
    }

    [Fact]
    public void Validate_WithInvalidDate_ShouldReturnFalse()
    {
        // Arrange
        var dt = new DT("20231332"); // Month 13, day 32

        // Act
        var isValid = dt.Validate(out var errors);

        // Assert
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldWork()
    {
        // Arrange & Act
        DT dt = "20230115";

        // Assert
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        // Arrange
        var dt = new DT("20230115");

        // Act
        string value = dt;

        // Assert
        value.ShouldBe("20230115");
    }

    [Fact]
    public void ImplicitConversion_FromDateTime_ShouldWork()
    {
        // Arrange & Act
        DT dt = new DateTime(2023, 1, 15);

        // Assert
        dt.Value.ShouldBe("20230115");
    }

    [Fact]
    public void TypeCode_ShouldReturnDT()
    {
        // Arrange
        var dt = new DT("20230115");

        // Assert
        dt.TypeCode.ShouldBe("DT");
    }
}
