namespace KeryxPars.HL7.Definitions
{
    /// <summary>
    /// Defines which message types (ADT/Medication) the segment is used in.  Used for logic routing of data parsing.
    /// </summary>
    public enum SegmentType
    {
        ADT,
        MedOrder,
        Universal
    }
}
