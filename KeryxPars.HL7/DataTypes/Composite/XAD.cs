using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// XAD - Extended Address
/// HL7 2.5 Composite Data Type representing an extended address.
/// Format: StreetAddress^OtherDesignation^City^StateOrProvince^ZipOrPostalCode^Country^AddressType^OtherGeographicDesignation^CountyParishCode^CensusTract^AddressRepresentationCode^AddressValidityRange^EffectiveDate^ExpirationDate
/// </summary>
public readonly struct XAD : ICompositeDataType
{
    private readonly SAD _streetAddress;
    private readonly ST _otherDesignation;
    private readonly ST _city;
    private readonly ST _stateOrProvince;
    private readonly ST _zipOrPostalCode;
    private readonly ID _country;
    private readonly ID _addressType;
    private readonly ST _otherGeographicDesignation;
    private readonly IS _countyParishCode;
    private readonly IS _censusTract;
    private readonly ID _addressRepresentationCode;
    private readonly DR _addressValidityRange;
    private readonly DTM _effectiveDate;
    private readonly DTM _expirationDate;

    /// <summary>
    /// Initializes a new instance of the XAD data type with all components.
    /// </summary>
    public XAD(
        SAD streetAddress = default,
        ST otherDesignation = default,
        ST city = default,
        ST stateOrProvince = default,
        ST zipOrPostalCode = default,
        ID country = default,
        ID addressType = default,
        ST otherGeographicDesignation = default,
        IS countyParishCode = default,
        IS censusTract = default,
        ID addressRepresentationCode = default,
        DR addressValidityRange = default,
        DTM effectiveDate = default,
        DTM expirationDate = default)
    {
        _streetAddress = streetAddress;
        _otherDesignation = otherDesignation;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _zipOrPostalCode = zipOrPostalCode;
        _country = country;
        _addressType = addressType;
        _otherGeographicDesignation = otherGeographicDesignation;
        _countyParishCode = countyParishCode;
        _censusTract = censusTract;
        _addressRepresentationCode = addressRepresentationCode;
        _addressValidityRange = addressValidityRange;
        _effectiveDate = effectiveDate;
        _expirationDate = expirationDate;
    }

    /// <inheritdoc/>
    public string TypeCode => "XAD";

    /// <inheritdoc/>
    public int ComponentCount => 14;

    /// <inheritdoc/>
    public bool IsEmpty => _streetAddress.IsEmpty && _otherDesignation.IsEmpty &&
                          _city.IsEmpty && _stateOrProvince.IsEmpty && _zipOrPostalCode.IsEmpty &&
                          _country.IsEmpty && _addressType.IsEmpty && _otherGeographicDesignation.IsEmpty &&
                          _countyParishCode.IsEmpty && _censusTract.IsEmpty &&
                          _addressRepresentationCode.IsEmpty && _addressValidityRange.IsEmpty &&
                          _effectiveDate.IsEmpty && _expirationDate.IsEmpty;

    /// <summary>
    /// XAD.1 - Street Address (can have subcomponents)
    /// </summary>
    public SAD StreetAddress => _streetAddress;

    /// <summary>
    /// XAD.2 - Other Designation
    /// </summary>
    public ST OtherDesignation => _otherDesignation;

    /// <summary>
    /// XAD.3 - City
    /// </summary>
    public ST City => _city;

    /// <summary>
    /// XAD.4 - State or Province
    /// </summary>
    public ST StateOrProvince => _stateOrProvince;

    /// <summary>
    /// XAD.5 - Zip or Postal Code
    /// </summary>
    public ST ZipOrPostalCode => _zipOrPostalCode;

    /// <summary>
    /// XAD.6 - Country
    /// </summary>
    public ID Country => _country;

    /// <summary>
    /// XAD.7 - Address Type
    /// </summary>
    public ID AddressType => _addressType;

    /// <summary>
    /// XAD.8 - Other Geographic Designation
    /// </summary>
    public ST OtherGeographicDesignation => _otherGeographicDesignation;

    /// <summary>
    /// XAD.9 - County/Parish Code
    /// </summary>
    public IS CountyParishCode => _countyParishCode;

    /// <summary>
    /// XAD.10 - Census Tract
    /// </summary>
    public IS CensusTract => _censusTract;

    /// <summary>
    /// XAD.11 - Address Representation Code
    /// </summary>
    public ID AddressRepresentationCode => _addressRepresentationCode;

    /// <summary>
    /// XAD.12 - Address Validity Range
    /// </summary>
    public DR AddressValidityRange => _addressValidityRange;

    /// <summary>
    /// XAD.13 - Effective Date
    /// </summary>
    public DTM EffectiveDate => _effectiveDate;

    /// <summary>
    /// XAD.14 - Expiration Date
    /// </summary>
    public DTM ExpirationDate => _expirationDate;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _streetAddress,
            1 => _otherDesignation,
            2 => _city,
            3 => _stateOrProvince,
            4 => _zipOrPostalCode,
            5 => _country,
            6 => _addressType,
            7 => _otherGeographicDesignation,
            8 => _countyParishCode,
            9 => _censusTract,
            10 => _addressRepresentationCode,
            11 => _addressValidityRange,
            12 => _effectiveDate,
            13 => _expirationDate,
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
                if (value is SAD sad) System.Runtime.CompilerServices.Unsafe.AsRef(in _streetAddress) = sad;
                break;
            case 1:
                if (value is ST st1) System.Runtime.CompilerServices.Unsafe.AsRef(in _otherDesignation) = st1;
                break;
            case 2:
                if (value is ST st2) System.Runtime.CompilerServices.Unsafe.AsRef(in _city) = st2;
                break;
            case 3:
                if (value is ST st3) System.Runtime.CompilerServices.Unsafe.AsRef(in _stateOrProvince) = st3;
                break;
            case 4:
                if (value is ST st4) System.Runtime.CompilerServices.Unsafe.AsRef(in _zipOrPostalCode) = st4;
                break;
            case 5:
                if (value is ID id5) System.Runtime.CompilerServices.Unsafe.AsRef(in _country) = id5;
                break;
            case 6:
                if (value is ID id6) System.Runtime.CompilerServices.Unsafe.AsRef(in _addressType) = id6;
                break;
            case 7:
                if (value is ST st7) System.Runtime.CompilerServices.Unsafe.AsRef(in _otherGeographicDesignation) = st7;
                break;
            case 8:
                if (value is IS is8) System.Runtime.CompilerServices.Unsafe.AsRef(in _countyParishCode) = is8;
                break;
            case 9:
                if (value is IS is9) System.Runtime.CompilerServices.Unsafe.AsRef(in _censusTract) = is9;
                break;
            case 10:
                if (value is ID id10) System.Runtime.CompilerServices.Unsafe.AsRef(in _addressRepresentationCode) = id10;
                break;
            case 11:
                if (value is DR dr11) System.Runtime.CompilerServices.Unsafe.AsRef(in _addressValidityRange) = dr11;
                break;
            case 12:
                if (value is DTM dtm12) System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = dtm12;
                break;
            case 13:
                if (value is DTM dtm13) System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = dtm13;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _streetAddress, delimiters, false);
        AppendComponent(sb, _otherDesignation, delimiters);
        AppendComponent(sb, _city, delimiters);
        AppendComponent(sb, _stateOrProvince, delimiters);
        AppendComponent(sb, _zipOrPostalCode, delimiters);
        AppendComponent(sb, _country, delimiters);
        AppendComponent(sb, _addressType, delimiters);
        AppendComponent(sb, _otherGeographicDesignation, delimiters);
        AppendComponent(sb, _countyParishCode, delimiters);
        AppendComponent(sb, _censusTract, delimiters);
        AppendComponent(sb, _addressRepresentationCode, delimiters);
        AppendComponent(sb, _addressValidityRange, delimiters);
        AppendComponent(sb, _effectiveDate, delimiters);
        AppendComponent(sb, _expirationDate, delimiters);
        
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
                    var sad = new SAD();
                    sad.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _streetAddress) = sad;
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _otherDesignation) = new ST(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _city) = new ST(component.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _stateOrProvince) = new ST(component.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _zipOrPostalCode) = new ST(component.ToString());
                    break;
                case 5:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _country) = new ID(component.ToString());
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _addressType) = new ID(component.ToString());
                    break;
                case 7:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _otherGeographicDesignation) = new ST(component.ToString());
                    break;
                case 8:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _countyParishCode) = new IS(component.ToString());
                    break;
                case 9:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _censusTract) = new IS(component.ToString());
                    break;
                case 10:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _addressRepresentationCode) = new ID(component.ToString());
                    break;
                case 11:
                    var dr = new DR();
                    dr.Parse(component, delimiters);
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _addressValidityRange) = dr;
                    break;
                case 12:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _effectiveDate) = new DTM(component.ToString());
                    break;
                case 13:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _expirationDate) = new DTM(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_streetAddress.Validate(out var streetErrors))
            errors.AddRange(streetErrors.Select(e => $"StreetAddress: {e}"));
        if (!_otherDesignation.Validate(out var odErrors))
            errors.AddRange(odErrors.Select(e => $"OtherDesignation: {e}"));
        if (!_city.Validate(out var cityErrors))
            errors.AddRange(cityErrors.Select(e => $"City: {e}"));
        if (!_stateOrProvince.Validate(out var stateErrors))
            errors.AddRange(stateErrors.Select(e => $"StateOrProvince: {e}"));
        if (!_zipOrPostalCode.Validate(out var zipErrors))
            errors.AddRange(zipErrors.Select(e => $"ZipOrPostalCode: {e}"));
        if (!_country.Validate(out var countryErrors))
            errors.AddRange(countryErrors.Select(e => $"Country: {e}"));
        if (!_addressType.Validate(out var typeErrors))
            errors.AddRange(typeErrors.Select(e => $"AddressType: {e}"));
        if (!_otherGeographicDesignation.Validate(out var ogdErrors))
            errors.AddRange(ogdErrors.Select(e => $"OtherGeographicDesignation: {e}"));
        if (!_countyParishCode.Validate(out var countyErrors))
            errors.AddRange(countyErrors.Select(e => $"CountyParishCode: {e}"));
        if (!_censusTract.Validate(out var censusErrors))
            errors.AddRange(censusErrors.Select(e => $"CensusTract: {e}"));
        if (!_addressRepresentationCode.Validate(out var arcErrors))
            errors.AddRange(arcErrors.Select(e => $"AddressRepresentationCode: {e}"));
        if (!_addressValidityRange.Validate(out var avrErrors))
            errors.AddRange(avrErrors.Select(e => $"AddressValidityRange: {e}"));
        if (!_effectiveDate.Validate(out var edErrors))
            errors.AddRange(edErrors.Select(e => $"EffectiveDate: {e}"));
        if (!_expirationDate.Validate(out var exErrors))
            errors.AddRange(exErrors.Select(e => $"ExpirationDate: {e}"));

        return errors.Count == 0;
    }

    /// <summary>
    /// Gets a formatted representation of the address.
    /// </summary>
    /// <returns>The formatted address string.</returns>
    public string GetFormattedAddress()
    {
        var parts = new List<string>();
        
        if (!_streetAddress.IsEmpty) 
            parts.Add(_streetAddress.ToString());
        
        if (!_city.IsEmpty) 
            parts.Add(_city.Value);
        
        if (!_stateOrProvince.IsEmpty && !_zipOrPostalCode.IsEmpty)
            parts.Add($"{_stateOrProvince.Value} {_zipOrPostalCode.Value}");
        else if (!_stateOrProvince.IsEmpty)
            parts.Add(_stateOrProvince.Value);
        else if (!_zipOrPostalCode.IsEmpty)
            parts.Add(_zipOrPostalCode.Value);
        
        if (!_country.IsEmpty)
            parts.Add(_country.Value);
        
        return string.Join(", ", parts);
    }

    /// <inheritdoc/>
    public override string ToString() => GetFormattedAddress();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is XAD other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to XAD.
    /// </summary>
    public static implicit operator XAD(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var xad = new XAD();
        xad.Parse(value.AsSpan(), HL7Delimiters.Default);
        return xad;
    }

    /// <summary>
    /// Implicit conversion from XAD to string.
    /// </summary>
    public static implicit operator string(XAD xad) => xad.ToString();
}
