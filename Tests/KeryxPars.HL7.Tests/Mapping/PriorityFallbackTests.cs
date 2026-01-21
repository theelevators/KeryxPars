using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for PRIORITY-BASED FALLBACK - multiple [HL7Field] with priority!
/// </summary>
public class PriorityFallbackTests
{
    [Fact]
    public void PriorityFallback_FirstPriority_ShouldUseFirst()
    {
        // Arrange - All 3 phone numbers present
        // PID.13 = home phone, PID.14 = work phone, PID.40 = mobile phone
        var pidFields = new string[41]; // Need up to field 40
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "";
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 12; i++) pidFields[i] = "";
        pidFields[13] = "555-HOME";   // Home phone (Priority 0 - HIGHEST)
        pidFields[14] = "555-WORK";   // Work phone (Priority 1)
        for (int i = 15; i <= 39; i++) pidFields[i] = "";
        pidFields[40] = "555-MOBILE"; // Mobile phone (Priority 2 - LOWEST)
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use FIRST priority (PID.13 - home phone)
        patient.PhoneNumber.ShouldBe("555-HOME");
    }

    [Fact]
    public void PriorityFallback_SecondPriority_FirstEmpty()
    {
        // Arrange - Home phone empty, work and mobile present
        var pidFields = new string[41];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "";
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 12; i++) pidFields[i] = "";
        pidFields[13] = "";           // Home phone EMPTY
        pidFields[14] = "555-WORK";   // Work phone (should use this)
        for (int i = 15; i <= 39; i++) pidFields[i] = "";
        pidFields[40] = "555-MOBILE"; // Mobile phone
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use SECOND priority (PID.14 - work phone)
        patient.PhoneNumber.ShouldBe("555-WORK");
    }

    [Fact]
    public void PriorityFallback_ThirdPriority_FirstTwoEmpty()
    {
        // Arrange - Only mobile phone present (third priority)
        var pidFields = new string[41];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "";
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 12; i++) pidFields[i] = "";
        pidFields[13] = "";           // Home phone EMPTY
        pidFields[14] = "";           // Work phone EMPTY
        for (int i = 15; i <= 39; i++) pidFields[i] = "";
        pidFields[40] = "555-MOBILE"; // Mobile phone (should use this)
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use THIRD priority (PID.40 - mobile phone)
        patient.PhoneNumber.ShouldBe("555-MOBILE");
    }

    [Fact]
    public void PriorityFallback_AllEmpty_ShouldBeNull()
    {
        // Arrange - All phone fields empty
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||||||||||||||||||||||||||||||||||\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should be null (all empty)
        patient.PhoneNumber.ShouldBeNull();
    }

    [Fact]
    public void PriorityFallback_MultiID_FirstAvailable()
    {
        // Arrange - SSN present (first priority)
        // PID.19 = SSN, PID.20 = DL, PID.21.1 = Passport, PID.2 = Patient ID
        var pidFields = new string[22];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "PAT123";      // Patient ID (Priority 3 - lowest)
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "123-45-6789";  // SSN (Priority 0 - HIGHEST, should use this)
        pidFields[20] = "DL12345";       // Driver's License (Priority 1)
        pidFields[21] = "PASSPORT123";   // Passport (Priority 2)
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use SSN (first priority)
        patient.PatientIdentifier.ShouldBe("123-45-6789");
    }

    [Fact]
    public void PriorityFallback_MultiID_SecondAvailable()
    {
        // Arrange - SSN empty, driver's license present
        var pidFields = new string[22];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "PAT123";
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "";              // SSN EMPTY
        pidFields[20] = "DL12345";       // Driver's License (should use this)
        pidFields[21] = "PASSPORT123";
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use driver's license (second priority)
        patient.PatientIdentifier.ShouldBe("DL12345");
    }

    [Fact]
    public void PriorityFallback_MultiID_FourthFallback()
    {
        // Arrange - Only patient ID present (last/fourth priority)
        var pidFields = new string[22];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "PAT123";      // Patient ID (Priority 3 - should use this)
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "";           // SSN EMPTY
        pidFields[20] = "";           // DL EMPTY
        pidFields[21] = "";           // Passport EMPTY
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use patient ID (fourth/last priority)
        patient.PatientIdentifier.ShouldBe("PAT123");
    }

    [Fact]
    public void PriorityFallback_RealWorld_MixedScenario()
    {
        // Arrange - Mixed: phone uses 2nd priority, ID uses 1st, doctor uses 3rd
        // Phone: PID.13 empty, PID.14 has value, PID.40 has value ? should use PID.14
        // ID: PID.19 has value ? should use PID.19
        // Doctor: PV1.7 empty, PV1.9 has value, PV1.17 empty ? should use PV1.9
        var pidFields = new string[41];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "PAT456";
        pidFields[3] = "MRN456";
        pidFields[4] = "";
        pidFields[5] = "SMITH^JANE";
        pidFields[6] = "";
        pidFields[7] = "19900101";
        pidFields[8] = "F";
        for (int i = 9; i <= 12; i++) pidFields[i] = "";
        pidFields[13] = "";              // Home phone EMPTY
        pidFields[14] = "555-WORK";      // Work phone (should use for phone)
        for (int i = 15; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "123-45-6789";   // SSN (should use for ID)
        for (int i = 20; i <= 39; i++) pidFields[i] = "";
        pidFields[40] = "555-MOBILE";    // Mobile
        
        // PV1: Need PV1.7 empty, PV1.9 with value, PV1.17 empty
        var pv1Fields = new string[18];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I";
        for (int i = 3; i <= 6; i++) pv1Fields[i] = "";
        pv1Fields[7] = "";                    // Attending doctor EMPTY
        pv1Fields[8] = "";
        pv1Fields[9] = "CONSULT^DR^C";       // Consulting doctor (should use this)
        for (int i = 10; i <= 16; i++) pv1Fields[i] = "";
        pv1Fields[17] = "";                   // Admitting doctor EMPTY
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            string.Join("|", pv1Fields);

        // Act
        var patient = PatientWithPriorityFallbackMapper.MapFromSpan(message.AsSpan());

        // Assert - All priority fallbacks working!
        patient.PhoneNumber.ShouldBe("555-WORK");        // 2nd priority (home empty)
        patient.PatientIdentifier.ShouldBe("123-45-6789"); // 1st priority (SSN)
        patient.PrimaryDoctor.ShouldBe("CONSULT^DR^C");   // 2nd priority (attending empty, consulting present)
    }
}
