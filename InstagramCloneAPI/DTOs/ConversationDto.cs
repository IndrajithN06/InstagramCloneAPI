public class ConversationDto
{
    public int Id { get; set; }
    public int User1Id { get; set; }  // ✅ Add this
    public int User2Id { get; set; }
    public List<MessageDto> Messages { get; set; } = new();
}
