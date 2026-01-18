namespace KeryxPars.HL7.Serialization.Converters;

using KeryxPars.Core.Models;
using KeryxPars.HL7.Contracts;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Segments;
using KeryxPars.HL7.Serialization.Configuration;
using System;

/// <summary>
/// Generic segment converter that works with any ISegment implementation.
/// Uses the segment's SetValue method for parsing and GetValues for serialization.
/// </summary>
/// <typeparam name="TSegment">The segment type that implements ISegment</typeparam>
public class GenericSegmentConverter<TSegment> : ISegmentConverter where TSegment : ISegment, new()
{
    private readonly string _segmentId;

    public GenericSegmentConverter()
    {
        var instance = new TSegment();
        _segmentId = instance.SegmentId;
    }

    public string SegmentId => _segmentId;

    public bool CanConvert(ReadOnlySpan<char> segmentId)
    {
        return segmentId.SequenceEqual(_segmentId.AsSpan());
    }

    public Result<ISegment, HL7Error> Read(
        ref SegmentReader reader,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        try
        {
            var segment = new TSegment();
            int fieldIndex = 1;

            // Skip segment ID (first field)
            if (!reader.TryReadField(delimiters.FieldSeparator, out _))
            {
                return new HL7Error(
                    ErrorSeverity.Error,
                    ErrorCode.SegmentSequenceError,
                    $"Failed to read segment ID for {_segmentId}");
            }

            // Special handling for MSH segment (field 1 is the encoding characters)
            if (_segmentId == "MSH")
            {
                // Read the encoding characters field (they appear right after MSH|)
                if (reader.TryReadField(delimiters.FieldSeparator, out var encodingCharsField))
                {
                    segment.SetValue(encodingCharsField.ToString(), fieldIndex++);
                }
            }

            // Read remaining fields
            while (reader.TryReadField(delimiters.FieldSeparator, out var field))
            {
                segment.SetValue(field.ToString(), fieldIndex);
                fieldIndex++;
            }

            return segment;
        }
        catch (Exception ex)
        {
            return new HL7Error(
                ErrorSeverity.Error,
                ErrorCode.ApplicationInternalError,
                $"Error parsing {_segmentId} segment: {ex.Message}");
        }
    }

    public void Write(
        ISegment segment,
        ref SegmentWriter writer,
        in HL7Delimiters delimiters,
        SerializerOptions options)
    {
        if (segment is not TSegment)
        {
            throw new ArgumentException(
                $"Segment must be of type {typeof(TSegment).Name}",
                nameof(segment));
        }

        var values = segment.GetValues();
        
        if (values.Length == 0)
            return;

        // Write segment ID
        writer.Write(values[0]);
        writer.Write(delimiters.FieldSeparator);

        // Special handling for MSH segment
        if (_segmentId == "MSH")
        {
            // MSH field 1 is the encoding characters ^~\&
            // Write them manually based on delimiters
            writer.Write(delimiters.ComponentSeparator);       // ^
            writer.Write(delimiters.FieldRepeatSeparator);     // ~
            writer.Write(delimiters.EscapeCharacter);          // \
            writer.Write(delimiters.SubComponentSeparator);    // &
            writer.Write(delimiters.FieldSeparator);           // |
            
            // Write remaining fields starting from index 3 (skip index 1=null, index 2=encoding chars)
            for (int i = 3; i < values.Length; i++)
            {
                if (i > 3)
                    writer.Write(delimiters.FieldSeparator);
                
                if (!string.IsNullOrEmpty(values[i]))
                    writer.Write(values[i]);
            }
        }
        else
        {
            // Write all fields for non-MSH segments
            for (int i = 1; i < values.Length; i++)
            {
                if (i > 1)
                    writer.Write(delimiters.FieldSeparator);
                
                if (!string.IsNullOrEmpty(values[i]))
                    writer.Write(values[i]);
            }
        }
    }
}
