using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class CQTests
{
    [Fact]
    public void Parse_SimpleQuantity_ShouldParseCorrectly()
    {
        // Arrange
        var input = "500^MG";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var cq = new CQ();
        cq.Parse(input.AsSpan(), delimiters);
        
        // Assert
        cq.Quantity.Value.ShouldBe("500");
        cq.Units.Identifier.Value.ShouldBe("MG");
    }

    [Fact]
    public void ToHL7String_SimpleQuantity_ShouldFormatCorrectly()
    {
        // Arrange
        var cq = new CQ();
        cq.Parse("500^MG".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = cq.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("500^MG");
    }

    [Fact]
    public void RoundTrip_SimpleQuantity_ShouldMaintainData()
    {
        // Arrange
        var original = "500^MG";
        
        // Act
        var cq = new CQ();
        cq.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = cq.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void ToString_WithUnitsText_ShouldFormatCorrectly()
    {
        // Arrange
        var cq = new CQ();
        cq.Parse("500^milligrams".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = cq.ToString();
        
        // Assert
        result.ShouldBe("500 milligrams");
    }

    [Fact]
    public void ToString_WithoutUnits_ShouldReturnQuantity()
    {
        // Arrange
        var cq = new CQ();
        cq.Parse("500".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = cq.ToString();
        
        // Assert
        result.ShouldBe("500");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        CQ cq = "500^MG";
        
        // Assert
        cq.Quantity.Value.ShouldBe("500");
    }
}
