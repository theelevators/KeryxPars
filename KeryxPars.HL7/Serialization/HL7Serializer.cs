namespace KeryxPars.HL7.Serialization;

using global::KeryxPars.Core.Models;
using global::KeryxPars.HL7.Contracts;
using global::KeryxPars.HL7.Definitions;
using global::KeryxPars.HL7.Extensions;
using global::KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization.Configuration;
using System.Buffers;

/// <summary>
/// High-performance static HL7 serializer using span-based parsing.
/// </summary>
public static class HL7Serializer
{
    private static readonly SearchValues<char> s_lineBreaks = SearchValues.Create("\r\n");

    /// <summary>
    /// Deserializes an HL7 message into HL7DefaultMessage (backward compatible).
    /// For specialized message types, use Deserialize&lt;T&gt; or pass SerializerOptions with MessageType set.
    /// </summary>
    public static Result<HL7DefaultMessage, HL7Error[]> Deserialize(ReadOnlySpan<char> message, SerializerOptions? options = null)
    {
        var result = DeserializeToInterface(message, options);
        if (!result.IsSuccess)
            return Result<HL7DefaultMessage, HL7Error[]>.Failure(result.Error!);
        
        // Cast or convert to HL7DefaultMessage for backward compatibility
        if (result.Value is HL7DefaultMessage defaultMsg)
            return Result<HL7DefaultMessage, HL7Error[]>.Success(defaultMsg);
        
        // If it's an HL7Message (legacy), it inherits from HL7DefaultMessage
        if (result.Value is HL7Message legacyMsg)
            return Result<HL7DefaultMessage, HL7Error[]>.Success(legacyMsg);
            
        // This shouldn't happen with default options, but handle it gracefully
        return Result<HL7DefaultMessage, HL7Error[]>.Failure(
            new[] { new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, 
                "Unexpected message type returned from deserialization") });
    }

    /// <summary>
    /// Deserializes an HL7 message into a specific message type.
    /// </summary>
    public static Result<T, HL7Error[]> Deserialize<T>(ReadOnlySpan<char> message, SerializerOptions? options = null) 
        where T : class, IHL7Message
    {
        // Override the message type in options if provided
        var opts = options ?? SerializerOptions.Default;
        if (opts.MessageType != typeof(T))
        {
            opts = new SerializerOptions
            {
                SegmentRegistry = opts.SegmentRegistry,
                IgnoreUnknownSegments = opts.IgnoreUnknownSegments,
                ErrorHandling = opts.ErrorHandling,
                ValidationStrategy = opts.ValidationStrategy,
                UseStringPooling = opts.UseStringPooling,
                InitialBufferSize = opts.InitialBufferSize,
                MessageType = typeof(T),
                OrderGrouping = opts.OrderGrouping
            };
        }

        var result = DeserializeToInterface(message, opts);
        if (!result.IsSuccess)
            return Result<T, HL7Error[]>.Failure(result.Error!);

        if (result.Value is T typedMessage)
            return Result<T, HL7Error[]>.Success(typedMessage);

        return Result<T, HL7Error[]>.Failure(
            new[] { new HL7Error(ErrorSeverity.Error, ErrorCode.ApplicationInternalError, 
                $"Expected message type {typeof(T).Name} but got {result.Value?.GetType().Name}") });
    }

    private static Result<IHL7Message, HL7Error[]> DeserializeToInterface(ReadOnlySpan<char> message, SerializerOptions? options = null)
    {
        var opts = options ?? SerializerOptions.Default;
        var context = new DeserializationContext(opts);

        if (message.Length < 3 || !message[..3].SequenceEqual("MSH"))
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
        {
            context.Message.AddOrderGroup(currentOrder);
        }

        return context.ToResult();
    }

    public static Result<string, HL7Error> Serialize(IHL7Message message, HL7Delimiters delimiters = default, SerializerOptions? options = null)
    {
        var opts = options ?? SerializerOptions.Default;
        delimiters = delimiters.AreUninitialized ? HL7Delimiters.Default : delimiters;

        if (message == null)
            return new HL7Error(ErrorSeverity.Error,
                ErrorCode.ApplicationInternalError, "HL7 Message is null");

        var writer = new SegmentWriter(opts.InitialBufferSize);

        // Write common segments
        writer.WriteSegmentIfPresent(message.Msh, in delimiters, opts);
        writer.WriteSegmentIfPresent(message.Evn, in delimiters, opts);
        writer.WriteSegmentIfPresent(message.Pid, in delimiters, opts);

        // Write message type-specific segments using extension methods (DDD pattern)
        message.WriteMessageSpecificSegments(ref writer, in delimiters, opts);

        return writer.ToString();
    }

    /// <summary>
    /// Writes message-specific segments using pattern matching and extension methods.
    /// Follows Domain-Driven Design by delegating to message-specific extension methods.
    /// </summary>
    private static void WriteMessageSpecificSegments(this IHL7Message message, ref SegmentWriter writer, in HL7Delimiters delimiters, SerializerOptions options)
    {
        switch (message)
        {
            case HospiceMessage hospice:
                hospice.WriteSegments(ref writer, in delimiters, options);
                break;
            case PharmacyMessage pharmacy:
                pharmacy.WriteSegments(ref writer, in delimiters, options);
                break;
            case LabMessage lab:
                lab.WriteSegments(ref writer, in delimiters, options);
                break;
            case SchedulingMessage scheduling:
                scheduling.WriteSegments(ref writer, in delimiters, options);
                break;
            case FinancialMessage financial:
                financial.WriteSegments(ref writer, in delimiters, options);
                break;
            case DietaryMessage dietary:
                dietary.WriteSegments(ref writer, in delimiters, options);
                break;
            case HL7DefaultMessage defaultMsg:
                defaultMsg.WriteSegments(ref writer, in delimiters, options);
                break;
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
        // Skip if config is None (empty trigger)
        if (config != null && !string.IsNullOrEmpty(config.TriggerSegmentId) && config.BelongsToOrderGroup(segmentId))
        {
            HandleOrderGroupSegment(segment, segmentId, config, context, ref currentOrder);
            return;
        }


        // Handle non-order segments using polymorphic AddSegment
        switch (segment)
        {
            case MSH msh:
                HandleMSH(msh, context, ref mshProcessed, in delimiters);
                break;

            case EVN evn:
                context.Message.Evn = evn;
                evnProcessed = true;
                break;

            case PID pid:
                context.Message.Pid = pid;
                pidProcessed = true;
                break;

            case PV1:
            case PV2:
                // Use polymorphic AddSegment for all other segments
                context.Message.AddSegment(segment);
                if (segment is PV1) pv1Processed = true;
                if (segment is PV2) pv2Processed = true;
                break;

            default:
                // Let the message decide how to handle this segment
                context.Message.AddSegment(segment);
                break;
        }
    }


    private static void HandleOrderGroupSegment(
        ISegment segment,
        ReadOnlySpan<char> segmentId,
        OrderGroupingConfiguration config,
        DeserializationContext context,
        ref OrderGroup? currentOrder)
    {
        if (config.IsTriggerSegment(segmentId))
        {
            // Save previous order group
            if (currentOrder != null)
            {
                context.Message.AddOrderGroup(currentOrder);
            }

            // Start new order group with configuration
            currentOrder = new OrderGroup
            {
                OrderType = config.OrderType,
                PrimarySegment = segment,
                Configuration = config
            };
            currentOrder.SegmentStatus[segmentId.ToString()] = true;
        }
        else if (currentOrder != null)
        {
            // Add to current order group
            currentOrder.AddSegment(segment);
        }
    }


    private static void HandleMSH(MSH msh, DeserializationContext context, ref bool mshProcessed, in HL7Delimiters delimiters)
    {
        if (mshProcessed)
        {
            context.AddError(ErrorSeverity.Error, ErrorCode.SegmentSequenceError, "Multiple MSH segments");
            return;
        }

        context.Message.Msh = msh;
        context.Message.MessageControlID = msh.MessageControlID;
        mshProcessed = true;

        // Parse event type
        var messageTypeValue = msh.MessageType.Value;
        if (!string.IsNullOrEmpty(messageTypeValue))
        {
            var parts = messageTypeValue.Split(delimiters.ComponentSeparator);
            if (parts.Length > 1 && parts[1] is string eventCode)
            {
                (context.Message.EventType, context.Message.MessageType) = ParseEventString(eventCode);
            }
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
