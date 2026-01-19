using KeryxPars.MessageViewer.Core.Interfaces;
using KeryxPars.MessageViewer.Core.Models;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.MessageViewer.Client.Services;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly List<StoredMessage> _messages = new();
    private readonly IMessageParserService _parserService;

    public InMemoryMessageRepository(IMessageParserService parserService)
    {
        _parserService = parserService;
    }

    public Task<IEnumerable<StoredMessage>> GetRecentMessagesAsync(int count)
    {
        var recent = _messages
            .OrderByDescending(m => m.ImportedDate)
            .Take(count)
            .ToList();
        
        return Task.FromResult<IEnumerable<StoredMessage>>(recent);
    }

    public Task<StoredMessage> SaveMessageAsync(string rawMessage, HL7DefaultMessage parsed)
    {
        var stored = new StoredMessage
        {
            Id = Guid.NewGuid(),
            RawMessage = rawMessage,
            ImportedDate = DateTime.UtcNow,
            MessageType = parsed.MessageType.ToString(),
            MessageControlId = parsed.MessageControlID,
            PatientName = GetPatientName(parsed),
            Metadata = new Dictionary<string, object>
            {
                { "HL7Version", parsed.Msh.VersionID.ToString() },
                { "EventType", parsed.EventType.ToString() }
            }
        };

        _messages.Add(stored);
        return Task.FromResult(stored);
    }

    public Task<bool> DeleteMessageAsync(Guid id)
    {
        var message = _messages.FirstOrDefault(m => m.Id == id);
        if (message != null)
        {
            _messages.Remove(message);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<StoredMessage>> SearchAsync(string query)
    {
        var lowerQuery = query.ToLowerInvariant();
        var results = _messages
            .Where(m => 
                m.MessageType.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase) ||
                m.PatientName.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase) ||
                m.MessageControlId.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Task.FromResult<IEnumerable<StoredMessage>>(results);
    }

    public Task<StoredMessage?> GetMessageByIdAsync(Guid id)
    {
        var message = _messages.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(message);
    }

    private string GetPatientName(HL7DefaultMessage message)
    {
        if (message.Pid?.PatientName != null && message.Pid.PatientName.Count() > 0)
        {
            var name = message.Pid.PatientName[0];
            var familyName = name.FamilyName.Surname.ToString();
            var givenName = name.GivenName.ToString();
            return $"{familyName}, {givenName}";
        }
        return "Unknown";
    }
}
