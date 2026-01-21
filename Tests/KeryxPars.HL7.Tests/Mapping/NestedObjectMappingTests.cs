using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

public class NestedObjectMappingTests
{
    private const string SampleAdtWithNested = 
        "MSH|^~\\&|APP|FAC|RECEIVING|FAC|20230615143022||ADT^A01|MSG12345|P|2.5||\r" +
        "EVN|A01|20230615143020||\r" +
        "PID|1||MRN123456||DOE^JOHN^MICHAEL^JR^DR^MD||19800115|M|||123 MAIN ST^APT 4B^CITYVILLE^CA^90210||555-1234|||||||||\r" +
        "PV1|1|I|ICU^201^A||||||||||||||||VISIT123|||||||||||||||||||||||||||||||||||||||";

    [Fact]
    public void NestedObjects_Name_ShouldMap()
    {
        // Act
        var patient = PatientWithNestedObjectsMapper.MapFromSpan(SampleAdtWithNested.AsSpan());

        // Assert
        patient.Name.ShouldNotBeNull();
        patient.Name.LastName.ShouldBe("DOE");
        patient.Name.FirstName.ShouldBe("JOHN");
        patient.Name.MiddleName.ShouldBe("MICHAEL");
        patient.Name.Suffix.ShouldBe("JR");
        patient.Name.Prefix.ShouldBe("DR");
        patient.Name.Degree.ShouldBe("MD");
    }

    [Fact]
    public void NestedObjects_Address_ShouldMap()
    {
        // Act
        var patient = PatientWithNestedObjectsMapper.MapFromSpan(SampleAdtWithNested.AsSpan());

        // Assert
        patient.HomeAddress.ShouldNotBeNull();
        patient.HomeAddress.StreetAddress.ShouldBe("123 MAIN ST");
        patient.HomeAddress.OtherDesignation.ShouldBe("APT 4B");
        patient.HomeAddress.City.ShouldBe("CITYVILLE");
        patient.HomeAddress.StateOrProvince.ShouldBe("CA");
        patient.HomeAddress.ZipOrPostalCode.ShouldBe("90210");
    }

    [Fact]
    public void NestedObjects_PhoneNumber_ShouldMap()
    {
        // Act
        var patient = PatientWithNestedObjectsMapper.MapFromSpan(SampleAdtWithNested.AsSpan());

        // Assert
        patient.HomePhone.ShouldNotBeNull();
        patient.HomePhone.PhoneNumberFormatted.ShouldBe("555-1234");
    }

    [Fact]
    public void NestedObjects_AllFields_ShouldMap()
    {
        // Act
        var patient = PatientWithNestedObjectsMapper.MapFromSpan(SampleAdtWithNested.AsSpan());

        // Assert
        patient.PatientId.ShouldBe("MRN123456");
        patient.DateOfBirth.ShouldBe(new DateTime(1980, 1, 15));
        patient.Gender.ShouldBe(Gender.M);
        
        // Nested name
        patient.Name.FirstName.ShouldBe("JOHN");
        patient.Name.LastName.ShouldBe("DOE");
        
        // Nested address  
        patient.HomeAddress.City.ShouldBe("CITYVILLE");
        patient.HomeAddress.ZipOrPostalCode.ShouldBe("90210");
        
        // Nested phone
        patient.HomePhone.PhoneNumberFormatted.ShouldBe("555-1234");
        
        // Visit info
        patient.PatientClass.ShouldBe(PatientClass.I);
        patient.VisitNumber.ShouldBe("VISIT123");
    }
}
