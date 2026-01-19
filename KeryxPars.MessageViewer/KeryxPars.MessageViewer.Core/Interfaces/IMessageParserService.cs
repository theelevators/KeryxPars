using KeryxPars.MessageViewer.Core.Models;
using KeryxPars.HL7.Serialization.Configuration;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.MessageViewer.Core.Interfaces;

public interface IMessageParserService
{
    Task<ParsedMessageResult> ParseAsync(string rawMessage, SerializerOptions? options = null);
    Task<IEnumerable<MessageValidationError>> ValidateAsync(HL7DefaultMessage message);
}

public class MessageValidationError
{
    public ValidationSeverity Severity { get; set; }
    public string SegmentId { get; set; } = string.Empty;
    public int FieldIndex { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Suggestion { get; set; }
}

public enum ValidationSeverity
{
    Info,
    Warning,
    Error,
    Critical
}
