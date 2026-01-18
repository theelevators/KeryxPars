using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using KeryxPars.Benchmarks.Data;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.DataTypes.Composite;

namespace KeryxPars.Benchmarks.Benchmarks;

/// <summary>
/// Benchmarks for serialization (writing) performance.
/// </summary>
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class SerializationBenchmarks
{
    private HL7Message _simpleMessage = null!;
    private HL7Message _messageWithOrders = null!;
    private HL7Message _complexMessage = null!;

    [GlobalSetup]
    public void Setup()
    {
        // Create simple message
        _simpleMessage = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "TEST_APP",
                SendingFacility = "TEST_FAC",
                ReceivingApplication = "REC_APP",
                ReceivingFacility = "REC_FAC",
                DateTimeOfMessage = "20230101120000",
                MessageType = "ADT^A01",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID
            {
                SetID = "1",
                PatientIdentifierList = new[] { (CX)"123456" },
                PatientName = new[] { (XPN)"DOE^JOHN^A" },
                DateTimeofBirth = "19800101",
                AdministrativeSex = "M"
            }
        };

        // Create message with orders
        _messageWithOrders = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "TEST_APP",
                SendingFacility = "TEST_FAC",
                MessageType = "RDE^O11",
                MessageControlID = "MSG002",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID
            {
                PatientIdentifierList = new[] { (CX)"987654" },
                PatientName = new[] { (XPN)"SMITH^JANE^M" }
            }
        };

        var order = new OrderGroup
        {
            OrderType = "Medication",
            PrimarySegment = new ORC
            {
                OrderControl = "NW",
                PlacerOrderNumber = "ORD123",
                FillerOrderNumber = "FIL456"
            }
        };
        order.DetailSegments["RXE"] = new RXE
        {
            GiveCode = "00378-1805-10^Metformin^NDC",
            GiveAmountMinimum = "500",
            GiveUnits = "MG"
        };
        _messageWithOrders.Orders.Add(order);

        // Create complex message
        _complexMessage = new HL7Message
        {
            Msh = new MSH
            {
                SendingApplication = "COMPLEX_APP",
                SendingFacility = "COMPLEX_FAC",
                MessageType = "ADT^A01",
                MessageControlID = "MSG003"
            },
            Pid = new PID
            {
                PatientName = new[] { (XPN)"COMPLEX^PATIENT^TEST" },
                DateTimeofBirth = "19900101"
            },
            Pv1 = new PV1
            {
                PatientClass = "I",
                AssignedPatientLocation = "ICU^201^A"
            }
        };

        _complexMessage.Allergies.Add(new AL1
        {
            SetID = "1",
            AllergenTypeCode = "DA",
            AllergenCodeMnemonicDescription = "PENICILLIN",
            AllergySeverityCode = "SV"
        });

        _complexMessage.Diagnoses.Add(new DG1
        {
            SetID = "1",
            DiagnosisCode = "E11.9",
            DiagnosisDescription = "Type 2 diabetes"
        });
    }

    [Benchmark(Baseline = true, Description = "Simple Message")]
    [BenchmarkCategory("Serialize", "Size")]
    public string SerializeSimple()
    {
        var result = HL7Serializer.Serialize(_simpleMessage);
        return result.IsSuccess ? result.Value : string.Empty;
    }

    [Benchmark(Description = "Message with Orders")]
    [BenchmarkCategory("Serialize", "Size")]
    public string SerializeWithOrders()
    {
        var result = HL7Serializer.Serialize(_messageWithOrders);
        return result.IsSuccess ? result.Value : string.Empty;
    }

    [Benchmark(Description = "Complex Message")]
    [BenchmarkCategory("Serialize", "Size")]
    public string SerializeComplex()
    {
        var result = HL7Serializer.Serialize(_complexMessage);
        return result.IsSuccess ? result.Value : string.Empty;
    }

    [Benchmark(Description = "Roundtrip - Parse then Serialize")]
    [BenchmarkCategory("Serialize", "Roundtrip")]
    public string RoundtripSimple()
    {
        var parseResult = HL7Serializer.Deserialize(TestMessages.SimpleADT);
        if (parseResult.IsSuccess)
        {
            var serializeResult = HL7Serializer.Serialize(parseResult.Value);
            return serializeResult.IsSuccess ? serializeResult.Value : string.Empty;
        }
        return string.Empty;
    }

    [Benchmark(Description = "Roundtrip - Medication Order")]
    [BenchmarkCategory("Serialize", "Roundtrip")]
    public string RoundtripMedicationOrder()
    {
        var parseResult = HL7Serializer.Deserialize(TestMessages.MedicationOrder);
        if (parseResult.IsSuccess)
        {
            var serializeResult = HL7Serializer.Serialize(parseResult.Value);
            return serializeResult.IsSuccess ? serializeResult.Value : string.Empty;
        }
        return string.Empty;
    }
}
