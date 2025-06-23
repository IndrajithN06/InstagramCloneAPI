using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    [JsonIgnore]
    public ICollection<Post> Posts { get; set; } = new List<Post>();

    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public ICollection<Like> Likes { get; set; } = new List<Like>();

    [JsonIgnore]
    public ICollection<Follow> Followers { get; set; } = new List<Follow>();

    [JsonIgnore]
    public ICollection<Follow> Following { get; set; } = new List<Follow>();

    [JsonIgnore]
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [JsonIgnore]
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();

    [JsonIgnore]
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

    [JsonIgnore]
    public ICollection<Story> Stories { get; set; } = new List<Story>();
}
