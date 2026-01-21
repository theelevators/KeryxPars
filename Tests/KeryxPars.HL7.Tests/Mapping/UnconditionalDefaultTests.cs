using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for UNCONDITIONAL defaults (else fallback)!
/// </summary>
public class UnconditionalDefaultTests
{
    [Fact]
    public void UnconditionalDefault_MatchingCondition_ShouldUseConditional()
    {
        // Arrange - Emergency (matches first condition)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use conditional default (999), NOT unconditional (10)
        patient.Priority.ShouldBe(999);
        patient.RiskLevel.ShouldBe("HIGH");
        patient.RequiresReview.ShouldBeTrue();
    }

    [Fact]
    public void UnconditionalDefault_NoMatchingCondition_ShouldUseUnconditional()
    {
        // Arrange - Outpatient (doesn't match any condition)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||SMITH^BOB||19700101|M||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";  // Outpatient

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use UNCONDITIONAL defaults (else fallback)!
        patient.Priority.ShouldBe(10);  // Unconditional int default
        patient.RiskLevel.ShouldBe("LOW");  // Unconditional string default
        patient.RequiresReview.ShouldBeFalse();  // Unconditional bool default
    }

    [Fact]
    public void UnconditionalDefault_UnknownValue_ShouldUseUnconditional()
    {
        // Arrange - Recurring patient (R) - doesn't match E or I
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||JONES^ALICE||19900101|F||||||||||||\r" +
            "PV1|1|R||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Falls back to unconditional defaults
        patient.Priority.ShouldBe(10);
        patient.RiskLevel.ShouldBe("LOW");
        patient.RequiresReview.ShouldBeFalse();
    }

    [Fact]
    public void UnconditionalDefault_Inpatient_ShouldUseInpatientConditional()
    {
        // Arrange - Inpatient (matches second condition)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||GREEN^PAT||19750601|M||||||||||||\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use conditional, not unconditional
        patient.Priority.ShouldBe(100);  // NOT 10!
        patient.RiskLevel.ShouldBe("MEDIUM");  // NOT "LOW"!
        patient.RequiresReview.ShouldBeFalse();  // Unconditional (no Emergency condition)
    }

    [Fact]
    public void UnconditionalDefault_EnumFallback()
    {
        // Arrange - Message with empty DG1.2 (doesn't match ICD9 condition)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||WHITE^SAM||19950715|M||||||||||||\r" +
            "DG1|1||\r" +  // Empty coding system
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use unconditional enum default (SNOMED_CT)
        patient.CodingSystem.ShouldBe(DiagnosisCodingSystem.SNOMED_CT);
    }

    [Fact]
    public void UnconditionalDefault_ICD9Upgrade_ShouldUseConditional()
    {
        // Arrange - ICD9 in message (matches condition)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||BROWN^KIM||19820922|F||||||||||||\r" +
            "DG1|1|ICD9|\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should upgrade to ICD10, NOT use SNOMED_CT unconditional
        patient.CodingSystem.ShouldBe(DiagnosisCodingSystem.ICD10);
    }

    [Fact]
    public void UnconditionalDefault_RealWorld_SafetyNet()
    {
        // Arrange - Preadmit patient (P) - not E or I
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN999||GOLD^MAX||19550101|M||||||||||||\r" +
            "PV1|1|P||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Unconditional defaults act as a safety net!
        patient.PatientClass.ShouldBe(PatientClass.P);  // Regular mapping still works
        patient.Priority.ShouldBe(10);  // Unconditional fallback
        patient.RiskLevel.ShouldBe("LOW");  // Unconditional fallback
    }

    [Fact]
    public void UnconditionalDefault_AllTypes_Working()
    {
        // Arrange - Obstetrics (B) - doesn't match any condition
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN777||SILVER^JADE||19920101|F||||||||||||\r" +
            "DG1|1|CUSTOM|\r" +
            "PV1|1|B||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithUnconditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - ALL unconditional defaults working!
        patient.Priority.ShouldBe(10);  // Int unconditional
        patient.RiskLevel.ShouldBe("LOW");  // String unconditional
        patient.RequiresReview.ShouldBeFalse();  // Bool unconditional
        patient.CodingSystem.ShouldBe(DiagnosisCodingSystem.SNOMED_CT);  // Enum unconditional
    }
}
