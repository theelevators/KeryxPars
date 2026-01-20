using System;

namespace KeryxPars.HL7.Mapping;

/// <summary>
/// Marks a complex type for component-level mapping.
/// Properties of this type can use HL7ComponentAttribute.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
public sealed class HL7ComplexAttribute : Attribute
{
    /// <summary>
    /// Optional base field path (e.g., "PID.5" for a PersonName complex type).
    /// If not specified, properties must use full paths.
    /// </summary>
    public string? BaseFieldPath { get; set; }
}

/// <summary>
/// Maps a property to a component index within a complex field.
/// Used in classes marked with [HL7Complex].
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class HL7ComponentAttribute : Attribute
{
    /// <summary>
    /// Component index (1-based) or full path.
    /// </summary>
    public object ComponentIdentifier { get; }

    /// <summary>
    /// Maps to a component by index.
    /// </summary>
    /// <param name="index">1-based component index</param>
    public HL7ComponentAttribute(int index)
    {
        if (index < 1)
        {
            throw new ArgumentException("Component index must be >= 1", nameof(index));
        }

        ComponentIdentifier = index;
    }

    /// <summary>
    /// Maps to a component by full path.
    /// </summary>
    /// <param name="path">Full component path (e.g., "PID.5.1")</param>
    public HL7ComponentAttribute(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Component path cannot be null or empty", nameof(path));
        }

        ComponentIdentifier = path;
    }
}
