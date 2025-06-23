﻿public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
