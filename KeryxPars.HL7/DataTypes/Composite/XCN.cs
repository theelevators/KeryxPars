using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// XCN - Extended Composite ID Number and Name for Persons
/// HL7 2.5 Composite Data Type representing a person's identification and name (typically for providers).
/// Format: ID^FamilyName^GivenName^SecondNames^Suffix^Prefix^Degree^SourceTable^AssigningAuthority^NameTypeCode^IdentifierCheckDigit^CheckDigitScheme^IdentifierTypeCode^AssigningFacility^NameRepresentationCode^NameContext^NameValidityRange^NameAssemblyOrder^EffectiveDate^ExpirationDate^ProfessionalSuffix^AssigningJurisdiction^AssigningAgency
/// </summary>
public readonly struct XCN : ICompositeDataType
{
    private readonly ST _id;
    private readonly FN _familyName;
    private readonly ST _givenName;
    private readonly ST _secondNames;
    private readonly ST _suffix;
    private readonly ST _prefix;
    private readonly IS _degree;
    private readonly IS _sourceTable;
    private readonly HD _assigningAuthority;
    private readonly ID _nameTypeCode;
    private readonly ST _identifierCheckDigit;
    private readonly ID _checkDigitScheme;
    private readonly ID _identifierTypeCode;
    private readonly HD _assigningFacility;
    private readonly ID _nameRepresentationCode;
    private readonly CE _nameContext;
    private readonly DR _nameValidityRange;
    private readonly ID _nameAssemblyOrder;
    private readonly DTM _effectiveDate;
    private readonly DTM _expirationDate;
    private readonly ST _professionalSuffix;
    private readonly CWE _assigningJurisdiction;
    private readonly CWE _assigningAgency;

    /// <summary>
    /// Initializes a new instance of the XCN data type with all components.
    /// </summary>
    public XCN(
        ST id = default,
        FN familyName = default,
        ST givenName = default,
        ST secondNames = default,
        ST suffix = default,
        ST prefix = default,
        IS degree = default,
        IS sourceTable = default,
        HD assigningAuthority = default,
        ID nameTypeCode = default,
        ST identifierCheckDigit = default,
        ID checkDigitScheme = default,
        ID identifierTypeCode = default,
        HD assigningFacility = default,
        ID nameRepresentationCode = default,
        CE nameContext = default,
        DR nameValidityRange = default,
        ID nameAssemblyOrder = default,
        DTM effectiveDate = default,
        DTM expirationDate = default,
        ST professionalSuffix = default,
        CWE assigningJurisdiction = default,
        CWE assigningAgency = default)
    {
        _id = id;
        _familyName = familyName;
        _givenName = givenName;
        _secondNames = secondNames;
        _suffix = suffix;
        _prefix = prefix;
        _degree = degree;
        _sourceTable = sourceTable;
        _assigningAuthority = assigningAuthority;
        _nameTypeCode = nameTypeCode;
        _identifierCheckDigit = identifierCheckDigit;
        _checkDigitScheme = checkDigitScheme;
        _identifierTypeCode = identifierTypeCode;
        _assigningFacility = assigningFacility;
        _nameRepresentationCode = nameRepresentationCode;
        _nameContext = nameContext;
        _nameValidityRange = nameValidityRange;
        _nameAssemblyOrder = nameAssemblyOrder;
        _effectiveDate = effectiveDate;
        _expirationDate = expirationDate;
        _professionalSuffix = professionalSuffix;
        _assigningJurisdiction = assigningJurisdiction;
        _assigningAgency = assigningAgency;
    }

    /// <inheritdoc/>
    public string TypeCode => "XCN";

    /// <inheritdoc/>
    public int ComponentCount => 23;

    /// <inheritdoc/>
    public bool IsEmpty => _id.IsEmpty && _familyName.IsEmpty && _givenName.IsEmpty &&
                          _secondNames.IsEmpty && _suffix.IsEmpty && _prefix.IsEmpty &&
                          _degree.IsEmpty && _sourceTable.IsEmpty && _assigningAuthority.IsEmpty &&
                          _nameTypeCode.IsEmpty && _identifierCheckDigit.IsEmpty &&
                          _checkDigitScheme.IsEmpty && _identifierTypeCode.IsEmpty &&
                          _assigningFacility.IsEmpty && _nameRepresentationCode.IsEmpty &&
                          _nameContext.IsEmpty && _nameValidityRange.IsEmpty &&
                          _nameAssemblyOrder.IsEmpty && _effectiveDate.IsEmpty &&
                          _expirationDate.IsEmpty && _professionalSuffix.IsEmpty &&
                          _assigningJurisdiction.IsEmpty && _assigningAgency.IsEmpty;

    /// <summary>
    /// XCN.1 - ID Number
    /// </summary>
    public ST Id => _id;

    /// <summary>
    /// XCN.2 - Family Name
    /// </summary>
    public FN FamilyName => _familyName;

    /// <summary>
    /// XCN.3 - Given Name
    /// </summary>
    public ST GivenName => _givenName;

    /// <summary>
    /// XCN.4 - Second and Further Given Names or Initials
    /// </summary>
    public ST SecondNames => _secondNames;

    /// <summary>
    /// XCN.5 - Suffix (e.g., JR, SR, III)
    /// </summary>
    public ST Suffix => _suffix;

    /// <summary>
    /// XCN.6 - Prefix (e.g., DR, MR, MS)
    /// </summary>
    public ST Prefix => _prefix;

    /// <summary>
    /// XCN.7 - Degree (e.g., MD, PhD)
    /// </summary>
    public IS Degree => _degree;

    /// <summary>
    /// XCN.8 - Source Table
    /// </summary>
    public IS SourceTable => _sourceTable;

    /// <summary>
    /// XCN.9 - Assigning Authority
    /// </summary>
    public HD AssigningAuthority => _assigningAuthority;

    /// <summary>
    /// XCN.10 - Name Type Code
    /// </summary>
    public ID NameTypeCode => _nameTypeCode;

    /// <summary>
    /// XCN.11 - Identifier Check Digit
    /// </summary>
    public ST IdentifierCheckDigit => _identifierCheckDigit;

    /// <summary>
    /// XCN.12 - Check Digit Scheme
    /// </summary>
    public ID CheckDigitScheme => _checkDigitScheme;

    /// <summary>
    /// XCN.13 - Identifier Type Code
    /// </summary>
    public ID IdentifierTypeCode => _identifierTypeCode;

    /// <summary>
    /// XCN.14 - Assigning Facility
    /// </summary>
    public HD AssigningFacility => _assigningFacility;

    /// <summary>
    /// XCN.15 - Name Representation Code
    /// </summary>
    public ID NameRepresentationCode => _nameRepresentationCode;

    /// <summary>
    /// XCN.16 - Name Context
    /// </summary>
    public CE NameContext => _nameContext;

    /// <summary>
    /// XCN.17 - Name Validity Range
    /// </summary>
    public DR NameValidityRange => _nameValidityRange;

    /// <summary>
    /// XCN.18 - Name Assembly Order
    /// </summary>
    public ID NameAssemblyOrder => _nameAssemblyOrder;

    /// <summary>
    /// XCN.19 - Effective Date
    /// </summary>
    public DTM EffectiveDate => _effectiveDate;

    /// <summary>
    /// XCN.20 - Expiration Date
    /// </summary>
    public DTM ExpirationDate => _expirationDate;

    /// <summary>
    /// XCN.21 - Professional Suffix
    /// </summary>
    public ST ProfessionalSuffix => _professionalSuffix;

    /// <summary>
    /// XCN.22 - Assigning Jurisdiction
    /// </summary>
    public CWE AssigningJurisdiction => _assigningJurisdiction;

    /// <summary>
    /// XCN.23 - Assigning Agency or Department
    /// </summary>
    public CWE AssigningAgency => _assigningAgency;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _id,
            1 => _familyName,
            2 => _givenName,
            3 => _secondNames,
            4 => _suffix,
            5 => _prefix,
            6 => _degree,
            7 => _sourceTable,
            8 => _assigningAuthority,
            9 => _nameTypeCode,
            10 => _identifierCheckDigit,
            11 => _checkDigitScheme,
            12 => _identifierTypeCode,
            13 => _assigningFacility,
            14 => _nameRepresentationCode,
            15 => _nameContext,
            16 => _nameValidityRange,
            17 => _nameAssemblyOrder,
            18 => _effectiveDate,
            19 => _expirationDate,
            20 => _professionalSuffix,
            21 => _assigningJurisdiction,
            22 => _assigningAgency,
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
                if (value is FN fn1) System.Runtime.CompilerServices.Unsafe.AsRef(in _familyName) = fn1;
                break;
            case 2:
                if (value is ST st2) System.Runtime.CompilerServices.Unsafe.AsRef(in _givenName) = st2;
                break;
            case 3:
                if (value is ST st3) System.Runtime.CompilerServices.Unsafe.AsRef(in _secondNames) = st3;
                break;
            case 4:
                if (value is ST st4) System.Runtime.CompilerServices.Unsafe.AsRef(in _suffix) = st4;
                break;
            case 5:
                if (value is ST st5) System.Runtime.CompilerServices.Unsafe.AsRef(in _prefix) = st5;
                break;
            case 6:
                if (value is IS is6) System.Runtime.CompilerServices.Unsafe.AsRef(in _degree) = is6;
                break;
            case 7:
                if (value is IS is7) System.Runtime.CompilerServices.Unsafe.AsRef(in _sourceTable) = is7;
                break;
            case 8:
                if (value is HD hd8) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd8;
                break;
            case 9:
                if (value is ID id9) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameTypeCode) = id9;
                break;
            case 10:
                if (value is ST st10) System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierCheckDigit) = st10;
                break;
            case 11:
                if (value is ID id11) System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigitScheme) = id11;
                break;
            case 12:
                if (value is ID id12) System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierTypeCode) = id12;
                break;
            case 13:
                if (value is HD hd13) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningFacility) = hd13;
                break;
            case 14:
                if (value is ID id14) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameRepresentationCode) = id14;
                break;
            case 15:
                if (value is CE ce15) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameContext) = ce15;
                break;
            case 16:
                if (value is DR dr16) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameValidityRange) = dr16;
                break;
            case 17:
                if (value is ID id17) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameAssemblyOrder) = id17;
                break;
            case 18:
                if (value is DTM dtm18) System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = dtm18;
                break;
            case 19:
                if (value is DTM dtm19) System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = dtm19;
                break;
            case 20:
                if (value is ST st20) System.Runtime.CompilerServices.Unsafe.AsRef(in _professionalSuffix) = st20;
                break;
            case 21:
                if (value is CWE cwe21) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningJurisdiction) = cwe21;
                break;
            case 22:
                if (value is CWE cwe22) System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAgency) = cwe22;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _id, delimiters, false);
        AppendComponent(sb, _familyName, delimiters);
        AppendComponent(sb, _givenName, delimiters);
        AppendComponent(sb, _secondNames, delimiters);
        AppendComponent(sb, _suffix, delimiters);
        AppendComponent(sb, _prefix, delimiters);
        AppendComponent(sb, _degree, delimiters);
        AppendComponent(sb, _sourceTable, delimiters);
        AppendComponent(sb, _assigningAuthority, delimiters);
        AppendComponent(sb, _nameTypeCode, delimiters);
        AppendComponent(sb, _identifierCheckDigit, delimiters);
        AppendComponent(sb, _checkDigitScheme, delimiters);
        AppendComponent(sb, _identifierTypeCode, delimiters);
        AppendComponent(sb, _assigningFacility, delimiters);
        AppendComponent(sb, _nameRepresentationCode, delimiters);
        AppendComponent(sb, _nameContext, delimiters);
        AppendComponent(sb, _nameValidityRange, delimiters);
        AppendComponent(sb, _nameAssemblyOrder, delimiters);
        AppendComponent(sb, _effectiveDate, delimiters);
        AppendComponent(sb, _expirationDate, delimiters);
        AppendComponent(sb, _professionalSuffix, delimiters);
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
                    var fn = new FN();
                    fn.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _familyName) = fn;
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _givenName) = new ST(component.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _secondNames) = new ST(component.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _suffix) = new ST(component.ToString());
                    break;
                case 5:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _prefix) = new ST(component.ToString());
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _degree) = new IS(component.ToString());
                    break;
                case 7:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _sourceTable) = new IS(component.ToString());
                    break;
                case 8:
                    var hd8 = new HD();
                    hd8.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAuthority) = hd8;
                    break;
                case 9:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameTypeCode) = new ID(component.ToString());
                    break;
                case 10:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierCheckDigit) = new ST(component.ToString());
                    break;
                case 11:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _checkDigitScheme) = new ID(component.ToString());
                    break;
                case 12:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _identifierTypeCode) = new ID(component.ToString());
                    break;
                case 13:
                    var hd13 = new HD();
                    hd13.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningFacility) = hd13;
                    break;
                case 14:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameRepresentationCode) = new ID(component.ToString());
                    break;
                case 15:
                    var ce = new CE();
                    ce.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameContext) = ce;
                    break;
                case 16:
                    var dr = new DR();
                    dr.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameValidityRange) = dr;
                    break;
                case 17:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameAssemblyOrder) = new ID(component.ToString());
                    break;
                case 18:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = new DTM(component.ToString());
                    break;
                case 19:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = new DTM(component.ToString());
                    break;
                case 20:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _professionalSuffix) = new ST(component.ToString());
                    break;
                case 21:
                    var cwe21 = new CWE();
                    cwe21.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningJurisdiction) = cwe21;
                    break;
                case 22:
                    var cwe22 = new CWE();
                    cwe22.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _assigningAgency) = cwe22;
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
        if (!_familyName.Validate(out var fnErrors))
            errors.AddRange(fnErrors.Select(e => $"FamilyName: {e}"));
        if (!_givenName.Validate(out var gnErrors))
            errors.AddRange(gnErrors.Select(e => $"GivenName: {e}"));
        if (!_secondNames.Validate(out var snErrors))
            errors.AddRange(snErrors.Select(e => $"SecondNames: {e}"));
        if (!_suffix.Validate(out var sufErrors))
            errors.AddRange(sufErrors.Select(e => $"Suffix: {e}"));
        if (!_prefix.Validate(out var preErrors))
            errors.AddRange(preErrors.Select(e => $"Prefix: {e}"));
        if (!_degree.Validate(out var degErrors))
            errors.AddRange(degErrors.Select(e => $"Degree: {e}"));
        if (!_sourceTable.Validate(out var stErrors))
            errors.AddRange(stErrors.Select(e => $"SourceTable: {e}"));
        if (!_assigningAuthority.Validate(out var aaErrors))
            errors.AddRange(aaErrors.Select(e => $"AssigningAuthority: {e}"));
        if (!_nameTypeCode.Validate(out var ntcErrors))
            errors.AddRange(ntcErrors.Select(e => $"NameTypeCode: {e}"));
        if (!_identifierCheckDigit.Validate(out var icdErrors))
            errors.AddRange(icdErrors.Select(e => $"IdentifierCheckDigit: {e}"));
        if (!_checkDigitScheme.Validate(out var cdsErrors))
            errors.AddRange(cdsErrors.Select(e => $"CheckDigitScheme: {e}"));
        if (!_identifierTypeCode.Validate(out var itcErrors))
            errors.AddRange(itcErrors.Select(e => $"IdentifierTypeCode: {e}"));
        if (!_assigningFacility.Validate(out var afErrors))
            errors.AddRange(afErrors.Select(e => $"AssigningFacility: {e}"));
        if (!_nameRepresentationCode.Validate(out var nrcErrors))
            errors.AddRange(nrcErrors.Select(e => $"NameRepresentationCode: {e}"));
        if (!_nameContext.Validate(out var ncErrors))
            errors.AddRange(ncErrors.Select(e => $"NameContext: {e}"));
        if (!_nameValidityRange.Validate(out var nvrErrors))
            errors.AddRange(nvrErrors.Select(e => $"NameValidityRange: {e}"));
        if (!_nameAssemblyOrder.Validate(out var naoErrors))
            errors.AddRange(naoErrors.Select(e => $"NameAssemblyOrder: {e}"));
        if (!_effectiveDate.Validate(out var edErrors))
            errors.AddRange(edErrors.Select(e => $"EffectiveDate: {e}"));
        if (!_expirationDate.Validate(out var exErrors))
            errors.AddRange(exErrors.Select(e => $"ExpirationDate: {e}"));
        if (!_professionalSuffix.Validate(out var psErrors))
            errors.AddRange(psErrors.Select(e => $"ProfessionalSuffix: {e}"));
        if (!_assigningJurisdiction.Validate(out var ajErrors))
            errors.AddRange(ajErrors.Select(e => $"AssigningJurisdiction: {e}"));
        if (!_assigningAgency.Validate(out var agErrors))
            errors.AddRange(agErrors.Select(e => $"AssigningAgency: {e}"));

        return errors.Count == 0;
    }

    /// <summary>
    /// Gets a formatted representation of the provider name.
    /// </summary>
    /// <param name="format">The format to use for the name.</param>
    /// <returns>The formatted name string.</returns>
    public string GetFormattedName(NameFormat format = NameFormat.FamilyGiven)
    {
        return format switch
        {
            NameFormat.FamilyGiven => $"{_familyName.Surname.Value}, {_givenName.Value}".Trim(' ', ','),
            NameFormat.GivenFamily => $"{_givenName.Value} {_familyName.Surname.Value}".Trim(),
            NameFormat.Full => $"{_prefix.Value} {_givenName.Value} {_secondNames.Value} {_familyName.Surname.Value} {_suffix.Value} {_degree.Value}".Trim(),
            _ => ToHL7String(HL7Delimiters.Default)
        };
    }

    /// <inheritdoc/>
    public override string ToString() => !_id.IsEmpty ? 
        $"{_id.Value}: {GetFormattedName(NameFormat.GivenFamily)}" : 
        GetFormattedName(NameFormat.GivenFamily);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is XCN other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to XCN.
    /// </summary>
    public static implicit operator XCN(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var xcn = new XCN();
        xcn.Parse(value.AsSpan(), HL7Delimiters.Default);
        return xcn;
    }

    /// <summary>
    /// Implicit conversion from XCN to string.
    /// </summary>
    public static implicit operator string(XCN xcn) => xcn.ToString();
}
