public class Story
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ContentUrl { get; set; } // URL to the photo or video
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }

    // Navigation property
    public User User { get; set; }
}
