using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class CETests
{
    [Fact]
    public void Parse_SimpleCode_ShouldParseCorrectly()
    {
        // Arrange
        var input = "E11.9^Type 2 diabetes^ICD10";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var ce = new CE();
        ce.Parse(input.AsSpan(), delimiters);
        
        // Assert
        ce.Identifier.Value.ShouldBe("E11.9");
        ce.Text.Value.ShouldBe("Type 2 diabetes");
        ce.NameOfCodingSystem.Value.ShouldBe("ICD10");
    }

    [Fact]
    public void ToHL7String_SimpleCode_ShouldFormatCorrectly()
    {
        // Arrange
        var ce = new CE();
        ce.Parse("E11.9^Type 2 diabetes^ICD10".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = ce.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("E11.9^Type 2 diabetes^ICD10");
    }

    [Fact]
    public void RoundTrip_SimpleCode_ShouldMaintainData()
    {
        // Arrange
        var original = "E11.9^Type 2 diabetes^ICD10";
        
        // Act
        var ce = new CE();
        ce.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = ce.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void ToString_ShouldReturnText()
    {
        // Arrange
        var ce = new CE();
        ce.Parse("E11.9^Type 2 diabetes^ICD10".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = ce.ToString();
        
        // Assert
        result.ShouldBe("Type 2 diabetes");
    }

    [Fact]
    public void ToString_NoText_ShouldReturnIdentifier()
    {
        // Arrange
        var ce = new CE();
        ce.Parse("E11.9".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = ce.ToString();
        
        // Assert
        result.ShouldBe("E11.9");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        CE ce = "E11.9^Type 2 diabetes^ICD10";
        
        // Assert
        ce.Identifier.Value.ShouldBe("E11.9");
        ce.Text.Value.ShouldBe("Type 2 diabetes");
    }
}
