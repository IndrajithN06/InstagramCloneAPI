public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; } // User to whom the notification is sent
    public string Type { get; set; } // e.g., 'like', 'comment', 'follow'
    public int EntityId { get; set; } // ID of the related entity (e.g., PostId, CommentId)
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }

    // Navigation property
    public User User { get; set; }
}
