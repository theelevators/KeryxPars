using KeryxPars.Core.Models;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Serialization;
using KeryxPars.HL7.Serialization.Configuration;


namespace KeryxPars.HL7.Contracts;

/// <summary>
/// Span-based segment converter for zero-allocation parsing.
/// </summary>
public interface ISegmentConverter
{
    string SegmentId { get; }
    bool CanConvert(ReadOnlySpan<char> segmentId);

    Result<ISegment, HL7Error> Read(
        ref SegmentReader reader,
        in HL7Delimiters delimiters,
        SerializerOptions options);

    void Write(
        ISegment segment,
        ref SegmentWriter writer,
        in HL7Delimiters delimiters,
        SerializerOptions options);
}