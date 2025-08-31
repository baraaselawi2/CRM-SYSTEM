using CRM__Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    public class LeadMenu
    {

        private readonly LeadService leadService;
        public LeadMenu(LeadService leadService)
        {
            this.leadService = leadService;
        }
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Customer Management");
                Console.WriteLine("1. Add Leads");
                Console.WriteLine("2. List Leads");
                Console.WriteLine("3. Update Leads");
                Console.WriteLine("4. Delete Lead");
                Console.WriteLine("5. SearchByName");
                Console.WriteLine("6. ConvertLeadToCustomer");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        leadService.AddLead();
                        break;
                    case "2":
                        leadService.ListLead();
                        break;
                    case "3":
                        leadService.UpdateLead();
                        break;
                    case "4":
                        leadService.DeleteLead();
                        break;
                    case "5":
                        leadService.SearchLeadByName();
                        break;
                    case "6":
                        leadService.ConvertLeadToCustomer();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }

    }
}
