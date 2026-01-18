namespace KeryxPars.HL7.Tests.Converters;

using KeryxPars.HL7.Serialization.Converters;
using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Tests for GenericSegmentConverter to ensure correct reading and writing
/// </summary>
public class GenericSegmentConverterTests
{
    [Fact]
    public void Read_WithValidPIDSegment_ShouldParseCorrectly()
    {

        var segment = "PID|1||12345||DOE^JOHN||19800101|M".AsSpan();
        var reader = new SegmentReader(segment);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<PID>();
        var options = SerializerOptions.Default;


        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeOfType<PID>();
    }

    [Fact]
    public void Read_WithValidMSHSegment_ShouldHandleEncodingChars()
    {

        var segment = "MSH|^~\\&|SEND|FAC|REC|FAC|20230101||ADT^A01|1|P|2.5||".AsSpan();
        var reader = new SegmentReader(segment);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<MSH>();
        var options = SerializerOptions.Default;


        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        var msh = result.Value as MSH;
        msh.ShouldNotBeNull();
        msh.SendingApplication.ShouldBe("SEND");
    }

    [Fact]
    public void Read_WithEmptyFields_ShouldHandleGracefully()
    {

        var segment = "PID||||||||".AsSpan();
        var reader = new SegmentReader(segment);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<PID>();
        var options = SerializerOptions.Default;


        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Write_WithValidSegment_ShouldSerializeCorrectly()
    {

        var msh = new MSH
        {
            SendingApplication = "APP",
            SendingFacility = "FAC",
            MessageControlID = "123"
        };
        var writer = new SegmentWriter(1024);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<MSH>();
        var options = SerializerOptions.Default;


        converter.Write(msh, ref writer, in delimiters, options);
        var result = writer.ToString();


        result.ShouldStartWith("MSH|");
        result.ShouldContain("APP");
        result.ShouldContain("123");
    }

    [Fact]
    public void Write_WithMSHSegment_ShouldIncludeEncodingChars()
    {

        var msh = new MSH
        {
            SendingApplication = "APP",
            MessageControlID = "1"
        };
        var writer = new SegmentWriter(1024);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<MSH>();
        var options = SerializerOptions.Default;


        converter.Write(msh, ref writer, in delimiters, options);
        var result = writer.ToString();


        result.ShouldStartWith("MSH|^~\\&|");
    }

    [Fact]
    public void Write_WithNonMSHSegment_ShouldNotIncludeEncodingChars()
    {

        var pid = new PID();
        var writer = new SegmentWriter(1024);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<PID>();
        var options = SerializerOptions.Default;


        converter.Write(pid, ref writer, in delimiters, options);
        var result = writer.ToString();


        result.ShouldStartWith("PID|");
        result.ShouldNotContain("^~\\&");
    }

    [Fact]
    public void CanConvert_WithMatchingSegmentId_ShouldReturnTrue()
    {

        var converter = new GenericSegmentConverter<PID>();
        var segmentId = "PID".AsSpan();


        var result = converter.CanConvert(segmentId);


        result.ShouldBeTrue();
    }

    [Fact]
    public void CanConvert_WithNonMatchingSegmentId_ShouldReturnFalse()
    {

        var converter = new GenericSegmentConverter<PID>();
        var segmentId = "MSH".AsSpan();


        var result = converter.CanConvert(segmentId);


        result.ShouldBeFalse();
    }

    [Fact]
    public void SegmentId_ShouldMatchTemplateType()
    {

        var pidConverter = new GenericSegmentConverter<PID>();
        var mshConverter = new GenericSegmentConverter<MSH>();


        pidConverter.SegmentId.ShouldBe("PID");
        mshConverter.SegmentId.ShouldBe("MSH");
    }

    [Fact]
    public void Read_WithVeryLongSegment_ShouldHandleCorrectly()
    {

        var fields = string.Join("|", Enumerable.Range(1, 100).Select(i => $"FIELD{i}"));
        var segment = $"PID|{fields}".AsSpan();
        var reader = new SegmentReader(segment);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<PID>();
        var options = SerializerOptions.Default;


        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Read_WithCustomDelimiters_ShouldUseCorrectDelimiter()
    {

        var segment = "PID$1$$12345$$DOE*JOHN$$19800101$M".AsSpan();
        var reader = new SegmentReader(segment);
        var delimiters = new HL7Delimiters('$', '*', '#', '@', '%');
        var converter = new GenericSegmentConverter<PID>();
        var options = SerializerOptions.Default;


        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Write_ThenRead_ShouldRoundTrip()
    {

        var original = new MSH
        {
            SendingApplication = "TESTAPP",
            SendingFacility = "TESTFAC",
            MessageControlID = "CTRL123",
            ProcessingID = "P"
        };
        var converter = new GenericSegmentConverter<MSH>();
        var delimiters = HL7Delimiters.Default;
        var options = SerializerOptions.Default;


        var writer = new SegmentWriter(1024);
        converter.Write(original, ref writer, in delimiters, options);
        var serialized = writer.ToString();


        var reader = new SegmentReader(serialized.AsSpan());
        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeTrue();
        var deserialized = result.Value as MSH;
        deserialized.ShouldNotBeNull();
        deserialized.SendingApplication.ShouldBe("TESTAPP");
        deserialized.MessageControlID.ShouldBe("CTRL123");
    }

    [Fact]
    public void Read_WithMalformedSegment_ShouldReturnError()
    {
        var segment = "".AsSpan();
        var reader = new SegmentReader(segment);
        var delimiters = HL7Delimiters.Default;
        var converter = new GenericSegmentConverter<PID>();
        var options = SerializerOptions.Default;


        var result = converter.Read(ref reader, in delimiters, options);


        result.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Write_WithWrongSegmentType_ShouldThrow()
    {

        var pid = new PID();
        var converter = new GenericSegmentConverter<MSH>(); // MSH converter
        var options = SerializerOptions.Default;


        Should.Throw<ArgumentException>(() =>
        {
            var writer = new SegmentWriter(1024);
            var delimiters = HL7Delimiters.Default;
            converter.Write(pid, ref writer, in delimiters, options);
        });
    }

    [Fact]
    public void Read_WithAllSegmentTypes_ShouldWorkForEach()
    {

        var delimiters = HL7Delimiters.Default;
        var options = SerializerOptions.Default;

        // Test MSH
        var mshReader = new SegmentReader("MSH|^~\\&|APP|FAC|REC|RECFAC|20230101||ADT^A01|1|P|2.5||".AsSpan());
        var mshConverter = new GenericSegmentConverter<MSH>();
        var mshResult = mshConverter.Read(ref mshReader, in delimiters, options);
        mshResult.IsSuccess.ShouldBeTrue();

        // Test PID
        var pidReader = new SegmentReader("PID|1||12345||DOE^JOHN||19800101|M".AsSpan());
        var pidConverter = new GenericSegmentConverter<PID>();
        var pidResult = pidConverter.Read(ref pidReader, in delimiters, options);
        pidResult.IsSuccess.ShouldBeTrue();

        // Test PV1
        var pv1Reader = new SegmentReader("PV1|1|I|WARD||||".AsSpan());
        var pv1Converter = new GenericSegmentConverter<PV1>();
        var pv1Result = pv1Converter.Read(ref pv1Reader, in delimiters, options);
        pv1Result.IsSuccess.ShouldBeTrue();

        // Test AL1
        var al1Reader = new SegmentReader("AL1|1|DA|PENICILLIN|SV|RASH".AsSpan());
        var al1Converter = new GenericSegmentConverter<AL1>();
        var al1Result = al1Converter.Read(ref al1Reader, in delimiters, options);
        al1Result.IsSuccess.ShouldBeTrue();

        // Test DG1
        var dg1Reader = new SegmentReader("DG1|1|I10|E11.9||20230101|A|".AsSpan());
        var dg1Converter = new GenericSegmentConverter<DG1>();
        var dg1Result = dg1Converter.Read(ref dg1Reader, in delimiters, options);
        dg1Result.IsSuccess.ShouldBeTrue();
    }
}
