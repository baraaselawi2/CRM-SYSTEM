using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Service
{
    public class LeadService: IMenu, INotifier, IClock
    {
        public static event Action<int, int, string>? LeadConverted;

        public static List<Lead> leads = new List<Lead>();
        public static List<Customer> customers = new List<Customer>();
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
            Console.Write("Enter the id of the Lead to convert to Customer: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id.");
                return;
            }

            var lead = leads.FirstOrDefault(l => l.Id == id);
            if (lead == null)
            {
                Console.WriteLine("Lead not found.");
                return;
            }

            if (lead.Email == null || string.IsNullOrWhiteSpace(lead.Email.Address))
            {
                Console.WriteLine("Lead has no valid email");
                return;
            }

            var existingCustomer = customers.FirstOrDefault(c =>
                c.Email != null && c.Email.Address.Equals(lead.Email.Address, StringComparison.OrdinalIgnoreCase));

            if (existingCustomer != null)
            {
   
                leads.Remove(lead);
                Notify($"Lead matched existing Customer (id={existingCustomer.Id}). Lead removed.");
                LeadConverted?.Invoke(lead.Id, existingCustomer.Id, lead.Email.Address);
                return;
            }

            int newCustomerId = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;

            var newCustomer = new Customer(
                id: newCustomerId,
                name: lead.Name,
                email: lead.Email,
                phone: lead.Phone,
                address: new Address("", "", ""),
                industry: "",
                numberOfEmployees: 0
            );

            customers.Add(newCustomer);
            leads.Remove(lead);

            Notify($"Lead {lead.Name} has been converted to a Customer (id={newCustomer.Id})");

          
            LeadConverted?.Invoke(lead.Id, newCustomer.Id, lead.Email.Address);
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
                        ConvertLeadToCustomer();
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

