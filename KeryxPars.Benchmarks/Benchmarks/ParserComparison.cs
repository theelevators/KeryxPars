using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using KeryxPars.Benchmarks.Data;
using KeryxPars.HL7.Serialization;
using HL7.Dotnetcore;
using NHapi.Base.Parser;
using NHapi.Base.Model;

namespace KeryxPars.Benchmarks.Benchmarks;

/// <summary>
/// Compares KeryxPars against popular third-party HL7 parsers.
/// 
/// Parsers tested:
/// - KeryxPars (our implementation)
/// - HL7-dotnetcore (popular library)
/// - NHapi (enterprise-grade library)
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ParserComparison
{
    private string _simpleMessage = null!;
    private string _adtWithClinical = null!;
    private string _medicationOrder = null!;
    private string _largeMessage = null!;

    [GlobalSetup]
    public void Setup()
    {
        _simpleMessage = TestMessages.SimpleADT;
        _adtWithClinical = TestMessages.ADTWithClinical;
        _medicationOrder = TestMessages.MedicationOrder;
        _largeMessage = TestMessages.LargeMessage;
    }

    #region Simple Message Benchmarks

    [Benchmark(Description = "KeryxPars - Simple ADT")]
    [BenchmarkCategory("Simple", "Parse")]
    public object KeryxPars_SimpleMessage()
    {
        var result = HL7Serializer.Deserialize(_simpleMessage);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "HL7-dotnetcore - Simple ADT")]
    [BenchmarkCategory("Simple", "Parse")]
    public object HL7DotNetCore_SimpleMessage()
    {
        var message = new Message(_simpleMessage);
        message.ParseMessage();
        return message;
    }

    [Benchmark(Description = "NHapi - Simple ADT")]
    [BenchmarkCategory("Simple", "Parse")]
    public IMessage NHapi_SimpleMessage()
    {
        var parser = new PipeParser();
        return parser.Parse(_simpleMessage);
    }

    #endregion

    #region ADT with Clinical Data Benchmarks

    [Benchmark(Description = "KeryxPars - ADT + Clinical")]
    [BenchmarkCategory("Clinical", "Parse")]
    public object KeryxPars_ClinicalMessage()
    {
        var result = HL7Serializer.Deserialize(_adtWithClinical);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "HL7-dotnetcore - ADT + Clinical")]
    [BenchmarkCategory("Clinical", "Parse")]
    public object HL7DotNetCore_ClinicalMessage()
    {
        var message = new Message(_adtWithClinical);
        message.ParseMessage();
        return message;
    }

    [Benchmark(Description = "NHapi - ADT + Clinical")]
    [BenchmarkCategory("Clinical", "Parse")]
    public IMessage NHapi_ClinicalMessage()
    {
        var parser = new PipeParser();
        return parser.Parse(_adtWithClinical);
    }

    #endregion

    #region Medication Order Benchmarks

    [Benchmark(Description = "KeryxPars - Medication Order")]
    [BenchmarkCategory("MedOrder", "Parse")]
    public object KeryxPars_MedicationOrder()
    {
        var result = HL7Serializer.Deserialize(_medicationOrder);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "HL7-dotnetcore - Medication Order")]
    [BenchmarkCategory("MedOrder", "Parse")]
    public object HL7DotNetCore_MedicationOrder()
    {
        var message = new Message(_medicationOrder);
        message.ParseMessage();
        return message;
    }

    [Benchmark(Description = "NHapi - Medication Order")]
    [BenchmarkCategory("MedOrder", "Parse")]
    public IMessage NHapi_MedicationOrder()
    {
        var parser = new PipeParser();
        return parser.Parse(_medicationOrder);
    }

    #endregion

    #region Large Message Benchmarks

    [Benchmark(Description = "KeryxPars - Large Message")]
    [BenchmarkCategory("Large", "Parse")]
    public object KeryxPars_LargeMessage()
    {
        var result = HL7Serializer.Deserialize(_largeMessage);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "HL7-dotnetcore - Large Message")]
    [BenchmarkCategory("Large", "Parse")]
    public object HL7DotNetCore_LargeMessage()
    {
        var message = new Message(_largeMessage);
        message.ParseMessage();
        return message;
    }

    [Benchmark(Description = "NHapi - Large Message")]
    [BenchmarkCategory("Large", "Parse")]
    public IMessage NHapi_LargeMessage()
    {
        var parser = new PipeParser();
        return parser.Parse(_largeMessage);
    }

    #endregion
}
