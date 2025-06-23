public class CreatePostDto
{
    public int UserId { get; set; }
    public string ContentUrl { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public List<int> TagIds { get; set; } = new(); // for associating tags with the post
}
