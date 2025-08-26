using CRM__Project.Interface;

public class ActivityService : IMenu, INotifier, IClock
{
    private List<Activity> activities = new List<Activity>();
    private int nextId = 1;

    public void AddActivity()
    {
        Console.Write("Enter RelatedTo (Lead, Customer,) ");
        string relatedToInput = Console.ReadLine() ?? string.Empty;
        RelatedToType relatedToEnum = (RelatedToType)Enum.Parse(typeof(RelatedToType), relatedToInput, true);

        Console.Write("Enter RelatedId: ");
        int relatedId = int.TryParse(Console.ReadLine(), out int rid) ? rid : 0;

        Console.Write("Enter Type (Call, Email, Meeting): ");
        string typeInput = Console.ReadLine() ?? string.Empty;
        ActivityType typeEnum = (ActivityType)Enum.Parse(typeof(ActivityType), typeInput, true);

        Console.Write("Enter Date and Time (yyyy-MM-dd HH:mm): ");
        DateTime when = DateTime.TryParse(Console.ReadLine(), out DateTime dt) ? dt : DateTime.Now;

        Console.Write("Enter Note: ");
        string noteText = Console.ReadLine() ?? string.Empty;

        var activityItem = new Activity(nextId++, relatedToEnum, relatedId, typeEnum, when, noteText);
        activities.Add(activityItem);

        Console.WriteLine("Activity added successfully!");
    }

    public void ListActivities()
    {
        if (activities.Count == 0)
        {
            Console.WriteLine("No activities found.");
            return;
        }

        foreach (var a in activities)
        {
            Console.WriteLine($"Id: {a.Id}, RelatedTo: {a.RelatedTo}, RelatedId: {a.RelatedId}, Type: {a.Type}, Date: {a.When}, Note: {a.Note}");
        }
    }

    public void ShowMainMenu()
    {
        while (true)
        {
            Console.WriteLine("Activity Management");
            Console.WriteLine("1. Add Activity");
            Console.WriteLine("2. List Activities");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    AddActivity();
                    break;
                case "2":
                    ListActivities();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
