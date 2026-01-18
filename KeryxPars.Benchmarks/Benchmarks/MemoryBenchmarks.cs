using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using KeryxPars.Benchmarks.Data;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.Benchmarks.Benchmarks;

/// <summary>
/// Benchmarks focused on memory allocation patterns.
/// Demonstrates the lazy initialization improvements.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class MemoryBenchmarks
{
    private string _minimalMessage = null!;
    private string _simpleMessage = null!;
    private string _largeMessage = null!;

    [GlobalSetup]
    public void Setup()
    {
        _minimalMessage = TestMessages.MinimalMessage;
        _simpleMessage = TestMessages.SimpleADT;
        _largeMessage = TestMessages.LargeMessage;
    }

    [Benchmark(Description = "Minimal Message (MSH only)")]
    [BenchmarkCategory("Memory", "Allocation")]
    public object ParseMinimalMessage()
    {
        var result = HL7Serializer.Deserialize(_minimalMessage);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Simple Message (MSH, EVN, PID, PV1)")]
    [BenchmarkCategory("Memory", "Allocation")]
    public object ParseSimpleMessage()
    {
        var result = HL7Serializer.Deserialize(_simpleMessage);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Large Message (All segments)")]
    [BenchmarkCategory("Memory", "Allocation")]
    public object ParseLargeMessage()
    {
        var result = HL7Serializer.Deserialize(_largeMessage);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Parse and Access MSH only")]
    [BenchmarkCategory("Memory", "Access")]
    public string? AccessMshOnly()
    {
        var result = HL7Serializer.Deserialize(_simpleMessage);
        if (result.IsSuccess)
        {
            var message = result.Value;
            return message.Msh?.MessageControlID;
        }
        return null;
    }

    [Benchmark(Description = "Parse and Access All Segments")]
    [BenchmarkCategory("Memory", "Access")]
    public (string?, string?, string?, int, int, int) AccessAllSegments()
    {
        var result = HL7Serializer.Deserialize(_largeMessage);
        if (result.IsSuccess)
        {
            var message = result.Value;
            return (
                message.Msh?.MessageControlID.Value,
                message.Pid?.PatientName?[0].GetFormattedName(NameFormat.GivenFamily) ?? string.Empty,
                message.Pv1?.PatientClass.Value,
                message.Allergies.Count,
                message.Diagnoses.Count,
                message.Insurance.Count
            );
        }
        return (null, null, null, 0, 0, 0);
    }

    [Benchmark(Description = "100 Messages - Minimal")]
    [BenchmarkCategory("Memory", "Batch")]
    public int ParseBatchMinimal()
    {
        int count = 0;
        for (int i = 0; i < 100; i++)
        {
            var result = HL7Serializer.Deserialize(_minimalMessage);
            if (result.IsSuccess) count++;
        }
        return count;
    }

    [Benchmark(Description = "100 Messages - Large")]
    [BenchmarkCategory("Memory", "Batch")]
    public int ParseBatchLarge()
    {
        int count = 0;
        for (int i = 0; i < 100; i++)
        {
            var result = HL7Serializer.Deserialize(_largeMessage);
            if (result.IsSuccess) count++;
        }
        return count;
    }
}
