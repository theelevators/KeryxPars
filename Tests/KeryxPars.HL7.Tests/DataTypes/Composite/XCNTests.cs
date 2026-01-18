using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class XCNTests
{
    [Fact]
    public void Parse_SimpleProviderName_ShouldParseCorrectly()
    {
        
        var input = "1234567^SMITH^JOHN";
        var delimiters = HL7Delimiters.Default;
        
        
        var xcn = new XCN();
        xcn.Parse(input.AsSpan(), delimiters);
        
        
        xcn.Id.Value.ShouldBe("1234567");
        xcn.FamilyName.Surname.Value.ShouldBe("SMITH");
        xcn.GivenName.Value.ShouldBe("JOHN");
    }

    [Fact]
    public void Parse_CompleteProviderName_ShouldParseAllComponents()
    {
        
        var input = "1234567^SMITH^JOHN^A^^^MD";
        var delimiters = HL7Delimiters.Default;
        
        
        var xcn = new XCN();
        xcn.Parse(input.AsSpan(), delimiters);
        
        
        xcn.Id.Value.ShouldBe("1234567");
        xcn.FamilyName.Surname.Value.ShouldBe("SMITH");
        xcn.GivenName.Value.ShouldBe("JOHN");
        xcn.SecondNames.Value.ShouldBe("A");
        xcn.Degree.Value.ShouldBe("MD");
    }

    [Fact]
    public void ToHL7String_CompleteProviderName_ShouldFormatCorrectly()
    {
        
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN^A^^^MD".AsSpan(), HL7Delimiters.Default);
        
        
        var result = xcn.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe("1234567^SMITH^JOHN^A^^^MD");
    }

    [Fact]
    public void RoundTrip_CompleteProviderName_ShouldMaintainData()
    {
        
        var original = "1234567^SMITH^JOHN^A^^^MD";
        
        
        var xcn = new XCN();
        xcn.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = xcn.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedName_ShouldFormatCorrectly()
    {
        
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN".AsSpan(), HL7Delimiters.Default);
        
        
        var result = xcn.GetFormattedName(NameFormat.GivenFamily);
        
        
        result.ShouldBe("JOHN SMITH");
    }

    [Fact]
    public void ToString_WithId_ShouldIncludeIdAndName()
    {
        
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN".AsSpan(), HL7Delimiters.Default);
        
        
        var result = xcn.ToString();
        
        
        result.ShouldContain("1234567");
        result.ShouldContain("JOHN SMITH");
    }

    [Fact]
    public void Validate_ValidProviderName_ShouldReturnTrue()
    {
        
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN".AsSpan(), HL7Delimiters.Default);
        
        
        var isValid = xcn.Validate(out var errors);
        
        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyProviderName_ShouldReturnTrue()
    {
        
        var xcn = new XCN();
        
         
        xcn.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        
        XCN xcn = "1234567^SMITH^JOHN";
        
        
        xcn.Id.Value.ShouldBe("1234567");
        xcn.FamilyName.Surname.Value.ShouldBe("SMITH");
    }
}
