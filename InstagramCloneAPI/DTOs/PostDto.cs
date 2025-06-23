public class PostDto
{
    public int Id { get; set; }
    public string ContentUrl { get; set; }
    public string Caption { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto User { get; set; }
    public List<CommentDto> Comments { get; set; }
    public List<LikeDto> Likes { get; set; }
    public List<TagDto> Tags { get; set; }
}
