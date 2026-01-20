namespace KeryxPars.HL7.Attributes;

/// <summary>
/// Marks a class for automatic generation of AddSegment() method.
/// The generator will create an optimized switch statement based on
/// properties that implement ISegment or List&lt;ISegment&gt;.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class GenerateSegmentHandlersAttribute : Attribute
{
}
