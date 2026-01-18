using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class CXTests
{
    [Fact]
    public void Parse_SimpleId_ShouldParseCorrectly()
    {
        
        var input = "12345^^^HOSPITAL^MR";
        var delimiters = HL7Delimiters.Default;
        
        
        var cx = new CX();
        cx.Parse(input.AsSpan(), delimiters);
        
        
        cx.Id.Value.ShouldBe("12345");
        cx.AssigningAuthority.NamespaceId.Value.ShouldBe("HOSPITAL");
        cx.IdentifierTypeCode.Value.ShouldBe("MR");
    }

    [Fact]
    public void ToHL7String_SimpleId_ShouldFormatCorrectly()
    {
        
        var cx = new CX();
        cx.Parse("12345^^^HOSPITAL^MR".AsSpan(), HL7Delimiters.Default);
        
        
        var result = cx.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe("12345^^^HOSPITAL^MR");
    }

    [Fact]
    public void RoundTrip_SimpleId_ShouldMaintainData()
    {
        
        var original = "12345^^^HOSPITAL^MR";
        
        
        var cx = new CX();
        cx.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = cx.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe(original);
    }

    [Fact]
    public void ToString_ShouldReturnId()
    {
        
        var cx = new CX();
        cx.Parse("12345^^^HOSPITAL^MR".AsSpan(), HL7Delimiters.Default);
        
        
        var result = cx.ToString();
        
        
        result.ShouldBe("12345");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        
        CX cx = "12345^^^HOSPITAL^MR";
        
        
        cx.Id.Value.ShouldBe("12345");
        cx.AssigningAuthority.NamespaceId.Value.ShouldBe("HOSPITAL");
    }
}
