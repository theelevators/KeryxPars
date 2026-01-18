using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// XON - Extended Composite Name and Identification Number for Organizations
/// HL7 2.5 Composite Data Type representing an organization's name and identifiers.
/// Format: OrganizationName^OrganizationNameTypeCode^IDNumber^CheckDigit^CheckDigitScheme^AssigningAuthority^IdentifierTypeCode^AssigningFacility^NameRepresentationCode^OrganizationIdentifier
/// </summary>
public readonly struct XON : ICompositeDataType
{
    private readonly ST _organizationName;
    private readonly IS _organizationNameTypeCode;
    private readonly NM _idNumber;
    private readonly NM _checkDigit;
    private readonly ID _checkDigitScheme;
    private readonly HD _assigningAuthority;
    private readonly ID _identifierTypeCode;
    private readonly HD _assigningFacility;
    private readonly ID _nameRepresentationCode;
    private readonly ST _organizationIdentifier;

    /// <summary>
    /// Initializes a new instance of the XON data type with all components.
    /// </summary>
    public XON(
        ST organizationName = default,
        IS organizationNameTypeCode = default,
        NM idNumber = default,
        NM checkDigit = default,
        ID checkDigitScheme = default,
        HD assigningAuthority = default,
        ID identifierTypeCode = default,
        HD assigningFacility = default,
        ID nameRepresentationCode = default,
        ST organizationIdentifier = default)
    {
        _organizationName = organizationName;
        _organizationNameTypeCode = organizationNameTypeCode;
        _idNumber = idNumber;
        _checkDigit = checkDigit;
        _checkDigitScheme = checkDigitScheme;
        _assigningAuthority = assigningAuthority;
        _identifierTypeCode = identifierTypeCode;
        _assigningFacility = assigningFacility;
        _nameRepresentationCode = nameRepresentationCode;
        _organizationIdentifier = organizationIdentifier;
    }

    /// <inheritdoc/>
    public string TypeCode => "XON";

    /// <inheritdoc/>
    public int ComponentCount => 10;

    /// <inheritdoc/>
    public bool IsEmpty => _organizationName.IsEmpty && _organizationNameTypeCode.IsEmpty &&
                          _idNumber.IsEmpty && _checkDigit.IsEmpty && _checkDigitScheme.IsEmpty &&
                          _assigningAuthority.IsEmpty && _identifierTypeCode.IsEmpty &&
                          _assigningFacility.IsEmpty && _nameRepresentationCode.IsEmpty &&
                          _organizationIdentifier.IsEmpty;

    /// <summary>
    /// XON.1 - Organization Name
    /// </summary>
    public ST OrganizationName => _organizationName;

    /// <summary>
    /// XON.2 - Organization Name Type Code
    /// </summary>
    public IS OrganizationNameTypeCode => _organizationNameTypeCode;

    /// <summary>
    /// XON.3 - ID Number
    /// </summary>
    public NM IdNumber => _idNumber;

    /// <summary>
    /// XON.4 - Check Digit
    /// </summary>
    public NM CheckDigit => _checkDigit;

    /// <summary>
    /// XON.5 - Check Digit Scheme
    /// </summary>
    public ID CheckDigitScheme => _checkDigitScheme;

    /// <summary>
    /// XON.6 - Assigning Authority
    /// </summary>
    public HD AssigningAuthority => _assigningAuthority;

    /// <summary>
    /// XON.7 - Identifier Type Code
    /// </summary>
    public ID IdentifierTypeCode => _identifierTypeCode;

    /// <summary>
    /// XON.8 - Assigning Facility
    /// </summary>
    public HD AssigningFacility => _assigningFacility;

    /// <summary>
    /// XON.9 - Name Representation Code
    /// </summary>
    public ID NameRepresentationCode => _nameRepresentationCode;

    /// <summary>
    /// XON.10 - Organization Identifier
    /// </summary>
    public ST OrganizationIdentifier => _organizationIdentifier;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _organizationName,
            1 => _organizationNameTypeCode,
            2 => _idNumber,
            3 => _checkDigit,
            4 => _checkDigitScheme,
            5 => _assigningAuthority,
            6 => _identifierTypeCode,
            7 => _assigningFacility,
            8 => _nameRepresentationCode,
            9 => _organizationIdentifier,
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
                if (value is ST st0) System.Runtime.CompilerServices.Unsafe.AsRef(in _organizationName) = st0;
                break;
            case 1:
                if (value is IS is1) System.Runtime.CompilerServices.Unsafe.AsRef(in _organizationNameTypeCode) = is1;
                break;
            case 2:
                if (value is NM nm2) System.Runtime.CompilerServices.Unsafe.AsRef(in _idNumber) = nm2;
                break;
            case 3:
                if (value is NM nm3) System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigit) = nm3;
                break;
            case 4:
                if (value is ID id4) System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigitScheme) = id4;
                break;
            case 5:
                if (value is HD hd5) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd5;
                break;
            case 6:
                if (value is ID id6) System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierTypeCode) = id6;
                break;
            case 7:
                if (value is HD hd7) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningFacility) = hd7;
                break;
            case 8:
                if (value is ID id8) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameRepresentationCode) = id8;
                break;
            case 9:
                if (value is ST st9) System.Runtime.CompilerServices.Unsafe.AsRef(in _organizationIdentifier) = st9;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _organizationName, delimiters, false);
        AppendComponent(sb, _organizationNameTypeCode, delimiters);
        AppendComponent(sb, _idNumber, delimiters);
        AppendComponent(sb, _checkDigit, delimiters);
        AppendComponent(sb, _checkDigitScheme, delimiters);
        AppendComponent(sb, _assigningAuthority, delimiters);
        AppendComponent(sb, _identifierTypeCode, delimiters);
        AppendComponent(sb, _assigningFacility, delimiters);
        AppendComponent(sb, _nameRepresentationCode, delimiters);
        AppendComponent(sb, _organizationIdentifier, delimiters);
        
        return sb.ToString().TrimEnd(delimiters.ComponentSeparator);
    }

    private static void AppendComponent<T>(StringBuilder sb, T component, in HL7Delimiters delimiters, bool addSeparator = true) where T : IHL7DataType
    {
        if (addSeparator)
            sb.Append(delimiters.ComponentSeparator);
        if (!component.IsEmpty)
            sb.Append(component.ToHL7String(delimiters));
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _organizationName) = new ST(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _organizationNameTypeCode) = new IS(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _idNumber) = new NM(component.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigit) = new NM(component.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigitScheme) = new ID(component.ToString());
                    break;
                case 5:
                    var hd5 = new HD();
                    hd5.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd5;
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierTypeCode) = new ID(component.ToString());
                    break;
                case 7:
                    var hd7 = new HD();
                    hd7.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningFacility) = hd7;
                    break;
                case 8:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameRepresentationCode) = new ID(component.ToString());
                    break;
                case 9:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _organizationIdentifier) = new ST(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_organizationName.Validate(out var onErrors))
            errors.AddRange(onErrors.Select(e => $"OrganizationName: {e}"));
        if (!_organizationNameTypeCode.Validate(out var ontcErrors))
            errors.AddRange(ontcErrors.Select(e => $"OrganizationNameTypeCode: {e}"));
        if (!_idNumber.Validate(out var idErrors))
            errors.AddRange(idErrors.Select(e => $"IdNumber: {e}"));
        if (!_checkDigit.Validate(out var cdErrors))
            errors.AddRange(cdErrors.Select(e => $"CheckDigit: {e}"));
        if (!_checkDigitScheme.Validate(out var cdsErrors))
            errors.AddRange(cdsErrors.Select(e => $"CheckDigitScheme: {e}"));
        if (!_assigningAuthority.Validate(out var aaErrors))
            errors.AddRange(aaErrors.Select(e => $"AssigningAuthority: {e}"));
        if (!_identifierTypeCode.Validate(out var itcErrors))
            errors.AddRange(itcErrors.Select(e => $"IdentifierTypeCode: {e}"));
        if (!_assigningFacility.Validate(out var afErrors))
            errors.AddRange(afErrors.Select(e => $"AssigningFacility: {e}"));
        if (!_nameRepresentationCode.Validate(out var nrcErrors))
            errors.AddRange(nrcErrors.Select(e => $"NameRepresentationCode: {e}"));
        if (!_organizationIdentifier.Validate(out var oiErrors))
            errors.AddRange(oiErrors.Select(e => $"OrganizationIdentifier: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => _organizationName.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is XON other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to XON.
    /// </summary>
    public static implicit operator XON(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var xon = new XON();
        xon.Parse(value.AsSpan(), HL7Delimiters.Default);
        return xon;
    }

    /// <summary>
    /// Implicit conversion from XON to string.
    /// </summary>
    public static implicit operator string(XON xon) => xon.ToString();
}
