namespace KeryxPars.HL7.Tests.Segments;

/// <summary>
/// Tests for segment parsing to ensure field boundaries and validation
/// </summary>
public class SegmentParsingTests
{
    [Fact]
    public void MSH_WithAllFields_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SENDAPP|SENDFAC|RECAPP|RECFAC|20230101120000|SEC|ADT^A01|MSG001|P|2.5|123||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        var msh = result.Value!.Msh;
        msh.SendingApplication.ShouldBe("SENDAPP");
        msh.SendingFacility.ShouldBe("SENDFAC");
        msh.ReceivingApplication.ShouldBe("RECAPP");
        msh.ReceivingFacility.ShouldBe("RECFAC");
        msh.DateTimeOfMessage.ShouldBe("20230101120000");
        msh.Security.ShouldBe("SEC");
        msh.MessageType.ShouldBe("ADT^A01");
        msh.MessageControlID.ShouldBe("MSG001");
        msh.ProcessingID.ShouldBe("P");
        msh.VersionID.ShouldBe("2.5");
        msh.SequenceNumber.ShouldBe("123");
    }

    [Fact]
    public void MSH_WithMinimalFields_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ACK|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.SendingApplication.ShouldBe("SEND");
        result.Value.Msh.MessageControlID.ShouldBe("1");
    }

    [Fact]
    public void PID_WithAllFields_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
        "PID|1|EXTERNAL123|INTERNAL456||DOE^JOHN^MIDDLE^JR|MAIDEN^NAME|19800101|M|ALIAS^NAME||123 MAIN ST^^CITY^ST^12345|COUNTY|(555)123-4567|(555)987-6543|ENG|M|CATH|ACCT789|123-45-6789|DL123456|SSN987|||||||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        var pid = result.Value!.Pid;
        pid.ShouldNotBeNull();
        // Note: Specific field assertions would depend on PID segment implementation
    }

    [Fact]
    public void PV1_WithValidData_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|I|WARD^ROOM^BED||||ATTENDING^DOCTOR|||||||||||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pv1.ShouldNotBeNull();
    }

    [Fact]
    public void AL1_MultipleAllergies_ShouldParseAll()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|1545^PENICILLIN^RX|SV|HIVES~RASH\r\n" +
                      "AL1|2|MA|^DUST^ENV|MO|SNEEZING\r\n" +
                      "AL1|3|FA|^PEANUTS^FOOD|MI|ANAPHYLAXIS";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(3);
    }

    [Fact]
    public void DG1_MultipleDiagnoses_ShouldParseAll()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "DG1|1|I10|E11.9^Type 2 diabetes^I10||20230101|A|\r\n" +
                      "DG1|2|I10|I10^Hypertension^I10||20230101|A|\r\n" +
                      "DG1|3|I10|J44.9^COPD^I10||20230101|W|";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(3);
    }

    [Fact]
    public void Segments_WithEmptyFields_ShouldHandleCorrectly()
    {

        var message = "MSH|^~\\&|SEND||REC||20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.SendingFacility.ShouldBe("");
        result.Value.Msh.ReceivingFacility.ShouldBe("");
    }

    [Fact]
    public void Segments_WithTrailingDelimiters_ShouldHandleCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5|||||||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M|||||||||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Segments_WithVeryLongFieldValue_ShouldHandleCorrectly()
    {

        var longAddress = new string('X', 5000);
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      $"PID|1||12345||DOE^JOHN||19800101|M||{longAddress}";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Segments_OutOfOrderNonCritical_ShouldStillParse()
    {
        //- AL1 before PID (unusual but should handle)
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|RASH\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(1);
        result.Value.Pid.ShouldNotBeNull();
    }

    [Fact]
    public void EVN_Segment_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "EVN|A01|20230101120000|20230101110000|01|USER123^LASTNAME^FIRSTNAME\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Evn.ShouldNotBeNull();
    }

    [Fact]
    public void PV2_Segment_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "PV1|1|I|WARD||||\r\n" +
                      "PV2||PREADMIT|REASON|||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Pv2.ShouldNotBeNull();
    }

    [Fact]
    public void Segments_WithComponentSeparators_ShouldPreserveComponents()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN^MIDDLE^JR||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        // The parser should preserve the component structure
    }

    [Fact]
    public void Segments_WithRepetitionSeparators_ShouldPreserveRepetitions()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "AL1|1|DA|1545^PENICILLIN|SV|HIVES~RASH~ITCHING";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        // The parser should preserve repetition structure
    }

    [Fact]
    public void IN1_Insurance_ShouldParseCorrectly()
    {

        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
        "IN1|1|PLAN001|INS123|INSURANCE COMPANY|PO BOX 123^^CITY^ST^12345|(800)555-1212||GROUP123";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Insurance.Count.ShouldBe(1);
    }

    [Fact]
    public void Segments_WithSpecialCharactersInData_ShouldHandleCorrectly()
    {
        //- Special chars that aren't delimiters
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
        "PID|1||12345||DOE-SMITH^JOHN'S||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void SegmentGetField_WithValidIndex_ShouldReturnField()
    {

        var msh = new MSH
        {
            SendingApplication = "TEST",
            MessageControlID = "MSG001"
        };


        var field2 = msh.GetField(2);
        var field9 = msh.GetField(9);


        field2.ShouldBe("TEST");
        field9.ShouldBe("MSG001");
    }

    [Fact]
    public void SegmentGetField_WithInvalidIndex_ShouldReturnNull()
    {

        var msh = new MSH();


        var field = msh.GetField(999);


        field.ShouldBeNull();
    }

    [Fact]
    public void SegmentSetValue_WithValidIndex_ShouldSetField()
    {

        var msh = new MSH();


        msh.SetValue("TESTAPP", 2);
        msh.SetValue("CTRL123", 9);


        msh.SendingApplication.ShouldBe("TESTAPP");
        msh.MessageControlID.ShouldBe("CTRL123");
    }

    [Fact]
    public void SegmentSetValue_WithInvalidIndex_ShouldNotThrow()
    {

        var msh = new MSH();


        Action act = () => msh.SetValue("VALUE", 999);


        act.ShouldNotThrow();
    }
}
