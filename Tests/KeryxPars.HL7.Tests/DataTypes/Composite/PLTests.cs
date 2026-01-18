using KeryxPars.HL7.DataTypes.Composite;
using KeryxPars.HL7.Definitions;
using Shouldly;

namespace KeryxPars.HL7.Tests.DataTypes.Composite;

public class PLTests
{
    [Fact]
    public void Parse_SimpleLocation_ShouldParseCorrectly()
    {

        var input = "ICU^201^A";
        var delimiters = HL7Delimiters.Default;


        var pl = new PL();
        pl.Parse(input.AsSpan(), delimiters);


        pl.PointOfCare.Value.ShouldBe("ICU");
        pl.Room.Value.ShouldBe("201");
        pl.Bed.Value.ShouldBe("A");
    }

    [Fact]
    public void Parse_CompleteLocation_ShouldParseAllComponents()
    {

        var input = "ICU^201^A^MAIN HOSPITAL^N^^NORTH^2";
        var delimiters = HL7Delimiters.Default;


        var pl = new PL();
        pl.Parse(input.AsSpan(), delimiters);


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

        var pl = new PL();
        pl.Parse("ICU^201^A^MAIN HOSPITAL".AsSpan(), HL7Delimiters.Default);


        var result = pl.ToHL7String(HL7Delimiters.Default);


        result.ShouldBe("ICU^201^A^MAIN HOSPITAL");
    }

    [Fact]
    public void RoundTrip_CompleteLocation_ShouldMaintainData()
    {

        var original = "ICU^201^A^MAIN HOSPITAL";


        var pl = new PL();
        pl.Parse(original.AsSpan(), HL7Delimiters.Default);
        var result = pl.ToHL7String(HL7Delimiters.Default);


        result.ShouldBe(original);
    }

    [Fact]
    public void GetFormattedLocation_ShouldFormatCorrectly()
    {

        var pl = new PL();
        pl.Parse("ICU^201^A^MAIN HOSPITAL^N^^NORTH^2".AsSpan(), HL7Delimiters.Default);


        var result = pl.GetFormattedLocation();


        result.ShouldContain("ICU");
        result.ShouldContain("Room 201");
        result.ShouldContain("Bed A");
        result.ShouldContain("Building NORTH");
        result.ShouldContain("Floor 2");
    }

    [Fact]
    public void Validate_ValidLocation_ShouldReturnTrue()
    {

        var pl = new PL();
        pl.Parse("ICU^201^A".AsSpan(), HL7Delimiters.Default);


        var isValid = pl.Validate(out var errors);


        isValid.ShouldBeTrue();
        errors.ShouldBeEmpty();
    }

    [Fact]
    public void IsEmpty_EmptyLocation_ShouldReturnTrue()
    {

        var pl = new PL();


        pl.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_FromString_ShouldParse()
    {

        PL pl = "ICU^201^A";


        pl.PointOfCare.Value.ShouldBe("ICU");
        pl.Room.Value.ShouldBe("201");
    }

    [Fact]
    public void ToString_ShouldReturnFormattedLocation()
    {

        var pl = new PL();
        pl.Parse("ICU^201^A".AsSpan(), HL7Delimiters.Default);


        var result = pl.ToString();


        result.ShouldContain("ICU");
        result.ShouldContain("201");
        result.ShouldContain("A");
    }
}
