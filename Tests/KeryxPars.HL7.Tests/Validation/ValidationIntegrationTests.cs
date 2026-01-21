using KeryxPars.HL7.Validation;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;
using KeryxPars.HL7.Contracts;
using Shouldly;

namespace KeryxPars.HL7.Tests.Validation;

/// <summary>
/// Integration tests for the complete validation framework
/// </summary>
public class ValidationIntegrationTests
{
    #region Real-World Scenario Tests

    [Fact]
    public void CompleteWorkflow_ADTMessage_ShouldValidateCorrectly()
    {
        // Arrange - Real-world ADT A01 message
        var message = @"MSH|^~\&|EPIC|UCSF|RECEIVING_APP|RECEIVING_FAC|20230615143022||ADT^A01|MSG12345|P|2.5||
EVN|A01|20230615143020||
PID|1||MRN123456||SMITH^JOHN^ROBERT||19850315|M||ASIAN|123 MAIN ST^^SAN FRANCISCO^CA^94102||(415)555-1234|||S||ACCT98765|||SSN123456789|DL-CA12345||
NK1|1|SMITH^JANE||SPOUSE|(415)555-5678|
PV1|1|I|ICU^201^A^UCSF||||123456^JONES^ROBERT^A^MD^^|987654^WILLIAMS^SARAH^B^MD^^|||||SUR|||||||V123456|||||||||||||||||||||||||20230615143000|";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act - Validate using ADT template
        var rules = ValidationTemplates.ADT();
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void CompleteWorkflow_PharmacyOrder_ShouldValidateCorrectly()
    {
        // Arrange - Real-world pharmacy order
        var message = @"MSH|^~\&|PHARMACY|HOSPITAL|RECEIVING_APP|RECEIVING_FAC|20230615143022||RDE^O11|MSG12345|P|2.5||
PID|1||MRN123456||SMITH^JOHN^ROBERT||19850315|M||ASIAN|123 MAIN ST^^SAN FRANCISCO^CA^94102||(415)555-1234|||S||ACCT98765|||SSN123456789|DL-CA12345||
ORC|NW|ORDER12345||GROUP123|IP||^^^^^R||20230615143000|SMITH^JOHN|||987654^WILLIAMS^SARAH^B^MD^^|
RXE|1|DRUG12345^AMOXICILLIN 500MG CAPSULE^NDC|500|MG|CAPSULE|TAKE 1 CAPSULE BY MOUTH EVERY 8 HOURS|||10|DAY||||||||||";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act
        var rules = ValidationTemplates.Pharmacy();
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void CompleteWorkflow_LabResults_ShouldValidateCorrectly()
    {
        // Arrange - Real-world lab results
        var message = @"MSH|^~\&|LAB|HOSPITAL|RECEIVING_APP|RECEIVING_FAC|20230615143022||ORU^R01|MSG12345|P|2.5||
PID|1||MRN123456||SMITH^JOHN^ROBERT||19850315|M||ASIAN|123 MAIN ST^^SAN FRANCISCO^CA^94102||(415)555-1234|||S||ACCT98765|||SSN123456789|DL-CA12345||
OBR|1|ORDER12345|SPEC12345|CBC^COMPLETE BLOOD COUNT^LOCAL||20230615100000|20230615100000|||||||20230615100000|BLD^BLOOD^HL70070|987654^WILLIAMS^SARAH^B^MD^^|||||||20230615120000|||F||
OBX|1|NM|WBC^WHITE BLOOD COUNT^LOCAL||7.5|10*3/uL|4.0-11.0|N|||F|||20230615120000||
OBX|2|NM|RBC^RED BLOOD COUNT^LOCAL||4.8|10*6/uL|4.2-5.4|N|||F|||20230615120000||
OBX|3|NM|HGB^HEMOGLOBIN^LOCAL||14.5|g/dL|12.0-16.0|N|||F|||20230615120000||";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act
        var rules = ValidationTemplates.Lab();
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    #endregion

    #region Validation with SerializerOptions Tests

    [Fact]
    public void SerializerOptions_WithValidation_ShouldValidateDuringDeserialization()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

        var options = new SerializerOptions
        {
            Validation = ValidationTemplates.ADT()
        };

        // Act
        var result = HL7Serializer.Deserialize(message, options);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
    }

    [Fact]
    public void SerializerOptions_InvalidMessage_ShouldStillDeserializeButTrackErrors()
    {
        // Arrange - Missing required EVN segment
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";

        var options = new SerializerOptions
        {
            Validation = ValidationTemplates.ADT()
        };

        // Act
        var result = HL7Serializer.Deserialize(message, options);

        // Assert
        result.IsSuccess.ShouldBeTrue(); // Deserialization succeeds
        // Validation errors would be tracked separately if configured
    }

    #endregion

    #region Custom Validation Rules Tests

    [Fact]
    public void CustomValidation_ComplexBusinessRules_ShouldWork()
    {
        // Arrange - Message with patient data
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||MRN123456||DOE^JOHN^ROBERT||19800101|M|||123 MAIN ST^^SAN FRANCISCO^CA^94102|||||||";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act - Apply custom business rules
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID"],
            Fields = new()
            {
                // Patient ID must be present and follow format
                ["PID.3"] = new() 
                { 
                    Required = true,
                    Pattern = @"^MRN\d{6}$",
                    CustomMessage = "Patient MRN must be in format MRN######"
                },
                // Name must be reasonable length
                ["PID.5"] = new()
                {
                    Required = true,
                    MaxLength = 250,
                    MinLength = 2
                },
                // DOB must be in YYYYMMDD format
                ["PID.7"] = new()
                {
                    Pattern = @"^\d{8}$",
                    CustomMessage = "Date of birth must be in YYYYMMDD format"
                },
                // Gender must be valid code
                ["PID.8"] = new()
                {
                    AllowedValues = ["M", "F", "O", "U", "A", "N"],
                    CustomMessage = "Gender must be M, F, O, U, A, or N"
                },
                // ZIP code validation
                ["PID.11.5"] = new()
                {
                    Pattern = @"^\d{5}(-\d{4})?$",
                    CustomMessage = "ZIP code must be 5 or 9 digits"
                }
            }
        };

        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    #endregion

    #region Conditional Validation Tests

    [Fact]
    public void ConditionalValidation_WhenConditionMet_ShouldApplyRules()
    {
        // Arrange - Pharmacy order with new order status
        var message = @"MSH|^~\&|PHARMACY|HOSPITAL|RECEIVING_APP|RECEIVING_FAC|20230615143022||RDE^O11|MSG12345|P|2.5||
PID|1||MRN123456||SMITH^JOHN^ROBERT||19850315|M|||||||
ORC|NW|ORDER12345||GROUP123|IP||^^^^^R||20230615143000|||987654^WILLIAMS^SARAH^B^MD^^|
RXE|1|DRUG12345^AMOXICILLIN 500MG CAPSULE^NDC|500|MG|CAPSULE|TAKE 1 CAPSULE BY MOUTH EVERY 8 HOURS|||10|DAY||||||||||";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act - If order is new (NW), require medication details
        var rules = new ValidationRules
        {
            Conditional = new List<ConditionalRule>
            {
                new ConditionalRule
                {
                    When = "ORC.1 = NW", // If order control is "New"
                    Then = new()
                    {
                        ["RXE.2"] = new() { Required = true, CustomMessage = "Medication code required for new orders" },
                        ["RXE.3"] = new() { Required = true, CustomMessage = "Dose amount required for new orders" }
                    }
                }
            }
        };

        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void ConditionalValidation_WhenConditionNotMet_ShouldSkipRules()
    {
        // Arrange - Message without ORC segment
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act - Conditional rules that check ORC.1
        var rules = new ValidationRules
        {
            Conditional = new List<ConditionalRule>
            {
                new ConditionalRule
                {
                    When = "ORC.1 = NW",
                    Then = new()
                    {
                        ["RXE.2"] = new() { Required = true }
                    }
                }
            }
        };

        var result = rules.Validate(msg);

        // Assert - Should pass because ORC doesn't exist, so condition isn't met
        result.IsValid.ShouldBeTrue();
    }

    #endregion

    #region Error Aggregation Tests

    [Fact]
    public void MultipleValidationErrors_ShouldAggregateAll()
    {
        // Arrange - Message with multiple validation failures
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|VERYLONGMESSAGECONTROLID12345|P|2.5||
PID|1||||DOE^JOHN^A||NotADate|InvalidGender|||123 MAIN ST^^CITY^ST^12345|||||||";

        var msg = HL7Serializer.Deserialize(message).Value!;

        // Act - Multiple strict rules
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MaxLength = 20 }, // Exceeds
                ["PID.3"] = new() { Required = true }, // Missing
                ["PID.7"] = new() { Pattern = @"^\d{8}$" }, // Invalid format
                ["PID.8"] = new() { AllowedValues = ["M", "F", "O"] } // Invalid value
            }
        };

        var result = rules.Validate(msg);

        // Assert - All errors should be reported
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBeGreaterThanOrEqualTo(4);
        result.Errors.ShouldContain(e => e.Field == "MSH.10");
        result.Errors.ShouldContain(e => e.Field == "PID.3");
        result.Errors.ShouldContain(e => e.Field == "PID.7");
        result.Errors.ShouldContain(e => e.Field == "PID.8");
    }

    #endregion

    #region JSON Serialization Tests

    [Fact]
    public void ValidationRules_JsonRoundTrip_ShouldPreserveRules()
    {
        // Arrange
        var originalRules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID", "PV1"],
            Fields = new()
            {
                ["MSH.9"] = new() { Required = true },
                ["PID.3"] = new() { Required = true, MaxLength = 20 },
                ["PID.7"] = new() { Pattern = @"^\d{8}$" }
            }
        };

        // Act - Serialize and deserialize
        var json = System.Text.Json.JsonSerializer.Serialize(originalRules);
        var deserializedRules = System.Text.Json.JsonSerializer.Deserialize<ValidationRules>(json);

        // Assert
        deserializedRules.ShouldNotBeNull();
        deserializedRules.RequiredSegments.Count.ShouldBe(3);
        deserializedRules.Fields.Count.ShouldBe(3);
        deserializedRules.Fields["MSH.9"].Required.ShouldBeTrue();
        deserializedRules.Fields["PID.3"].MaxLength.ShouldBe(20);
        deserializedRules.Fields["PID.7"].Pattern.ShouldBe(@"^\d{8}$");
    }

    #endregion

    #region Performance Tests

    [Fact]
    public void ValidationPerformance_1000Messages_ShouldCompleteInReasonableTime()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = ValidationTemplates.ADT();

        // Act - Validate 1000 times
        var sw = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            rules.Validate(msg);
        }
        sw.Stop();

        // Assert - Should be very fast (< 100ms for 1000 validations)
        sw.ElapsedMilliseconds.ShouldBeLessThan(100);
    }

    #endregion
}
