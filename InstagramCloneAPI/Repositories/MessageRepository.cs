using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Message> SendMessageAsync(int conversationId, int senderId, int receiverId, string text)
    {
        var message = new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            ReceiverId = receiverId,
            Text = text,
            CreatedAt = DateTime.UtcNow,
            IsRead = false
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(int conversationId)
    {
        return await _context.Messages
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkMessagesAsReadAsync(int conversationId, int userId)
    {
        var unreadMessages = await _context.Messages
            .Where(m => m.ConversationId == conversationId && m.ReceiverId == userId && !m.IsRead)
            .ToListAsync();

        foreach (var message in unreadMessages)
        {
            message.IsRead = true;
        }

        await _context.SaveChangesAsync();
    }
    public async Task<bool> DeleteMessageAsync(int messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null)
            return false;

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Message?> UpdateMessageAsync(int messageId, string newText)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null)
            return null;

        message.Text = newText;
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<int> GetUnreadMessageCountAsync(int userId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .CountAsync();
    }
    public async Task<IEnumerable<Message>> SearchMessagesAsync(int userId, string query)
    {
        return await _context.Messages
            .Where(m =>
                (m.SenderId == userId || m.ReceiverId == userId) &&
                m.Text.Contains(query))
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

}
