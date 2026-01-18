using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class PLTests
{
    [Fact]
    public void Parse_SimpleLocation_ShouldParseCorrectly()
    {
        // Arrange
        var input = "ICU^201^A";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var pl = new PL();
        pl.Parse(input.AsSpan(), delimiters);
        
        // Assert
        pl.PointOfCare.Value.ShouldBe("ICU");
        pl.Room.Value.ShouldBe("201");
        pl.Bed.Value.ShouldBe("A");
    }

    [Fact]
    public void Parse_CompleteLocation_ShouldParseAllComponents()
    {
        // Arrange
        var input = "ICU^201^A^MAIN HOSPITAL^N^^NORTH^2";
        var delimiters = HL7Delimiters.Default;
        
        // Act
        var pl = new PL();
        pl.Parse(input.AsSpan(), delimiters);
        
        // Assert
        pl.PointOfCare.Value.ShouldBe("ICU");
        pl.Room.Value.ShouldBe("201");
        pl.Bed.Value.ShouldBe("A");
        pl.Facility.NamespaceId.Value.ShouldBe("MAIN HOSPITAL");
        pl.LocationStatus.Value.ShouldBe("N");
        pl.Building.Value.ShouldBe("NORTH");
        pl.Floor.Value.ShouldBe("2");
    }

    [Fact]
    public void ToHL7String_CompleteLocation_ShouldFormatCorrectly()
    {
        // Arrange
        var pl = new PL();
        pl.Parse("ICU^201^A^MAIN HOSPITAL".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = pl.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe("ICU^201^A^MAIN HOSPITAL");
    }

    [Fact]
    public void RoundTrip_CompleteLocation_ShouldMaintainData()
    {
        // Arrange
        var original = "ICU^201^A^MAIN HOSPITAL";
        
        // Act
        var pl = new PL();
        pl.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = pl.ToHL7String(HL7Delimiters.Default);
        
        // Assert
        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedLocation_ShouldFormatCorrectly()
    {
        // Arrange
        var pl = new PL();
        pl.Parse("ICU^201^A^MAIN HOSPITAL^N^^NORTH^2".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = pl.GetFormattedLocation();
        
        // Assert
        result.ShouldContain("ICU");
        result.ShouldContain("Room 201");
        result.ShouldContain("Bed A");
        result.ShouldContain("Building NORTH");
        result.ShouldContain("Floor 2");
    }

    [Fact]
    public void Validate_ValidLocation_ShouldReturnTrue()
    {
        // Arrange
        var pl = new PL();
        pl.Parse("ICU^201^A".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var isValid = pl.Validate(out var errors);
        
        // Assert
        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyLocation_ShouldReturnTrue()
    {
        // Arrange
        var pl = new PL();
        
        // Act & Assert
        pl.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {
        // Act
        PL pl = "ICU^201^A";
        
        // Assert
        pl.PointOfCare.Value.ShouldBe("ICU");
        pl.Room.Value.ShouldBe("201");
    }

    [Fact]
    public void ToString_ShouldReturnFormattedLocation()
    {
        // Arrange
        var pl = new PL();
        pl.Parse("ICU^201^A".AsSpan(), HL7Delimiters.Default);
        
        // Act
        var result = pl.ToString();
        
        // Assert
        result.ShouldContain("ICU");
        result.ShouldContain("201");
        result.ShouldContain("A");
    }
}
