namespace KeryxPars.HL7.Tests.Performance;

/// <summary>
/// Stress tests for HL7 Parser to ensure it handles extreme conditions
/// </summary>
public class StressTests
{
    [Fact]
    public void Deserialize_WithMaximumRealisticMessage_ShouldSucceed()
    {
        ////- Simulate a very large patient record
        var allergies = string.Join("\r\n", Enumerable.Range(1, 50)
            .Select(i => $"AL1|{i}|DA|CODE{i}^Allergy{i}|SV|Reaction{i}"));

        var diagnoses = string.Join("\r\n", Enumerable.Range(1, 100)
            .Select(i => $"DG1|{i}|I10|CODE{i}^Diagnosis{i}||20230101|A|"));

        var insurance = string.Join("\r\n", Enumerable.Range(1, 5)
            .Select(i => $"IN1|{i}|PLAN{i}|INS{i}|Company{i}|||"));

        var message = $@"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ADT^A01|MSG001|P|2.5||
PID|1||12345||DOE^JOHN||19800101|M
PV1|1|I|WARD||||
{allergies}
{diagnoses}
{insurance}";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Allergies.Count.ShouldBe(50);
        result.Value.Diagnoses.Count.ShouldBe(100);
        result.Value.Insurance.Count.ShouldBe(5);
    }

    [Fact]
    public void Deserialize_WithThousandsOfEmptyFields_ShouldNotSlowDown()
    {
        //-Message with many empty fields
        var emptyFields = string.Join("|", Enumerable.Repeat("", 1000));
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\nPID|{emptyFields}";


        var sw = System.Diagnostics.Stopwatch.StartNew();
        var result = HL7Serializer.Deserialize(message.AsSpan());
        sw.Stop();


        result.IsSuccess.ShouldBeTrue();
        sw.ElapsedMilliseconds.ShouldBeLessThan(1000, "parsing should be fast");
    }

    [Fact]
    public void Deserialize_WithRepeatedSegments_ThousandsOfTimes_ShouldSucceed()
    {
        //-Many diagnosis segments
        var diagnoses = string.Join("\r\n", Enumerable.Range(1, 1000)
            .Select(i => $"DG1|{i}|I10|CODE{i}||20230101|A|"));

        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\nPID|1||12345||DOE^JOHN||19800101|M\r\n{diagnoses}";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(1000);
    }

    [Fact]
    public void Serialize_ThenDeserialize_LargeMessage_ShouldRoundTrip()
    {
        //-Build a large message programmatically
        var original = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "APP",
                SendingFacility = "FAC",
                ReceivingApplication = "REC",
                ReceivingFacility = "RECFAC",
                DateTimeOfMessage = "20230101120000",
                MessageType = "ADT^A01",
                MessageControlID = "CTRL123",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID(),
            Pv1 = new PV1()
        };

        // Add many allergies
        for (int i = 0; i < 50; i++)
        {
            original.Allergies.Add(new AL1() { 
                SetID = (i + 1).ToString(),
                AllergenCodeMnemonicDescription = $"CODE{i}^Allergy{i}"
            });
        }

        // Add many diagnoses
        for (int i = 0; i < 100; i++)
        {
            original.Diagnoses.Add(new DG1() {
                AttestationDateTime = "20230101",
            });
        }


        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize(serialized.Value.AsSpan());


        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.Allergies.Count.ShouldBe(50);
        deserialized.Value.Diagnoses.Count.ShouldBe(100);
    }

    [Fact]
    public void Deserialize_WithManyLineBreaks_ShouldHandleEfficiently()
    {
        //-Message with excessive line breaks
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      string.Concat(Enumerable.Repeat("\r\n", 100)) +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      string.Concat(Enumerable.Repeat("\r\n", 100));


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void FieldEnumerator_WithThousandsOfFields_ShouldNotStackOverflow()
    {

        var fields = string.Join("|", Enumerable.Range(1, 10000).Select(i => $"F{i}"));
        var span = fields.AsSpan();
        var enumerator = new FieldEnumerator(span, '|');


        var count = 0;
        while (enumerator.MoveNext())
        {
            count++;
        }


        count.ShouldBe(10000);
    }

    [Fact]
    public void LineEnumerator_WithThousandsOfLines_ShouldNotStackOverflow()
    {

        var lines = string.Join("\n", Enumerable.Range(1, 10000).Select(i => $"Line{i}"));
        var span = lines.AsSpan();
        var enumerator = new LineEnumerator(span);


        var count = 0;
        while (enumerator.MoveNext())
        {
            count++;
        }


        count.ShouldBe(10000);
    }

    [Fact]
    public void Deserialize_ConcurrentMultipleMessages_ShouldSucceed()
    {

        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||
PID|1||12345||DOE^JOHN||19800101|M
AL1|1|DA|PENICILLIN|SV|RASH";

        //-Simulate concurrent parsing
        var tasks = Enumerable.Range(1, 100).Select(_ =>
            Task.Run(() => HL7Serializer.Deserialize(message.AsSpan()))
        );

        var results = Task.WhenAll(tasks).Result;


        foreach (var r in results)
        {
            r.IsSuccess.ShouldBeTrue();
        }
    }

    [Fact]
    public void Deserialize_WithExtremeLongPatientName_ShouldHandle()
    {
        //-Patient name with 10,000 characters
        var longName = new string('A', 10000);
        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      $"PID|1||12345||{longName}||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Deserialize_WithDeepComponentNesting_ShouldPreserve()
    {
        //-Field with many component levels
        var message = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN^MIDDLE^JR^SR^MD^PHD^ESQ||19800101|M";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void SegmentReader_ReadingMillionCharacters_ShouldNotThrow()
    {

        var largeSegment = $"PID|{new string('X', 1000000)}|END";
        var reader = new SegmentReader(largeSegment.AsSpan());


        var success1 = reader.TryReadField('|', out var field1);
        var success2 = reader.TryReadField('|', out var field2);
        var success3 = reader.TryReadField('|', out var field3);


        success1.ShouldBeTrue();
        success2.ShouldBeTrue();
        success3.ShouldBeTrue();
        field2.Length.ShouldBe(1000000);
    }

    [Fact]
    public void Deserialize_WithAllPossibleSegmentTypes_ShouldHandleEach()
    {
        //-Message with many different segment types
        var message = @"MSH|^~\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||
EVN|A01|20230101||
PID|1||12345||DOE^JOHN||19800101|M
PV1|1|I|WARD||||
PV2||PREADMIT|||
AL1|1|DA|PENICILLIN|SV|RASH
DG1|1|I10|E11.9||20230101|A|
IN1|1|PLAN001|INS123|||";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Msh.ShouldNotBeNull();
        result.Value.Evn.ShouldNotBeNull();
        result.Value.Pid.ShouldNotBeNull();
        result.Value.Pv1.ShouldNotBeNull();
        result.Value.Pv2.ShouldNotBeNull();
        result.Value.Allergies.Count.ShouldBe(1);
        result.Value.Diagnoses.Count.ShouldBe(1);
        result.Value.Insurance.Count.ShouldBe(1);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(5000)]
    public void Deserialize_WithVariousMessageSizes_ShouldScaleLinearly(int segmentCount)
    {

        var segments = string.Join("\r\n", Enumerable.Range(1, segmentCount)
            .Select(i => $"DG1|{i}|I10|CODE{i}||20230101|A|"));

        var message = $"MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||\r\nPID|1||12345||DOE^JOHN||19800101|M\r\n{segments}";


        var sw = System.Diagnostics.Stopwatch.StartNew();
        var result = HL7Serializer.Deserialize(message.AsSpan());
        sw.Stop();


        result.IsSuccess.ShouldBeTrue();
        result.Value!.Diagnoses.Count.ShouldBe(segmentCount);

        // Performance check //- should be roughly linear with segment count
        var timePerSegment = sw.ElapsedMilliseconds / (double)segmentCount;
        timePerSegment.ShouldBeLessThan(1.0, "should process at least 1 segment per millisecond");
    }

    [Fact]
    public void Deserialize_WithAlternatingLongAndShortFields_ShouldHandle()
    {
        //-Mix of very long and very short fields
        var longField = new string('L', 50000);
        var message = $"MSH|^~\\&|{longField}|S|{longField}|S|20230101||ADT^A01|1|P|2.5||\r\n" +
                      $"PID|1|{longField}|S|{longField}|S|{longField}|S|S";


        var result = HL7Serializer.Deserialize(message.AsSpan());


        result.IsSuccess.ShouldBeTrue();
    }
}
