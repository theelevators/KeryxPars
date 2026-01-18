using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Tests.DataTypes.Primitive;

public class STTests
{
    [Fact]
    public void Constructor_WithValidString_ShouldSetValue()
    {
        // Arrange & Act
        var st = new ST("Test Value");

        // Assert
        st.Value.ShouldBe("Test Value");
        st.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithNull_ShouldSetEmptyString()
    {
        // Arrange & Act
        var st = new ST(null);

        // Assert
        st.Value.ShouldBe(string.Empty);
        st.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Parse_WithValidValue_ShouldSetValue()
    {
        // Arrange
        var st = new ST();
        var delimiters = HL7Delimiters.Default;

        // Act
        st.Parse("Parsed Value".AsSpan(), delimiters);

        // Assert
        st.Value.ShouldBe("Parsed Value");
    }

    [Fact]
    public void ToHL7String_ShouldReturnValue()
    {
        // Arrange
        var st = new ST("HL7 String");
        var delimiters = HL7Delimiters.Default;

        // Act
        var result = st.ToHL7String(delimiters);

        // Assert
        result.ShouldBe("HL7 String");
    }

    [Fact]
    public void Validate_WithValidValue_ShouldReturnTrue()
    {
        // Arrange
        var st = new ST("Short text");

        // Act
        var isValid = st.Validate(out var errors);

        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithTooLongValue_ShouldReturnFalse()
    {
        // Arrange
        var longText = new string('X', 200);
        var st = new ST(longText);

        // Act
        var isValid = st.Validate(out var errors);

        // Assert
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("exceeds maximum length of 199");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldWork()
    {
        // Arrange & Act
        ST st = "Test";

        // Assert
        st.Value.ShouldBe("Test");
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        // Arrange
        var st = new ST("Test");

        // Act
        string value = st;

        // Assert
        value.ShouldBe("Test");
    }

    [Fact]
    public void TypeCode_ShouldReturnST()
    {
        // Arrange
        var st = new ST("Test");

        // Assert
        st.TypeCode.ShouldBe("ST");
    }
}
