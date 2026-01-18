using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class XCNTests
{
    [Fact]
    public void Parse_SimpleProviderName_ShouldParseCorrectly()
    {
        // Arrange
        var input = "1234567^SMITH^JOHN";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xcn = new XCN();
        xcn.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xcn.Id.Value.ShouldBe("1234567");
        xcn.FamilyName.Surname.Value.ShouldBe("SMITH");
        xcn.GivenName.Value.ShouldBe("JOHN");
    }

    [Fact]
    public void Parse_CompleteProviderName_ShouldParseAllComponents()
    {
        // Arrange
        var input = "1234567^SMITH^JOHN^A^^^MD";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xcn = new XCN();
        xcn.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xcn.Id.Value.ShouldBe("1234567");
        xcn.FamilyName.Surname.Value.ShouldBe("SMITH");
        xcn.GivenName.Value.ShouldBe("JOHN");
        xcn.SecondNames.Value.ShouldBe("A");
        xcn.Degree.Value.ShouldBe("MD");
    }

    [Fact]
    public void ToHL7String_CompleteProviderName_ShouldFormatCorrectly()
    {
        // Arrange
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN^A^^^MD".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xcn.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("1234567^SMITH^JOHN^A^^^MD");
    }

    [Fact]
    public void RoundTrip_CompleteProviderName_ShouldMaintainData()
    {
        // Arrange
        var original = "1234567^SMITH^JOHN^A^^^MD";
        
        // Act
        var xcn = new XCN();
        xcn.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = xcn.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedName_ShouldFormatCorrectly()
    {
        // Arrange
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xcn.GetFormattedName(NameFormat.GivenFamily);
        
        // Assert
        result.ShouldBe("JOHN SMITH");
    }

    [Fact]
    public void ToString_WithId_ShouldIncludeIdAndName()
    {
        // Arrange
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xcn.ToString();
        
        // Assert
        result.ShouldContain("1234567");
        result.ShouldContain("JOHN SMITH");
    }

    [Fact]
    public void Validate_ValidProviderName_ShouldReturnTrue()
    {
        // Arrange
        var xcn = new XCN();
        xcn.Parse("1234567^SMITH^JOHN".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var isValid = xcn.Validate(out var errors);
        
        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyProviderName_ShouldReturnTrue()
    {
        // Arrange
        var xcn = new XCN();
        
        // Act & Assert
        xcn.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        XCN xcn = "1234567^SMITH^JOHN";
        
        // Assert
        xcn.Id.Value.ShouldBe("1234567");
        xcn.FamilyName.Surname.Value.ShouldBe("SMITH");
    }
}
