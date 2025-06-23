public class UpdatePostDto
{
    public string ContentUrl { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public List<int> TagIds { get; set; } = new(); // update tags if needed
}
