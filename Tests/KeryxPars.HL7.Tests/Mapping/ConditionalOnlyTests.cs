using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for ConditionalOnly - ONLY map when conditions match!
/// </summary>
public class ConditionalOnlyTests
{
    [Fact]
    public void ConditionalOnly_MatchingCondition_ShouldUseDefault()
    {
        // Arrange - Emergency patient (matches condition)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            "PV1|1|E||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use conditional default
        patient.Priority.ShouldBe(999);
        patient.RiskLevel.ShouldBe("HIGH");
        patient.RequiresReview.ShouldBeTrue();
    }

    [Fact]
    public void ConditionalOnly_NoMatchingCondition_ShouldStayAtDefault()
    {
        // Arrange - Recurring patient "R" (no matching condition for our conditional defaults)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||SMITH^BOB||19700101|M||||||||||||\r" +
            "PV1|1|R||||||||||||||||||||||||||||||||||||||||||||||||||";  // Recurring - not E, I, or O!

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert - Should stay at C# default values!
        patient.Priority.ShouldBe(0);  // int default
        patient.RiskLevel.ShouldBeNull();  // string? default
        patient.RequiresReview.ShouldBeFalse();  // bool default
    }

    [Fact]
    public void ConditionalOnly_MessageHasValue_ButNoConditionMatch_ShouldIgnoreMessage()
    {
        // Arrange - Message has PV1.25 = "555" but patient class is Recurring (not handled)
        var pv1Fields = new string[26];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "R";  // Recurring - no condition matches!
        for (int i = 3; i <= 24; i++) pv1Fields[i] = "";
        pv1Fields[25] = "555";  // Message has a value!
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||JONES^ALICE||19900101|F||||||||||||\r" +
            pv1Segment;

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert - Should IGNORE the message value "555" because no condition matched!
        // This is the KEY difference with ConditionalOnly = true
        patient.Priority.ShouldBe(0);  // NOT 555!
    }

    [Fact]
    public void ConditionalOnly_Inpatient_ShouldUseInpatientDefault()
    {
        // Arrange - Inpatient
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||GREEN^PAT||19750601|M||||||||||||\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert
        patient.Priority.ShouldBe(100);
        patient.RiskLevel.ShouldBe("MEDIUM");
        patient.RequiresReview.ShouldBeFalse();  // No condition for inpatient review
    }

    [Fact]
    public void ConditionalOnly_Outpatient_ShouldUseOutpatientDefault()
    {
        // Arrange - Outpatient
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||WHITE^SAM||19950715|M||||||||||||\r" +
            "PV1|1|O||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert
        patient.Priority.ShouldBe(10);
        patient.RiskLevel.ShouldBe("LOW");
        patient.RequiresReview.ShouldBeFalse();
    }

    [Fact]
    public void ConditionalOnly_ICULocation_ShouldTriggerReview()
    {
        // Arrange - ICU patient (matches second condition)
        var pv1Fields = new string[4];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I";  // Inpatient
        pv1Fields[3] = "ICU^201^A";  // ICU!
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||BROWN^KIM||19820922|F||||||||||||\r" +
            pv1Segment;

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert - ICU condition should trigger review
        patient.RequiresReview.ShouldBeTrue();
        patient.Priority.ShouldBe(100);  // Inpatient
        patient.RiskLevel.ShouldBe("MEDIUM");
    }

    [Fact]
    public void ConditionalOnly_RealWorld_UnknownPatientClass_SafeDefaults()
    {
        // Arrange - Real scenario: new patient class "P" (preadmit) that we don't handle yet
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN999||GOLD^MAX||19550101|M||||||||||||\r" +
            "PV1|1|P||||||||||||||||||||||||||||||||||||||||||||||||||";  // PREADMIT - not in our conditions!

        // Act
        var patient = PatientWithConditionalOnlyMapper.MapFromSpan(message.AsSpan());

        // Assert - Should safely default to C# defaults
        // This prevents mapping garbage values for unknown scenarios!
        patient.PatientClass.ShouldBe(PatientClass.P);  // Regular field still maps
        patient.Priority.ShouldBe(0);  // ConditionalOnly field stays at default
        patient.RiskLevel.ShouldBeNull();
        patient.RequiresReview.ShouldBeFalse();
    }
}
