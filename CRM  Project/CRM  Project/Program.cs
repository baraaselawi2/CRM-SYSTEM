using CRM__Project.Service;
while (true)
{
    Console.WriteLine("Choose Service: \n 1. Customer Service \n 2. Lead Service \n 3. Opportunity Service \n 4. Exit");
    int choice = int.Parse(Console.ReadLine() ?? string.Empty);
    if (choice == 4)
    {
        break;
    }
    switch (choice)
    {

        case 1:
            new CoustomerService().ShowMainMenu();
            break;
        case 2:
           new LeadService().ShowMainMenu();
            break;
        case 3:
           new OpportunityService().ShowMainMenu();
            break;

            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

