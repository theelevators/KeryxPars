using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// DR - Date Range
/// HL7 2.5 Composite Data Type representing a date range.
/// Format: StartDate^EndDate
/// </summary>
public readonly struct DR : ICompositeDataType
{
    private readonly DTM _startDate;
    private readonly DTM _endDate;

    /// <summary>
    /// Initializes a new instance of the DR data type with start and end dates.
    /// </summary>
    public DR(DTM startDate = default, DTM endDate = default)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    /// <inheritdoc/>
    public string TypeCode => "DR";

    /// <inheritdoc/>
    public int ComponentCount => 2;

    /// <inheritdoc/>
    public bool IsEmpty => _startDate.IsEmpty && _endDate.IsEmpty;

    /// <summary>
    /// DR.1 - Range Start Date/Time
    /// </summary>
    public DTM StartDate => _startDate;

    /// <summary>
    /// DR.2 - Range End Date/Time
    /// </summary>
    public DTM EndDate => _endDate;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _startDate,
            1 => _endDate,
            _ => null
        };
    }

    /// <inheritdoc/>
    public void SetComponent(int index, IHL7DataType? value)
    {
        if (value == null || value is not DTM dtm) return;

        switch (index)
        {
            case 0:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _startDate) = dtm;
                break;
            case 1:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _endDate) = dtm;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        if (!_startDate.IsEmpty)
            sb.Append(_startDate.ToHL7String(delimiters));
        
        sb.Append(delimiters.ComponentSeparator);
        
        if (!_endDate.IsEmpty)
            sb.Append(_endDate.ToHL7String(delimiters));
        
        return sb.ToString().TrimEnd(delimiters.ComponentSeparator);
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _startDate) = new DTM(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _endDate) = new DTM(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_startDate.Validate(out var startErrors))
            errors.AddRange(startErrors.Select(e => $"StartDate: {e}"));
        if (!_endDate.Validate(out var endErrors))
            errors.AddRange(endErrors.Select(e => $"EndDate: {e}"));

        // Validate that end date is after start date if both are present
        if (!_startDate.IsEmpty && !_endDate.IsEmpty)
        {
            var start = _startDate.ToDateTime();
            var end = _endDate.ToDateTime();
            
            if (start.HasValue && end.HasValue && end.Value < start.Value)
            {
                errors.Add($"End date ({end.Value}) is before start date ({start.Value})");
            }
        }

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is DR other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to DR.
    /// </summary>
    public static implicit operator DR(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var dr = new DR();
        dr.Parse(value.AsSpan(), HL7Delimiters.Default);
        return dr;
    }

    /// <summary>
    /// Implicit conversion from DR to string.
    /// </summary>
    public static implicit operator string(DR dr) => dr.ToString();
}
