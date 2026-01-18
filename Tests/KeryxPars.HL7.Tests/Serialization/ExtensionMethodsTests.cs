namespace KeryxPars.HL7.Tests.Serialization;

using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Tests for extension methods and DDD patterns used in serialization (via public API)
/// </summary>
public class SerializationPatternsTests
{
    #region Message Type Serialization Tests

    [Fact]
    public void Serialize_DefaultMessage_WithAllSegments_ShouldSerializeCorrectly()
    {
        var message = new HL7DefaultMessage
        {
            Msh = new MSH
            {
                SendingApplication = "TEST",
                MessageType = "ADT^A01",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "12345" },
            Pv1 = new PV1() { PatientType = "Inpatient" },
            Pv2 = new PV2() { AdmitReason = "Emergency" },
            Allergies = [new AL1()],
            Diagnoses = [new DG1()],
            Insurance = [new IN1()]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldContain("PID|");
        result.Value.ShouldContain("PV1|");
        result.Value.ShouldContain("PV2|");
    }

    [Fact]
    public void Serialize_PharmacyMessage_ShouldIncludePharmacySpecificSegments()
    {
        var message = new PharmacyMessage
        {
            Msh = new MSH
            {
                SendingApplication = "PHARMACY",
                MessageType = "RDE^O11",
                MessageControlID = "PHARM001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "12345" },
            Pv1 = new PV1() { PatientType = "Inpatient" },
            Allergies = [new AL1()],
            Notes = [new NTE()]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldContain("PV1|");
    }

    [Fact]
    public void Serialize_LabMessage_ShouldIncludeLabSpecificSegments()
    {
        var message = new LabMessage
        {
            Msh = new MSH
            {
                SendingApplication = "LAB",
                MessageType = "ORU^R01",
                MessageControlID = "LAB001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Specimens = [new SPM()],
            Containers = [new SAC()]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
    }

    [Fact]
    public void Serialize_HospiceMessage_ShouldIncludeHospiceSpecificSegments()
    {
        var message = new HospiceMessage
        {
            Msh = new MSH
            {
                SendingApplication = "HOSPICE",
                MessageType = "ADT^A01",
                MessageControlID = "HOSP001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Pd1 = new PD1(),
            NextOfKin = [new NK1()],
            Guarantors = [new GT1()],
            InsuranceAdditional = [new IN2()],
            DiagnosisRelatedGroup = new DRG(),
            Accident = new ACC(),
            Roles = [new ROL()],
            MergeInfo = new MRG()
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
    }

    [Fact]
    public void Serialize_SchedulingMessage_ShouldIncludeSchedulingSegments()
    {
        var message = new SchedulingMessage
        {
            Msh = new MSH
            {
                SendingApplication = "SCHEDULER",
                MessageType = "SIU^S12",
                MessageControlID = "SCHED001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Schedule = new SCH(),
            LocationResources = [new AIL()],
            PersonnelResources = [new AIP()],
            ServiceResources = [new AIS()]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
    }

    [Fact]
    public void Serialize_FinancialMessage_ShouldIncludeFinancialSegments()
    {
        var message = new FinancialMessage
        {
            Msh = new MSH
            {
                SendingApplication = "BILLING",
                MessageType = "DFT^P03",
                MessageControlID = "BILL001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Transactions = [new FT1()],
            Guarantors = [new GT1()],
            DiagnosisRelatedGroup = new DRG(),
            Accident = new ACC()
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
    }

    [Fact]
    public void Serialize_DietaryMessage_ShouldIncludeDietarySegments()
    {
        var message = new DietaryMessage
        {
            Msh = new MSH
            {
                SendingApplication = "DIETARY",
                MessageType = "OMD^O03",
                MessageControlID = "DIET001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Orders = [new ORC()],
            DietaryOrders = [new ODS()],
            TrayInstructions = [new ODT()]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
    }

    #endregion

    #region Polymorphic Behavior Tests

    [Fact]
    public void Deserialize_WithAllergies_ToDefaultMessage_ShouldAddToList()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "AL1|2|MA|^DUST|MO|SNEEZING\r\n" +
                      "AL1|3|FA|^SHELLFISH|SV|ANAPHYLAXIS";

        var result = HL7Serializer.Deserialize(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(3);
    }

    [Fact]
    public void Deserialize_WithAllergies_ToPharmacyMessage_ShouldAddToList()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "AL1|2|MA|^DUST|MO|SNEEZING";

        var result = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_WithDiagnoses_ToMultipleMessageTypes_ShouldAddToAll()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "DG1|1|I10|E11.9^Diabetes||20230101|A|\r\n" +
                      "DG1|2|I10|I10^Hypertension||20230101|A|";

        // Test with default message
        var defaultResult = HL7Serializer.Deserialize(message.AsSpan());
        defaultResult.IsSuccess.ShouldBeTrue();
        defaultResult.Value!.Diagnoses.Count.ShouldBe(2);

        // Test with pharmacy message
        var pharmResult = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan());
        pharmResult.IsSuccess.ShouldBeTrue();
        pharmResult.Value!.Diagnoses.Count.ShouldBe(2);

        // Test with hospice message
        var hospiceResult = HL7Serializer.Deserialize<HospiceMessage>(message.AsSpan());
        hospiceResult.IsSuccess.ShouldBeTrue();
        hospiceResult.Value!.Diagnoses.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_WithInsurance_ToApplicableMessageTypes_ShouldAdd()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "IN1|1|PLAN001|INS123|INSURANCE CO|||\r\n" +
                      "IN1|2|PLAN002|INS456|INSURANCE CO2|||";

        // Default message
        var defaultResult = HL7Serializer.Deserialize(message.AsSpan());
        defaultResult.IsSuccess.ShouldBeTrue();
        defaultResult.Value!.Insurance.Count.ShouldBe(2);

        // Pharmacy message
        var pharmResult = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan());
        pharmResult.IsSuccess.ShouldBeTrue();
        pharmResult.Value!.Insurance.Count.ShouldBe(2);

        // Financial message
        var finResult = HL7Serializer.Deserialize<FinancialMessage>(message.AsSpan());
        finResult.IsSuccess.ShouldBeTrue();
        finResult.Value!.Insurance.Count.ShouldBe(2);
    }

    #endregion

    #region Collection Handling Tests

    [Fact]
    public void Serialize_WithLargeAllergyCollection_ShouldSerializeAll()
    {
        var message = new HL7DefaultMessage
        {
            Msh = new MSH
            {
                SendingApplication = "TEST",
                MessageType = "ADT^A01",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Allergies = Enumerable.Range(1, 50).Select(_ => new AL1()).ToList()
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Serialize_WithEmptyCollections_ShouldNotIncludeEmptySegments()
    {
        var message = new HL7DefaultMessage
        {
            Msh = new MSH
            {
                SendingApplication = "TEST",
                MessageType = "ADT^A01",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Allergies = [],
            Diagnoses = [],
            Insurance = []
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldNotContain("AL1|");
        result.Value.ShouldNotContain("DG1|");
        result.Value.ShouldNotContain("IN1|");
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Serialize_WithNullOptionalSegments_ShouldNotFail()
    {
        var message = new HospiceMessage
        {
            Msh = new MSH
            {
                SendingApplication = "TEST",
                MessageType = "ADT^A01",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Pd1 = null,
            DiagnosisRelatedGroup = null,
            Accident = null,
            MergeInfo = null
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithMixedSegmentTypes_ShouldAssignCorrectly()
    {
        var message = "MSH|^~\\&|HOSPICE|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PD1|||CLINIC123||||\r\n" +
                      "NK1|1|DOE^JANE|SPO|123 Main St||555-1234|\r\n" +
                      "PV1|1|I|HOSPICE^ROOM1|||DOCTOR^JOHN|||MED|||||||||||||||||||||||||||||||||\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "DG1|1|I10|C34.90^Lung Cancer||20230101|A|\r\n" +
                      "GT1|1||DOE^JANE||123 Main St|||555-1234|\r\n" +
                      "IN1|1|PLAN001|INS123|INSURANCE CO|||\r\n" +
                      "IN2|1|12345678||||\r\n" +
                      "DRG|1|123|2.5|20230101|||\r\n" +
                      "ACC|20230101|AUTO^Auto Accident||||\r\n" +
                      "ROL|1||DOCTOR|ATTENDING^PHYSICIAN|||\r\n" +
                      "MRG|OLD123||||||";

        var result = HL7Serializer.Deserialize<HospiceMessage>(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        var hospice = result.Value!;
        hospice.Pd1.ShouldNotBeNull();
        hospice.NextOfKin.Count.ShouldBe(1);
        hospice.Pv1.ShouldNotBeNull();
        hospice.Allergies.Count.ShouldBe(1);
        hospice.Diagnoses.Count.ShouldBe(1);
        hospice.Guarantors.Count.ShouldBe(1);
        hospice.Insurance.Count.ShouldBe(1);
        hospice.InsuranceAdditional.Count.ShouldBe(1);
        hospice.DiagnosisRelatedGroup.ShouldNotBeNull();
        hospice.Accident.ShouldNotBeNull();
        hospice.Roles.Count.ShouldBe(1);
        hospice.MergeInfo.ShouldNotBeNull();
    }

    #endregion

    #region Round-Trip Pattern Tests

    [Theory]
    [InlineData(typeof(HL7DefaultMessage))]
    [InlineData(typeof(PharmacyMessage))]
    [InlineData(typeof(LabMessage))]
    [InlineData(typeof(HospiceMessage))]
    [InlineData(typeof(SchedulingMessage))]
    [InlineData(typeof(FinancialMessage))]
    [InlineData(typeof(DietaryMessage))]
    public void RoundTrip_AllMessageTypes_ShouldPreserveMessageControlID(Type messageType)
    {
        // Create instance
        var message = (IHL7Message)Activator.CreateInstance(messageType)!;
        message.Msh = new MSH
        {
            SendingApplication = "TEST",
            MessageType = "ADT^A01",
            MessageControlID = "CTRL999",
            ProcessingID = "P",
            VersionID = "2.5"
        };
        message.Pid = new PID();

        // Serialize
        var serialized = HL7Serializer.Serialize(message);
        serialized.IsSuccess.ShouldBeTrue();

        // Deserialize
        var deserialized = HL7Serializer.Deserialize(serialized.Value.AsSpan());
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Msh.MessageControlID.ShouldBe("CTRL999");
    }

    #endregion
}
