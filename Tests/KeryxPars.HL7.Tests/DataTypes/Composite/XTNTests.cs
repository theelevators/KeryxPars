using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class XTNTests
{
    [Fact]
    public void Parse_SimplePhone_ShouldParseCorrectly()
    {
        // Arrange
        var input = "(555)123-4567^PRN^PH";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xtn = new XTN();
        xtn.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xtn.TelephoneNumber.Value.ShouldBe("(555)123-4567");
        xtn.TelecommunicationUseCode.Value.ShouldBe("PRN");
        xtn.TelecommunicationEquipmentType.Value.ShouldBe("PH");
    }

    [Fact]
    public void Parse_Email_ShouldParseCorrectly()
    {
        // Arrange
        var input = "^^Internet^user@example.com";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xtn = new XTN();
        xtn.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xtn.EmailAddress.Value.ShouldBe("user@example.com");
        xtn.TelecommunicationEquipmentType.Value.ShouldBe("Internet");
    }

    [Fact]
    public void ToHL7String_SimplePhone_ShouldFormatCorrectly()
    {
        // Arrange
        var xtn = new XTN();
        xtn.Parse("(555)123-4567^PRN^PH".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xtn.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("(555)123-4567^PRN^PH");
    }

    [Fact]
    public void RoundTrip_SimplePhone_ShouldMaintainData()
    {
        // Arrange
        var original = "(555)123-4567^PRN^PH";
        
        // Act
        var xtn = new XTN();
        xtn.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = xtn.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedNumber_WithTelephoneNumber_ShouldReturnIt()
    {
        // Arrange
        var xtn = new XTN();
        xtn.Parse("(555)123-4567".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xtn.GetFormattedNumber();
        
        // Assert
        result.ShouldBe("(555)123-4567");
    }

    [Fact]
    public void ToString_WithEmail_ShouldReturnEmail()
    {
        // Arrange
        var xtn = new XTN();
        xtn.Parse("^^Internet^user@example.com".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xtn.ToString();
        
        // Assert
        result.ShouldBe("user@example.com");
    }

    [Fact]
    public void ToString_WithPhone_ShouldReturnFormattedNumber()
    {
        // Arrange
        var xtn = new XTN();
        xtn.Parse("(555)123-4567".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xtn.ToString();
        
        // Assert
        result.ShouldBe("(555)123-4567");
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        XTN xtn = "(555)123-4567^PRN^PH";
        
        // Assert
        xtn.TelephoneNumber.Value.ShouldBe("(555)123-4567");
    }
}
