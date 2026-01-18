using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// CE - Coded Element
/// HL7 2.5 Composite Data Type representing a coded value.
/// Format: Identifier^Text^NameOfCodingSystem^AlternateIdentifier^AlternateText^NameOfAlternateCodingSystem
/// </summary>
public readonly struct CE : ICompositeDataType
{
    private readonly ST _identifier;
    private readonly ST _text;
    private readonly ID _nameOfCodingSystem;
    private readonly ST _alternateIdentifier;
    private readonly ST _alternateText;
    private readonly ID _nameOfAlternateCodingSystem;

    /// <summary>
    /// Initializes a new instance of the CE data type with all components.
    /// </summary>
    public CE(
        ST identifier = default,
        ST text = default,
        ID nameOfCodingSystem = default,
        ST alternateIdentifier = default,
        ST alternateText = default,
        ID nameOfAlternateCodingSystem = default)
    {
        _identifier = identifier;
        _text = text;
        _nameOfCodingSystem = nameOfCodingSystem;
        _alternateIdentifier = alternateIdentifier;
        _alternateText = alternateText;
        _nameOfAlternateCodingSystem = nameOfAlternateCodingSystem;
    }

    /// <inheritdoc/>
    public string TypeCode => "CE";

    /// <inheritdoc/>
    public int ComponentCount => 6;

    /// <inheritdoc/>
    public bool IsEmpty => _identifier.IsEmpty && _text.IsEmpty &&
                          _nameOfCodingSystem.IsEmpty && _alternateIdentifier.IsEmpty &&
                          _alternateText.IsEmpty && _nameOfAlternateCodingSystem.IsEmpty;

    /// <summary>
    /// CE.1 - Identifier
    /// </summary>
    public ST Identifier => _identifier;

    /// <summary>
    /// CE.2 - Text
    /// </summary>
    public ST Text => _text;

    /// <summary>
    /// CE.3 - Name of Coding System
    /// </summary>
    public ID NameOfCodingSystem => _nameOfCodingSystem;

    /// <summary>
    /// CE.4 - Alternate Identifier
    /// </summary>
    public ST AlternateIdentifier => _alternateIdentifier;

    /// <summary>
    /// CE.5 - Alternate Text
    /// </summary>
    public ST AlternateText => _alternateText;

    /// <summary>
    /// CE.6 - Name of Alternate Coding System
    /// </summary>
    public ID NameOfAlternateCodingSystem => _nameOfAlternateCodingSystem;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _identifier,
            1 => _text,
            2 => _nameOfCodingSystem,
            3 => _alternateIdentifier,
            4 => _alternateText,
            5 => _nameOfAlternateCodingSystem,
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
                if (value is ST st0) System.Runtime.CompilerServices.Unsafe.AsRef(in _identifier) = st0;
                break;
            case 1:
                if (value is ST st1) System.Runtime.CompilerServices.Unsafe.AsRef(in _text) = st1;
                break;
            case 2:
                if (value is ID id2) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameOfCodingSystem) = id2;
                break;
            case 3:
                if (value is ST st3) System.Runtime.CompilerServices.Unsafe.AsRef(in _alternateIdentifier) = st3;
                break;
            case 4:
                if (value is ST st4) System.Runtime.CompilerServices.Unsafe.AsRef(in _alternateText) = st4;
                break;
            case 5:
                if (value is ID id5) System.Runtime.CompilerServices.Unsafe.AsRef(in _nameOfAlternateCodingSystem) = id5;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        AppendComponent(sb, _identifier, delimiters, false);
        AppendComponent(sb, _text, delimiters);
        AppendComponent(sb, _nameOfCodingSystem, delimiters);
        AppendComponent(sb, _alternateIdentifier, delimiters);
        AppendComponent(sb, _alternateText, delimiters);
        AppendComponent(sb, _nameOfAlternateCodingSystem, delimiters);

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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _identifier) = new ST(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _text) = new ST(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameOfCodingSystem) = new ID(component.ToString());
                    break;
                case 3:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _alternateIdentifier) = new ST(component.ToString());
                    break;
                case 4:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _alternateText) = new ST(component.ToString());
                    break;
                case 5:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _nameOfAlternateCodingSystem) = new ID(component.ToString());
                    break;
            }

            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_identifier.Validate(out var idErrors))
            errors.AddRange(idErrors.Select(e => $"Identifier: {e}"));
        if (!_text.Validate(out var textErrors))
            errors.AddRange(textErrors.Select(e => $"Text: {e}"));
        if (!_nameOfCodingSystem.Validate(out var ncsErrors))
            errors.AddRange(ncsErrors.Select(e => $"NameOfCodingSystem: {e}"));
        if (!_alternateIdentifier.Validate(out var altIdErrors))
            errors.AddRange(altIdErrors.Select(e => $"AlternateIdentifier: {e}"));
        if (!_alternateText.Validate(out var altTextErrors))
            errors.AddRange(altTextErrors.Select(e => $"AlternateText: {e}"));
        if (!_nameOfAlternateCodingSystem.Validate(out var nacsErrors))
            errors.AddRange(nacsErrors.Select(e => $"NameOfAlternateCodingSystem: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => !_text.IsEmpty ? _text.Value : _identifier.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is CE other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to CE.
    /// </summary>
    public static implicit operator CE(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;

        var ce = new CE();
        var delimeters = HL7Delimiters.Default;
        ce.Parse(value.AsSpan(), delimeters);
        return ce;
    }

    /// <summary>
    /// Implicit conversion from CE to string.
    /// </summary>
    public static implicit operator string(CE ce) => ce.ToString();
}
