using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// XTN - Extended Telecommunication Number
/// HL7 2.5 Composite Data Type representing a telecommunication number.
/// Format: [NNN][(999)]999-9999[X99999][B99999][C any text]^TelecommunicationUseCode^TelecommunicationEquipmentType^EmailAddress^CountryCode^AreaCityCode^LocalNumber^Extension^AnyText^ExtensionPrefix^SpeedDialCode^UnformattedTelephoneNumber
/// </summary>
public readonly struct XTN : ICompositeDataType
{
    private readonly ST _telephoneNumber;
    private readonly ID _telecommunicationUseCode;
    private readonly ID _telecommunicationEquipmentType;
    private readonly ST _emailAddress;
    private readonly NM _countryCode;
    private readonly NM _areaCityCode;
    private readonly NM _localNumber;
    private readonly NM _extension;
    private readonly ST _anyText;
    private readonly ST _extensionPrefix;
    private readonly ST _speedDialCode;
    private readonly ST _unformattedTelephoneNumber;

    /// <summary>
    /// Initializes a new instance of the XTN data type with all components.
    /// </summary>
    public XTN(
        ST telephoneNumber = default,
        ID telecommunicationUseCode = default,
        ID telecommunicationEquipmentType = default,
        ST emailAddress = default,
        NM countryCode = default,
        NM areaCityCode = default,
        NM localNumber = default,
        NM extension = default,
        ST anyText = default,
        ST extensionPrefix = default,
        ST speedDialCode = default,
        ST unformattedTelephoneNumber = default)
    {
        _telephoneNumber = telephoneNumber;
        _telecommunicationUseCode = telecommunicationUseCode;
        _telecommunicationEquipmentType = telecommunicationEquipmentType;
        _emailAddress = emailAddress;
        _countryCode = countryCode;
        _areaCityCode = areaCityCode;
        _localNumber = localNumber;
        _extension = extension;
        _anyText = anyText;
        _extensionPrefix = extensionPrefix;
        _speedDialCode = speedDialCode;
        _unformattedTelephoneNumber = unformattedTelephoneNumber;
    }

    /// <inheritdoc/>
    public string TypeCode => "XTN";

    /// <inheritdoc/>
    public int ComponentCount => 12;

    /// <inheritdoc/>
    public bool IsEmpty => _telephoneNumber.IsEmpty && _telecommunicationUseCode.IsEmpty &&
                          _telecommunicationEquipmentType.IsEmpty && _emailAddress.IsEmpty &&
                          _countryCode.IsEmpty && _areaCityCode.IsEmpty && _localNumber.IsEmpty &&
                          _extension.IsEmpty && _anyText.IsEmpty && _extensionPrefix.IsEmpty &&
                          _speedDialCode.IsEmpty && _unformattedTelephoneNumber.IsEmpty;

    /// <summary>
    /// XTN.1 - Telephone Number (formatted)
    /// </summary>
    public ST TelephoneNumber => _telephoneNumber;

    /// <summary>
    /// XTN.2 - Telecommunication Use Code
    /// </summary>
    public ID TelecommunicationUseCode => _telecommunicationUseCode;

    /// <summary>
    /// XTN.3 - Telecommunication Equipment Type
    /// </summary>
    public ID TelecommunicationEquipmentType => _telecommunicationEquipmentType;

    /// <summary>
    /// XTN.4 - Email Address
    /// </summary>
    public ST EmailAddress => _emailAddress;

    /// <summary>
    /// XTN.5 - Country Code
    /// </summary>
    public NM CountryCode => _countryCode;

    /// <summary>
    /// XTN.6 - Area/City Code
    /// </summary>
    public NM AreaCityCode => _areaCityCode;

    /// <summary>
    /// XTN.7 - Local Number
    /// </summary>
    public NM LocalNumber => _localNumber;

    /// <summary>
    /// XTN.8 - Extension
    /// </summary>
    public NM Extension => _extension;

    /// <summary>
    /// XTN.9 - Any Text
    /// </summary>
    public ST AnyText => _anyText;

    /// <summary>
    /// XTN.10 - Extension Prefix
    /// </summary>
    public ST ExtensionPrefix => _extensionPrefix;

    /// <summary>
    /// XTN.11 - Speed Dial Code
    /// </summary>
    public ST SpeedDialCode => _speedDialCode;

    /// <summary>
    /// XTN.12 - Unformatted Telephone Number
    /// </summary>
    public ST UnformattedTelephoneNumber => _unformattedTelephoneNumber;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _telephoneNumber,
            1 => _telecommunicationUseCode,
            2 => _telecommunicationEquipmentType,
            3 => _emailAddress,
            4 => _countryCode,
            5 => _areaCityCode,
            6 => _localNumber,
            7 => _extension,
            8 => _anyText,
            9 => _extensionPrefix,
            10 => _speedDialCode,
            11 => _unformattedTelephoneNumber,
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
                if (value is ST st0) System.Runtime.CompilerServices.Unsafe.AsRef(in _telephoneNumber) = st0;
                break;
            case 1:
                if (value is ID id1) System.Runtime.CompilerServices.Unsafe.AsRef(in _telecommunicationUseCode) = id1;
                break;
            case 2:
                if (value is ID id2) System.Runtime.CompilerServices.Unsafe.AsRef(in _telecommunicationEquipmentType) = id2;
                break;
            case 3:
                if (value is ST st3) System.Runtime.CompilerServices.Unsafe.AsRef(in _emailAddress) = st3;
                break;
            case 4:
                if (value is NM nm4) System.Runtime.CompilerServices.Unsafe.AsRef(in _countryCode) = nm4;
                break;
            case 5:
                if (value is NM nm5) System.Runtime.CompilerServices.Unsafe.AsRef(in _areaCityCode) = nm5;
                break;
            case 6:
                if (value is NM nm6) System.Runtime.CompilerServices.Unsafe.AsRef(in _localNumber) = nm6;
                break;
            case 7:
                if (value is NM nm7) System.Runtime.CompilerServices.Unsafe.AsRef(in _extension) = nm7;
                break;
            case 8:
                if (value is ST st8) System.Runtime.CompilerServices.Unsafe.AsRef(in _anyText) = st8;
                break;
            case 9:
                if (value is ST st9) System.Runtime.CompilerServices.Unsafe.AsRef(in _extensionPrefix) = st9;
                break;
            case 10:
                if (value is ST st10) System.Runtime.CompilerServices.Unsafe.AsRef(in _speedDialCode) = st10;
                break;
            case 11:
                if (value is ST st11) System.Runtime.CompilerServices.Unsafe.AsRef(in _unformattedTelephoneNumber) = st11;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _telephoneNumber, delimiters, false);
        AppendComponent(sb, _telecommunicationUseCode, delimiters);
        AppendComponent(sb, _telecommunicationEquipmentType, delimiters);
        AppendComponent(sb, _emailAddress, delimiters);
        AppendComponent(sb, _countryCode, delimiters);
        AppendComponent(sb, _areaCityCode, delimiters);
        AppendComponent(sb, _localNumber, delimiters);
        AppendComponent(sb, _extension, delimiters);
        AppendComponent(sb, _anyText, delimiters);
        AppendComponent(sb, _extensionPrefix, delimiters);
        AppendComponent(sb, _speedDialCode, delimiters);
        AppendComponent(sb, _unformattedTelephoneNumber, delimiters);
        
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _telephoneNumber) = new ST(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _telecommunicationUseCode) = new ID(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _telecommunicationEquipmentType) = new ID(component.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _emailAddress) = new ST(component.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _countryCode) = new NM(component.ToString());
                    break;
                case 5:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _areaCityCode) = new NM(component.ToString());
                    break;
                case 6:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _localNumber) = new NM(component.ToString());
                    break;
                case 7:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _extension) = new NM(component.ToString());
                    break;
                case 8:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _anyText) = new ST(component.ToString());
                    break;
                case 9:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _extensionPrefix) = new ST(component.ToString());
                    break;
                case 10:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _speedDialCode) = new ST(component.ToString());
                    break;
                case 11:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _unformattedTelephoneNumber) = new ST(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_telephoneNumber.Validate(out var tnErrors))
            errors.AddRange(tnErrors.Select(e => $"TelephoneNumber: {e}"));
        if (!_telecommunicationUseCode.Validate(out var tucErrors))
            errors.AddRange(tucErrors.Select(e => $"TelecommunicationUseCode: {e}"));
        if (!_telecommunicationEquipmentType.Validate(out var tetErrors))
            errors.AddRange(tetErrors.Select(e => $"TelecommunicationEquipmentType: {e}"));
        if (!_emailAddress.Validate(out var emailErrors))
            errors.AddRange(emailErrors.Select(e => $"EmailAddress: {e}"));
        if (!_countryCode.Validate(out var ccErrors))
            errors.AddRange(ccErrors.Select(e => $"CountryCode: {e}"));
        if (!_areaCityCode.Validate(out var accErrors))
            errors.AddRange(accErrors.Select(e => $"AreaCityCode: {e}"));
        if (!_localNumber.Validate(out var lnErrors))
            errors.AddRange(lnErrors.Select(e => $"LocalNumber: {e}"));
        if (!_extension.Validate(out var extErrors))
            errors.AddRange(extErrors.Select(e => $"Extension: {e}"));
        if (!_anyText.Validate(out var atErrors))
            errors.AddRange(atErrors.Select(e => $"AnyText: {e}"));
        if (!_extensionPrefix.Validate(out var epErrors))
            errors.AddRange(epErrors.Select(e => $"ExtensionPrefix: {e}"));
        if (!_speedDialCode.Validate(out var sdcErrors))
            errors.AddRange(sdcErrors.Select(e => $"SpeedDialCode: {e}"));
        if (!_unformattedTelephoneNumber.Validate(out var utnErrors))
            errors.AddRange(utnErrors.Select(e => $"UnformattedTelephoneNumber: {e}"));

        return errors.Count == 0;
    }

    /// <summary>
    /// Gets a formatted representation of the telephone number.
    /// </summary>
    /// <returns>The formatted telephone number.</returns>
    public string GetFormattedNumber()
    {
        if (!_telephoneNumber.IsEmpty)
            return _telephoneNumber.Value;

        if (!_unformattedTelephoneNumber.IsEmpty)
            return _unformattedTelephoneNumber.Value;

        // Build from components
        var parts = new List<string>();
        
        if (!_countryCode.IsEmpty)
            parts.Add($"+{_countryCode.Value}");
        
        if (!_areaCityCode.IsEmpty && !_localNumber.IsEmpty)
            parts.Add($"({_areaCityCode.Value}) {_localNumber.Value}");
        else if (!_areaCityCode.IsEmpty)
            parts.Add(_areaCityCode.Value);
        else if (!_localNumber.IsEmpty)
            parts.Add(_localNumber.Value);
        
        var result = string.Join(" ", parts);
        
        if (!_extension.IsEmpty)
            result += $" x{_extension.Value}";
        
        return result;
    }

    /// <inheritdoc/>
    public override string ToString() => !_emailAddress.IsEmpty ? _emailAddress.Value : GetFormattedNumber();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is XTN other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to XTN.
    /// </summary>
    public static implicit operator XTN(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var xtn = new XTN();
        xtn.Parse(value.AsSpan(), HL7Delimiters.Default);
        return xtn;
    }

    /// <summary>
    /// Implicit conversion from XTN to string.
    /// </summary>
    public static implicit operator string(XTN xtn) => xtn.ToString();
}
