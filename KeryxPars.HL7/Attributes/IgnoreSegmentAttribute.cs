namespace KeryxPars.HL7.Attributes;

/// <summary>
/// Marks a property to be ignored by the segment handler generator.
/// Use this for properties that match ISegment or List&lt;ISegment&gt; pattern
/// but should not be included in automatic AddSegment() handling.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class IgnoreSegmentAttribute : Attribute
{
}
