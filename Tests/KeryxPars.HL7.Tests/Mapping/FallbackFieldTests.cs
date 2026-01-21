using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for FALLBACK FIELD mapping - try primary, fallback if empty!
/// </summary>
public class FallbackFieldTests
{
    [Fact]
    public void FallbackField_PrimaryHasValue_ShouldUsePrimary()
    {
        // Arrange - Both home and work phone present, should use home (primary)
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M|||123 MAIN ST^^SF^CA^94102||555-1234|555-5678|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use PRIMARY field (PID.13), NOT fallback
        patient.PhoneNumber.ShouldBe("555-1234");
    }

    [Fact]
    public void FallbackField_PrimaryEmpty_ShouldUseFallback()
    {
        // Arrange - Home phone empty, work phone present
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M|||123 MAIN ST^^SF^CA^94102|||555-5678|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use FALLBACK field (PID.14)
        patient.PhoneNumber.ShouldBe("555-5678");
    }

    [Fact]
    public void FallbackField_BothEmpty_ShouldBeNull()
    {
        // Arrange - Both phone fields empty
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M|||123 MAIN ST^^SF^CA^94102||||\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should be null (both empty)
        patient.PhoneNumber.ShouldBeNull();
    }

    [Fact]
    public void FallbackField_ComponentLevel_Primary()
    {
        // Arrange - Primary email component present
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M|||123 MAIN ST^^SF^CA^94102||555-1234^PRN^PH^^^john@email.com|555-5678^WPN^PH^^^work@email.com|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use primary email component
        patient.Email.ShouldBe("john@email.com");
    }

    [Fact]
    public void FallbackField_ComponentLevel_Fallback()
    {
        // Arrange - Primary email component empty, fallback present
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M|||123 MAIN ST^^SF^CA^94102||555-1234^PRN^PH|555-5678^WPN^PH^^^work@email.com|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use fallback email component
        patient.Email.ShouldBe("work@email.com");
    }

    [Fact]
    public void FallbackField_CrossSegment_PrimaryPresent()
    {
        // Arrange - NK1 segment present with name
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M|||||||||||SPOUSE^JANE|\r" +
            "NK1|1|CONTACT^MARY^L|SISTER|123 OAK ST^^SF^CA^94103|555-9999|\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use NK1.2 (primary), not PID.25
        patient.EmergencyContactName.ShouldBe("CONTACT^MARY^L");
    }

    [Fact]
    public void FallbackField_CrossSegment_FallbackToOtherSegment()
    {
        // Arrange - No NK1 segment, use PID.25 as fallback
        var pidFields = new string[26];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "";
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 24; i++) pidFields[i] = "";
        pidFields[25] = "SPOUSE^JANE";  // Emergency contact fallback
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use PID.25 (fallback) since NK1 is missing
        patient.EmergencyContactName.ShouldBe("SPOUSE^JANE");
    }

    [Fact]
    public void FallbackField_SSN_Primary()
    {
        // Arrange - SSN present in PID.19
        var pidFields = new string[20];
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
        pidFields[19] = "123-45-6789";  // SSN
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use PID.19 (SSN field)
        patient.SSN.ShouldBe("123-45-6789");
    }

    [Fact]
    public void FallbackField_SSN_FallbackToPatientID()
    {
        // Arrange - SSN empty, fallback to patient ID (PID.2)
        var pidFields = new string[20];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "PAT123";  // Patient ID (fallback)
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JOHN";
        pidFields[6] = "";
        pidFields[7] = "19800115";
        pidFields[8] = "M";
        for (int i = 9; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "";  // SSN EMPTY
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "PV1|1|I||||||||||||||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use PID.2 (patient ID) as fallback
        patient.SSN.ShouldBe("PAT123");
    }

    [Fact]
    public void FallbackField_Doctor_Primary()
    {
        // Arrange - Attending doctor present (PV1.7.1)
        var pv1Fields = new string[18];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I";
        for (int i = 3; i <= 6; i++) pv1Fields[i] = "";
        pv1Fields[7] = "ATTEND^DOCTOR^A";  // Attending doctor
        for (int i = 8; i <= 16; i++) pv1Fields[i] = "";
        pv1Fields[17] = "ADMIT^DOCTOR^B";  // Admitting doctor
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            string.Join("|", pv1Fields);

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use PV1.7.1 (attending doctor component 1)
        patient.AttendingDoctor.ShouldBe("ATTEND");
    }

    [Fact]
    public void FallbackField_Doctor_FallbackToAdmitting()
    {
        // Arrange - Attending doctor empty, admitting doctor present (PV1.17.1)
        var pv1Fields = new string[18];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I";
        for (int i = 3; i <= 6; i++) pv1Fields[i] = "";
        pv1Fields[7] = "";  // Attending doctor EMPTY
        for (int i = 8; i <= 16; i++) pv1Fields[i] = "";
        pv1Fields[17] = "ADMIT^DOCTOR^B";  // Admitting doctor
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            "PID|1||MRN123||DOE^JOHN||19800115|M||||||||||||\r" +
            string.Join("|", pv1Fields);

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Should use PV1.17.1 (admitting doctor component 1) as fallback
        patient.AttendingDoctor.ShouldBe("ADMIT");
    }

    [Fact]
    public void FallbackField_RealWorld_MultipleFields()
    {
        // Arrange - Mixed scenario: some primary, some fallback
        // Phone: PID.13 empty ? fallback to PID.14 (work phone)
        // SSN: PID.19 has value
        // Emergency contact: NK1.2 has value
        // Doctor: PV1.7 empty ? fallback to PV1.17
        var pidFields = new string[20];
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
        pidFields[13] = "";             // Home phone EMPTY
        pidFields[14] = "555-WORK";     // Work phone (fallback)
        for (int i = 15; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "999-88-7777";  // SSN
        
        var pv1Fields = new string[18];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I";
        for (int i = 3; i <= 6; i++) pv1Fields[i] = "";
        pv1Fields[7] = "";                   // Attending EMPTY
        for (int i = 8; i <= 16; i++) pv1Fields[i] = "";
        pv1Fields[17] = "ADMIT^DOCTOR^B";    // Admitting (fallback)
        
        var message = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            string.Join("|", pidFields) + "\r" +
            "NK1|1|CONTACT^MARY^L|SISTER|123 OAK ST^^SF^CA^94103|555-9999|\r" +
            string.Join("|", pv1Fields);

        // Act
        var patient = PatientWithFallbackFieldsMapper.MapFromSpan(message.AsSpan());

        // Assert - Multiple fallbacks working together
        patient.PhoneNumber.ShouldBe("555-WORK");  // Fallback to PID.14 (work phone)
        patient.SSN.ShouldBe("999-88-7777");  // Primary PID.19
        patient.EmergencyContactName.ShouldBe("CONTACT^MARY^L");  // Primary NK1.2
        patient.AttendingDoctor.ShouldBe("ADMIT");  // Fallback to PV1.17.1 component
    }
}
