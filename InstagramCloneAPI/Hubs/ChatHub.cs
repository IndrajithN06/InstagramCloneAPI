using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ChatHub(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task SendMessage(CreateMessageDto dto)
    {
        var conversationId = await GetOrCreateConversationId(dto.SenderId, dto.ReceiverId);

        var message = new Message
        {
            SenderId = dto.SenderId,
            ReceiverId = dto.ReceiverId,
            Text = dto.Text,
            CreatedAt = DateTime.UtcNow,
            IsRead = false,
            ConversationId = conversationId
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        // AutoMapper handles the DTO conversion
        var messageDto = _mapper.Map<MessageDto>(message);

        await Clients.Users(
            dto.SenderId.ToString(),
            dto.ReceiverId.ToString()
        ).SendAsync("ReceiveMessage", messageDto);
    }

    private async Task<int> GetOrCreateConversationId(int senderId, int receiverId)
    {
        var existing = await _context.Conversations.FirstOrDefaultAsync(c =>
            (c.User1Id == senderId && c.User2Id == receiverId) ||
            (c.User1Id == receiverId && c.User2Id == senderId));

        if (existing != null)
            return existing.Id;

        var newConversation = new Conversation
        {
            User1Id = senderId,
            User2Id = receiverId
        };

        _context.Conversations.Add(newConversation);
        await _context.SaveChangesAsync();
        return newConversation.Id;
    }
}
