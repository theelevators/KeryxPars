namespace KeryxPars.HL7.Attributes;

/// <summary>
/// Explicitly marks a property to be included in segment handling.
/// By default, all properties of type ISegment or List&lt;ISegment&gt; are auto-detected.
/// Use this attribute to explicitly control priority or add documentation.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class SegmentHandlerAttribute : Attribute
{
    /// <summary>
    /// Priority in the switch statement. Lower numbers = earlier in switch.
    /// Default is 0. Use for frequently accessed segments.
    /// </summary>
    public int Priority { get; set; } = 0;
}
