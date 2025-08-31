using CRM__Project.Clasess;
using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRM__Project.Service
{
    internal class CoustomerService : INotifier, IClock
    {
        private readonly IConsoleHelper consoleHelper;
        private readonly CrmContext context;
        private readonly CustomerRepository customerRepository;
        public CoustomerService(IConsoleHelper consoleHelper, CrmContext context, CustomerRepository customerRepository)
        {
            this.consoleHelper = consoleHelper;
            this.context = context;
            this.customerRepository = customerRepository;

        }


        public void AddCustomer()
        {
            var id = consoleHelper.Prompt<int>("Enter Your id \n");
            var name = consoleHelper.Prompt<string>("Enter Your Name:\n");
            Email email = new Email(consoleHelper.Prompt<string>("Enter The Email: \n"));
            Phone phone = new Phone(consoleHelper.Prompt<string>("Enter The Phone: \n "));
            Address address = new Address(
             consoleHelper.Prompt<string>("Enter The Street: \n "),
             consoleHelper.Prompt<string>("Enter The City: \n "),
             consoleHelper.Prompt<string>("Enter The Country:  \n  "));
            var industry = consoleHelper.Prompt<string>("Enter The Industry  \n ");
            Console.WriteLine("Enter The Number Of Employees ");
            var numberOfEmployees = consoleHelper.Prompt<int>("enter numberOfEmployees  \n ");
            Customer customer = new Customer(id, name, email, phone, address, industry, numberOfEmployees);
            customerRepository.Add(customer);
            Console.WriteLine("Customer added successfully!");

        }


        public void ListCustomers()
        {
            var all = customerRepository.GetAll();
            if (!all.Any())
            {
                Console.WriteLine("No customers.");
                return;
            }

            foreach (var c in all)
                Console.WriteLine($"{c.Id} - {c.Name} - {c.Email.Address}");
        }
        public void UpdateCustomer()
        {

            int id = consoleHelper.Prompt<int>("Enter ID to update: ");
            var customer = customerRepository.GetById(id);
            if (customer == null) { Console.WriteLine("Not found."); return; }

            string name = consoleHelper.Prompt<string>($"Name ({customer.Name}): ");
            if (!string.IsNullOrWhiteSpace(name)) customer.Name = name;

            Email email = new Email(consoleHelper.Prompt<string>("Enter The Email: \n"));
            if (!string.IsNullOrWhiteSpace(email.ToString()))
            {
                customer.Email = new Email(email.ToString());
            }
            Phone phone = new Phone(consoleHelper.Prompt<string>("Enter The Phone: \n "));
            if (!string.IsNullOrWhiteSpace(phone.ToString()))
            {
                customer.Phone = new Phone(phone.ToString());
            }
            Address address = new Address(
              consoleHelper.Prompt<string>("Enter The Street: \n "),
              consoleHelper.Prompt<string>("Enter The City: \n "),
              consoleHelper.Prompt<string>("Enter The Country:  \n  "));
            if (!string.IsNullOrWhiteSpace(address.ToString()))
            {
                customer.Address = new Address(address.ToString(), "", "");
            }
            var industry = consoleHelper.Prompt<string>("Enter The Industry  \n ");
            if (!string.IsNullOrWhiteSpace(industry))
            {
                customer.Industry = industry;
            }
            int numberOfEmployees = consoleHelper.Prompt<int>("Enter Number of Employees: ");
            customer.NumberOfEmployees = numberOfEmployees;
            {
                customer.NumberOfEmployees = numberOfEmployees;
            }
            Console.WriteLine("Customer updated successfully!");
            customerRepository.Update(customer);
            Console.WriteLine("Updated.");

        }

        public void DeleteCustomer()
        {
            int id = consoleHelper.Prompt<int>("Enter ID to delete: ");
            customerRepository.Remove(id);
            Console.WriteLine("Deleted.");
        }

        public void SearchCustomerByName()
        {
            Console.WriteLine("Enter the name or email to search:");
            string searchTerm = Console.ReadLine() ?? string.Empty;
            var matchedCustomers = context.customers.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.Email != null && !string.IsNullOrEmpty(c.Email.Address) && c.Email.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                )//from chat
                .ToList();
            if (matchedCustomers.Count == 0)
            {
                Console.WriteLine("No customers found with the given name or email.");
                return;
            }
            foreach (var customer in matchedCustomers)
            {
                Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Email: {customer.Email.Address}, Phone: {customer.Phone.Number}, Address: {customer.Address.Street}, Industry: {customer.Industry}, Number of Employees: {customer.NumberOfEmployees}");
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



    }
}

