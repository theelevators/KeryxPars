using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class XADTests
{
    [Fact]
    public void Parse_SimpleAddress_ShouldParseCorrectly()
    {
        // Arrange
        var input = "123 MAIN ST^^SPRINGFIELD^IL^62701";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xad = new XAD();
        xad.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xad.StreetAddress.StreetOrMailingAddress.Value.ShouldBe("123 MAIN ST");
        xad.City.Value.ShouldBe("SPRINGFIELD");
        xad.StateOrProvince.Value.ShouldBe("IL");
        xad.ZipOrPostalCode.Value.ShouldBe("62701");
    }

    [Fact]
    public void Parse_CompleteAddress_ShouldParseAllComponents()
    {
        // Arrange
        var input = "123 MAIN ST^^SPRINGFIELD^IL^62701^USA^H";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xad = new XAD();
        xad.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xad.StreetAddress.StreetOrMailingAddress.Value.ShouldBe("123 MAIN ST");
        xad.City.Value.ShouldBe("SPRINGFIELD");
        xad.StateOrProvince.Value.ShouldBe("IL");
        xad.ZipOrPostalCode.Value.ShouldBe("62701");
        xad.Country.Value.ShouldBe("USA");
        xad.AddressType.Value.ShouldBe("H");
    }

    [Fact]
    public void ToHL7String_CompleteAddress_ShouldFormatCorrectly()
    {
        // Arrange
        SAD streetAddress = "123 MAIN ST";
        ST city = "SPRINGFIELD";
        ST state = "IL";
        ST zip = "62701";
        ID country = "USA";

        var xad = new XAD(
            streetAddress: streetAddress,
            city: city,
            stateOrProvince: state,
            zipOrPostalCode: zip,
            country: country
        );
        
        // Act
        var result = xad.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("123 MAIN ST^^SPRINGFIELD^IL^62701^USA");
    }

    [Fact]
    public void RoundTrip_CompleteAddress_ShouldMaintainData()
    {
        // Arrange
        var original = "123 MAIN ST^^SPRINGFIELD^IL^62701^USA";
        
        // Act
        var xad = new XAD();
        xad.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = xad.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedAddress_ShouldFormatCorrectly()
    {
        // Arrange
        var xad = new XAD();
        xad.Parse("123 MAIN ST^^SPRINGFIELD^IL^62701^USA".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xad.GetFormattedAddress();
        
        // Assert
        result.ShouldContain("123 MAIN ST");
        result.ShouldContain("SPRINGFIELD");
        result.ShouldContain("IL 62701");
        result.ShouldContain("USA");
    }

    [Fact]
    public void Validate_ValidAddress_ShouldReturnTrue()
    {
        // Arrange
        var xad = new XAD();
        xad.Parse("123 MAIN ST^^SPRINGFIELD^IL^62701".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var isValid = xad.Validate(out var errors);
        
        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyAddress_ShouldReturnTrue()
    {
        // Arrange
        var xad = new XAD();
        
        // Act & Assert
        xad.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        XAD xad = "123 MAIN ST^^SPRINGFIELD^IL^62701";
        
        // Assert
        xad.City.Value.ShouldBe("SPRINGFIELD");
    }

    [Fact]
    public void ToString_ShouldReturnFormattedAddress()
    {
        // Arrange
        var xad = new XAD();
        xad.Parse("123 MAIN ST^^SPRINGFIELD^IL^62701".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xad.ToString();
        
        // Assert
        result.ShouldContain("123 MAIN ST");
        result.ShouldContain("SPRINGFIELD");
    }
}
