using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using KeryxPars.Benchmarks.Data;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Extensions;
using KeryxPars.HL7.Serialization.Configuration;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.Benchmarks.Benchmarks;

/// <summary>
/// Benchmarks for deserialization performance with different configurations.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class DeserializationBenchmarks
{
    private string _medicationOrder = null!;
    private string _immunization = null!;
    private string _complexOrder = null!;
    private SerializerOptions _defaultOptions = null!;
    private SerializerOptions _medicationOptions = null!;
    private SerializerOptions _failFastOptions = null!;

    [GlobalSetup]
    public void Setup()
    {
        _medicationOrder = TestMessages.MedicationOrder;
        _immunization = TestMessages.ImmunizationMessage;
        _complexOrder = TestMessages.ComplexMedicationOrder;

        _defaultOptions = SerializerOptions.Default;
        _medicationOptions = SerializerOptions.ForMedicationOrders();
        
        _failFastOptions = new SerializerOptions
        {
            ErrorHandling = ErrorHandlingStrategy.FailFast
        };
    }

    [Benchmark(Description = "Default Options")]
    [BenchmarkCategory("Options", "Deserialization")]
    public object ParseWithDefaultOptions()
    {
        var result = HL7Serializer.Deserialize(_medicationOrder, _defaultOptions);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Medication Options")]
    [BenchmarkCategory("Options", "Deserialization")]
    public object ParseWithMedicationOptions()
    {
        var result = HL7Serializer.Deserialize(_medicationOrder, _medicationOptions);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "FailFast Options")]
    [BenchmarkCategory("Options", "Deserialization")]
    public object ParseWithFailFastOptions()
    {
        var result = HL7Serializer.Deserialize(_medicationOrder, _failFastOptions);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Baseline = true, Description = "Simple Medication Order")]
    [BenchmarkCategory("MessageType", "Deserialization")]
    public object ParseSimpleMedicationOrder()
    {
        var result = HL7Serializer.Deserialize(_medicationOrder);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Complex Medication Order")]
    [BenchmarkCategory("MessageType", "Deserialization")]
    public object ParseComplexMedicationOrder()
    {
        var result = HL7Serializer.Deserialize(_complexOrder);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Immunization Message")]
    [BenchmarkCategory("MessageType", "Deserialization")]
    public object ParseImmunizationMessage()
    {
        var result = HL7Serializer.Deserialize(_immunization);
        return result.IsSuccess ? result.Value : result.Error!;
    }

    [Benchmark(Description = "Parse + Extract Order")]
    [BenchmarkCategory("Workflow", "Deserialization")]
    public object ParseAndExtractOrder()
    {
        var result = HL7Serializer.Deserialize(_medicationOrder);
        if (result.IsSuccess)
        {
            var message = result.Value;
            return message.Orders.Count > 0 ? message.Orders[0] : null!;
        }
        return result.Error!;
    }

    [Benchmark(Description = "Parse + Navigate Segments")]
    [BenchmarkCategory("Workflow", "Deserialization")]
    public (string?, string?, int) ParseAndNavigate()
    {
        var result = HL7Serializer.Deserialize(_complexOrder);
        if (result.IsSuccess)
        {
            var message = result.Value;
            var patientName = message.Pid?.PatientName?[0].GetFormattedName(NameFormat.GivenFamily) ?? string.Empty;
            var orderControl = message.Orders.Count > 0 ? message.Orders[0].AsMedicationOrder().Orc?.OrderControl.Value : null;
            var orderCount = message.Orders.Count;
            return (patientName, orderControl, orderCount);
        }
        return (null, null, 0);
    }
}
