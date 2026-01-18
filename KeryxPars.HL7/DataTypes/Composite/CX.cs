using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// CX - Extended Composite ID with Check Digit
/// HL7 2.5 Composite Data Type representing an extended identifier.
/// Format: ID^CheckDigit^CheckDigitScheme^AssigningAuthority^IdentifierTypeCode^AssigningFacility^EffectiveDate^ExpirationDate^AssigningJurisdiction^AssigningAgency
/// </summary>
public readonly struct CX : ICompositeDataType
{
    private readonly ST _id;
    private readonly ST _checkDigit;
    private readonly ID _checkDigitScheme;
    private readonly HD _assigningAuthority;
    private readonly ID _identifierTypeCode;
    private readonly HD _assigningFacility;
    private readonly DT _effectiveDate;
    private readonly DT _expirationDate;
    private readonly CWE _assigningJurisdiction;
    private readonly CWE _assigningAgency;

    /// <summary>
    /// Initializes a new instance of the CX data type with all components.
    /// </summary>
    public CX(
        ST id = default,
        ST checkDigit = default,
        ID checkDigitScheme = default,
        HD assigningAuthority = default,
        ID identifierTypeCode = default,
        HD assigningFacility = default,
        DT effectiveDate = default,
        DT expirationDate = default,
        CWE assigningJurisdiction = default,
        CWE assigningAgency = default)
    {
        _id = id;
        _checkDigit = checkDigit;
        _checkDigitScheme = checkDigitScheme;
        _assigningAuthority = assigningAuthority;
        _identifierTypeCode = identifierTypeCode;
        _assigningFacility = assigningFacility;
        _effectiveDate = effectiveDate;
        _expirationDate = expirationDate;
        _assigningJurisdiction = assigningJurisdiction;
        _assigningAgency = assigningAgency;
    }

    /// <inheritdoc/>
    public string TypeCode => "CX";

    /// <inheritdoc/>
    public int ComponentCount => 10;

    /// <inheritdoc/>
    public bool IsEmpty => _id.IsEmpty && _checkDigit.IsEmpty && _checkDigitScheme.IsEmpty &&
                          _assigningAuthority.IsEmpty && _identifierTypeCode.IsEmpty &&
                          _assigningFacility.IsEmpty && _effectiveDate.IsEmpty &&
                          _expirationDate.IsEmpty && _assigningJurisdiction.IsEmpty &&
                          _assigningAgency.IsEmpty;

    /// <summary>
    /// CX.1 - ID Number
    /// </summary>
    public ST Id => _id;

    /// <summary>
    /// CX.2 - Check Digit
    /// </summary>
    public ST CheckDigit => _checkDigit;

    /// <summary>
    /// CX.3 - Check Digit Scheme
    /// </summary>
    public ID CheckDigitScheme => _checkDigitScheme;

    /// <summary>
    /// CX.4 - Assigning Authority
    /// </summary>
    public HD AssigningAuthority => _assigningAuthority;

    /// <summary>
    /// CX.5 - Identifier Type Code
    /// </summary>
    public ID IdentifierTypeCode => _identifierTypeCode;

    /// <summary>
    /// CX.6 - Assigning Facility
    /// </summary>
    public HD AssigningFacility => _assigningFacility;

    /// <summary>
    /// CX.7 - Effective Date
    /// </summary>
    public DT EffectiveDate => _effectiveDate;

    /// <summary>
    /// CX.8 - Expiration Date
    /// </summary>
    public DT ExpirationDate => _expirationDate;

    /// <summary>
    /// CX.9 - Assigning Jurisdiction
    /// </summary>
    public CWE AssigningJurisdiction => _assigningJurisdiction;

    /// <summary>
    /// CX.10 - Assigning Agency or Department
    /// </summary>
    public CWE AssigningAgency => _assigningAgency;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _id,
            1 => _checkDigit,
            2 => _checkDigitScheme,
            3 => _assigningAuthority,
            4 => _identifierTypeCode,
            5 => _assigningFacility,
            6 => _effectiveDate,
            7 => _expirationDate,
            8 => _assigningJurisdiction,
            9 => _assigningAgency,
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
                if (value is ST st0) System.Runtime.CompilerServices.Unsafe.AsRef(in _id) = st0;
                break;
            case 1:
                if (value is ST st1) System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigit) = st1;
                break;
            case 2:
                if (value is ID id2) System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigitScheme) = id2;
                break;
            case 3:
                if (value is HD hd3) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd3;
                break;
            case 4:
                if (value is ID id4) System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierTypeCode) = id4;
                break;
            case 5:
                if (value is HD hd5) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningFacility) = hd5;
                break;
            case 6:
                if (value is DT dt6) System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = dt6;
                break;
            case 7:
                if (value is DT dt7) System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = dt7;
                break;
            case 8:
                if (value is CWE cwe8) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningJurisdiction) = cwe8;
                break;
            case 9:
                if (value is CWE cwe9) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAgency) = cwe9;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _id, delimiters, false);
        AppendComponent(sb, _checkDigit, delimiters);
        AppendComponent(sb, _checkDigitScheme, delimiters);
        AppendComponent(sb, _assigningAuthority, delimiters);
        AppendComponent(sb, _identifierTypeCode, delimiters);
        AppendComponent(sb, _assigningFacility, delimiters);
        AppendComponent(sb, _effectiveDate, delimiters);
        AppendComponent(sb, _expirationDate, delimiters);
        AppendComponent(sb, _assigningJurisdiction, delimiters);
        AppendComponent(sb, _assigningAgency, delimiters);
        
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _id) = new ST(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigit) = new ST(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigitScheme) = new ID(component.ToString());
                    break;
                case 3:
                    var hd3 = new HD();
                    hd3.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd3;
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierTypeCode) = new ID(component.ToString());
                    break;
                case 5:
                    var hd5 = new HD();
                    hd5.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningFacility) = hd5;
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = new DT(component.ToString());
                    break;
                case 7:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = new DT(component.ToString());
                    break;
                case 8:
                    var cwe8 = new CWE();
                    cwe8.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningJurisdiction) = cwe8;
                    break;
                case 9:
                    var cwe9 = new CWE();
                    cwe9.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAgency) = cwe9;
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_id.Validate(out var idErrors))
            errors.AddRange(idErrors.Select(e => $"Id: {e}"));
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
        if (!_effectiveDate.Validate(out var edErrors))
            errors.AddRange(edErrors.Select(e => $"EffectiveDate: {e}"));
        if (!_expirationDate.Validate(out var exErrors))
            errors.AddRange(exErrors.Select(e => $"ExpirationDate: {e}"));
        if (!_assigningJurisdiction.Validate(out var ajErrors))
            errors.AddRange(ajErrors.Select(e => $"AssigningJurisdiction: {e}"));
        if (!_assigningAgency.Validate(out var agErrors))
            errors.AddRange(agErrors.Select(e => $"AssigningAgency: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => _id.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is CX other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to CX.
    /// </summary>
    public static implicit operator CX(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var cx = new CX();
        cx.Parse(value.AsSpan(), HL7Delimiters.Default);
        return cx;
    }

    /// <summary>
    /// Implicit conversion from CX to string.
    /// </summary>
    public static implicit operator string(CX cx) => cx.ToString();
}
