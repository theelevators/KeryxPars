namespace KeryxPars.HL7.Serialization;

using global::KeryxPars.Core.Models;
using global::KeryxPars.HL7.Definitions;
using global::KeryxPars.HL7.Segments;
using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Serialization.Configuration;
using System.Buffers;

/// <summary>
/// High-performance static HL7 serializer using span-based parsing.
/// </summary>
public static class HL7Serializer
{
    private static readonly SearchValues<char> s_lineBreaks = SearchValues.Create("\r\n");

    public static Result<HL7Message, HL7Error[]> Deserialize(ReadOnlySpan<char> message, SerializerOptions? options = null)
    {
        var opts = options ?? SerializerOptions.Default;
        var context = new DeserializationContext(opts);

        if (!message[..3].SequenceEqual("MSH"))
        {
            return new HL7Error[] { new(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Invalid message start") };
        }

        if (!HL7Delimiters.TryParse(message, out var delimiters, out var delimiterError))
        {
            context.AddError(delimiterError!);
            return context.ToResult();
        }

        var lineEnumerator = new LineEnumerator(message);

        OrderGroup? currentOrder = null;
        bool mshProcessed = false, evnProcessed = false, pidProcessed = false;
        bool pv1Processed = false, pv2Processed = false;

        while (lineEnumerator.MoveNext())
        {
            var line = lineEnumerator.Current;
            if (line.IsEmpty) continue;

            // Get segment ID (first 3 characters)
            var segmentId = line.Length >= 3 ? line[..3] : line;

            var converter = opts.SegmentRegistry.GetConverter(segmentId);

            if (converter == null)
            {
                if (!opts.IgnoreUnknownSegments)
                    context.AddWarning($"Unknown segment: {segmentId.ToString()}");
                continue;
            }

            var reader = new SegmentReader(line);
            var result = converter.Read(ref reader, in delimiters, opts);

            if (!result.IsSuccess)
            {
                context.AddError(result.Error!);
                if (opts.ErrorHandling == ErrorHandlingStrategy.FailFast)
                    return context.ToResult();
                continue;
            }

            // Process segment based on type
            ProcessSegment(result.Value!, context, ref currentOrder,
                          ref mshProcessed, ref evnProcessed, ref pidProcessed,
                          ref pv1Processed, ref pv2Processed, in delimiters, opts);
        }

        // Add last order group if exists
        if (currentOrder != null)
            context.Message.Orders.Add(currentOrder);

        return context.ToResult();
    }

    public static Result<string, HL7Error> Serialize(HL7Message message, HL7Delimiters delimiters = default, SerializerOptions? options = null)
    {
        var opts = options ?? SerializerOptions.Default;
        delimiters = delimiters.AreUninitialized ? HL7Delimiters.Default : delimiters;

        if (message == null)
            return new HL7Error(ErrorSeverity.Error,
                ErrorCode.ApplicationInternalError, "HL7 Message is null");

        var writer = new SegmentWriter(opts.InitialBufferSize);

        // Write MSH segment
        WriteSegmentIfPresent(message.Msh, ref writer, in delimiters, opts);

        // Write other segments
        WriteSegmentIfPresent(message.Evn, ref writer, in delimiters, opts);
        WriteSegmentIfPresent(message.Pid, ref writer, in delimiters, opts);
        WriteSegmentIfPresent(message.Pv1, ref writer, in delimiters, opts);
        WriteSegmentIfPresent(message.Pv2, ref writer, in delimiters, opts);

        // Write repeatable segments
        foreach (var allergy in message.Allergies)
            WriteSegmentIfPresent(allergy, ref writer, in delimiters, opts);

        foreach (var diagnosis in message.Diagnoses)
            WriteSegmentIfPresent(diagnosis, ref writer, in delimiters, opts);

        foreach (var insurance in message.Insurance)
            WriteSegmentIfPresent(insurance, ref writer, in delimiters, opts);

        // Write order groups
        foreach (var orderGroup in message.Orders)
            WriteOrderGroup(orderGroup, ref writer, in delimiters, opts);

        return writer.ToString();
    }

    private static void WriteSegmentIfPresent(
        ISegment? segment,
        ref SegmentWriter writer,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        if (segment == null)
            return;

        var values = segment.GetValues();
        if (values.Length > 1 && values[1..].Any(v => !string.IsNullOrEmpty(v)))
        {
            var converter = options.SegmentRegistry.GetConverter(segment.SegmentId);
            converter?.Write(segment, ref writer, in delimiters, options);
            writer.Write("\r\n");
        }
    }

    private static void WriteOrderGroup(
        OrderGroup orderGroup,
        ref SegmentWriter writer,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        // Write primary segment (e.g., ORC)
        if (orderGroup.PrimarySegment != null)
            WriteSegmentIfPresent(orderGroup.PrimarySegment, ref writer, in delimiters, options);

        // Write detail segments (RXE, RXO, OBR, etc.)
        foreach (var detailSegment in orderGroup.DetailSegments.Values)
            WriteSegmentIfPresent(detailSegment, ref writer, in delimiters, options);

        // Write repeatable segments (RXR, RXC, TQ1, OBX, NTE, etc.)
        foreach (var segmentList in orderGroup.RepeatableSegments.Values)
        {
            foreach (var segment in segmentList)
                WriteSegmentIfPresent(segment, ref writer, in delimiters, options);
        }
    }

    private static void ProcessSegment(
        ISegment segment,
        DeserializationContext context,
        ref OrderGroup? currentOrder,
        ref bool mshProcessed,
        ref bool evnProcessed,
        ref bool pidProcessed,
        ref bool pv1Processed,
        ref bool pv2Processed,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        var segmentId = segment.SegmentId;
        var config = options.OrderGrouping;

        // Check if this segment belongs to an order group
        if (config != null && config.BelongsToOrderGroup(segmentId))
        {
            // Check if this is a trigger segment (starts new order group)
            if (config.IsTriggerSegment(segmentId))
            {
                // Save previous order group
                if (currentOrder != null)
                    context.Message.Orders.Add(currentOrder);

                // Start new order group
                currentOrder = new OrderGroup
                {
                    OrderType = config.OrderType,
                    PrimarySegment = segment
                };
                currentOrder.SegmentStatus[segmentId.ToString()] = true;
            }
            else if (currentOrder != null)
            {
                // Add to current order group
                currentOrder.AddSegment(segment);
            }
            return;
        }

        // Fallback: Handle order segments even without configuration (for backward compatibility)
        switch (segment)
        {
            // Handle non-order segments
            case MSH msh:
                if (mshProcessed)
                {
                    context.AddError(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple MSH segments");
                    return;
                }
                context.Message.Msh = msh;
                context.Message.MessageControlID = msh.MessageControlID;
                mshProcessed = true;

                // Parse event type
                var eventCode = msh.MessageType.Split(delimiters.ComponentSeparator).ElementAtOrDefault(1);
                if (eventCode != null)
                    (context.Message.EventType, context.Message.MessageType) = ParseEventString(eventCode);
                break;

            case EVN evn:
                context.Message.Evn = evn;
                evnProcessed = true;
                break;

            case PID pid:
                context.Message.Pid = pid;
                pidProcessed = true;
                break;

            case PV1 pv1:
                context.Message.Pv1 = pv1;
                pv1Processed = true;
                break;

            case PV2 pv2:
                context.Message.Pv2 = pv2;
                pv2Processed = true;
                break;

            case AL1 al1:
                context.Message.Allergies.Add(al1);
                break;

            case DG1 dg1:
                context.Message.Diagnoses.Add(dg1);
                break;

            case IN1 in1:
                context.Message.Insurance.Add(in1);
                break;
        }
    }

    private static (EventType, IncomingMessageType) ParseEventString(string eventType) =>
        eventType switch
        {
            "A01" or "A04" => (EventType.NewAdmit, IncomingMessageType.ADT),
            "A05" or "A14" => (EventType.Preadmit, IncomingMessageType.ADT),
            "A08" => (EventType.Update, IncomingMessageType.ADT),
            "A03" => (EventType.Discharge, IncomingMessageType.ADT),
            "O01" or "O09" or "O11" => (EventType.MedicationOrder, IncomingMessageType.MedOrder),
            "A10" => (EventType.TrackingTrigger, IncomingMessageType.MedOrder),
            _ => (EventType.Unknown, IncomingMessageType.Others),
        };
}
