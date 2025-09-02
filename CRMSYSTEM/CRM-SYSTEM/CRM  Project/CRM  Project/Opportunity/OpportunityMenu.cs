using CRM__Project.Interface;
using CRM__Project.Service;
using System;

namespace CRM__Project.Clasess
{
    internal class OpportunityMenu : IMenu
    {
        private readonly OpportunityService opportunityService;

        public OpportunityMenu(OpportunityService opportunityService)
        {
            this.opportunityService = opportunityService;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Opportunity Management");
                Console.WriteLine("1. Add Opportunity");
                Console.WriteLine("2. List Opportunities");
                Console.WriteLine("3. Update Opportunity");
                Console.WriteLine("4. Delete Opportunity");
                Console.WriteLine("5. Advance Stage");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        opportunityService.AddOpportunity();
                        break;
                    case "2":
                        opportunityService.ListOpportunities();
                        break;
                    case "3":
                        opportunityService.UpdateOpportunity();
                        break;
                    case "4":
                        opportunityService.DeleteOpportunity();
                        break;
                    case "5":
                        opportunityService.AdvanceOpportunityStage();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
