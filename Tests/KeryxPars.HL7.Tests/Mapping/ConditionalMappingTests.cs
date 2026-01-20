using System;
using KeryxPars.HL7.Tests.Mapping.Examples;
using Shouldly;

namespace KeryxPars.HL7.Tests.Mapping;

/// <summary>
/// Tests for CONDITIONAL MAPPING - the killer feature!
/// </summary>
public class ConditionalMappingTests
{
    private const string FemaleInpatient = 
        "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
        "PID|1||MRN123||DOE^JANE||19850315|F|||123 MAIN ST|||||||||\r" +
        "PV1|1|I|ICU^201^A||||||||||||||||VISIT123|||||||||||||||||||||||||||||||||||||||";

    private const string MaleOutpatient = 
        "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG002|P|2.5||\r" +
        "PID|1||MRN456||DOE^JOHN||19800115|M|||456 ELM ST|||||||||\r" +
        "PV1|1|O||||||||||||||||||VISIT456|||||||||||||||||||||||||||||||||||||||";

    [Fact]
    public void Conditional_FemaleOnly_ShouldMapPregnancyStatus()
    {
        // Arrange - Create proper HL7 with field at position 31
        // PID fields: 1=SetID, 2=PatientID, 3=MRN, 4=AltID, 5=Name, 6=MotherName, 7=DOB, 8=Gender...31=PregnancyStatus
        var pidFields = new string[32]; // Need index 31, so array of 32
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "";
        pidFields[3] = "MRN123";
        pidFields[4] = "";
        pidFields[5] = "DOE^JANE";
        pidFields[6] = "";
        pidFields[7] = "19850315";
        pidFields[8] = "F";
        // Fields 9-30 empty
        for (int i = 9; i <= 30; i++) pidFields[i] = "";
        pidFields[31] = "Y"; // Pregnancy indicator
        
        var pidSegment = string.Join("|", pidFields);
        
        var femaleMessage = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG001|P|2.5||\r" +
            pidSegment + "\r" +
            "PV1|1|O||||||||||||||||||VISIT123|||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(femaleMessage.AsSpan());

        // Assert
        patient.Gender.ShouldBe(Gender.F);
        patient.PregnancyStatus.ShouldNotBeNull();
        patient.PregnancyStatus.ShouldBe("Y");
    }

    [Fact]
    public void Conditional_MalePatient_ShouldNotMapPregnancyStatus()
    {
        // Arrange - Male patient message (same field has data but should be ignored)
        var maleMessage = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG002|P|2.5||\r" +
            "PID|1||MRN456||DOE^JOHN||19800115|M|||||||||||||||||||||||||||Y\r" +  // Field 31 exists but patient is male
            "PV1|1|O||||||||||||||||||VISIT456|||||||||||||||||||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(maleMessage.AsSpan());

        // Assert
        patient.Gender.ShouldBe(Gender.M);
        patient.PregnancyStatus.ShouldBeNull(); // Should NOT be mapped for males!
    }

    [Fact]
    public void Conditional_Inpatient_ShouldMapBedAssignment()
    {
        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(FemaleInpatient.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.I);
        patient.BedAssignment.ShouldBe("ICU^201^A");
        patient.RoomNumber.ShouldBe("ICU");
    }

    [Fact]
    public void Conditional_Outpatient_ShouldNotMapBedAssignment()
    {
        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(MaleOutpatient.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.O);
        patient.BedAssignment.ShouldBe(string.Empty); // Should NOT be mapped for outpatients!
        patient.RoomNumber.ShouldBe(string.Empty);
    }

    [Fact]
    public void Conditional_NotOutpatient_ShouldMapDischargeDate()
    {
        // Arrange - Inpatient with discharge date at PV1.45
        var pv1Fields = new string[46];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I"; // Inpatient
        for (int i = 3; i <= 44; i++) pv1Fields[i] = "";
        pv1Fields[45] = "20231220"; // Discharge date
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var messageWithDischarge = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG003|P|2.5||\r" +
            "PID|1||MRN789||DOE^BOB||19750520|M||||||||||||\r" +
            pv1Segment;

        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(messageWithDischarge.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.I);
        patient.DischargeDate.ShouldNotBeNull();
        patient.DischargeDate.Value.ShouldBe(new DateTime(2023, 12, 20));
    }

    [Fact]
    public void Conditional_Outpatient_ShouldNotMapDischargeDate()
    {
        // Arrange - Outpatient (discharge date field has data but should be skipped)
        var messageWithDischarge = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG004|P|2.5||\r" +
            "PID|1||MRN999||DOE^ALICE||19700101|F||||||||||||\r" +
            "PV1|1|O|||||||||||||||||||||||||||||||||||||20231220|||||||||||||||||||||||";

        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(messageWithDischarge.AsSpan());

        // Assert
        patient.PatientClass.ShouldBe(PatientClass.O);
        patient.DischargeDate.ShouldBeNull(); // Should NOT be mapped for outpatients!
    }

    [Fact]
    public void SkipIfEmpty_ShouldHandleOptionalFields()
    {
        // Arrange - Message without SSN
        var messageNoSSN = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG005|P|2.5||\r" +
            "PID|1||MRN111||SMITH^ALEX||19900101|M||||||||||||\r" +  // No SSN
            "PV1|1|O||||||||||||||||||VISIT111|||||||||||||||||||||||||||||||||||||||";

        // Act - Should not throw even though SSN is empty
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(messageNoSSN.AsSpan());

        // Assert
        patient.SSN.ShouldBe(string.Empty);
        patient.MRN.ShouldBe("MRN111");
    }

    [Fact]
    public void ConditionalMapping_RealWorldScenario_FemaleInpatientPregnant()
    {
        // Arrange - Complex real-world case
        var pidFields = new string[32];
        pidFields[0] = "PID";
        pidFields[1] = "1";
        pidFields[2] = "";
        pidFields[3] = "MRN555";
        pidFields[4] = "";
        pidFields[5] = "JONES^MARY^ANN";
        pidFields[6] = "";
        pidFields[7] = "19920725";
        pidFields[8] = "F";
        pidFields[9] = "";
        pidFields[10] = "";
        pidFields[11] = "789 OAK AVE";
        for (int i = 12; i <= 18; i++) pidFields[i] = "";
        pidFields[19] = "123-45-6789"; // SSN
        for (int i = 20; i <= 30; i++) pidFields[i] = "";
        pidFields[31] = "Y"; // Pregnancy
        
        var pidSegment = string.Join("|", pidFields);
        
        var pv1Fields = new string[46];
        pv1Fields[0] = "PV1";
        pv1Fields[1] = "1";
        pv1Fields[2] = "I"; // Inpatient
        pv1Fields[3] = "MATERNITY^301^B";
        for (int i = 4; i <= 18; i++) pv1Fields[i] = "";
        pv1Fields[19] = "VISIT555";
        for (int i = 20; i <= 44; i++) pv1Fields[i] = "";
        pv1Fields[45] = "20231218"; // Discharge date
        
        var pv1Segment = string.Join("|", pv1Fields);
        
        var complexMessage = 
            "MSH|^~\\&|APP|FAC|REC|FAC|20231215143022||ADT^A01|MSG006|P|2.5||\r" +
            pidSegment + "\r" +
            pv1Segment;

        // Act
        var patient = PatientWithConditionalFieldsMapper.MapFromSpan(complexMessage.AsSpan());

        // Assert - All conditions should work together
        patient.FirstName.ShouldBe("MARY");
        patient.Gender.ShouldBe(Gender.F);
        patient.PatientClass.ShouldBe(PatientClass.I);
        
        // Female + has value = mapped
        patient.PregnancyStatus.ShouldBe("Y");
        
        // Inpatient = mapped
        patient.BedAssignment.ShouldBe("MATERNITY^301^B");
        patient.RoomNumber.ShouldBe("MATERNITY");
        
        // Not outpatient = mapped
        patient.DischargeDate.ShouldNotBeNull();
        
        // Optional field with value
        patient.SSN.ShouldBe("123-45-6789");
    }

    [Fact]
    public void Performance_ConditionalMapping_ShouldBeFast()
    {
        // Arrange
        var iterations = 1000;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Act - Map 1000 messages with conditional logic
        for (int i = 0; i < iterations; i++)
        {
            _ = PatientWithConditionalFieldsMapper.MapFromSpan(FemaleInpatient.AsSpan());
        }

        sw.Stop();

        // Assert - Should still be blazing fast!
        sw.ElapsedMilliseconds.ShouldBeLessThan(50);

        Console.WriteLine($"Conditional Mapping: {iterations} messages in {sw.ElapsedMilliseconds}ms");
        Console.WriteLine($"Average: {sw.Elapsed.TotalMilliseconds / iterations:F4}ms per message");
        Console.WriteLine($"Throughput: {iterations / sw.Elapsed.TotalSeconds:N0} messages/second");
    }
}
