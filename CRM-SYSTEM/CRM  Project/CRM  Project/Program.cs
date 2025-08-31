using CRM__Project.Clasess;
using CRM__Project.Service;


var consoleHelper = new ConsoleHelper();
var context = new CrmContext();
var reposotry= new CustomerRepository(context);
var coustomerService = new CoustomerService(consoleHelper, context, reposotry);
var customerMenu  = new CustomerMenu(coustomerService);
var leadRepository = new LeadRepostroy(context);
var leadService = new LeadService(consoleHelper, leadRepository);
var leadMenu = new LeadMenu(leadService);
while (true)
{
    Console.WriteLine("Choose Service: \n 1. Customer Service \n 2. Lead Service \n 3. Opportunity Service \n 4. Notes \n 5. Activity \n 6. Report \n 7. Exit \n ===================================================================");
    int choice = int.Parse(Console.ReadLine() ?? string.Empty);
    if (choice == 7)
    {
        break;
    }
    switch (choice)
    {

        case 1:
         new CustomerMenu(coustomerService).ShowMainMenu();
            break;
        case 2:
           new LeadMenu(leadService).ShowMainMenu();
            break;
        case 3:
           new OpportunityService(consoleHelper, context).ShowMainMenu();
            break;
        case 4:
           new NoteSercive(consoleHelper, context).ShowMainMenu();
            break;
        case 5:
           new ActivityService(consoleHelper, context).ShowMainMenu();
            break;
        case 6:
           new Reports().ShowMainMenu();
            break;

            Console.WriteLine("Invalid choice Please try again");
            break;
    }
}

