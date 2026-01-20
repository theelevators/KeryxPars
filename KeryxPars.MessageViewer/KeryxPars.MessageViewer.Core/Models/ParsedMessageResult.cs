using KeryxPars.HL7.Definitions;

namespace KeryxPars.MessageViewer.Core.Models;

public class ParsedMessageResult
{
    public bool IsSuccess { get; set; }
    public HL7DefaultMessage? Message { get; set; }
    public string[]? Errors { get; set; }
    public ParseMetadata Metadata { get; set; } = new();
    public TimeSpan ParseDuration { get; set; }
}



public class ParseMetadata
{
    public int SegmentCount { get; set; }
    public int FieldCount { get; set; }
    public Dictionary<string, int> SegmentFrequency { get; set; } = new();
    public string HL7Version { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    
    /// <summary>
    /// The detected specialized message class name (e.g., "PharmacyMessage", "LabMessage")
    /// </summary>
    public string DetectedMessageClass { get; set; } = string.Empty;
    
    /// <summary>
    /// Human-readable description of the detected message class
    /// </summary>
    public string MessageClassDescription { get; set; } = string.Empty;
}

