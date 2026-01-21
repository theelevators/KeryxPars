using System;
using System.Text;

namespace KeryxPars.HL7.Mapping.Builders;

/// <summary>
/// Ultra-fast HL7 message builder using StringBuilder.
/// Builds HL7 messages with proper field/component/segment delimiters.
/// </summary>
public class HL7MessageBuilder
{
    private readonly StringBuilder _sb;
    private readonly string _fieldSeparator;
    private readonly string _componentSeparator;
    private readonly string _repetitionSeparator;
    private readonly string _escapeChar;
    private readonly string _subcomponentSeparator;

    public HL7MessageBuilder()
    {
        _sb = new StringBuilder(2048); // Pre-allocate for typical message
        _fieldSeparator = "|";
        _componentSeparator = "^";
        _repetitionSeparator = "~";
        _escapeChar = "\\";
        _subcomponentSeparator = "&";
    }

    /// <summary>
    /// Starts a new segment.
    /// </summary>
    public HL7MessageBuilder StartSegment(string segmentId)
    {
        if (_sb.Length > 0)
        {
            _sb.Append('\r'); // Segment delimiter
        }
        _sb.Append(segmentId);
        return this;
    }

    /// <summary>
    /// Sets a field value at a specific index (1-based).
    /// </summary>
    public HL7MessageBuilder SetField(string segmentId, int fieldIndex, string? value)
    {
        // This is called by generated code - it handles field positioning
        // For now, we'll build a simpler API
        return this;
    }

    /// <summary>
    /// Appends a field to the current segment.
    /// </summary>
    public HL7MessageBuilder AppendField(string? value)
    {
        _sb.Append(_fieldSeparator);
        if (!string.IsNullOrEmpty(value))
        {
            _sb.Append(EscapeValue(value));
        }
        return this;
    }

    /// <summary>
    /// Appends a component within the current field.
    /// </summary>
    public HL7MessageBuilder AppendComponent(string? value)
    {
        _sb.Append(_componentSeparator);
        if (!string.IsNullOrEmpty(value))
        {
            _sb.Append(EscapeValue(value));
        }
        return this;
    }

    /// <summary>
    /// Gets the built HL7 message.
    /// </summary>
    public string Build()
    {
        return _sb.ToString();
    }

    /// <summary>
    /// Clears the builder for reuse.
    /// </summary>
    public void Clear()
    {
        _sb.Clear();
    }

    private string EscapeValue(string value)
    {
        // HL7 escaping rules
        if (value.IndexOfAny(new[] { '|', '^', '~', '\\', '&' }) >= 0)
        {
            return value
                .Replace("\\", "\\E\\")
                .Replace("|", "\\F\\")
                .Replace("^", "\\S\\")
                .Replace("~", "\\R\\")
                .Replace("&", "\\T\\");
        }
        return value;
    }
}

/// <summary>
/// Simplified HL7 message builder for source-generated code.
/// Uses a segment-based approach for clean generation.
/// </summary>
public class HL7SegmentBuilder
{
    private readonly string[] _fields;
    private readonly string _segmentId;

    public HL7SegmentBuilder(string segmentId, int maxFieldIndex)
    {
        _segmentId = segmentId;
        _fields = new string[maxFieldIndex + 1]; // 1-based indexing
    }

    /// <summary>
    /// Sets a field value.
    /// </summary>
    public void SetField(int index, string? value)
    {
        if (index >= 0 && index < _fields.Length && value != null)
        {
            _fields[index] = value;
        }
    }

    /// <summary>
    /// Builds the segment string.
    /// </summary>
    public string Build()
    {
        var sb = new StringBuilder();
        sb.Append(_segmentId);

        for (int i = 1; i < _fields.Length; i++)
        {
            sb.Append('|');
            if (_fields[i] != null)
            {
                sb.Append(_fields[i]);
            }
        }

        return sb.ToString();
    }
}
