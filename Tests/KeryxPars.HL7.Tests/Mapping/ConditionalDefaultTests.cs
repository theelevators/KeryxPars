using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for CONDITIONAL DEFAULT VALUES - the revolutionary feature!
/// </summary>
public class ConditionalDefaultTests
{
    [Fact]
    public void ConditionalDefault_ICD9ToICD10_ShouldApplyDefault()
    {
        // Arrange - Message with ICD9 coding (old format)
        var dg1Fields = new string[3];
        dg1Fields[0] = "DG1";
        dg1Fields[1] = "1";
        dg1Fields[2] = "ICD9"; // Old coding system
        
        var dg1Segment = string.Join("|", dg1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            dg1Segment + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to ICD10!
        patient.CodingSystem.ShouldBe("ICD10");
    }

    [Fact]
    public void ConditionalDefault_EmptyCodingSystem_ShouldApplyDefault()
    {
        // Arrange - Message with no coding system
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "DG1|1||\r" + // Empty DG1.2
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to ICD10 for empty!
        patient.CodingSystem.ShouldBe("ICD10");
    }

    [Fact]
    public void ConditionalDefault_SelfPay_ShouldApplyWhenNoInsurance()
    {
        // Arrange - Message WITHOUT insurance segment
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";
        // Note: NO IN1 segment!

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to SELF-PAY!
        patient.PaymentType.ShouldBe("SELF-PAY");
    }

    [Fact]
    public void ConditionalDefault_MultipleConditions_ShouldUseFirstMatch()
    {
        // Arrange - CCU location (should match first condition)
        var pv1Fields = new string[11];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I"; // Inpatient
        pv1Fields[3] = "CCU^101^A"; // CCU location
        for (int i = 4; i <= 10; i++) pv1Fields[i] = "";
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            pv1Segment;

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should match CCU condition and default to CARDIOLOGY!
        patient.Location.ShouldBe("CCU");
        patient.HospitalService.ShouldBe("CARDIOLOGY");
    }

    [Fact]
    public void ConditionalDefault_PriorityOrdering_ShouldRespectPriority()
    {
        // Arrange - Emergency patient (lower priority than CCU)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||"; // Emergency

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should default to EMERGENCY (priority 3)
        patient.PatientClass.ShouldBe(PatientClass.E);
        patient.HospitalService.ShouldBe("EMERGENCY");
    }

    [Fact]
    public void ConditionalDefault_AdmissionType_Emergency()
    {
        // Arrange - Emergency visit
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JANE||19850315|F||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||"; // Emergency

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.E);
        patient.AdmissionType.ShouldBe("EMERGENCY");
    }

    [Fact]
    public void ConditionalDefault_AdmissionType_Elective()
    {
        // Arrange - Inpatient (elective)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||SMITH^BOB||19700101|M||||||||||||\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||"; // Inpatient

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.I);
        patient.AdmissionType.ShouldBe("ELECTIVE");
    }

    [Fact]
    public void ConditionalDefault_DischargeDisposition_Outpatient()
    {
        // Arrange - Outpatient visit
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||JONES^ALICE||19900101|F||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||"; // Outpatient

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.O);
        patient.DischargeDisposition.ShouldBe("HOME");
    }

    [Fact]
    public void ConditionalDefault_CombinedWithConditionalMapping_ShouldWork()
    {
        // Arrange - Complex scenario with both conditional mapping AND defaults
        var pv1Fields = new string[37];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I"; // Inpatient
        pv1Fields[3] = "CCU^201^A";
        for (int i = 4; i <= 36; i++) pv1Fields[i] = "";
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN555||DOE^MARY||19920725|F||||||||||||\r" +
            "DG1|1|ICD9|\r" + // ICD9 coding (will default to ICD10)
            pv1Segment;

        // Act
        var patient = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());

        // Assert - Multiple features working together!
        patient.CodingSystem.ShouldBe("ICD10"); // Conditional default
        patient.HospitalService.ShouldBe("CARDIOLOGY"); // Conditional default
        patient.AdmissionType.ShouldBe("ELECTIVE"); // Conditional default
        patient.Location.ShouldBe("CCU"); // Regular mapping
    }

    [Fact]
    public void Performance_ConditionalDefaults_ShouldBeFast()
    {
        // Arrange
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "DG1|1|ICD9|\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        var iterations = 1000;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Act - Map 1000 messages with conditional defaults
        for (int i = 0; i < iterations; i++)
        {
            _ = PatientWithConditionalDefaultsMapper.MapFromSpan(message.AsSpan());
        }

        sw.Stop();

        // Assert - Should still be blazing fast!
        sw.ElapsedMilliseconds.ShouldBeLessThan(50);

        Console.WriteLine($"Conditional Defaults: {iterations} messages in {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Average: {sw.Elapsed.TotalMilliseconds / iterations:F4}ms per message");
        Console.WriteLine($"Throughput: {iterations / sw.Elapsed.TotalSeconds:N0} messages/second");
    }
}
