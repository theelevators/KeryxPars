using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Tests.DataTypes.Primitive;

public class STTests
{
    [Fact]
    public void Constructor_WithValidString_ShouldSetValue()
    {
         
        var st = new ST("Test Value");

        
        st.Value.ShouldBe("Test Value");
        st.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void Constructor_WithNull_ShouldSetEmptyString()
    {
         
        var st = new ST(null);

        
        st.Value.ShouldBe(string.Empty);
        st.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Parse_WithValidValue_ShouldSetValue()
    {
        
        var st = new ST();
        var delimiters = HL7Delimiters.Default;

        
        st.Parse("Parsed Value".AsSpan(), delimiters);

        
        st.Value.ShouldBe("Parsed Value");
    }

    [Fact]
    public void ToHL7String_ShouldReturnValue()
    {
        
        var st = new ST("HL7 String");
        var delimiters = HL7Delimiters.Default;

        
        var result = st.ToHL7String(delimiters);

        
        result.ShouldBe("HL7 String");
    }

    [Fact]
    public void Validate_WithValidValue_ShouldReturnTrue()
    {
        
        var st = new ST("Short text");

        
        var isValid = st.Validate(out var errors);

        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithTooLongValue_ShouldReturnFalse()
    {
        
        var longText = new string('X', 200);
        var st = new ST(longText);

        
        var isValid = st.Validate(out var errors);

        
        isValid.ShouldBeFalse();
        errors.ShouldNotBeEmpty();
        errors[0].ShouldContain("exceeds maximum length of 199");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldWork()
    {
         
        ST st = "Test";

        
        st.Value.ShouldBe("Test");
    }

    [Fact]
    public void ImplicitConversion_ToString_ShouldWork()
    {
        
        var st = new ST("Test");

        
        string value = st;

        
        value.ShouldBe("Test");
    }

    [Fact]
    public void TypeCode_ShouldReturnST()
    {
        
        var st = new ST("Test");

        
        st.TypeCode.ShouldBe("ST");
    }
}
