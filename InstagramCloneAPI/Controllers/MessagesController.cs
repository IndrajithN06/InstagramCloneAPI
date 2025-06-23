using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using InstagramCloneAPI.DTOs;


[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepo;
    private readonly IConversationRepository _conversationRepo;

    public MessagesController(IMessageRepository messageRepo, IConversationRepository conversationRepo)
    {
        _messageRepo = messageRepo;
        _conversationRepo = conversationRepo;
    }

    // POST: api/messages
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var conversation = await _conversationRepo.StartConversationAsync(dto.SenderId, dto.ReceiverId);

        var message = new Message
        {
            SenderId = dto.SenderId,
            ReceiverId = dto.ReceiverId,
            Text = dto.Text,
            CreatedAt = DateTime.UtcNow,
            IsRead = false,
            ConversationId = conversation.Id
        };

        var createdMessage = await _messageRepo.SendMessageAsync(
            conversation.Id, dto.SenderId, dto.ReceiverId, dto.Text
        );

        var result = new MessageDto
        {
            Id = createdMessage.Id,
            SenderId = createdMessage.SenderId,
            ReceiverId = createdMessage.ReceiverId,
            Text = createdMessage.Text,
            CreatedAt = createdMessage.CreatedAt,
            IsRead = createdMessage.IsRead
        };

        return Ok(result);
    }

    [HttpPost("mark-read")]
    public async Task<IActionResult> MarkMessagesAsRead([FromBody] MarkReadDto dto)
    {
        await _messageRepo.MarkMessagesAsReadAsync(dto.ConversationId, dto.UserId);
        return Ok("Messages marked as read.");
    }


    // GET: api/messages/{conversationId}
    [HttpGet("{conversationId}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessages(int conversationId)
    {
        var messages = await _messageRepo.GetMessagesAsync(conversationId);

        var messageDtos = messages.Select(m => new MessageDto
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Text = m.Text,
            CreatedAt = m.CreatedAt,
            IsRead = m.IsRead
        });

        return Ok(messageDtos);
    }

    // GET: api/messages/conversations/{userId}
    [HttpGet("conversations/{userId}")]
    public async Task<ActionResult<IEnumerable<ConversationDto>>> GetUserConversations(int userId)
    {
        var conversations = await _conversationRepo.GetUserConversationsAsync(userId);

        var result = conversations.Select(c => new ConversationDto
        {
            Id = c.Id,
            User1Id = c.User1Id,
            User2Id = c.User2Id,
            Messages = c.Messages?.Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Text = m.Text,
                CreatedAt = m.CreatedAt,
                IsRead = m.IsRead
            }).ToList()

        });

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var success = await _messageRepo.DeleteMessageAsync(id);
        if (!success)
            return NotFound("Message not found.");

        return Ok("Message deleted.");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMessage(int id, [FromBody] UpdateMessageDto dto)
    {
        var updated = await _messageRepo.UpdateMessageAsync(id, dto.Text);
        if (updated == null)
            return NotFound("Message not found.");

        var result = new MessageDto
        {
            Id = updated.Id,
            SenderId = updated.SenderId,
            ReceiverId = updated.ReceiverId,
            Text = updated.Text,
            CreatedAt = updated.CreatedAt,
            IsRead = updated.IsRead
        };

        return Ok(result);
    }

    [HttpGet("unread-count/{userId}")]
    public async Task<IActionResult> GetUnreadCount(int userId)
    {
        var count = await _messageRepo.GetUnreadMessageCountAsync(userId);
        return Ok(count);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> SearchMessages([FromQuery] string query, [FromQuery] int userId)
    {
        var messages = await _messageRepo.SearchMessagesAsync(userId, query);

        var result = messages.Select(m => new MessageDto
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Text = m.Text,
            CreatedAt = m.CreatedAt,
            IsRead = m.IsRead
        });

        return Ok(result);
    }


}
