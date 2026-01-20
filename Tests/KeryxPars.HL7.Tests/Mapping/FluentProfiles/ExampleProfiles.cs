using KeryxPars.HL7.Mapping.Fluent;
using KeryxPars.HL7.Tests.Mapping.Examples;

namespace KeryxPars.HL7.Tests.Mapping.FluentProfiles;

/// <summary>
/// Example fluent mapping profile for Patient Admission.
/// Demonstrates the fluent API as an alternative to attributes.
/// </summary>
public class PatientAdmissionFluentProfile : HL7MappingProfile<PatientAdmission>
{
    public PatientAdmissionFluentProfile()
    {
        // Specify message types
        ForMessages("ADT^A01", "ADT^A04", "ADT^A08");

        // Map simple fields
        Map(x => x.PatientId)
            .FromField("PID.3")
            .Required();

        Map(x => x.LastName)
            .FromField("PID.5.1");

        Map(x => x.FirstName)
            .FromField("PID.5.2");

        Map(x => x.MiddleName)
            .FromField("PID.5.3");

        // Map address components
        Map(x => x.StreetAddress)
            .FromField("PID.11.1");

        Map(x => x.City)
            .FromField("PID.11.3");

        Map(x => x.State)
            .FromField("PID.11.4");

        Map(x => x.ZipCode)
            .FromField("PID.11.5");
    }
}

/// <summary>
/// Example fluent profile with DateTime and Enum conversions.
/// </summary>
public class PatientAdmissionEnhancedFluentProfile : HL7MappingProfile<PatientAdmissionEnhanced>
{
    public PatientAdmissionEnhancedFluentProfile()
    {
        ForMessages("ADT^A01", "ADT^A04", "ADT^A08");

        // Simple fields
        Map(x => x.PatientId).FromField("PID.3").Required();
        Map(x => x.LastName).FromField("PID.5.1");
        Map(x => x.FirstName).FromField("PID.5.2");

        // DateTime with format
        Map(x => x.DateOfBirth)
            .FromField("PID.7")
            .AsDateTime("yyyyMMdd");

        Map(x => x.MessageDateTime)
            .FromField("MSH.7")
            .AsDateTime("yyyyMMddHHmmss");

        Map(x => x.EventDateTime)
            .FromField("EVN.2")
            .AsDateTime("yyyyMMddHHmmss");

        // Enum fields
        Map(x => x.Gender)
            .FromField("PID.8")
            .AsEnum<Gender>();

        Map(x => x.PatientClass)
            .FromField("PV1.2")
            .AsEnum<PatientClass>();

        // Address components
        Map(x => x.StreetAddress).FromField("PID.11.1");
        Map(x => x.City).FromField("PID.11.3");
        Map(x => x.State).FromField("PID.11.4");
        Map(x => x.ZipCode).FromField("PID.11.5");

        // Visit info
        Map(x => x.AssignedLocation).FromField("PV1.3.1");
        Map(x => x.VisitNumber).FromField("PV1.19");
    }
}

/// <summary>
/// Example fluent profile with nested objects.
/// </summary>
public class PatientWithNestedObjectsFluentProfile : HL7MappingProfile<PatientWithNestedObjects>
{
    public PatientWithNestedObjectsFluentProfile()
    {
        ForMessages("ADT^A01", "ADT^A04", "ADT^A08");

        // Simple fields
        Map(x => x.PatientId).FromField("PID.3").Required();
        
        Map(x => x.DateOfBirth)
            .FromField("PID.7")
            .AsDateTime("yyyyMMdd");

        Map(x => x.Gender)
            .FromField("PID.8")
            .AsEnum<Gender>();

        // Complex/nested objects
        MapComplex(x => x.Name)
            .FromField("PID.5")
            .Using(typeof(PersonNameMapper));

        MapComplex(x => x.HomeAddress)
            .FromField("PID.11")
            .Using(typeof(AddressMapper));

        MapComplex(x => x.HomePhone)
            .FromField("PID.13")
            .Using(typeof(PhoneNumberMapper));

        // Visit info
        Map(x => x.PatientClass)
            .FromField("PV1.2")
            .AsEnum<PatientClass>();

        Map(x => x.VisitNumber).FromField("PV1.19");
    }
}

/// <summary>
/// Example fluent profile with collections.
/// </summary>
public class LabResultFluentProfile : HL7MappingProfile<LabResult>
{
    public LabResultFluentProfile()
    {
        ForMessages("ORU^R01");

        // Patient info
        Map(x => x.PatientId).FromField("PID.3");
        Map(x => x.LastName).FromField("PID.5.1");
        Map(x => x.FirstName).FromField("PID.5.2");

        // Order info
        Map(x => x.PlacerOrderNumber).FromField("OBR.2");
        Map(x => x.TestName).FromField("OBR.4.1");
        
        Map(x => x.ObservationDateTime)
            .FromField("OBR.7")
            .AsDateTime("yyyyMMddHHmmss");

        // Collection mapping
        MapCollection(x => x.Observations)
            .FromSegments("OBX");
    }
}
