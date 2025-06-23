public class Conversation
{
    public int Id { get; set; }

    // These represent the two participants in the conversation
    public int User1Id { get; set; }
    public int User2Id { get; set; }

    // Navigation properties
    public User User1 { get; set; }
    public User User2 { get; set; }

    // Optional: List of messages in this conversation
    public ICollection<Message> Messages { get; set; }
}
