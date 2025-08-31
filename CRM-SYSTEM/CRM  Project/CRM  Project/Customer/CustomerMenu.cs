using CRM__Project.Interface;
using CRM__Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CRM__Project.Clasess
{
    internal class CustomerMenu :IMenu
    {
        private readonly CoustomerService coustomerService;

        public CustomerMenu(CoustomerService coustomerService)
        {
           this.coustomerService = coustomerService;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Customer Management");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. List Customers");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. SearchByName");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        coustomerService.AddCustomer();
                        break;
                    case "2":
                        coustomerService.ListCustomers();
                        break;
                    case "3":
                        coustomerService.UpdateCustomer();
                        break;
                    case "4":
                        coustomerService.DeleteCustomer();
                        break;
                    case "5":
                        coustomerService.SearchCustomerByName();
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
