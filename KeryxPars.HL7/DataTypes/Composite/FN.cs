using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// FN - Family Name
/// HL7 2.5 Composite Data Type representing a family name with multiple components.
/// Format: Surname^OwnSurnamePrefix^OwnSurname^SurnamePrefixFromPartner^SurnameFromPartner
/// </summary>
public readonly struct FN : ICompositeDataType
{
    private readonly ST _surname;
    private readonly ST _ownSurnamePrefix;
    private readonly ST _ownSurname;
    private readonly ST _surnamePrefixFromPartner;
    private readonly ST _surnameFromPartner;

    /// <summary>
    /// Initializes a new instance of the FN data type with all components.
    /// </summary>
    public FN(
        ST surname = default,
        ST ownSurnamePrefix = default,
        ST ownSurname = default,
        ST surnamePrefixFromPartner = default,
        ST surnameFromPartner = default)
    {
        _surname = surname;
        _ownSurnamePrefix = ownSurnamePrefix;
        _ownSurname = ownSurname;
        _surnamePrefixFromPartner = surnamePrefixFromPartner;
        _surnameFromPartner = surnameFromPartner;
    }

    /// <inheritdoc/>
    public string TypeCode => "FN";

    /// <inheritdoc/>
    public int ComponentCount => 5;

    /// <inheritdoc/>
    public bool IsEmpty => _surname.IsEmpty && _ownSurnamePrefix.IsEmpty && 
                          _ownSurname.IsEmpty && _surnamePrefixFromPartner.IsEmpty &&
                          _surnameFromPartner.IsEmpty;

    /// <summary>
    /// FN.1 - Surname
    /// </summary>
    public ST Surname => _surname;

    /// <summary>
    /// FN.2 - Own Surname Prefix
    /// </summary>
    public ST OwnSurnamePrefix => _ownSurnamePrefix;

    /// <summary>
    /// FN.3 - Own Surname
    /// </summary>
    public ST OwnSurname => _ownSurname;

    /// <summary>
    /// FN.4 - Surname Prefix From Partner/Spouse
    /// </summary>
    public ST SurnamePrefixFromPartner => _surnamePrefixFromPartner;

    /// <summary>
    /// FN.5 - Surname From Partner/Spouse
    /// </summary>
    public ST SurnameFromPartner => _surnameFromPartner;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _surname,
            1 => _ownSurnamePrefix,
            2 => _ownSurname,
            3 => _surnamePrefixFromPartner,
            4 => _surnameFromPartner,
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
                System.Runtime.CompilerServices.Unsafe.AsRef(in _surname) = st;
                break;
            case 1:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _ownSurnamePrefix) = st;
                break;
            case 2:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _ownSurname) = st;
                break;
            case 3:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _surnamePrefixFromPartner) = st;
                break;
            case 4:
                System.Runtime.CompilerServices.Unsafe.AsRef(in _surnameFromPartner) = st;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendSubcomponent(sb, _surname, delimiters, false);
        AppendSubcomponent(sb, _ownSurnamePrefix, delimiters);
        AppendSubcomponent(sb, _ownSurname, delimiters);
        AppendSubcomponent(sb, _surnamePrefixFromPartner, delimiters);
        AppendSubcomponent(sb, _surnameFromPartner, delimiters);
        
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _surname) = new ST(subcomponent.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _ownSurnamePrefix) = new ST(subcomponent.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _ownSurname) = new ST(subcomponent.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _surnamePrefixFromPartner) = new ST(subcomponent.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _surnameFromPartner) = new ST(subcomponent.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_surname.Validate(out var surnameErrors))
            errors.AddRange(surnameErrors.Select(e => $"Surname: {e}"));
        if (!_ownSurnamePrefix.Validate(out var prefixErrors))
            errors.AddRange(prefixErrors.Select(e => $"OwnSurnamePrefix: {e}"));
        if (!_ownSurname.Validate(out var ownErrors))
            errors.AddRange(ownErrors.Select(e => $"OwnSurname: {e}"));
        if (!_surnamePrefixFromPartner.Validate(out var partnerPrefixErrors))
            errors.AddRange(partnerPrefixErrors.Select(e => $"SurnamePrefixFromPartner: {e}"));
        if (!_surnameFromPartner.Validate(out var partnerErrors))
            errors.AddRange(partnerErrors.Select(e => $"SurnameFromPartner: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => _surname.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is FN other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to FN.
    /// </summary>
    public static implicit operator FN(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var fn = new FN();
        fn.Parse(value.AsSpan(), HL7Delimiters.Default);
        return fn;
    }

    /// <summary>
    /// Implicit conversion from FN to string.
    /// </summary>
    public static implicit operator string(FN fn) => fn.ToString();
}
