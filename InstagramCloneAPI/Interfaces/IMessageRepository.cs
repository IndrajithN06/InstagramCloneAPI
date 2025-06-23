using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMessageRepository
{
    Task<Message> SendMessageAsync(int conversationId, int senderId, int receiverId, string text);
    Task<IEnumerable<Message>> GetMessagesAsync(int conversationId);

    Task MarkMessagesAsReadAsync(int conversationId, int userId);
    Task<bool> DeleteMessageAsync(int messageId);
    Task<Message?> UpdateMessageAsync(int messageId, string newText);
    Task<int> GetUnreadMessageCountAsync(int userId);
    Task<IEnumerable<Message>> SearchMessagesAsync(int userId, string query);

}
