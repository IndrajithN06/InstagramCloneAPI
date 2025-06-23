

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ContentUrl { get; set; } // URL to the photo or video
    public string Caption { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
