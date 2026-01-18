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
        
        var input = "123 MAIN ST^^SPRINGFIELD^IL^62701";
        var delimiters = HL7Delimiters.Default;
        
        
        var xad = new XAD();
        xad.Parse(input.AsSpan(), delimiters);
        
        
        xad.StreetAddress.StreetOrMailingAddress.Value.ShouldBe("123 MAIN ST");
        xad.City.Value.ShouldBe("SPRINGFIELD");
        xad.StateOrProvince.Value.ShouldBe("IL");
        xad.ZipOrPostalCode.Value.ShouldBe("62701");
    }

    [Fact]
    public void Parse_CompleteAddress_ShouldParseAllComponents()
    {
        
        var input = "123 MAIN ST^^SPRINGFIELD^IL^62701^USA^H";
        var delimiters = HL7Delimiters.Default;
        
        
        var xad = new XAD();
        xad.Parse(input.AsSpan(), delimiters);
        
        
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
        
        
        var result = xad.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe("123 MAIN ST^^SPRINGFIELD^IL^62701^USA");
    }

    [Fact]
    public void RoundTrip_CompleteAddress_ShouldMaintainData()
    {
        
        var original = "123 MAIN ST^^SPRINGFIELD^IL^62701^USA";
        
        
        var xad = new XAD();
        xad.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = xad.ToHL7String(HL7Delimiters.Default);
        
        
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedAddress_ShouldFormatCorrectly()
    {
        
        var xad = new XAD();
        xad.Parse("123 MAIN ST^^SPRINGFIELD^IL^62701^USA".AsSpan(), HL7Delimiters.Default);
        
        
        var result = xad.GetFormattedAddress();
        
        
        result.ShouldContain("123 MAIN ST");
        result.ShouldContain("SPRINGFIELD");
        result.ShouldContain("IL 62701");
        result.ShouldContain("USA");
    }

    [Fact]
    public void Validate_ValidAddress_ShouldReturnTrue()
    {
        
        var xad = new XAD();
        xad.Parse("123 MAIN ST^^SPRINGFIELD^IL^62701".AsSpan(), HL7Delimiters.Default);
        
        
        var isValid = xad.Validate(out var errors);
        
        
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyAddress_ShouldReturnTrue()
    {
        
        var xad = new XAD();
        
         
        xad.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        
        XAD xad = "123 MAIN ST^^SPRINGFIELD^IL^62701";
        
        
        xad.City.Value.ShouldBe("SPRINGFIELD");
    }

    [Fact]
    public void ToString_ShouldReturnFormattedAddress()
    {
        
        var xad = new XAD();
        xad.Parse("123 MAIN ST^^SPRINGFIELD^IL^62701".AsSpan(), HL7Delimiters.Default);
        
        
        var result = xad.ToString();
        
        
        result.ShouldContain("123 MAIN ST");
        result.ShouldContain("SPRINGFIELD");
    }
}
