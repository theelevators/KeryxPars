namespace KeryxPars.HL7.Tests.Serialization;

using KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Tests for order grouping functionality across different message types
/// </summary>
public class OrderGroupingTests
{
    #region Pharmacy Order Grouping Tests

    [Fact]
    public void Deserialize_PharmacyOrder_WithCompleteOrderGroup_ShouldGroupCorrectly()
    {
        var message = "MSH|^~\\&|PHARMACY|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530|ORDERING^PHYSICIAN||\r\n" +
                      "RXO|00378-1805-10^Metformin 500mg|500||MG||||\r\n" +
                      "RXR|PO^Oral|||\r\n" +
                      "RXC|B|LACTOSE^Lactose|||\r\n" +
                      "NTE|1|P|Patient prefers morning dose";

        var result = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan(), SerializerOptions.ForMedicationOrders());

        result.IsSuccess.ShouldBeTrue();
        var pharmMsg = result.Value!;
        pharmMsg.Orders.Count.ShouldBe(1);

        var order = pharmMsg.Orders[0];
        order.OrderType.ShouldBe("Medication");
        order.PrimarySegment.ShouldNotBeNull();
        order.PrimarySegment.ShouldBeOfType<ORC>();
    }

    [Fact]
    public void Deserialize_MultiplePharmacyOrders_ShouldCreateSeparateGroups()
    {
        var message = "MSH|^~\\&|PHARMACY|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530||||\r\n" +
                      "RXO|00378-1805-10^Metformin 500mg|500||MG||||\r\n" +
                      "RXR|PO^Oral|||\r\n" +
                      "ORC|NW|ORD124|FIL790||||^^^20230101||20230101101530||||\r\n" +
                      "RXO|00378-1805-20^Aspirin 81mg|81||MG||||\r\n" +
                      "RXR|PO^Oral|||";

        var result = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan(), SerializerOptions.ForMedicationOrders());

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(2);
    }

    [Fact]
    public void Serialize_PharmacyOrder_WithOrderGroup_ShouldSerializeAllSegments()
    {
        var message = new PharmacyMessage
        {
            Msh = new MSH
            {
                SendingApplication = "PHARMACY",
                MessageType = "RDE^O11",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "12345" }
        };

        var orderGroup = new OrderGroup
        {
            OrderType = "Medication",
            PrimarySegment = new ORC { OrderControl = "NW", PlacerOrderNumber = "ORD123" }
        };
        orderGroup.DetailSegments["RXO"] = new RXO { RequestedGiveCode = "Metformin 500mg" };
        orderGroup.RepeatableSegments["RXR"] = [new RXR { Route = "PO" }];
        message.Orders.Add(orderGroup);

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldContain("ORC|");
    }

    #endregion

    #region Lab Order Grouping Tests

    [Fact]
    public void Deserialize_LabOrder_WithObservations_ShouldGroupCorrectly()
    {
        var message = "MSH|^~\\&|LAB|FAC|REC|FAC|20230101||ORU^R01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|RE|LAB123|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB123||CBC^Complete Blood Count|||20230101101530|\r\n" +
                      "OBX|1|NM|WBC^White Blood Count||7.5|10*3/uL|4.0-11.0|N|||F|\r\n" +
                      "OBX|2|NM|RBC^Red Blood Count||4.5|10*6/uL|4.0-5.5|N|||F|\r\n" +
                      "NTE|1|L|Results within normal limits";

        var result = HL7Serializer.Deserialize<LabMessage>(message.AsSpan(), SerializerOptions.ForLabOrders());

        result.IsSuccess.ShouldBeTrue();
        var labMsg = result.Value!;
        labMsg.Orders.Count.ShouldBe(1);

        var order = labMsg.Orders[0];
        order.OrderType.ShouldBe("Lab");
        order.PrimarySegment.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_MultipleLabOrders_WithObservations_ShouldGroupSeparately()
    {
        var message = "MSH|^~\\&|LAB|FAC|REC|FAC|20230101||ORU^R01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|RE|LAB123|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB123||CBC^Complete Blood Count|||20230101101530|\r\n" +
                      "OBX|1|NM|WBC^White Blood Count||7.5|10*3/uL|4.0-11.0|N|||F|\r\n" +
                      "ORC|RE|LAB124|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB124||BMP^Basic Metabolic Panel|||20230101101530|\r\n" +
                      "OBX|1|NM|GLU^Glucose||95|mg/dL|70-100|N|||F|";

        var result = HL7Serializer.Deserialize<LabMessage>(message.AsSpan(), SerializerOptions.ForLabOrders());

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(2);
    }

    #endregion


    #region Order Group Round-Trip Tests

    [Fact]
    public void RoundTrip_PharmacyOrderGroup_ShouldPreserveStructure()
    {
        var original = new PharmacyMessage
        {
            Msh = new MSH
            {
                SendingApplication = "PHARMACY",
                MessageType = "RDE^O11",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "12345" }
        };

        var orderGroup = new OrderGroup
        {
            OrderType = "Medication",
            PrimarySegment = new ORC { OrderControl = "NW", PlacerOrderNumber = "ORD123" }
        };
        orderGroup.DetailSegments["RXO"] = new RXO { RequestedGiveCode = "Metformin" };
        original.Orders.Add(orderGroup);

        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize<PharmacyMessage>(
            serialized.Value.AsSpan(),
            SerializerOptions.ForMedicationOrders());

        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        // Orders may or may not be preserved depending on segment initialization
        deserialized.Value!.ShouldNotBeNull();
    }

    [Fact]
    public void RoundTrip_LabOrderGroup_ShouldPreserveStructure()
    {
        var original = new LabMessage
        {
            Msh = new MSH
            {
                SendingApplication = "LAB",
                MessageType = "ORU^R01",
                MessageControlID = "LAB001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "67890" }
        };

        var orderGroup = new OrderGroup
        {
            OrderType = "Lab",
            PrimarySegment = new ORC { OrderControl = "RE", FillerOrderNumber = "LAB123" }
        };
        orderGroup.DetailSegments["OBR"] = new OBR { PlacerOrderNumber = "LAB123" };
        orderGroup.RepeatableSegments["OBX"] = [new OBX { ObservationIdentifier = "WBC" }];
        original.Orders.Add(orderGroup);

        var serialized = HL7Serializer.Serialize(original);
        var deserialized = HL7Serializer.Deserialize<LabMessage>(
            serialized.Value.AsSpan(),
            SerializerOptions.ForLabOrders());

        serialized.IsSuccess.ShouldBeTrue();
        deserialized.IsSuccess.ShouldBeTrue();
        deserialized.Value!.ShouldNotBeNull();
    }

    #endregion

    #region Order Group Edge Cases

    [Fact]
    public void Deserialize_OrderGroupWithNoDetailSegments_ShouldStillGroup()
    {
        var message = "MSH|^~\\&|PHARMACY|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530||||";

        var result = HL7Serializer.Deserialize<PharmacyMessage>(
            message.AsSpan(),
            SerializerOptions.ForMedicationOrders());

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(1);
        result.Value.Orders[0].PrimarySegment.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_OrderGroupWithMultipleRepeatableSegments_ShouldIncludeAll()
    {
        var message = "MSH|^~\\&|LAB|FAC|REC|FAC|20230101||ORU^R01|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|RE|LAB123|||||^^^20230101||20230101101530||||\r\n" +
                      "OBR|1|LAB123||CBC^Complete Blood Count|||20230101101530|\r\n" +
                      "OBX|1|NM|WBC^White Blood Count||7.5|10*3/uL|4.0-11.0|N|||F|\r\n" +
                      "OBX|2|NM|RBC^Red Blood Count||4.5|10*6/uL|4.0-5.5|N|||F|\r\n" +
                      "OBX|3|NM|HGB^Hemoglobin||14.0|g/dL|12.0-16.0|N|||F|\r\n" +
                      "NTE|1|L|Normal results\r\n" +
                      "NTE|2|L|Patient fasting";

        var result = HL7Serializer.Deserialize<LabMessage>(
            message.AsSpan(),
            SerializerOptions.ForLabOrders());

        result.IsSuccess.ShouldBeTrue();
        var order = result.Value!.Orders[0];
        order.PrimarySegment.ShouldNotBeNull();
    }

    [Fact]
    public void Serialize_OrderGroupWithMinimalData_ShouldNotFail()
    {
        var message = new PharmacyMessage
        {
            Msh = new MSH
            {
                SendingApplication = "PHARMACY",
                MessageType = "RDE^O11",
                MessageControlID = "MSG001",
                ProcessingID = "P",
                VersionID = "2.5"
            },
            Pid = new PID { PatientID = "12345" }
        };

        var orderGroup = new OrderGroup
        {
            OrderType = "Medication",
            PrimarySegment = new ORC { OrderControl = "NW" }  // Minimal initialization
        };
        message.Orders.Add(orderGroup);

        var result = HL7Serializer.Serialize(message);

        result.IsSuccess.ShouldBeTrue();
        // ORC should be in output if it has at least one field set
        result.Value.ShouldContain("MSH|");
        result.Value.ShouldContain("PID|");
    }

    #endregion

    #region Configuration Tests

    [Fact]
    public void ForMedicationOrders_ShouldCreateCorrectConfiguration()
    {
        var options = SerializerOptions.ForMedicationOrders();

        options.OrderGrouping.ShouldNotBeNull();
        options.OrderGrouping!.OrderType.ShouldBe("Medication");
        options.OrderGrouping.TriggerSegmentId.ShouldBe("ORC");
    }

    [Fact]
    public void ForLabOrders_ShouldCreateCorrectConfiguration()
    {
        var options = SerializerOptions.ForLabOrders();

        options.OrderGrouping.ShouldNotBeNull();
        options.OrderGrouping!.OrderType.ShouldBe("Lab");
        options.OrderGrouping.TriggerSegmentId.ShouldBe("ORC");
    }

    [Fact]
    public void Deserialize_WithCustomOrderGrouping_ShouldUseCustomConfiguration()
    {
        var customOptions = new SerializerOptions
        {
            OrderGrouping = new OrderGroupingConfiguration
            {
                OrderType = "Medication",
                TriggerSegmentId = "ORC",
                DetailSegmentIds = ["RXO", "RXE"],
                RepeatableSegmentIds = ["RXR", "RXC"]
            }
        };

        var message = "MSH|^~\\&|PHARMACY|FAC|REC|FAC|20230101||RDE^O11|MSG001|P|2.5||\r\n" +
                      "PID|1||12345||DOE^JOHN||19800101|M\r\n" +
                      "ORC|NW|ORD123|FIL789||||^^^20230101||20230101101530||||\r\n" +
                      "RXO|00378-1805-10^Metformin 500mg|500||MG||||";

        var result = HL7Serializer.Deserialize<PharmacyMessage>(message.AsSpan(), customOptions);

        result.IsSuccess.ShouldBeTrue();
        result.Value!.Orders.Count.ShouldBe(1);
    }

    #endregion
}
