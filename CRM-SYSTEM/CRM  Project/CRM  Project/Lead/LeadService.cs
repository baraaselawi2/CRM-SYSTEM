using CRM__Project.Clasess;
using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace CRM__Project.Service
{
   
    public class LeadService: INotifier, IClock
    {
        private readonly LeadRepostroy leadRepostroy;
        private readonly IConsoleHelper consoleHelper;
       
        public LeadService(IConsoleHelper consoleHelper, LeadRepostroy leadRepostroy)
        {
   
            this.consoleHelper = consoleHelper;
            this.leadRepostroy = leadRepostroy;
        }

        public static event Action<int, int, string>? LeadConverted;

        public  List<Lead> leads = new List<Lead>();
        public  List<Customer> customers = new List<Customer>();
        public  void AddLead()
        {
            int id = consoleHelper.Prompt<int>("Enter Id");
            string name = consoleHelper.Prompt<string>("Enter The Name ");
            string source = consoleHelper.Prompt<string>("Enter The Source");
            Email email = new Email(consoleHelper.Prompt<string>("Enter The Email: \n"));
            Phone phone = new Phone(consoleHelper.Prompt<string>("Enter The Phone: \n "));
            string status = consoleHelper.Prompt<string>("Enter The Status (New, Contacted, Qualified, Lost)");
            LeadStatus leadStatus;
            if (!Enum.TryParse(status, true, out leadStatus))
            {
                Console.WriteLine("Invalid status entered. Defaulting to 'New'.");
                leadStatus = LeadStatus.New;
            }
            Lead lead = new Lead(id, name, email, phone, source, leadStatus);
            leadRepostroy.Add(lead);

            Console.WriteLine("Lead added successfully!");
        }
        public void UpdateLead()
        {
            int id = consoleHelper.Prompt<int>("Enter the Id of the Lead to update: ");
            var lead = leadRepostroy.GetById(id);
            if (lead == null)
            {
                Console.WriteLine("Lead not found.");
                return;
            }

            string name = consoleHelper.Prompt<string>("Enter The Name ");
            if (!string.IsNullOrWhiteSpace(name))
                lead.Name = name;

            string emailInput = consoleHelper.Prompt<string>("Enter The Email: ");
            if (!string.IsNullOrWhiteSpace(emailInput))
                lead.Email = new Email(emailInput);

            string phoneInput = consoleHelper.Prompt<string>("Enter The Phone: ");
            if (!string.IsNullOrWhiteSpace(phoneInput))
                lead.Phone = new Phone(phoneInput);

            string source = consoleHelper.Prompt<string>("Enter The Source");
            if (!string.IsNullOrWhiteSpace(source))
                lead.Source = source;

            string status = consoleHelper.Prompt<string>("Enter The Status (New, Contacted, Qualified, Lost)");
            if (!Enum.TryParse(status, true, out LeadStatus leadStatus))
                leadStatus = LeadStatus.New;
            lead.LeadStatus = leadStatus;

            leadRepostroy.Update(lead);

            Console.WriteLine("Lead updated successfully!");
        }


        public void ListLead()
        {
            var all = leadRepostroy.GetAll();
            if (!all.Any())
            {
                Console.WriteLine("No Lead found.");
                return;
            }
            foreach (var c in all)
            {
                Console.WriteLine($"Id: {c.Id}, Name: {c.Name}, Email: {c.Email.Address}, Phone: {c.Phone.Number}, Source: {c.Source}, Stutas: {c.LeadStatus}");
            }
        }
     
        public  void DeleteLead()
        {
            int id = consoleHelper.Prompt<int>("Enter ID to delete: ");
            leadRepostroy.Remove(id);
            Console.WriteLine("Deleted.");
        }
        public  void SearchLeadByName()
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
        public  DateTime Now()
        {
            return DateTime.Now;
        }
        public static void Notify(string message)
        {
            Console.WriteLine($"Notification: {message}");
        }
        public  void ConvertLeadToCustomer()
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

       
       
    }

}

