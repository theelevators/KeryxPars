namespace KeryxPars.HL7.Tests.Integration;

/// <summary>
/// Integration tests using real-world HL7 message patterns
/// </summary>
public class RealWorldMessageTests
{
    [Fact]
    public void Deserialize_ADT_A01_NewAdmit_ShouldParseCompletely()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG001|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JOHN^A||19800101|M|||123 MAIN ST^^CITY^ST^12345|||||||
PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.EventType.ShouldBe(EventType.NewAdmit);
        result.Value.MessageType.ShouldBe(IncomingMessageType.ADT);
        result.Value.Msh.MessageControlID.ShouldBe("MSG001");
        result.Value.Pid.ShouldNotBeNull();
        result.Value.Pv1.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_ADT_A08_Update_ShouldParseCorrectly()
    {
        // Arrange
        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101120000||ADT^A08|MSG002|P|2.5||
EVN|A08|20230101120000||
PID|1||123456||DOE^JANE^M||19900215|F|||456 ELM ST^^CITY^ST^54321|||||||
PV1|1|I|ICU^201^A||||DOCTOR^ATTENDING|||||||||||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.EventType.ShouldBe(EventType.Update);
        result.Value.MessageType.ShouldBe(IncomingMessageType.ADT);
    }

    [Fact]
    public void Deserialize_MedicationOrder_RDE_O11_ShouldParseCorrectly()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APPLICATION|SENDING_FACILITY|RECEIVING_APPLICATION|RECEIVING_FACILITY|201305171259||RDE^O11|2244455|P|2.5||
PID|1||123456||DUCK^DAISY^L||19690912|F|||123 NORTHWOOD ST APT 9^^NEW CITY^NC^27262-9944||||||
ORC|NW|ORD123456|FIL789012||||^^^20230101||20230101101530|ORDERING^PHYSICIAN||
RXO|00378-1805-10^Metformin 500mg Tab^NDC|500||MG|TABS|||
RXR|PO^Oral^HL70162|||
TQ1|1|BID||20230101|20230201|||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan(), 
            KeryxPars.HL7.Serialization.Configuration.SerializerOptions.ForMedicationOrders());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.EventType.ShouldBe(EventType.MedicationOrder);
        result.Value.Orders.Count.ShouldBe(1);
    }

    [Fact]
    public void Deserialize_ADT_WithClinicalData_ShouldParseAllergiesAndDiagnoses()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG002|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||DOE^JANE^M||19900215|F|||456 ELM ST^^CITY^ST^54321|||||||
PV1|1|I|ICU^201^A||||DOCTOR^ATTENDING|||||||||||
AL1|1|DA|1545^PENICILLIN^RX|SV|RASH
AL1|2|MA|^DUST^RX|MO|SNEEZING
DG1|1|I10|E11.9^Type 2 diabetes mellitus^I10||20230101|A|
DG1|2|I10|I10^Essential hypertension^I10||20230101|A|";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(2);
        result.Value.Diagnoses.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_ComplexMedicationOrder_ShouldParseAllComponents()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||RDE^O11|MSG003|P|2.5||
PID|1||987654||SMITH^ROBERT^J||19750520|M|||789 OAK AVE^^CITY^ST^98765||||||
PV1|1|O|CLINIC^EXAM1||||PROVIDER^PRIMARY||||||||||
ORC|NW|ORD456789|FIL654321||||^^^20230101||20230101143000|ORDERING^PROVIDER||
RXE|^^^20230101^20230201|00004-0005-01^Acetaminophen 500mg^NDC|500||MG||||||10||||||||||||||
RXR|PO^Oral^HL70162|MOUTH^Mouth^HL70163|||
RXC|B|00004-0005-02^Component A^NDC|100|MG|||
RXC|B|00004-0006-03^Component B^NDC|50|MG|||
TQ1|1|PRN||20230101|20230131|||
TQ1|2|Q4H||20230101|20230131|||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan(),
            KeryxPars.HL7.Serialization.Configuration.SerializerOptions.ForMedicationOrders());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(1);
        result.Value.Pid.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_LargeMessage_WithMultipleSegmentTypes_ShouldParseCompletely()
    {
        // Arrange
        var message = @"MSH|^~\&|SENDING_APP|SENDING_FAC|RECEIVING_APP|RECEIVING_FAC|20230101120000||ADT^A01|MSG004|P|2.5||
EVN|A01|20230101120000||
PID|1||123456||PATIENT^TEST^LARGE||19850315|M|||1234 MAIN STREET APT 5B^^METROPOLIS^ST^12345-6789||(555)123-4567|(555)987-6543||S|PROTESTANT|987654321|||123-45-6789|||N||||||||
PV1|1|I|NORTH^201^A^MAIN HOSPITAL^N^^BED^2^200|28b|||1234567^DOCTOR^ATTENDING^A^^^MD|2345678^REFERRING^DOCTOR^^^MD|3456789^CONSULTING^DOCTOR^^^MD|MED||||19|A0|5678901^ADMITTING^DOCTOR^^^MD||VIP12345|INS||||||||||||||||||||HOSP||||20230101120000|
PV2||PREADMIT|REASON FOR ADMISSION||||||||||||||||||||||||||||||||||||
AL1|1|DA|1545^PENICILLIN^RX|SV|HIVES~ITCHING
AL1|2|MA|^PEANUTS^FOOD|MO|ANAPHYLAXIS
AL1|3|EA|^POLLEN^ENV|MI|SNEEZING
DG1|1|I10|E11.9^Type 2 diabetes mellitus without complications^I10||20230101|A|
DG1|2|I10|I10^Essential (primary) hypertension^I10||20230101|A|
DG1|3|I10|E78.5^Hyperlipidemia, unspecified^I10||20230101|W|
IN1|1|PLAN001|INS123|INSURANCE COMPANY NAME|PO BOX 12345^^INSURANCE CITY^ST^54321|(800)555-1212||GROUP123|GROUP NAME|||||||||PATIENT^TEST^LARGE|SELF|19850315|1234 MAIN STREET APT 5B^^METROPOLIS^ST^12345|||||||||||||||||POLICY123456|";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.ShouldNotBeNull();
        result.Value.Evn.ShouldNotBeNull();
        result.Value.Pid.ShouldNotBeNull();
        result.Value.Pv1.ShouldNotBeNull();
        result.Value.Pv2.ShouldNotBeNull();
        result.Value.Allergies.Count.ShouldBe(3);
        result.Value.Diagnoses.Count.ShouldBe(3);
        result.Value.Insurance.Count.ShouldBe(1);
    }

    [Fact]
    public void Deserialize_MinimalMessage_ShouldParseSuccessfully()
    {
        // Arrange
        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.MessageControlID.ShouldBe("1");
    }

    [Fact]
    public void Serialize_ThenDeserialize_CompleteMessage_ShouldRoundTrip()
    {
        // Arrange
        var original = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "SENDAPP",
                SendingFacility = "SENDFAC",
                ReceivingApplication = "RECAPP",
                ReceivingFacility = "RECFAC",
                DateTimeOfMessage = "20230101120000",
                MessageType = "ADT^A01",
                MessageControlID = "MSG123",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Pv1 = new PV1()
        };
        original.Allergies.Add(new AL1());
        original.Diagnoses.Add(new DG1());

        // Act
        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize(serialized.Value.AsSpan());

        // Assert
        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Msh.MessageControlID.ShouldBe("MSG123");
        deserialized.Value.Msh.SendingApplication.ShouldBe("SENDAPP");
    }

    [Fact]
    public void Deserialize_WithDifferentLineEndings_ShouldHandleAll()
    {
        // Arrange - Mixed \r\n, \n, and \r
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\n" +
                      "PV1|1|I|WARD||||\r" +
                      "AL1|1|DA|PENICILLIN|SV|RASH";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pid.ShouldNotBeNull();
        result.Value.Pv1.ShouldNotBeNull();
        result.Value.Allergies.Count.ShouldBe(1);
    }

    [Fact]
    public void Deserialize_MultipleOrders_ShouldGroupCorrectly()
    {
        // Arrange
        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||RDE^O11|1|P|2.5||
PID|1||12345||DOE^JOHN||19800101|M
ORC|NW|ORD001|FIL001||||^^^20230101||20230101101530|||
RXO|00001^Med1|100||MG||
ORC|NW|ORD002|FIL002||||^^^20230101||20230101101530|||
RXO|00002^Med2|200||MG|||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan(),
            KeryxPars.HL7.Serialization.Configuration.SerializerOptions.ForMedicationOrders());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(2);
    }

    [Fact]
    public void Deserialize_ADT_A03_Discharge_ShouldIdentifyCorrectly()
    {
        // Arrange
        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ADT^A03|1|P|2.5||
EVN|A03|20230101120000||
PID|1||12345||DOE^JOHN||19800101|M
PV1|1|I|WARD||||";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.EventType.ShouldBe(EventType.Discharge);
    }

    [Fact]
    public void Deserialize_WithComponentsAndSubComponents_ShouldPreserveStructure()
    {
        // Arrange
        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||
PID|1||12345||DOE^JOHN^MIDDLE^JR^DR&PHD||19800101|M";

        // Act
        var result = HL7Serializer.Deserialize(message.AsSpan());

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pid.ShouldNotBeNull();
    }
}
