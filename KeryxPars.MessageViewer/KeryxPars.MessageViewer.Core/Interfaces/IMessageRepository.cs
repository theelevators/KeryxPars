using KeryxPars.MessageViewer.Core.Models;
using KeryxPars.HL7.Definitions;

namespace KeryxPars.MessageViewer.Core.Interfaces;

public interface IMessageRepository
{
    Task<IEnumerable<StoredMessage>> GetRecentMessagesAsync(int count);
    Task<StoredMessage> SaveMessageAsync(string rawMessage, HL7DefaultMessage parsed);
    Task<bool> DeleteMessageAsync(Guid id);
    Task<IEnumerable<StoredMessage>> SearchAsync(string query);
    Task<StoredMessage?> GetMessageByIdAsync(Guid id);
}
