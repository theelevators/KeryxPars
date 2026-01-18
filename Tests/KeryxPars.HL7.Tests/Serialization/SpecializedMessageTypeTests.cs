namespace KeryxPars.HL7.Tests.Serialization;

using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Tests for specialized message type deserialization and serialization
/// </summary>
public class SpecializedMessageTypeTests
{
    #region PharmacyMessage Tests

    [Fact]
    public void Deserialize_AsPharmacyMessage_WithMedicationOrder_ShouldParseCorrectly()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|O|CLINIC|||DOCTOR^JOHN|||MED|||||||||||||||||||||||||||||||||\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530|ORDERING^PHYSICIAN||\r\n" +
                      "RXO|00378-1805-10^Metformin 500mg|500||MG||||\r\n" +
                      "RXR|PO^Oral|||\r\n" +
                      "NTE|1|P|Patient prefers morning dose";

        var result = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan(), SerializerOptions.ForMedicationOrders());

        result.IsSuccess.ShouldBeTrue();
        var pharmMsg = result.Value!;
        pharmMsg.ShouldBeOfType<PharmacyMessage>();
        pharmMsg.Pv1.ShouldNotBeNull();
        pharmMsg.Allergies.Count.ShouldBe(1);
        pharmMsg.Orders.Count.ShouldBe(1);
    }

    [Fact]
    public void Serialize_PharmacyMessage_ShouldIncludeAllSegments()
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
            Pv1 = new PV1 { PatientClass = "O" },
            Allergies = [new AL1 { SetID = "1" }],
            Notes = [new NTE { SetID = "1" }]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldContain("PID|");
        result.Value.ShouldContain("PV1|");
        result.Value.ShouldContain("AL1|");
        result.Value.ShouldContain("NTE|");
    }

    #endregion

    #region LabMessage Tests

    [Fact]
    public void Deserialize_AsLabMessage_WithObservations_ShouldParseCorrectly()
    {
        var message = "MSH|^~\\&|LAB|FAC|REC|FAC|20230101||ORU^R01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|O|LAB|||DOCTOR^JANE|||LAB|||||||||||||||||||||||||||||||||\r\n" +
                      "ORC|RE|LAB123|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB123||CBC^Complete Blood Count|||20230101101530|\r\n" +
                      "OBX|1|NM|WBC^White Blood Count||7.5|10*3/uL|4.0-11.0|N|||F|\r\n" +
                      "SPM|1|12345||BLOOD^Blood|||||||PSN^Patient|\r\n" +
                      "NTE|1|L|Specimen received in good condition";

        var result = HL7Serializer.Deserialize<LabMessage>(message.AsSpan(), SerializerOptions.ForLabOrders());

        result.IsSuccess.ShouldBeTrue();
        var labMsg = result.Value!;
        labMsg.ShouldBeOfType<LabMessage>();
        labMsg.Orders.Count.ShouldBe(1);
        labMsg.Specimens.Count.ShouldBe(1);
    }

    [Fact]
    public void Serialize_LabMessage_ShouldIncludeSpecimens()
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
            Pid = new PID { PatientID = "67890" },
            Specimens = [new SPM { SetID = "1" }],
            Containers = [new SAC()]  // Just use empty constructor
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldContain("SPM|");
        // SAC may not serialize if all fields are empty
    }

    #endregion

    #region HospiceMessage Tests

    [Fact]
    public void Deserialize_AsHospiceMessage_WithCompletePatientInfo_ShouldParseCorrectly()
    {
        var message = "MSH|^~\\&|HOSPICE|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "EVN|A01|20230101120000||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PD1|||CLINIC123||||\r\n" +
                      "NK1|1|DOE^JANE|SPO|123 Main St||555-1234|\r\n" +
                      "PV1|1|I|HOSPICE^ROOM1|||DOCTOR^JOHN|||MED|||||||||||||||||||||||||||||||||\r\n" +
                      "PV2|||||||||||||||||||||N||\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "DG1|1|I10|C34.90^Lung Cancer||20230101|A|\r\n" +
                      "GT1|1||DOE^JANE||123 Main St|||555-1234|\r\n" +
                      "IN1|1|PLAN001|INS123|INSURANCE CO|||\r\n" +
                      "IN2|1|12345678||||\r\n" +
                      "DRG|1|123|2.5|20230101|||\r\n" +
                      "ROL|1||DOCTOR|ATTENDING^PHYSICIAN|||";

        var result = HL7Serializer.Deserialize<HospiceMessage>(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        var hospiceMsg = result.Value!;
        hospiceMsg.ShouldBeOfType<HospiceMessage>();
        hospiceMsg.Pd1.ShouldNotBeNull();
        hospiceMsg.NextOfKin.Count.ShouldBe(1);
        hospiceMsg.Pv2.ShouldNotBeNull();
        hospiceMsg.Guarantors.Count.ShouldBe(1);
        hospiceMsg.InsuranceAdditional.Count.ShouldBe(1);
        hospiceMsg.DiagnosisRelatedGroup.ShouldNotBeNull();
        hospiceMsg.Roles.Count.ShouldBe(1);
    }

    [Fact]
    public void Serialize_HospiceMessage_ShouldIncludeAllHospiceSegments()
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
            Pid = new PID { PatientID = "99999" },
            Pd1 = new PD1 { LivingArrangement = "HOME" },
            NextOfKin = [new NK1 { SetID = "1" }],
            Guarantors = [new GT1 { SetID = "1" }],
            InsuranceAdditional = [new IN2()],  // Empty constructor
            Procedures = [new PR1 { SetID = "1" }],
            DiagnosisRelatedGroup = new DRG(),  // Empty constructor
            Accident = new ACC { AccidentDateTime = "20230101" },
            Roles = [new ROL { RoleInstanceID = "1" }],
            MergeInfo = new MRG()  // Empty constructor
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("PD1|");
        result.Value.ShouldContain("NK1|");
        result.Value.ShouldContain("GT1|");
        // IN2, DRG, MRG may not serialize if all fields are empty
        result.Value.ShouldContain("PR1|");
        result.Value.ShouldContain("ACC|");
        result.Value.ShouldContain("ROL|");
    }

    #endregion

    #region SchedulingMessage Tests

    [Fact]
    public void Deserialize_AsSchedulingMessage_WithAppointment_ShouldParseCorrectly()
    {
        var message = "MSH|^~\\&|SCHED|FAC|REC|FAC|20230101||SIU^S12|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|O|CLINIC|||DOCTOR^JOHN|||MED|||||||||||||||||||||||||||||||||\r\n" +
                      "SCH|12345|||||ROUTINE^Routine Appointment|REASON^Annual Checkup|||||60|MIN||20230115100000|||DOCTOR^JOHN|\r\n" +
                      "AIL|1|CLINIC|ROOM1^Exam Room 1||CONFIRMED|||20230115100000||\r\n" +
                      "AIP|1|DOCTOR|JOHN^DR||CONFIRMED|||20230115100000||\r\n" +
                      "AIS|1|SERVICE|CHECKUP^Annual Checkup||CONFIRMED|||20230115100000||\r\n" +
                      "NTE|1|L|Please arrive 15 minutes early\r\n" +
                      "DG1|1|I10|Z00.00^Annual Exam||20230101|A|";

        var result = HL7Serializer.Deserialize<SchedulingMessage>(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        var schedMsg = result.Value!;
        schedMsg.ShouldBeOfType<SchedulingMessage>();
        schedMsg.Schedule.ShouldNotBeNull();
        schedMsg.LocationResources.Count.ShouldBe(1);
        schedMsg.PersonnelResources.Count.ShouldBe(1);
        schedMsg.ServiceResources.Count.ShouldBe(1);
        schedMsg.Notes.Count.ShouldBe(1);
        schedMsg.Diagnoses.Count.ShouldBe(1);
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
            Pid = new PID { PatientID = "54321" },
            Schedule = new SCH { PlacerAppointmentID = "APPT123" },
            LocationResources = [new AIL { SegmentActionCode = "A" }],
            PersonnelResources = [new AIP { SegmentActionCode = "A" }],
            ServiceResources = [new AIS { SegmentActionCode = "A" }]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("SCH|");
        result.Value.ShouldContain("AIL|");
        result.Value.ShouldContain("AIP|");
        result.Value.ShouldContain("AIS|");
    }

    #endregion

    #region FinancialMessage Tests

    [Fact]
    public void Deserialize_AsFinancialMessage_WithTransactions_ShouldParseCorrectly()
    {
        var message = "MSH|^~\\&|BILLING|FAC|REC|FAC|20230101||DFT^P03|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|O|CLINIC|||DOCTOR^JOHN|||MED|||||||||||||||||||||||||||||||||\r\n" +
                      "FT1|1||20230101|20230101|CG|99213^Office Visit|||1|150.00|150.00|||||||||||\r\n" +
                      "GT1|1||DOE^JANE||123 Main St|||555-1234|\r\n" +
                      "IN1|1|PLAN001|INS123|INSURANCE CO|||\r\n" +
                      "IN2|1|12345678||||\r\n" +
                      "DRG|1|123|2.5|20230101|||\r\n" +
                      "PR1|1|CPT|99213|Office Visit|20230101||||\r\n" +
                      "DG1|1|I10|Z00.00^Annual Exam||20230101|A|";

        var result = HL7Serializer.Deserialize<FinancialMessage>(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        var finMsg = result.Value!;
        finMsg.ShouldBeOfType<FinancialMessage>();
        finMsg.Transactions.Count.ShouldBe(1);
        finMsg.Guarantors.Count.ShouldBe(1);
        finMsg.Insurance.Count.ShouldBe(1);
        finMsg.InsuranceAdditional.Count.ShouldBe(1);
        finMsg.DiagnosisRelatedGroup.ShouldNotBeNull();
        finMsg.Procedures.Count.ShouldBe(1);
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
            Pid = new PID { PatientID = "77777" },
            Transactions = [new FT1 { SetID = "1" }],
            Guarantors = [new GT1 { SetID = "1" }],
            InsuranceAdditional = [new IN2()],  // Empty constructor
            DiagnosisRelatedGroup = new DRG(),  // Empty constructor
            Accident = new ACC { AccidentDateTime = "20230101" }
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("FT1|");
        result.Value.ShouldContain("GT1|");
        // IN2, DRG may not serialize if all fields are empty
        result.Value.ShouldContain("ACC|");
    }

    #endregion

    #region DietaryMessage Tests

    [Fact]
    public void Deserialize_AsDietaryMessage_WithDietOrders_ShouldParseCorrectly()
    {
        var message = "MSH|^~\\&|DIETARY|FAC|REC|FAC|20230101||OMD^O03|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|I|FLOOR2^ROOM201|||DOCTOR^JOHN|||MED|||||||||||||||||||||||||||||||||\r\n" +
                      "ORC|NW|DIET123|||||^^^20230101||20230101101530||||\r\n" +
                      "ODS|D|REGULAR^Regular Diet|20230101|DIET123|ACTIVE||||\r\n" +
                      "ODT|BREAKFAST|0800||20230101||||\r\n" +
                      "NTE|1|L|Patient prefers low sodium\r\n" +
                      "AL1|1|FA|^SHELLFISH|MO|ANAPHYLAXIS\r\n" +
                      "DG1|1|I10|E11.9^Diabetes||20230101|A|";

        var result = HL7Serializer.Deserialize<DietaryMessage>(message.AsSpan(), SerializerOptions.ForDietary());

        result.IsSuccess.ShouldBeTrue();
        var dietMsg = result.Value!;
        dietMsg.ShouldBeOfType<DietaryMessage>();
        dietMsg.GroupedOrders.Count.ShouldBe(1);
        dietMsg.Allergies.Count.ShouldBe(1);
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
            Pid = new PID { PatientID = "88888" },
            Orders = [new ORC { OrderControl = "NW" }],
            DietaryOrders = [new ODS { Type = "D" }],
            TrayInstructions = [new ODT { TrayType = "BREAKFAST" }],
            Allergies = [new AL1 { SetID = "1" }]
        };

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("ORC|");
        result.Value.ShouldContain("ODS|");
        result.Value.ShouldContain("ODT|");
        result.Value.ShouldContain("AL1|");
    }

    #endregion

    #region Round-Trip Tests

    [Fact]
    public void RoundTrip_PharmacyMessage_ShouldPreserveData()
    {
        var original = new PharmacyMessage
        {
            Msh = new MSH
            {
                SendingApplication = "PHARMACY",
                MessageType = "RDE^O11",
                MessageControlID = "CTRL001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "12345" },
            Allergies = [new AL1 { SetID = "1" }],
            Notes = [new NTE { SetID = "1" }]
        };

        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize<PharmacyMessage>(serialized.Value.AsSpan());

        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Msh.MessageControlID.ShouldBe("CTRL001");
    }

    [Fact]
    public void RoundTrip_LabMessage_ShouldPreserveData()
    {
        var original = new LabMessage
        {
            Msh = new MSH
            {
                SendingApplication = "LAB",
                MessageType = "ORU^R01",
                MessageControlID = "LAB123",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "67890" },
            Specimens = [new SPM { SetID = "1" }]
        };

        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize<LabMessage>(serialized.Value.AsSpan());

        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Msh.MessageControlID.ShouldBe("LAB123");
    }

    [Fact]
    public void RoundTrip_HospiceMessage_ShouldPreserveComplexData()
    {
        var original = new HospiceMessage
        {
            Msh = new MSH
            {
                SendingApplication = "HOSPICE",
                MessageType = "ADT^A01",
                MessageControlID = "HOSP999",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "99999" },
            Pd1 = new PD1 { LivingArrangement = "HOME" },
            NextOfKin = [new NK1 { SetID = "1" }],
            Guarantors = [new GT1 { SetID = "1" }]
        };

        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize<HospiceMessage>(serialized.Value.AsSpan());

        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Msh.MessageControlID.ShouldBe("HOSP999");
        deserialized.Value.NextOfKin.Count.ShouldBe(1);
    }

    #endregion

    #region Default vs Typed Message Tests

    [Fact]
    public void Deserialize_AsDefaultMessage_ShouldWorkWithPharmacyData()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH";

        var result = HL7Serializer.Deserialize(message.AsSpan());

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeOfType<HL7DefaultMessage>();
        result.Value!.Allergies.Count.ShouldBe(1);
    }

    [Fact]
    public void Deserialize_TypedVsDefault_ShouldBothWork()
    {
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "DG1|1|I10|E11.9^Diabetes||20230101|A|";

        var defaultResult = HL7Serializer.Deserialize(message.AsSpan());
        var typedResult = HL7Serializer.Deserialize<HL7DefaultMessage>(message.AsSpan());

        defaultResult.IsSuccess.ShouldBeTrue();
        typedResult.IsSuccess.ShouldBeTrue();
        defaultResult.Value!.Diagnoses.Count.ShouldBe(1);
        typedResult.Value!.Diagnoses.Count.ShouldBe(1);
    }

    #endregion
}
