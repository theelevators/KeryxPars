using KeryxPars.HL7.Contracts;

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

    private static SerializerOptions? _default;
    public static SerializerOptions Default => _default ??= new SerializerOptions();

    //Grouping options

    /// <summary>
    /// Configuration for grouping order-related segments
    /// </summary>
    public OrderGroupingConfiguration OrderGrouping { get; init; } = OrderGroupingConfiguration.Medication;

    /// <summary>
    /// Creates options configured for medication orders
    /// </summary>
    public static SerializerOptions ForMedicationOrders() => new()
    {
        OrderGrouping = OrderGroupingConfiguration.Medication
    };

    /// <summary>
    /// Creates options configured for lab orders
    /// </summary>
    public static SerializerOptions ForLabOrders() => new()
    {
        OrderGrouping = OrderGroupingConfiguration.Lab
    };

    /// <summary>
    /// Creates options configured for imaging orders
    /// </summary>
    public static SerializerOptions ForImagingOrders() => new()
    {
        OrderGrouping = OrderGroupingConfiguration.Imaging
    };
}

public enum ErrorHandlingStrategy
{
    CollectAndContinue,
    FailFast,
    Silent
}