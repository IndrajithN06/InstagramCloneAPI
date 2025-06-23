public class Follow
{
    public int FollowerId { get; set; }
    public int FollowingId { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public User Follower { get; set; }
    public User Following { get; set; }
}
