using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// XPN - Extended Person Name
/// HL7 2.5 Composite Data Type representing a person's name with multiple components.
/// Format: FamilyName^GivenName^SecondNames^Suffix^Prefix^Degree^NameTypeCode^NameRepresentationCode^NameContext^NameValidityRange^NameAssemblyOrder^EffectiveDate^ExpirationDate^ProfessionalSuffix
/// </summary>
public readonly struct XPN : ICompositeDataType
{
    private readonly FN _familyName;
    private readonly ST _givenName;
    private readonly ST _secondNames;
    private readonly ST _suffix;
    private readonly ST _prefix;
    private readonly IS _degree;
    private readonly ID _nameTypeCode;
    private readonly ID _nameRepresentationCode;
    private readonly CE _nameContext;
    private readonly DR _nameValidityRange;
    private readonly ID _nameAssemblyOrder;
    private readonly DTM _effectiveDate;
    private readonly DTM _expirationDate;
    private readonly ST _professionalSuffix;

    /// <summary>
    /// Initializes a new instance of the XPN data type with all components.
    /// </summary>
    public XPN(
        FN familyName = default,
        ST givenName = default,
        ST secondNames = default,
        ST suffix = default,
        ST prefix = default,
        IS degree = default,
        ID nameTypeCode = default,
        ID nameRepresentationCode = default,
        CE nameContext = default,
        DR nameValidityRange = default,
        ID nameAssemblyOrder = default,
        DTM effectiveDate = default,
        DTM expirationDate = default,
        ST professionalSuffix = default)
    {
        _familyName = familyName;
        _givenName = givenName;
        _secondNames = secondNames;
        _suffix = suffix;
        _prefix = prefix;
        _degree = degree;
        _nameTypeCode = nameTypeCode;
        _nameRepresentationCode = nameRepresentationCode;
        _nameContext = nameContext;
        _nameValidityRange = nameValidityRange;
        _nameAssemblyOrder = nameAssemblyOrder;
        _effectiveDate = effectiveDate;
        _expirationDate = expirationDate;
        _professionalSuffix = professionalSuffix;
    }

    /// <inheritdoc/>
    public string TypeCode => "XPN";

    /// <inheritdoc/>
    public int ComponentCount => 14;

    /// <inheritdoc/>
    public bool IsEmpty => _familyName.IsEmpty && _givenName.IsEmpty && _secondNames.IsEmpty &&
                          _suffix.IsEmpty && _prefix.IsEmpty && _degree.IsEmpty &&
                          _nameTypeCode.IsEmpty && _nameRepresentationCode.IsEmpty &&
                          _nameContext.IsEmpty && _nameValidityRange.IsEmpty &&
                          _nameAssemblyOrder.IsEmpty && _effectiveDate.IsEmpty &&
                          _expirationDate.IsEmpty && _professionalSuffix.IsEmpty;

    /// <summary>
    /// XPN.1 - Family Name (can have subcomponents)
    /// </summary>
    public FN FamilyName => _familyName;

    /// <summary>
    /// XPN.2 - Given Name
    /// </summary>
    public ST GivenName => _givenName;

    /// <summary>
    /// XPN.3 - Second and Further Given Names or Initials Thereof
    /// </summary>
    public ST SecondNames => _secondNames;

    /// <summary>
    /// XPN.4 - Suffix (e.g., JR, SR, III)
    /// </summary>
    public ST Suffix => _suffix;

    /// <summary>
    /// XPN.5 - Prefix (e.g., DR, MR, MS)
    /// </summary>
    public ST Prefix => _prefix;

    /// <summary>
    /// XPN.6 - Degree (e.g., MD, PhD)
    /// </summary>
    public IS Degree => _degree;

    /// <summary>
    /// XPN.7 - Name Type Code (L=Legal, D=Display, etc.)
    /// </summary>
    public ID NameTypeCode => _nameTypeCode;

    /// <summary>
    /// XPN.8 - Name Representation Code (A=Alphabetic, I=Ideographic, P=Phonetic)
    /// </summary>
    public ID NameRepresentationCode => _nameRepresentationCode;

    /// <summary>
    /// XPN.9 - Name Context
    /// </summary>
    public CE NameContext => _nameContext;

    /// <summary>
    /// XPN.10 - Name Validity Range
    /// </summary>
    public DR NameValidityRange => _nameValidityRange;

    /// <summary>
    /// XPN.11 - Name Assembly Order
    /// </summary>
    public ID NameAssemblyOrder => _nameAssemblyOrder;

    /// <summary>
    /// XPN.12 - Effective Date
    /// </summary>
    public DTM EffectiveDate => _effectiveDate;

    /// <summary>
    /// XPN.13 - Expiration Date
    /// </summary>
    public DTM ExpirationDate => _expirationDate;

    /// <summary>
    /// XPN.14 - Professional Suffix
    /// </summary>
    public ST ProfessionalSuffix => _professionalSuffix;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _familyName,
            1 => _givenName,
            2 => _secondNames,
            3 => _suffix,
            4 => _prefix,
            5 => _degree,
            6 => _nameTypeCode,
            7 => _nameRepresentationCode,
            8 => _nameContext,
            9 => _nameValidityRange,
            10 => _nameAssemblyOrder,
            11 => _effectiveDate,
            12 => _expirationDate,
            13 => _professionalSuffix,
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
                if (value is FN fn) System.Runtime.CompilerServices.Unsafe.AsRef(in _familyName) = fn;
                break;
            case 1:
                if (value is ST st1) System.Runtime.CompilerServices.Unsafe.AsRef(in _givenName) = st1;
                break;
            case 2:
                if (value is ST st2) System.Runtime.CompilerServices.Unsafe.AsRef(in _secondNames) = st2;
                break;
            case 3:
                if (value is ST st3) System.Runtime.CompilerServices.Unsafe.AsRef(in _suffix) = st3;
                break;
            case 4:
                if (value is ST st4) System.Runtime.CompilerServices.Unsafe.AsRef(in _prefix) = st4;
                break;
            case 5:
                if (value is IS is5) System.Runtime.CompilerServices.Unsafe.AsRef(in _degree) = is5;
                break;
            case 6:
                if (value is ID id6) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameTypeCode) = id6;
                break;
            case 7:
                if (value is ID id7) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameRepresentationCode) = id7;
                break;
            case 8:
                if (value is CE ce8) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameContext) = ce8;
                break;
            case 9:
                if (value is DR dr9) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameValidityRange) = dr9;
                break;
            case 10:
                if (value is ID id10) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameAssemblyOrder) = id10;
                break;
            case 11:
                if (value is DTM dtm11) System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = dtm11;
                break;
            case 12:
                if (value is DTM dtm12) System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = dtm12;
                break;
            case 13:
                if (value is ST st13) System.Runtime.CompilerServices.Unsafe.AsRef(in _professionalSuffix) = st13;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _familyName, delimiters, false);
        AppendComponent(sb, _givenName, delimiters);
        AppendComponent(sb, _secondNames, delimiters);
        AppendComponent(sb, _suffix, delimiters);
        AppendComponent(sb, _prefix, delimiters);
        AppendComponent(sb, _degree, delimiters);
        AppendComponent(sb, _nameTypeCode, delimiters);
        AppendComponent(sb, _nameRepresentationCode, delimiters);
        AppendComponent(sb, _nameContext, delimiters);
        AppendComponent(sb, _nameValidityRange, delimiters);
        AppendComponent(sb, _nameAssemblyOrder, delimiters);
        AppendComponent(sb, _effectiveDate, delimiters);
        AppendComponent(sb, _expirationDate, delimiters);
        AppendComponent(sb, _professionalSuffix, delimiters);
        
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
                    var fn = new FN();
                    fn.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _familyName) = fn;
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _givenName) = new ST(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _secondNames) = new ST(component.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _suffix) = new ST(component.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _prefix) = new ST(component.ToString());
                    break;
                case 5:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _degree) = new IS(component.ToString());
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameTypeCode) = new ID(component.ToString());
                    break;
                case 7:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameRepresentationCode) = new ID(component.ToString());
                    break;
                case 8:
                    var ce = new CE();
                    ce.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameContext) = ce;
                    break;
                case 9:
                    var dr = new DR();
                    dr.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameValidityRange) = dr;
                    break;
                case 10:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameAssemblyOrder) = new ID(component.ToString());
                    break;
                case 11:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = new DTM(component.ToString());
                    break;
                case 12:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = new DTM(component.ToString());
                    break;
                case 13:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _professionalSuffix) = new ST(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

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
        if (!_nameTypeCode.Validate(out var ntcErrors))
            errors.AddRange(ntcErrors.Select(e => $"NameTypeCode: {e}"));
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

        return errors.Count == 0;
    }

    /// <summary>
    /// Gets a formatted representation of the person name.
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
    public override string ToString() => GetFormattedName(NameFormat.GivenFamily);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is XPN other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to XPN.
    /// </summary>
    public static implicit operator XPN(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var xpn = new XPN();
        xpn.Parse(value.AsSpan(), HL7Delimiters.Default);
        return xpn;
    }

    /// <summary>
    /// Implicit conversion from XPN to string.
    /// </summary>
    public static implicit operator string(XPN xpn) => xpn.ToString();
}

/// <summary>
/// Defines the format for displaying person names.
/// </summary>
public enum NameFormat
{
    /// <summary>
    /// Last, First (e.g., "Doe, John")
    /// </summary>
    FamilyGiven,

    /// <summary>
    /// First Last (e.g., "John Doe")
    /// </summary>
    GivenFamily,

    /// <summary>
    /// Full name with all components (e.g., "Dr. John Michael Doe Jr. MD")
    /// </summary>
    Full
}
