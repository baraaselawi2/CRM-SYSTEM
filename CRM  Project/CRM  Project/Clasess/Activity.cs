public enum ActivityType { Call, Email, Meeting }
public enum RelatedToType { Lead, Customer, Opportunity }

public class Activity
{
    public int Id { get; set; }
    public RelatedToType RelatedTo { get; set; }
    public int RelatedId { get; set; }
    public ActivityType Type { get; set; }
    public DateTime When { get; set; }
    public string Note { get; set; }

    public Activity(int id, RelatedToType relatedTo, int relatedId, ActivityType type, DateTime when, string note)
    {
        Id = id;
        RelatedTo = relatedTo;
        RelatedId = relatedId;
        Type = type;
        When = when;
        Note = note;
    }
}
