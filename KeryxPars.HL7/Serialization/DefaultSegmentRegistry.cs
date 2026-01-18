namespace KeryxPars.HL7.Serialization;

using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization.Converters;
using System.Collections.Frozen;

/// <summary>
/// Default implementation of segment registry for HL7 v2.x messages.
/// </summary>
public sealed class DefaultSegmentRegistry : ISegmentRegistry
{
    private static readonly Lazy<DefaultSegmentRegistry> s_instance = new(() => new DefaultSegmentRegistry());

    public static DefaultSegmentRegistry Instance => s_instance.Value;

    private readonly Dictionary<string, ISegmentConverter> _converters;
    private readonly Dictionary<string, SegmentMetadata> _metadata;
    private FrozenDictionary<string, ISegmentConverter>? _frozenConverters;

    private DefaultSegmentRegistry()
    {
        _converters = new Dictionary<string, ISegmentConverter>(StringComparer.Ordinal);
        _metadata = new Dictionary<string, SegmentMetadata>(StringComparer.Ordinal);

        RegisterDefaultConverters();
    }

    public void Register(ISegmentConverter converter)
    {
        ArgumentNullException.ThrowIfNull(converter);

        _converters[converter.SegmentId] = converter;
        _frozenConverters = null; // Invalidate frozen cache
    }

    public ISegmentConverter? GetConverter(ReadOnlySpan<char> segmentId)
    {
        // Use frozen dictionary for better read performance after initialization
        _frozenConverters ??= _converters.ToFrozenDictionary(StringComparer.Ordinal);

        // Convert span to string for dictionary lookup
        // In hot paths, consider using a Trie or custom span-based lookup
        var key = segmentId.ToString();
        return _frozenConverters.TryGetValue(key, out var converter) ? converter : null;
    }

    public bool IsRegistered(ReadOnlySpan<char> segmentId)
    {
        return GetConverter(segmentId) != null;
    }

    public SegmentMetadata? GetMetadata(ReadOnlySpan<char> segmentId)
    {
        var key = segmentId.ToString();
        return _metadata.TryGetValue(key, out var metadata) ? metadata : null;
    }

    private void RegisterDefaultConverters()
    {
        // Register common segments (required in most messages)
        RegisterWithMetadata(new GenericSegmentConverter<MSH>(), false, true, 1, "Message Header");
        RegisterWithMetadata(new GenericSegmentConverter<MSA>(), false, false, 1, "Message Acknowledgement");
        RegisterWithMetadata(new GenericSegmentConverter<ERR>(), true, false, null, "Error Information");
        RegisterWithMetadata(new GenericSegmentConverter<EVN>(), false, false, 1, "Event Type");

        // Register patient segments
        RegisterWithMetadata(new GenericSegmentConverter<PID>(), false, true, 1, "Patient Identification");
        RegisterWithMetadata(new GenericSegmentConverter<PD1>(), false, false, 1, "Additional Demographics");
        RegisterWithMetadata(new GenericSegmentConverter<NK1>(), true, false, null, "Next of Kin");
        RegisterWithMetadata(new GenericSegmentConverter<PV1>(), false, false, 1, "Patient Visit");
        RegisterWithMetadata(new GenericSegmentConverter<PV2>(), false, false, 1, "Patient Visit - Additional Info");
        RegisterWithMetadata(new GenericSegmentConverter<MRG>(), false, false, 1, "Merge Patient Information");

        // Register clinical segments
        RegisterWithMetadata(new GenericSegmentConverter<AL1>(), true, false, null, "Allergy Information");
        RegisterWithMetadata(new GenericSegmentConverter<DG1>(), true, false, null, "Diagnosis Information");
        RegisterWithMetadata(new GenericSegmentConverter<OBX>(), true, false, null, "Observation/Result");
        RegisterWithMetadata(new GenericSegmentConverter<OBR>(), true, false, null, "Observation Request");
        RegisterWithMetadata(new GenericSegmentConverter<NTE>(), true, false, null, "Notes and Comments");
        RegisterWithMetadata(new GenericSegmentConverter<PR1>(), true, false, null, "Procedures");
        RegisterWithMetadata(new GenericSegmentConverter<CTI>(), true, false, null, "Clinical Trial Identification");

        // Register insurance & financial segments
        RegisterWithMetadata(new GenericSegmentConverter<IN1>(), true, false, null, "Insurance Information");
        RegisterWithMetadata(new GenericSegmentConverter<IN2>(), true, false, null, "Insurance Additional Information");
        RegisterWithMetadata(new GenericSegmentConverter<GT1>(), true, false, null, "Guarantor");
        RegisterWithMetadata(new GenericSegmentConverter<DRG>(), false, false, 1, "Diagnosis Related Group");
        RegisterWithMetadata(new GenericSegmentConverter<FT1>(), true, false, null, "Financial Transaction");

        // Register pharmacy order segments
        RegisterWithMetadata(new GenericSegmentConverter<ORC>(), true, false, null, "Common Order");
        RegisterWithMetadata(new GenericSegmentConverter<RXO>(), false, false, 1, "Pharmacy Prescription Order");
        RegisterWithMetadata(new GenericSegmentConverter<RXE>(), false, false, 1, "Pharmacy Encoded Order");
        RegisterWithMetadata(new GenericSegmentConverter<RXR>(), true, false, null, "Pharmacy Route");
        RegisterWithMetadata(new GenericSegmentConverter<RXC>(), true, false, null, "Pharmacy Component");
        RegisterWithMetadata(new GenericSegmentConverter<TQ1>(), true, false, null, "Timing Quantity");
        RegisterWithMetadata(new GenericSegmentConverter<TQ2>(), true, false, null, "Timing/Quantity Relationship");

        // Register pharmacy treatment segments
        RegisterWithMetadata(new GenericSegmentConverter<RXA>(), true, false, null, "Pharmacy/Treatment Administration");
        RegisterWithMetadata(new GenericSegmentConverter<RXD>(), true, false, null, "Pharmacy/Treatment Dispense");
        RegisterWithMetadata(new GenericSegmentConverter<RXG>(), true, false, null, "Pharmacy/Treatment Give");

        // Register laboratory segments
        RegisterWithMetadata(new GenericSegmentConverter<SPM>(), true, false, null, "Specimen");
        RegisterWithMetadata(new GenericSegmentConverter<SAC>(), true, false, null, "Specimen Container Detail");

        // Register scheduling segments
        RegisterWithMetadata(new GenericSegmentConverter<SCH>(), false, false, 1, "Scheduling Activity Information");
        RegisterWithMetadata(new GenericSegmentConverter<AIL>(), true, false, null, "Appointment Information - Location Resource");
        RegisterWithMetadata(new GenericSegmentConverter<AIP>(), true, false, null, "Appointment Information - Personnel Resource");
        RegisterWithMetadata(new GenericSegmentConverter<AIS>(), true, false, null, "Appointment Information - Service");

        // Register dietary segments
        RegisterWithMetadata(new GenericSegmentConverter<ODS>(), true, false, null, "Dietary Orders, Supplements, and Preferences");
        RegisterWithMetadata(new GenericSegmentConverter<ODT>(), true, false, null, "Diet Tray Instructions");

        // Register administrative segments
        RegisterWithMetadata(new GenericSegmentConverter<ROL>(), true, false, null, "Role");
        RegisterWithMetadata(new GenericSegmentConverter<ACC>(), false, false, 1, "Accident Information");
        RegisterWithMetadata(new GenericSegmentConverter<CTD>(), true, false, null, "Contact Data");

        // Register query segments
        RegisterWithMetadata(new GenericSegmentConverter<QPD>(), false, false, 1, "Query Parameter Definition");
        RegisterWithMetadata(new GenericSegmentConverter<RCP>(), false, false, 1, "Response Control Parameter");
        RegisterWithMetadata(new GenericSegmentConverter<DSC>(), false, false, 1, "Continuation Pointer");
        RegisterWithMetadata(new GenericSegmentConverter<QRD>(), false, false, 1, "Query Definition (Deprecated)");
        RegisterWithMetadata(new GenericSegmentConverter<QRF>(), false, false, 1, "Query Filter (Deprecated)");
    }

    private void RegisterWithMetadata(
        ISegmentConverter converter,
        bool isRepeatable,
        bool isRequired,
        int? maxOccurrences = null,
        string? description = null)
    {
        Register(converter);

        _metadata[converter.SegmentId] = new SegmentMetadata(
            converter.SegmentId,
            isRepeatable,
            isRequired,
            maxOccurrences,
            description);
    }
}