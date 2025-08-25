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
    internal class CoustomerService :  IMenu, INotifier, IClock
    {
        public static List<Customer> customers = new List<Customer>();

        public static void AddCustomer()
        {
            Console.WriteLine("Enter Id ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Enter The Name ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter The Email ");
            string emailInput = Console.ReadLine() ?? string.Empty;
            Email email = new Email(emailInput);
            Console.WriteLine("Enter The Phone ");
            string phoneInput = Console.ReadLine() ?? string.Empty;
            Phone phone = new Phone(phoneInput);
            Console.WriteLine("Enter The Address ");
            string addressInput = Console.ReadLine() ?? string.Empty;
            Address address = new Address(addressInput, "", "");
            Console.WriteLine("Enter The Industry ");
            string industry = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter The Number Of Employees ");
            int numberOfEmployees = int.Parse(Console.ReadLine() ?? string.Empty);
            Customer customer = new Customer(id, name, email, phone, address, industry, numberOfEmployees);
            customers.Add(customer);

            Console.WriteLine("Customer added successfully!");

        }

        public static void ListCustomers()
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

        public static void UpdateCustomer()
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
            Console.WriteLine("Enter new Address");
            string addressInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(addressInput))
            {
                customer.Address = new Address(addressInput, "", "");
            }
            Console.WriteLine("Enter new Industry");
            string industry = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(industry))
            {
                customer.Industry = industry;
            }
            Console.WriteLine("Enter new Number Of Employees");
            string numberOfEmployeesInput = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(numberOfEmployeesInput, out int numberOfEmployees))
            {
                customer.NumberOfEmployees = numberOfEmployees;
            }
            Console.WriteLine("Customer updated successfully!");
        }

        public static void DeleteCustomer()
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

        public static void SearchCustomerByName()
        {
            Console.WriteLine("Enter the name or email to search:");
            string searchTerm = Console.ReadLine() ?? string.Empty;
            var matchedCustomers = customers
                .Where(c =>
                    (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Email != null && !string.IsNullOrEmpty(c.Email.Address) && c.Email.Address.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
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
                        AddCustomer();
                        break;
                    case "2":
                        ListCustomers();
                        break;
                    case "3":
                        UpdateCustomer();
                        break;
                    case "4":
                        DeleteCustomer();
                        break;
                    case "5":
                        SearchCustomerByName();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }

         
} }

