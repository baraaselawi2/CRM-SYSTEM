using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Service
{
    internal class LeadService: IMenu, INotifier, IClock
    {
        public static List<Lead> leads = new List<Lead>();
        public static void AddLead()
        {
            Console.WriteLine("Enter Id ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Enter The Name ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter The Source ");
            string source = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter The Email ");
            string emailInput = Console.ReadLine() ?? string.Empty;
            Email email = new Email(emailInput);
            Console.WriteLine("Enter The Phone ");
            string phoneInput = Console.ReadLine() ?? string.Empty;
            Phone phone = new Phone(phoneInput);
            Console.WriteLine("Enter The Status (New, Contacted, Qualified, Lost) ");
            string status = Console.ReadLine() ?? string.Empty;
            LeadStatus leadStatus;
            if (!Enum.TryParse(status, true, out leadStatus))
            {
                Console.WriteLine("Invalid status entered. Defaulting to 'New'.");
                leadStatus = LeadStatus.New;
            }
            Lead lead = new Lead(id, name, email, phone, source, leadStatus);
            leads.Add(lead);

            Console.WriteLine("Lead added successfully!");
        }
        public static void UpdateLead()
        {
            Console.WriteLine("Enter the Id of the customer to update:");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            var lead = leads.FirstOrDefault(c => c.Id == id);
            if (lead == null)
            {
                Console.WriteLine("Lead not found.");
                return;
            }
            Console.WriteLine("Enter new Name");
            string name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                lead.Name = name;
            }
            Console.WriteLine("Enter new Email");
            string emailInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(emailInput))
            {
                lead.Email = new Email(emailInput);
            }
            Console.WriteLine("Enter new Phone");
            string phoneInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(phoneInput))
            {
                lead.Phone = new Phone(phoneInput);
            }

            Console.WriteLine("Enter new Industry");
            string source = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(source))
            {
                lead.Source = source;
            }
            Console.WriteLine("Enter The Status (New, Contacted, Qualified, Lost) ");
            string status = Console.ReadLine() ?? string.Empty;
            LeadStatus leadStatus;
            if (!Enum.TryParse(status, true, out leadStatus))
            {
                Console.WriteLine("Invalid status entered. Defaulting to 'New'.");
                leadStatus = LeadStatus.New;
            }
            Console.WriteLine("Lead updated successfully!");
        }


        public static void ListLead()
        {
            if (leads.Count == 0)
            {
                Console.WriteLine("No Lead found.");
                return;
            }
            foreach (var lead in leads)
            {
                Console.WriteLine($"Id: {lead.Id}, Name: {lead.Name}, Email: {lead.Email.Address}, Phone: {lead.Phone.Number}, Source: {lead.Source}, Stutas: {lead.LeadStatus}");
            }
        }
        public static void DeleteLead()
        {
            Console.WriteLine("Enter the Id of the Lead to delete:");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            var lead = leads.FirstOrDefault(l => l.Id == id);
            if (lead == null)
            {
                Console.WriteLine("Lead not found.");
                return;
            }
            leads.Remove(lead);
            Console.WriteLine("Lead deleted successfully!");
        }
        public static void SearchLeadByName()
        {
            Console.WriteLine("Enter the name to search:");
            string name = Console.ReadLine() ?? string.Empty;
            var matchedLead = leads.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchedLead.Count == 0)
            {
                Console.WriteLine("No customers found with the given name.");
                return;
            }
            foreach (var lead in matchedLead)
            {
                Console.WriteLine($"Id: {lead.Id}, Name: {lead.Name}, Email: {lead.Email.Address}, Phone: {lead.Phone.Number}, Source: {lead.Source}, Stutas: {lead.LeadStatus}");
            }
        }
        public static DateTime Now()
        {
            return DateTime.Now;
        }
        public static void Notify(string message)
        {
            Console.WriteLine($"Notification: {message}");
        }
        public static void ConvertLeadToCustomer()
        {
            Console.WriteLine("Enter the Id of the Lead to convert:");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);

            var lead = leads.FirstOrDefault(l => l.Id == id);
            if (lead == null)
            {
                Console.WriteLine("Lead not found.");
                return;
                Customer customer = new Customer(lead.Id, lead.Name, lead.Email, lead.Phone, new Address("", "", ""), "", 0);
            }
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
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        AddLead();
                        break;
                    case "2":
                        ListLead();
                        break;
                    case "3":
                        UpdateLead();
                        break;
                    case "4":
                        DeleteLead();
                        break;
                    case "5":
                        SearchLeadByName();
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

