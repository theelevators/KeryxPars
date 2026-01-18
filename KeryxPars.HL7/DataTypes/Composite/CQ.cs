using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// CQ - Composite Quantity with Units
/// HL7 2.5 Composite Data Type representing a quantity with units.
/// Format: Quantity^Units
/// </summary>
public readonly struct CQ : ICompositeDataType
{
    private readonly NM _quantity;
    private readonly CE _units;

    /// <summary>
    /// Initializes a new instance of the CQ data type with quantity and units.
    /// </summary>
    public CQ(NM quantity = default, CE units = default)
    {
        _quantity = quantity;
        _units = units;
    }

    /// <inheritdoc/>
    public string TypeCode => "CQ";

    /// <inheritdoc/>
    public int ComponentCount => 2;

    /// <inheritdoc/>
    public bool IsEmpty => _quantity.IsEmpty && _units.IsEmpty;

    /// <summary>
    /// CQ.1 - Quantity
    /// </summary>
    public NM Quantity => _quantity;

    /// <summary>
    /// CQ.2 - Units
    /// </summary>
    public CE Units => _units;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _quantity,
            1 => _units,
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
                if (value is NM nm) System.Runtime.CompilerServices.Unsafe.AsRef(in _quantity) = nm;
                break;
            case 1:
                if (value is CE ce) System.Runtime.CompilerServices.Unsafe.AsRef(in _units) = ce;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        if (!_quantity.IsEmpty)
            sb.Append(_quantity.ToHL7String(delimiters));
        
        sb.Append(delimiters.ComponentSeparator);
        
        if (!_units.IsEmpty)
            sb.Append(_units.ToHL7String(delimiters));
        
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _quantity) = new NM(component.ToString());
                    break;
                case 1:
                    var ce = new CE();
                    ce.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _units) = ce;
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_quantity.Validate(out var quantityErrors))
            errors.AddRange(quantityErrors.Select(e => $"Quantity: {e}"));
        if (!_units.Validate(out var unitsErrors))
            errors.AddRange(unitsErrors.Select(e => $"Units: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => !_units.IsEmpty ? 
        $"{_quantity.Value} {_units.Text.Value}" : 
        _quantity.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is CQ other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to CQ.
    /// </summary>
    public static implicit operator CQ(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var cq = new CQ();
        cq.Parse(value.AsSpan(), HL7Delimiters.Default);
        return cq;
    }

    /// <summary>
    /// Implicit conversion from CQ to string.
    /// </summary>
    public static implicit operator string(CQ cq) => cq.ToString();
}
