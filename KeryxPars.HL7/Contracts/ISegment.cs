namespace KeryxPars.HL7.Contracts
{
    /// <summary>
    /// Interface to be shared by all segment classes.
    /// </summary>
    public interface ISegment
    {
        /// <summary>
        /// The segment identifier (e.g., "MSH", "PID", "PV1").
        /// </summary>
        string SegmentId { get; }

        /// <summary>
        /// Sets a field value at the specified index.
        /// </summary>
        void SetValue(string value, int fieldIndex);

        /// <summary>
        /// Gets all field values for this segment.
        /// </summary>
        string[] GetValues();

        /// <summary>
        /// Gets a specific field value by index.
        /// </summary>
        string? GetField(int index);
    }
}
