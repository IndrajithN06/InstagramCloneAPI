using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ConversationRepository : IConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Conversation> StartConversationAsync(int user1Id, int user2Id)
    {
        var existing = await _context.Conversations
            .FirstOrDefaultAsync(c =>
                (c.User1Id == user1Id && c.User2Id == user2Id) ||
                (c.User1Id == user2Id && c.User2Id == user1Id));

        if (existing != null)
            return existing;

        var conversation = new Conversation
        {
            User1Id = user1Id,
            User2Id = user2Id
        };

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync();
        return conversation;
    }

    public async Task<Conversation?> GetConversationByIdAsync(int conversationId)
    {
        return await _context.Conversations
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }

    public async Task<IEnumerable<Conversation>> GetUserConversationsAsync(int userId)
    {
        return await _context.Conversations
            .Where(c => c.User1Id == userId || c.User2Id == userId)
            .Include(c => c.Messages)
            .OrderByDescending(c => c.Messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault().CreatedAt)
            .ToListAsync();
    }
}
