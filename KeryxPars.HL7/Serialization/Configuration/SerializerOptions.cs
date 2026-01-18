using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.HL7.Serialization.Configuration;

/// <summary>
/// Configuration options for serialization with pooled resources.
/// </summary>
public sealed class SerializerOptions
{
    public ISegmentRegistry SegmentRegistry { get; set; } = DefaultSegmentRegistry.Instance;
    public bool IgnoreUnknownSegments { get; set; } = true;
    public ErrorHandlingStrategy ErrorHandling { get; set; } = ErrorHandlingStrategy.FailFast;
    public IValidationStrategy? ValidationStrategy { get; set; }

    // Performance options
    public bool UseStringPooling { get; set; } = true;
    public int InitialBufferSize { get; set; } = 8192;

    /// <summary>
    /// The type of message to create during deserialization.
    /// Defaults to HL7DefaultMessage.
    /// </summary>
    public Type MessageType { get; set; } = typeof(HL7DefaultMessage);

    private static SerializerOptions? _default;
    public static SerializerOptions Default => _default ??= new SerializerOptions();

    //Grouping options

    /// <summary>
    /// Configuration for grouping order-related segments
    /// </summary>
    public OrderGroupingConfiguration OrderGrouping { get; init; } = OrderGroupingConfiguration.None;

    /// <summary>
    /// Creates options configured for medication orders with PharmacyMessage
    /// </summary>
    public static SerializerOptions ForMedicationOrders() => new()
    {
        OrderGrouping = OrderGroupingConfiguration.Medication,
        MessageType = typeof(PharmacyMessage)
    };

    /// <summary>
    /// Creates options configured for lab orders with LabMessage
    /// </summary>
    public static SerializerOptions ForLabOrders() => new()
    {
        OrderGrouping = OrderGroupingConfiguration.Lab,
        MessageType = typeof(LabMessage)
    };

    /// <summary>
    /// Creates options configured for imaging orders with LabMessage
    /// </summary>
    public static SerializerOptions ForImagingOrders() => new()
    {
        OrderGrouping = OrderGroupingConfiguration.Imaging,
        MessageType = typeof(LabMessage)
    };

    /// <summary>
    /// Creates options configured for hospice messages
    /// </summary>
    public static SerializerOptions ForHospice() => new()
    {
        MessageType = typeof(HospiceMessage),
        OrderGrouping = OrderGroupingConfiguration.None
    };

    /// <summary>
    /// Creates options configured for scheduling messages
    /// </summary>
    public static SerializerOptions ForScheduling() => new()
    {
        MessageType = typeof(SchedulingMessage),
        OrderGrouping = OrderGroupingConfiguration.None
    };

    /// <summary>
    /// Creates options configured for financial/billing messages
    /// </summary>
    public static SerializerOptions ForFinancial() => new()
    {
        MessageType = typeof(FinancialMessage),
        OrderGrouping = OrderGroupingConfiguration.None
    };

    /// <summary>
    /// Creates options configured for dietary messages
    /// </summary>
    public static SerializerOptions ForDietary() => new()
    {
        MessageType = typeof(DietaryMessage),
        OrderGrouping = OrderGroupingConfiguration.Dietary
    };
}

public enum ErrorHandlingStrategy
{
    CollectAndContinue,
    FailFast,
    Silent
}