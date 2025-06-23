using System.Collections.Generic;
using System.Threading.Tasks;

public interface IConversationRepository
{
    Task<Conversation> StartConversationAsync(int user1Id, int user2Id);
    Task<Conversation?> GetConversationByIdAsync(int conversationId);
    Task<IEnumerable<Conversation>> GetUserConversationsAsync(int userId);
}
