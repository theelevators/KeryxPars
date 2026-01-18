using KeryxPars.HL7.DataTypes.Contracts;
using KeryxPars.HL7.DataTypes.Primitive;
using KeryxPars.HL7.Definitions;
using KeryxPars.HL7.Parsing;
using System.Text;

namespace KeryxPars.HL7.DataTypes.Composite;

/// <summary>
/// EI - Entity Identifier
/// HL7 2.5 Composite Data Type representing an entity identifier.
/// Format: EntityIdentifier^NamespaceID^UniversalID^UniversalIDType
/// </summary>
public readonly struct EI : ICompositeDataType
{
    private readonly ST _entityIdentifier;
    private readonly IS _namespaceId;
    private readonly ST _universalId;
    private readonly ID _universalIdType;

    /// <summary>
    /// Initializes a new instance of the EI data type with all components.
    /// </summary>
    public EI(
        ST entityIdentifier = default,
        IS namespaceId = default,
        ST universalId = default,
        ID universalIdType = default)
    {
        _entityIdentifier = entityIdentifier;
        _namespaceId = namespaceId;
        _universalId = universalId;
        _universalIdType = universalIdType;
    }

    /// <inheritdoc/>
    public string TypeCode => "EI";

    /// <inheritdoc/>
    public int ComponentCount => 4;

    /// <inheritdoc/>
    public bool IsEmpty => _entityIdentifier.IsEmpty && _namespaceId.IsEmpty && 
                          _universalId.IsEmpty && _universalIdType.IsEmpty;

    /// <summary>
    /// EI.1 - Entity Identifier
    /// </summary>
    public ST EntityIdentifier => _entityIdentifier;

    /// <summary>
    /// EI.2 - Namespace ID
    /// </summary>
    public IS NamespaceId => _namespaceId;

    /// <summary>
    /// EI.3 - Universal ID
    /// </summary>
    public ST UniversalId => _universalId;

    /// <summary>
    /// EI.4 - Universal ID Type
    /// </summary>
    public ID UniversalIdType => _universalIdType;

    /// <inheritdoc/>
    public IHL7DataType? GetComponent(int index)
    {
        return index switch
        {
            0 => _entityIdentifier,
            1 => _namespaceId,
            2 => _universalId,
            3 => _universalIdType,
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
                if (value is ST st0) System.Runtime.CompilerServices.Unsafe.AsRef(in _entityIdentifier) = st0;
                break;
            case 1:
                if (value is IS is1) System.Runtime.CompilerServices.Unsafe.AsRef(in _namespaceId) = is1;
                break;
            case 2:
                if (value is ST st2) System.Runtime.CompilerServices.Unsafe.AsRef(in _universalId) = st2;
                break;
            case 3:
                if (value is ID id3) System.Runtime.CompilerServices.Unsafe.AsRef(in _universalIdType) = id3;
                break;
        }
    }

    /// <inheritdoc/>
    public string ToHL7String(in HL7Delimiters delimiters)
    {
        var sb = new StringBuilder();
        if (!_entityIdentifier.IsEmpty)
            sb.Append(_entityIdentifier.ToHL7String(delimiters));
        
        sb.Append(delimiters.ComponentSeparator);
        
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
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _entityIdentifier) = new ST(component.ToString());
                    break;
                case 1:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _namespaceId) = new IS(component.ToString());
                    break;
                case 2:
                    System.Runtime.CompilerServices.Unsafe.AsRef(in _universalId) = new ST(component.ToString());
                    break;
                case 3:
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

        if (!_entityIdentifier.Validate(out var eiErrors))
            errors.AddRange(eiErrors.Select(e => $"EntityIdentifier: {e}"));
        if (!_namespaceId.Validate(out var nsErrors))
            errors.AddRange(nsErrors.Select(e => $"NamespaceId: {e}"));
        if (!_universalId.Validate(out var uidErrors))
            errors.AddRange(uidErrors.Select(e => $"UniversalId: {e}"));
        if (!_universalIdType.Validate(out var typeErrors))
            errors.AddRange(typeErrors.Select(e => $"UniversalIdType: {e}"));

        return errors.Count == 0;
    }

    /// <inheritdoc/>
    public override string ToString() => _entityIdentifier.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is EI other && ToHL7String(HL7Delimiters.Default) == other.ToHL7String(HL7Delimiters.Default);

    /// <inheritdoc/>
    public override int GetHashCode() => ToHL7String(HL7Delimiters.Default).GetHashCode();

    /// <summary>
    /// Implicit conversion from string to EI.
    /// </summary>
    public static implicit operator EI(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return default;
        
        var ei = new EI();
        ei.Parse(value.AsSpan(), HL7Delimiters.Default);
        return ei;
    }

    /// <summary>
    /// Implicit conversion from EI to string.
    /// </summary>
    public static implicit operator string(EI ei) => ei.ToString();
}
