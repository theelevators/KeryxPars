using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for TYPED CONDITIONAL DEFAULTS - enums and primitives!
/// </summary>
public class TypedConditionalDefaultTests
{
    [Fact]
    public void TypedDefault_Enum_ICD9ToICD10()
    {
        // Arrange - Message with ICD9 coding
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "DG1|1|ICD9|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to ICD10 ENUM!
        patient.CodingSystemEnum.ShouldBe(CodingSystem.ICD10);
    }

    [Fact]
    public void TypedDefault_Enum_PaymentMethod_SelfPay()
    {
        // Arrange - No insurance segment
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to SELF_PAY enum!
        patient.PaymentType.ShouldBe(PaymentMethod.SELF_PAY);
    }

    [Fact]
    public void TypedDefault_Int_EmergencyPriority()
    {
        // Arrange - Emergency patient
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JANE||19850315|F||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to 999 (CRITICAL)!
        patient.Priority.ShouldBe(999);
        patient.PatientClass.ShouldBe(PatientClass.E);
    }

    [Fact]
    public void TypedDefault_Int_ICUPriority()
    {
        // Arrange - ICU patient (not emergency)
        var pv1Fields = new string[4];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I"; // Inpatient (not emergency)
        pv1Fields[3] = "ICU^201^A";
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||SMITH^BOB||19700101|M||||||||||||\r" +
            pv1Segment;

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to 100 (HIGH) - ICU has higher priority than regular inpatient!
        patient.Priority.ShouldBe(100);
        patient.Location.ShouldBe("ICU");
    }

    [Fact]
    public void TypedDefault_EnumWithValues_PriorityLevel()
    {
        // Arrange - Emergency
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^ALICE||19900101|F||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Enum with int values!
        patient.PriorityEnum.ShouldBe(PriorityLevel.CRITICAL);
        ((int)patient.PriorityEnum).ShouldBe(999);
    }

    [Fact]
    public void TypedDefault_Bool_IsCritical_Emergency()
    {
        // Arrange - Emergency patient
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^MARY||19920725|F||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Boolean true!
        patient.IsCritical.ShouldBeTrue();
    }

    [Fact]
    public void TypedDefault_Bool_NotCritical_Outpatient()
    {
        // Arrange - Outpatient
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||JONES^TOM||19850101|M||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Boolean false!
        patient.IsCritical.ShouldBeFalse();
    }

    [Fact]
    public void TypedDefault_Bool_RequiresAuth_NoInsurance()
    {
        // Arrange - No insurance
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||BROWN^LISA||19880315|F||||||||||||\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Needs auth if no insurance!
        patient.RequiresAuthorization.ShouldBeTrue();
    }

    [Fact]
    public void TypedDefault_Decimal_ChargeAmount_NoInsurance()
    {
        // Arrange - No insurance = free
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||GREEN^PAT||19750601|M||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Decimal 0.0!
        patient.ChargeAmount.ShouldBe(0.0m);
    }

    [Fact]
    public void TypedDefault_Decimal_ChargeAmount_Emergency()
    {
        // Arrange - Emergency visit
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||WHITE^SAM||19950715|M||||||||||||\r" +
            "IN1|1|PLAN1|COMP1|\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Emergency copay $50!
        patient.ChargeAmount.ShouldBe(50.00m);
    }

    [Fact]
    public void TypedDefault_Decimal_Discount_Medicaid()
    {
        // Arrange - Medicaid
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||BLACK^KIM||19820922|F||||||||||||\r" +
            "IN1|1||MEDICAID|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - 100% discount!
        patient.DiscountPercentage.ShouldBe(100.0m);
    }

    [Fact]
    public void TypedDefault_Decimal_Discount_Medicare()
    {
        // Arrange - Medicare
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||GRAY^LEE||19450505|M||||||||||||\r" +
            "IN1|1||MEDICARE|\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - 80% discount!
        patient.DiscountPercentage.ShouldBe(80.0m);
    }

    [Fact]
    public void TypedDefault_AllTypes_RealWorldScenario()
    {
        // Arrange - Emergency ICU patient with Medicare
        var pv1Fields = new string[4];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "E"; // Emergency
        pv1Fields[3] = "ICU^301^A";
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN999||GOLD^MAX||19550101|M||||||||||||\r" +
            "DG1|1|ICD9|\r" +
            "IN1|1||MEDICARE|\r" +
            pv1Segment;

        // Act
        var patient = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - ALL typed defaults working together!
        patient.CodingSystemEnum.ShouldBe(CodingSystem.ICD10); // Enum
        patient.Priority.ShouldBe(999); // Int (emergency has highest priority)
        patient.PriorityEnum.ShouldBe(PriorityLevel.CRITICAL); // Enum with values
        patient.IsCritical.ShouldBeTrue(); // Bool
        patient.RequiresAuthorization.ShouldBeFalse(); // Bool (emergency doesn't need auth)
        patient.ChargeAmount.ShouldBe(50.00m); // Decimal
        patient.DiscountPercentage.ShouldBe(80.0m); // Decimal (Medicare)
    }

    [Fact]
    public void Performance_TypedConditionalDefaults_ShouldBeFast()
    {
        // Arrange
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "DG1|1|ICD9|\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        var iterations = 1000;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Act - Map 1000 messages with typed conditional defaults
        for (int i = 0; i < iterations; i++)
        {
            _ = PatientWithTypedConditionalDefaultsMapper.MapFromSpan(message.AsSpan());
        }

        sw.Stop();

        // Assert - Should still be blazing fast with enums and primitives!
        sw.ElapsedMilliseconds.ShouldBeLessThan(50);

        Console.WriteLine($"Typed Conditional Defaults: {iterations} messages in {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Average: {sw.Elapsed.TotalMilliseconds / iterations:F4}ms per message");
        Console.WriteLine($"Throughput: {iterations / sw.Elapsed.TotalSeconds:N0} messages/second");
    }
}
