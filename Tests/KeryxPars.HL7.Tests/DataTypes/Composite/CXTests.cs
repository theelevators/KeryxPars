using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class CXTests
{
    [Fact]
    public void Parse_SimpleId_ShouldParseCorrectly()
    {
        // Arrange
        var input = "12345^^^HOSPITAL^MR";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var cx = new CX();
        cx.Parse(input.AsSpan(), delimiters);
        
        // Assert
        cx.Id.Value.ShouldBe("12345");
        cx.AssigningAuthority.NamespaceId.Value.ShouldBe("HOSPITAL");
        cx.IdentifierTypeCode.Value.ShouldBe("MR");
    }

    [Fact]
    public void ToHL7String_SimpleId_ShouldFormatCorrectly()
    {
        // Arrange
        var cx = new CX();
        cx.Parse("12345^^^HOSPITAL^MR".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = cx.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("12345^^^HOSPITAL^MR");
    }

    [Fact]
    public void RoundTrip_SimpleId_ShouldMaintainData()
    {
        // Arrange
        var original = "12345^^^HOSPITAL^MR";
        
        // Act
        var cx = new CX();
        cx.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = cx.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void ToString_ShouldReturnId()
    {
        // Arrange
        var cx = new CX();
        cx.Parse("12345^^^HOSPITAL^MR".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = cx.ToString();
        
        // Assert
        result.ShouldBe("12345");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        CX cx = "12345^^^HOSPITAL^MR";
        
        // Assert
        cx.Id.Value.ShouldBe("12345");
        cx.AssigningAuthority.NamespaceId.Value.ShouldBe("HOSPITAL");
    }
}
