using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class XPNTests
{
    [Fact]
    public void Parse_SimpleName_ShouldParseCorrectly()
    {
        // Arrange
        var input = "DOE^JOHN";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xpn = new XPN();
        xpn.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xpn.FamilyName.Surname.Value.ShouldBe("DOE");
        xpn.GivenName.Value.ShouldBe("JOHN");
        xpn.SecondNames.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Parse_CompletePersonName_ShouldParseAllComponents()
    {
        // Arrange
        var input = "DOE^JOHN^MICHAEL^JR^DR^MD";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var xpn = new XPN();
        xpn.Parse(input.AsSpan(), delimiters);
        
        // Assert
        xpn.FamilyName.Surname.Value.ShouldBe("DOE");
        xpn.GivenName.Value.ShouldBe("JOHN");
        xpn.SecondNames.Value.ShouldBe("MICHAEL");
        xpn.Suffix.Value.ShouldBe("JR");
        xpn.Prefix.Value.ShouldBe("DR");
        xpn.Degree.Value.ShouldBe("MD");
    }

    [Fact]
    public void ToHL7String_CompletePersonName_ShouldFormatCorrectly()
    {
        // Arrange
        FN familyName = "DOE";
        ST givenName = "JOHN";
        ST secondNames = "MICHAEL";
        ST suffix = "JR";
        ST prefix = "DR";
        IS degree = "MD";

        var xpn = new XPN(
            familyName: familyName,
            givenName: givenName,
            secondNames: secondNames,
            suffix: suffix,
            prefix: prefix,
            degree: degree
        );
        
        // Act
        var result = xpn.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("DOE^JOHN^MICHAEL^JR^DR^MD");
    }

    [Fact]
    public void RoundTrip_CompletePersonName_ShouldMaintainData()
    {
        // Arrange
        var original = "DOE^JOHN^MICHAEL^JR^DR^MD";
        
        // Act
        var xpn = new XPN();
        xpn.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = xpn.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedName_FamilyGiven_ShouldFormatCorrectly()
    {
        // Arrange
        var xpn = new XPN();
        xpn.Parse("DOE^JOHN^MICHAEL".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xpn.GetFormattedName(NameFormat.FamilyGiven);
        
        // Assert
        result.ShouldBe("DOE, JOHN");
    }

    [Fact]
    public void GetFormattedName_GivenFamily_ShouldFormatCorrectly()
    {
        // Arrange
        var xpn = new XPN();
        xpn.Parse("DOE^JOHN^MICHAEL".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xpn.GetFormattedName(NameFormat.GivenFamily);
        
        // Assert
        result.ShouldBe("JOHN DOE");
    }

    [Fact]
    public void GetFormattedName_Full_ShouldIncludeAllComponents()
    {
        // Arrange
        var xpn = new XPN();
        xpn.Parse("DOE^JOHN^MICHAEL^JR^DR^MD".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xpn.GetFormattedName(NameFormat.Full);
        
        // Assert
        result.ShouldContain("DR");
        result.ShouldContain("JOHN");
        result.ShouldContain("MICHAEL");
        result.ShouldContain("DOE");
        result.ShouldContain("JR");
        result.ShouldContain("MD");
    }

    [Fact]
    public void Validate_ValidName_ShouldReturnTrue()
    {
        // Arrange
        var xpn = new XPN();
        xpn.Parse("DOE^JOHN".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var isValid = xpn.Validate(out var errors);
        
        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyName_ShouldReturnTrue()
    {
        // Arrange
        var xpn = new XPN();
        
        // Act & Assert
        xpn.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void IsEmpty_NameWithData_ShouldReturnFalse()
    {
        // Arrange
        var xpn = new XPN();
        xpn.Parse("DOE^JOHN".AsSpan(), HL7Delimiters.Default);
        
        // Act & Assert
        xpn.IsEmpty.ShouldBeFalse();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        XPN xpn = "DOE^JOHN";
        
        // Assert
        xpn.FamilyName.Surname.Value.ShouldBe("DOE");
        xpn.GivenName.Value.ShouldBe("JOHN");
    }

    [Fact]
    public void ToString_ShouldReturnGivenFamilyFormat()
    {
        // Arrange
        var xpn = new XPN();
        xpn.Parse("DOE^JOHN".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = xpn.ToString();
        
        // Assert
        result.ShouldBe("JOHN DOE");
    }
}
