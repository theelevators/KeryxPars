using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// SAD - Street Address
/// HL7 2.5 Composite Data Type representing a street address with subcomponents.
/// Format: StreetOrMailingAddress^StreetName^DwellingNumber
/// </summary>
public readonly struct SAD : ICompositeDataType
{
    private readonly ST _streetOrMailingAddress;
    private readonly ST _streetName;
    private readonly ST _dwellingNumber;

    /// <summary>
    /// Initializes a new instance of the SAD data type with all components.
    /// </summary>
    public SAD(
        ST streetOrMailingAddress = default,
        ST streetName = default,
        ST dwellingNumber = default)
    {
        _streetOrMailingAddress = streetOrMailingAddress;
        _streetName = streetName;
        _dwellingNumber = dwellingNumber;
    }

    /// <inheritdoc/>
    public string TypeCode => "SAD";

    /// <inheritdoc/>
    public int ComponentCount => 3;

    /// <inheritdoc/>
    public bool IsEmpty => _streetOrMailingAddress.IsEmpty && _streetName.IsEmpty && _dwellingNumber.IsEmpty;

    /// <summary>
    /// SAD.1 - Street or Mailing Address
    /// </summary>
    public ST StreetOrMailingAddress => _streetOrMailingAddress;

    /// <summary>
    /// SAD.2 - Street Name
    /// </summary>
    public ST StreetName => _streetName;

    /// <summary>
    /// SAD.3 - Dwelling Number
    /// </summary>
    public ST DwellingNumber => _dwellingNumber;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _streetOrMailingAddress,
            1 => _streetName,
            2 => _dwellingNumber,
            _ => null
        };
    }

    /// <inheritdoc/>
    public void SetComponent(int index, IHL7DataType? value)
    {
        if (value == null || value is not ST st) return;

        switch (index)
        {
            case 0:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _streetOrMailingAddress) = st;
                break;
            case 1:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _streetName) = st;
                break;
            case 2:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _dwellingNumber) = st;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendSubcomponent(sb, _streetOrMailingAddress, delimiters, false);
        AppendSubcomponent(sb, _streetName, delimiters);
        AppendSubcomponent(sb, _dwellingNumber, delimiters);
        
        return sb.ToString().TrimEnd(delimiters.SubComponentSeparator);
    }

    private static void AppendSubcomponent(StringBuilder sb, ST component, in HL7Delimiters delimiters, bool addSeparator = true)
    {
        if (addSeparator)
            sb.Append(delimiters.SubComponentSeparator);
        if (!component.IsEmpty)
            sb.Append(component.ToHL7String(delimiters));
    }

    /// <inheritdoc/>
    public void Parse(ReadOnlySpan<char> value, in HL7Delimiters delimiters)
    {
        var enumerator = new SubcomponentEnumerator(value, delimiters.SubComponentSeparator);
        int index = 0;

        while (enumerator.MoveNext() && index < ComponentCount)
        {
            var subcomponent = enumerator.Current;
            
            switch (index)
            {
                case 0:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _streetOrMailingAddress) = new ST(subcomponent.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _streetName) = new ST(subcomponent.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _dwellingNumber) = new ST(subcomponent.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_streetOrMailingAddress.Validate(out var addressErrors))
            errors.AddRange(addressErrors.Select(e => $"StreetOrMailingAddress: {e}"));
        if (!_streetName.Validate(out var nameErrors))
            errors.AddRange(nameErrors.Select(e => $"StreetName: {e}"));
        if (!_dwellingNumber.Validate(out var numberErrors))
            errors.AddRange(numberErrors.Select(e => $"DwellingNumber: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => _streetOrMailingAddress.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is SAD other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to SAD.
    /// </summary>
    public static implicit operator SAD(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var sad = new SAD();
        sad.Parse(value.AsSpan(), HL7Delimiters.Default);
        return sad;
    }

    /// <summary>
    /// Implicit conversion from SAD to string.
    /// </summary>
    public static implicit operator string(SAD sad) => sad.ToString();
}
