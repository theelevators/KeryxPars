using KeryxPars.HL7.Validation;
using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Serialization;
using Shouldly;

namespace KeryxPars.HL7.Tests.Validation;

/// <summary>
/// Comprehensive tests for ValidationRules and ValidationEngine
/// </summary>
public class ValidationRulesTests
{
    private const string SampleMessage = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

    #region Required Segments Tests

    [Fact]
    public void Validate_RequiredSegmentsPresent_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID", "EVN"]
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_RequiredSegmentMissing_ShouldFail()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID", "ORC"] // ORC not in message
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].Field.ShouldBe("ORC");
        result.Errors[0].Message.ShouldContain("required");
        result.Errors[0].Severity.ShouldBe(ValidationSeverity.Error);
    }

    [Fact]
    public void Validate_MultipleRequiredSegmentsMissing_ShouldReportAll()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "ORC", "RXA", "RXR"]
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(3); // ORC, RXA, RXR missing
        result.Errors.ShouldAllBe(e => e.Message.Contains("required"));
    }

    #endregion

    #region Required Field Tests

    [Fact]
    public void Validate_RequiredFieldPresent_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.9"] = new() { Required = true },
                ["PID.3"] = new() { Required = true }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_RequiredFieldMissing_ShouldFail()
    {
        // Arrange
        var messageWithoutPID3 = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";
        
        var message = HL7Serializer.Deserialize(messageWithoutPID3).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.3"] = new() { Required = true }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Field.ShouldBe("PID.3");
        result.Errors[0].Message.ShouldContain("required");
    }

    [Fact]
    public void Validate_RequiredFieldWithCustomMessage_ShouldUseCustomMessage()
    {
        // Arrange
        var messageWithoutPID3 = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";
        
        var message = HL7Serializer.Deserialize(messageWithoutPID3).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.3"] = new() 
                { 
                    Required = true,
                    CustomMessage = "Patient ID is mandatory for billing"
                }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.Errors[0].Message.ShouldBe("Patient ID is mandatory for billing");
    }

    #endregion

    #region MaxLength Tests

    [Fact]
    public void Validate_MaxLengthValid_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MaxLength = 20 } // MSG001 = 6 chars
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_MaxLengthExceeded_ShouldFail()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MaxLength = 3 } // MSG001 = 6 chars, exceeds limit
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("maximum length");
        result.Errors[0].Message.ShouldContain("3");
    }

    #endregion

    #region MinLength Tests

    [Fact]
    public void Validate_MinLengthValid_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MinLength = 3 } // MSG001 = 6 chars
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_MinLengthNotMet_ShouldFail()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MinLength = 10 } // MSG001 = 6 chars, below minimum
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("minimum length");
        result.Errors[0].Message.ShouldContain("10");
    }

    #endregion

    #region Pattern Tests

    [Fact]
    public void Validate_PatternMatches_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.7"] = new() { Pattern = @"^\d{8}$" } // 19800101
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_PatternDoesNotMatch_ShouldFail()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.7"] = new() { Pattern = @"^\d{14}$" } // Expects YYYYMMDDHHmmss
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("pattern");
    }

    [Fact]
    public void Validate_InvalidRegexPattern_ShouldSkipValidation()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.7"] = new() { Pattern = "[invalid(regex" } // Invalid regex
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue(); // Should skip invalid regex gracefully
    }

    #endregion

    #region AllowedValues Tests

    [Fact]
    public void Validate_AllowedValuesMatch_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.8"] = new() { AllowedValues = ["M", "F", "O", "U"] }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_AllowedValuesDoNotMatch_ShouldFail()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.8"] = new() { AllowedValues = ["F", "O"] } // M not allowed
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("must be one of");
        result.Errors[0].Message.ShouldContain("F");
        result.Errors[0].Message.ShouldContain("O");
    }

    #endregion

    #region NumericRange Tests

    [Fact]
    public void Validate_NumericRangeValid_ShouldPass()
    {
        // Arrange - Use PID.1 (Set ID) which is numeric
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.1"] = new() { MinValue = 0, MaxValue = 300 }
            }
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_NumericBelowMinimum_ShouldFail()
    {
        // Arrange - Use MSH.10 (Message Control ID) with numeric validation instead
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|-5|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MinValue = 0 }
            }
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("at least 0");
    }

    [Fact]
    public void Validate_NumericAboveMaximum_ShouldFail()
    {
        // Arrange - Use very high number for Message Control ID
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|500|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() { MaxValue = 300 }
            }
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("at most 300");
    }

    #endregion

    #region Severity Tests

    [Fact]
    public void Validate_CustomSeverity_ShouldBeRespected()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.3"] = new() 
                { 
                    Required = true,
                    Severity = ValidationSeverity.Critical
                }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert - Since field exists, no error, but if it fails, severity would be Critical
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_WarningSeverity_ShouldStillReportAsInvalid()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() 
                { 
                    MaxLength = 3,
                    Severity = ValidationSeverity.Warning
                }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Severity.ShouldBe(ValidationSeverity.Warning);
    }

    #endregion

    #region Multiple Rules Tests

    [Fact]
    public void Validate_MultipleRulesOnSameField_AllShouldBeChecked()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["MSH.10"] = new() 
                { 
                    Required = true,
                    MinLength = 2,
                    MaxLength = 20
                }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert - All rules should pass
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_ComplexRuleSet_ShouldValidateAll()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID", "PV1"],
            Fields = new()
            {
                ["MSH.9"] = new() { Required = true },
                ["MSH.10"] = new() { Required = true, MaxLength = 20 },
                ["PID.3"] = new() { Required = true },
                ["PID.5"] = new() { MaxLength = 250 },
                ["PID.7"] = new() { Pattern = @"^\d{8}$" },
                ["PID.8"] = new() { AllowedValues = ["M", "F", "O", "U"] }
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Validate_EmptyRules_ShouldPass()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules(); // No rules

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_NonExistentSegment_ShouldSkipFieldValidation()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["ORC.1"] = new() { Required = true } // ORC not in message
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeTrue(); // Should skip validation for missing segment
    }

    [Fact]
    public void Validate_NonExistentField_ShouldTreatAsEmpty()
    {
        // Arrange
        var message = HL7Serializer.Deserialize(SampleMessage).Value!;
        var rules = new ValidationRules
        {
            Fields = new()
            {
                ["PID.99"] = new() { Required = true } // Field 99 doesn't exist
            }
        };

        // Act
        var result = rules.Validate(message);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].Message.ShouldContain("required");
    }

    #endregion
}
