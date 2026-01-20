using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace KeryxPars.HL7.Tests.MessageViewer;

public class ViewerOrderGroupingTests
{
    private readonly ITestOutputHelper _output;

    public ViewerOrderGroupingTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ComprehensiveMessage_WithLabOrders_ShouldGroupMultipleOrders()
    {
        // This simulates what the MessageViewer does:
        // 1. Detects it's a lab message (ORU)
        // 2. Uses OrderGroupingConfiguration.Lab
        // 3. But parses with HL7ComprehensiveMessage

        var message = "MSH|^~\\&|LAB|FAC|REC|FAC|20230101||ORU^R01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|RE|LAB123|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB123||CBC^Complete Blood Count|||20230101101530|\r\n" +
                      "OBX|1|NM|WBC^White Blood Count||7.5|10*3/uL|4.0-11.0|N|||F|\r\n" +
                      "ORC|RE|LAB124|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB124||BMP^Basic Metabolic Panel|||20230101101530|\r\n" +
                      "OBX|1|NM|GLU^Glucose||95|mg/dL|70-100|N|||F|";

        var options = new SerializerOptions
        {
            MessageType = typeof(HL7ComprehensiveMessage),
            OrderGrouping = OrderGroupingConfiguration.Lab
        };

        var result = HL7Serializer.Deserialize<HL7ComprehensiveMessage>(message, options);

        _output.WriteLine($"Parse successful: {result.IsSuccess}");
        _output.WriteLine($"Orders count: {result.Value?.Orders?.Count ?? -1}");

        if (result.Value?.Orders != null)
        {
            for (int i = 0; i < result.Value.Orders.Count; i++)
            {
                var detailCount = result.Value.Orders[i].DetailSegments.Count;
                var repeatableCount = result.Value.Orders[i].RepeatableSegments.Values.Sum(list => list.Count);
                var totalSegments = detailCount + repeatableCount + (result.Value.Orders[i].PrimarySegment != null ? 1 : 0);
                _output.WriteLine($"Order {i + 1}: Type={result.Value.Orders[i].OrderType}, Segments={totalSegments}");
            }
        }

        result.IsSuccess.ShouldBeTrue();
        var comprehensiveMsg = result.Value!;
        
        comprehensiveMsg.Orders.ShouldNotBeNull();
        comprehensiveMsg.Orders.Count.ShouldBe(2, "Should have 2 separate order groups");
        
        // First order should have OBR in details
        comprehensiveMsg.Orders[0].OrderType.ShouldBe("Lab");
        comprehensiveMsg.Orders[0].DetailSegments.ShouldContainKey("OBR");
        
        // Second order should have OBR in details
        comprehensiveMsg.Orders[1].OrderType.ShouldBe("Lab");
        comprehensiveMsg.Orders[1].DetailSegments.ShouldContainKey("OBR");

    }

    [Fact]
    public void ComprehensiveMessage_WithPharmacyOrders_ShouldGroupMultipleOrders()
    {
        var message = "MSH|^~\\&|PHARMACY|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530||||\r\n" +
                      "RXO|00378-1805-10^Metformin 500mg|500||MG||||\r\n" +
                      "ORC|NW|ORD124|FIL790||||^^^20230101||20230101101530||||\r\n" +
                      "RXO|00002-1975-33^Lisinopril 10mg|10||MG||||";

        var options = new SerializerOptions
        {
            MessageType = typeof(HL7ComprehensiveMessage),
            OrderGrouping = OrderGroupingConfiguration.Medication
        };

        var result = HL7Serializer.Deserialize<HL7ComprehensiveMessage>(message, options);

        _output.WriteLine($"Parse successful: {result.IsSuccess}");
        _output.WriteLine($"Orders count: {result.Value?.Orders?.Count ?? -1}");

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(2, "Should have 2 separate medication orders");
    }
}
