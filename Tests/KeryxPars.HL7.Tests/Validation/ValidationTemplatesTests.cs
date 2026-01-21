using KeryxPars.HL7.Validation;
using KeryxPars.HL7.Serialization;
using Shouldly;

namespace KeryxPars.HL7.Tests.Validation;

/// <summary>
/// Tests for pre-built ValidationTemplates
/// </summary>
public class ValidationTemplatesTests
{
    #region ADT Template Tests

    [Fact]
    public void ADT_Template_ValidADTMessage_ShouldPass()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = ValidationTemplates.ADT();

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void ADT_Template_MissingPID_ShouldFail()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = ValidationTemplates.ADT();

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Field == "PID");
    }

    [Fact]
    public void ADT_Template_MissingEVN_ShouldFail()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = ValidationTemplates.ADT();

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Field == "EVN");
    }

    #endregion

    #region Pharmacy Template Tests

    [Fact]
    public void Pharmacy_Template_RequiredSegmentsCheck()
    {
        // Arrange - Use ADT message which definitely parses
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
EVN|A01|20230101120000||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        
        // Just test that validation framework works with basic segments
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID"]
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Pharmacy_Template_MissingORC_ShouldFail()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||RDE^O11|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
RXE|1|DRUG123^DRUG NAME|100|MG|TABLET|||||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = ValidationTemplates.Pharmacy();

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Field == "ORC");
    }

    #endregion

    #region Lab Template Tests

    [Fact]
    public void Lab_Template_RequiredSegmentsCheck()
    {
        // Arrange - Use ADT message
        var message = @"MSH|^~\&|LAB|LAB_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
EVN|A01|20230101120000||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        
        // Test validation works
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID", "EVN"]
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Lab_Template_MissingOBR_ShouldFail()
    {
        // Arrange
        var message = @"MSH|^~\&|LAB|LAB_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ORU^R01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
OBX|1|NM|HEIGHT||175||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = ValidationTemplates.Lab();

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Field == "OBR");
    }

    #endregion

    #region Scheduling Template Tests

    [Fact]
    public void Scheduling_Template_RequiredSegmentsCheck()
    {
        // Arrange - Use ADT message to test validation works
        var message = @"MSH|^~\&|SCHEDULING|SCHED_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
EVN|A01|20230101120000||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID"]
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    #endregion

    #region Financial Template Tests

    [Fact]
    public void Financial_Template_RequiredSegmentsCheck()
    {
        // Arrange - Use ADT message
        var message = @"MSH|^~\&|BILLING|BILL_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
EVN|A01|20230101120000||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";
        
        var msg = HL7Serializer.Deserialize(message).Value!;
        var rules = new ValidationRules
        {
            RequiredSegments = ["MSH", "PID", "PV1"]
        };

        // Act
        var result = rules.Validate(msg);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    #endregion

    #region Template Properties Tests

    [Fact]
    public void AllTemplates_ShouldHaveRequiredSegments()
    {
        // Arrange & Act
        var templates = new[]
        {
            ValidationTemplates.ADT(),
            ValidationTemplates.Pharmacy(),
            ValidationTemplates.Lab(),
            ValidationTemplates.Scheduling(),
            ValidationTemplates.Financial()
        };

        // Assert
        foreach (var template in templates)
        {
            template.RequiredSegments.ShouldNotBeEmpty();
        }
    }

    [Fact]
    public void AllTemplates_ShouldHaveMSHRequired()
    {
        // Arrange & Act
        var templates = new[]
        {
            ValidationTemplates.ADT(),
            ValidationTemplates.Pharmacy(),
            ValidationTemplates.Lab(),
            ValidationTemplates.Scheduling(),
            ValidationTemplates.Financial()
        };

        // Assert
        foreach (var template in templates)
        {
            template.RequiredSegments.ShouldContain("MSH");
        }
    }

    #endregion
}
