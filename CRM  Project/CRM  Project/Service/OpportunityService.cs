using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Service
{
    internal class OpportunityService: IMenu, INotifier, IClock
    {
        public List<Opportunity> opportunities = new List<Opportunity>();
        public static List<Lead> leads = new List<Lead>();
        public static List<Customer> customers = new List<Customer>();
        public static void AddOpportunity()
        {
            Console.WriteLine("Enter Id ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Enter The Customer Id ");
            int customerId = int.Parse(Console.ReadLine() ?? string.Empty);
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found. Please add the customer first.");
                return;
            }
            Console.WriteLine("Enter The Title ");
            string title = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter The Amount ");
            decimal amount = decimal.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Enter The Status  New, Qualified, Won, Lost  ");
            string status = Console.ReadLine() ?? string.Empty;
            Stage stage;
            if (!Enum.TryParse(status, true, out stage))
            {
                Console.WriteLine("Invalid status entered. Defaulting to 'New'.");
                stage = Stage.New;
            }
            Opportunity opportunity = new Opportunity(id, customerId, title, amount, stage);
            Console.WriteLine("Opportunity added successfully!");
        }
        public static void ListOpportunities()
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Email: {customer.Email.Address}, Phone: {customer.Phone.Number}, Address: {customer.Address.Street}, Industry: {customer.Industry}, Number of Employees: {customer.NumberOfEmployees}");
            }
        }
        public static void UpdateOpportunity()
        {
            Console.WriteLine("Enter the Id of the customer to update:");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }
            Console.WriteLine("Enter new Name");
            string name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                customer.Name = name;
            }
            Console.WriteLine("Enter new Email");
            string emailInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(emailInput))
            {
                customer.Email = new Email(emailInput);
            }
            Console.WriteLine("Enter new Phone");
            string phoneInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(phoneInput))
            {
                customer.Phone = new Phone(phoneInput);
            }
            Console.WriteLine("Enter new Industry");
            string industry = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(industry))
            {
                customer.Industry = industry;
            }
            Console.WriteLine("Enter The Number Of Employees ");
            string numberOfEmployeesInput = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(numberOfEmployeesInput, out int numberOfEmployees))
            {
                customer.NumberOfEmployees = numberOfEmployees;
            }
            Console.WriteLine("Customer updated successfully!");
        }
        public static void DeleteOpportunity()
        {
            Console.WriteLine("Enter the Id of the customer to delete:");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }
            customers.Remove(customer);
            Console.WriteLine("Customer deleted successfully!");
        }
        public static DateTime Now()
        {
            return DateTime.Now;
        }
        public static void Notify(string message)
        {
            Console.WriteLine($"Notification: {message}");
        }
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Customer Management");
                Console.WriteLine("1. Add Opportunity");
                Console.WriteLine("2. List Opportunity");
                Console.WriteLine("3. Update Opportunity");
                Console.WriteLine("4. Delete Opportunity");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        AddOpportunity();
                        break;
                    case "2":
                        ListOpportunities();
                        break;
                    case "3":
                        UpdateOpportunity();
                        break;
                    case "4":
                        DeleteOpportunity();
                        break;

                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
    }
