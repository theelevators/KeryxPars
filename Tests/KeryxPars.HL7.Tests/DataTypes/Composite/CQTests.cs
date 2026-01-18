using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class CQTests
{
    [Fact]
    public void Parse_SimpleQuantity_ShouldParseCorrectly()
    {
        
        var input = "500^MG";
        var delimiters = HL7Delimiters.Default;
        
        
        var cq = new CQ();
        cq.Parse(input.AsSpan(), delimiters);
        
        
        cq.Quantity.Value.ShouldBe("500");
        cq.Units.Identifier.Value.ShouldBe("MG");
    }

    [Fact]
    public void ToHL7String_SimpleQuantity_ShouldFormatCorrectly()
    {
        
        var cq = new CQ();
        cq.Parse("500^MG".AsSpan(), HL7Delimiters.Default);
        
        
        var result = cq.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe("500^MG");
    }

    [Fact]
    public void RoundTrip_SimpleQuantity_ShouldMaintainData()
    {
        
        var original = "500^MG";
        
        
        var cq = new CQ();
        cq.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = cq.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe(original);
    }

    [Fact]
    public void ToString_WithUnitsText_ShouldFormatCorrectly()
    {
        
        var cq = new CQ();
        cq.Parse("500^milligrams".AsSpan(), HL7Delimiters.Default);
        
        
        var result = cq.ToString();
        
        
        result.ShouldBe("500 milligrams");
    }

    [Fact]
    public void ToString_WithoutUnits_ShouldReturnQuantity()
    {
        
        var cq = new CQ();
        cq.Parse("500".AsSpan(), HL7Delimiters.Default);
        
        
        var result = cq.ToString();
        
        
        result.ShouldBe("500");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        
        CQ cq = "500^MG";
        
        
        cq.Quantity.Value.ShouldBe("500");
    }
}
