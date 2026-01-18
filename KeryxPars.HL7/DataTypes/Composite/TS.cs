using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// TS - Time Stamp
/// HL7 2.5 Composite Data Type representing a timestamp with optional degree of precision.
/// Format: Time^DegreeOfPrecision
/// </summary>
public readonly struct TS : ICompositeDataType
{
    private readonly DTM _time;
    private readonly ID _degreeOfPrecision;

    /// <summary>
    /// Initializes a new instance of the TS data type with time and degree of precision.
    /// </summary>
    public TS(DTM time = default, ID degreeOfPrecision = default)
    {
        _time = time;
        _degreeOfPrecision = degreeOfPrecision;
    }

    /// <summary>
    /// Initializes a new instance of the TS data type from a DateTime.
    /// </summary>
    public TS(DateTime dateTime)
    {
        _time = new DTM(dateTime);
        _degreeOfPrecision = default;
    }

    /// <inheritdoc/>
    public string TypeCode => "TS";

    /// <inheritdoc/>
    public int ComponentCount => 2;

    /// <inheritdoc/>
    public bool IsEmpty => _time.IsEmpty && _degreeOfPrecision.IsEmpty;

    /// <summary>
    /// TS.1 - Time
    /// </summary>
    public DTM Time => _time;

    /// <summary>
    /// TS.2 - Degree of Precision
    /// </summary>
    public ID DegreeOfPrecision => _degreeOfPrecision;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _time,
            1 => _degreeOfPrecision,
            _ => null
        };
    }

    /// <inheritdoc/>
    public void SetComponent(int index, IHL7DataType? value)
    {
        if (value == null) return;

        switch (index)
        {
            case 0:
                if (value is DTM dtm) System.Runtime.CompilerServices.Unsafe.AsRef(in _time) = dtm;
                break;
            case 1:
                if (value is ID id) System.Runtime.CompilerServices.Unsafe.AsRef(in _degreeOfPrecision) = id;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        if (!_time.IsEmpty)
            sb.Append(_time.ToHL7String(delimiters));
        
        if (!_degreeOfPrecision.IsEmpty)
        {
            sb.Append(delimiters.ComponentSeparator);
            sb.Append(_degreeOfPrecision.ToHL7String(delimiters));
        }
        
        return sb.ToString();
    }

    /// <inheritdoc/>
    public void Parse(ReadOnlySpan<char> value, in HL7Delimiters delimiters)
    {
        var enumerator = new ComponentEnumerator(value, delimiters.ComponentSeparator);
        int index = 0;

        while (enumerator.MoveNext() && index < ComponentCount)
        {
            var component = enumerator.Current;
            
            switch (index)
            {
                case 0:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _time) = new DTM(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _degreeOfPrecision) = new ID(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_time.Validate(out var timeErrors))
            errors.AddRange(timeErrors.Select(e => $"Time: {e}"));
        if (!_degreeOfPrecision.Validate(out var precisionErrors))
            errors.AddRange(precisionErrors.Select(e => $"DegreeOfPrecision: {e}"));

        return errors.Count == 0;
    }

    /// <summary>
    /// Converts the timestamp to a DateTime.
    /// </summary>
    /// <returns>The DateTime value, or null if the value is empty or invalid.</returns>
    public DateTime? ToDateTime()
    {
        return _time.ToDateTime();
    }

    /// <inheritdoc/>
    public override string ToString() => _time.ToString();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is TS other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to TS.
    /// </summary>
    public static implicit operator TS(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var ts = new TS();
        ts.Parse(value.AsSpan(), HL7Delimiters.Default);
        return ts;
    }

    /// <summary>
    /// Implicit conversion from TS to string.
    /// </summary>
    public static implicit operator string(TS ts) => ts.ToString();

    /// <summary>
    /// Implicit conversion from DateTime to TS.
    /// </summary>
    public static implicit operator TS(DateTime dateTime) => new(dateTime);

    /// <summary>
    /// Explicit conversion from TS to DateTime.
    /// </summary>
    public static explicit operator DateTime?(TS ts) => ts.ToDateTime();
}
