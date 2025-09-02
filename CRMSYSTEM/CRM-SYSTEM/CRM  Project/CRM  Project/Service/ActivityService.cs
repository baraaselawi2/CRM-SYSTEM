using CRM__Project.Clasess;
using CRM__Project.Interface;

public class ActivityService : IMenu, INotifier, IClock
{
    private readonly IConsoleHelper consoleHelper;
    private readonly CrmContext context;
    private int nextId = 1;

    public ActivityService(IConsoleHelper consoleHelper, CrmContext context)
    {
        this.context = context;
        this.consoleHelper = consoleHelper;
    }

    public void AddActivity()
    {
        var relatedToEnum = consoleHelper.Prompt<RelatedToType>("Enter RelatedTo (Lead, Customer): ");
        var relatedId = consoleHelper.Prompt<int>("Enter RelatedId: ");
        var typeEnum = consoleHelper.Prompt<ActivityType>("Enter Type (Call, Email, Meeting): ");
        var when = consoleHelper.Prompt<DateTime>("Enter Date and Time (yyyy-MM-dd HH:mm): ");
        var noteText = consoleHelper.Prompt<string>("Enter Note: ");
        var activityItem = new Activity(nextId++, relatedToEnum, relatedId, typeEnum, when, noteText);

        
        context.activities.Add(activityItem);

        Console.WriteLine("Activity added successfully!");
    }

    public void ListActivities()
    {
        if (context.activities.Count == 0)
        {
            Console.WriteLine("No activities found.");
            return;
        }

        foreach (var a in context.activities)
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
