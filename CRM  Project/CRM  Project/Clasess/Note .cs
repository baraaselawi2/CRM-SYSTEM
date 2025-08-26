public class Note
{
    public int Id { get; set; }
    public RelatedToType RelatedTo { get; set; }
    public int RelatedId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }

    public Note(int id, RelatedToType relatedTo, int relatedId, string text, DateTime createdAt)
    {
        Id = id;
        RelatedTo = relatedTo;
        RelatedId = relatedId;
        Text = text;
        CreatedAt = createdAt;
    }
}
