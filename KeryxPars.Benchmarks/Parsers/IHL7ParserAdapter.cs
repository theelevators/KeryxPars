namespace KeryxPars.Benchmarks.Parsers;

/// <summary>
/// Base interface for parser adapters to ensure consistent benchmarking.
/// </summary>
public interface IHL7ParserAdapter
{
    string Name { get; }
    string Version { get; }
    
    /// <summary>
    /// Parse an HL7 message and return the result.
    /// </summary>
    object Parse(string message);
    
    /// <summary>
    /// Serialize an HL7 message object back to string.
    /// </summary>
    string Serialize(object message);
}
