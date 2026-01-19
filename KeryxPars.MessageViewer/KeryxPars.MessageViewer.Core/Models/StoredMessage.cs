namespace KeryxPars.MessageViewer.Core.Models;

public class StoredMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RawMessage { get; set; } = string.Empty;
    public DateTime ImportedDate { get; set; } = DateTime.UtcNow;
    public string MessageType { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string MessageControlId { get; set; } = string.Empty;
    public Dictionary<string, object> Metadata { get; set; } = new();
}
