using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// HD - Hierarchic Designator
/// HL7 2.5 Composite Data Type representing a hierarchical identifier.
/// Format: NamespaceID^UniversalID^UniversalIDType
/// </summary>
public readonly struct HD : ICompositeDataType
{
    private readonly IS _namespaceId;
    private readonly ST _universalId;
    private readonly ID _universalIdType;

    /// <summary>
    /// Initializes a new instance of the HD data type with all components.
    /// </summary>
    public HD(IS namespaceId = default, ST universalId = default, ID universalIdType = default)
    {
        _namespaceId = namespaceId;
        _universalId = universalId;
        _universalIdType = universalIdType;
    }

    /// <inheritdoc/>
    public string TypeCode => "HD";

    /// <inheritdoc/>
    public int ComponentCount => 3;

    /// <inheritdoc/>
    public bool IsEmpty => _namespaceId.IsEmpty && _universalId.IsEmpty && _universalIdType.IsEmpty;

    /// <summary>
    /// HD.1 - Namespace ID
    /// </summary>
    public IS NamespaceId => _namespaceId;

    /// <summary>
    /// HD.2 - Universal ID
    /// </summary>
    public ST UniversalId => _universalId;

    /// <summary>
    /// HD.3 - Universal ID Type
    /// </summary>
    public ID UniversalIdType => _universalIdType;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _namespaceId,
            1 => _universalId,
            2 => _universalIdType,
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
                if (value is IS is0) System.Runtime.CompilerServices.Unsafe.AsRef(in _namespaceId) = is0;
                break;
            case 1:
                if (value is ST st1) System.Runtime.CompilerServices.Unsafe.AsRef(in _universalId) = st1;
                break;
            case 2:
                if (value is ID id2) System.Runtime.CompilerServices.Unsafe.AsRef(in _universalIdType) = id2;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        if (!_namespaceId.IsEmpty)
            sb.Append(_namespaceId.ToHL7String(delimiters));
        
        sb.Append(delimiters.ComponentSeparator);
        
        if (!_universalId.IsEmpty)
            sb.Append(_universalId.ToHL7String(delimiters));
        
        sb.Append(delimiters.ComponentSeparator);
        
        if (!_universalIdType.IsEmpty)
            sb.Append(_universalIdType.ToHL7String(delimiters));
        
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _namespaceId) = new IS(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _universalId) = new ST(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _universalIdType) = new ID(component.ToString());
                    break;
            }
            
            index++;
        }
    }

    /// <inheritdoc/>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (!_namespaceId.Validate(out var nsErrors))
            errors.AddRange(nsErrors.Select(e => $"NamespaceId: {e}"));
        if (!_universalId.Validate(out var uidErrors))
            errors.AddRange(uidErrors.Select(e => $"UniversalId: {e}"));
        if (!_universalIdType.Validate(out var typeErrors))
            errors.AddRange(typeErrors.Select(e => $"UniversalIdType: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => !_namespaceId.IsEmpty ? _namespaceId.Value : _universalId.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is HD other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to HD.
    /// </summary>
    public static implicit operator HD(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var hd = new HD();
        hd.Parse(value.AsSpan(), HL7Delimiters.Default);
        return hd;
    }

    /// <summary>
    /// Implicit conversion from HD to string.
    /// </summary>
    public static implicit operator string(HD hd) => hd.ToString();
}
