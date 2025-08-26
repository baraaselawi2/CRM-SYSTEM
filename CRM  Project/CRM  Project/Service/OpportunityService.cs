using System;
using System.Collections.Generic;
using System.Linq;
using CRM__Project.Clasess;
using CRM__Project.Interface;

namespace CRM__Project.Service
{
    public class OpportunityService : IMenu, INotifier, IClock
    {

        public static List<Opportunity> opportunities = new List<Opportunity>();
        public static List<Customer> customers = new List<Customer>(); 

        public static event Action<int, int, decimal>? OpportunityWon;
        public static event Action<int, int>? OpportunityLost;

        public static void AddOpportunity()
        {
            Console.Write("Enter Id:");
            var idInput = Console.ReadLine();
            int id;

             if (!int.TryParse(idInput, out id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            Console.Write("Enter The Customer Id ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid customer id");
                return;
            }

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("Enter The Title: ");
            string title = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title is required");
                return;
            }

            Console.Write("Enter The Amount: ");
            var amountInput = Console.ReadLine() ?? string.Empty;
            if (!decimal.TryParse(amountInput, out decimal amount) || amount < 0)
            {
                Console.WriteLine("Invalid amount");
                return;
            }

            Console.Write("Enter The Stage New, Qualified, Won, Lost");
            string statusInput = Console.ReadLine() ?? string.Empty;
            if (!Enum.TryParse(statusInput, true, out Stage stage))
            {
                stage = Stage.New;
            }

            var opp = new Opportunity(id, customerId, title, amount, stage);
            opportunities.Add(opp);

            Console.WriteLine("Opportunity added");

            if (stage == Stage.Won)
                OpportunityWon?.Invoke(opp.Id, opp.CustomerId, opp.Amount);
            else if (stage == Stage.Lost)
                OpportunityLost?.Invoke(opp.Id, opp.CustomerId);
        }

        public static void ListOpportunities()
        {
            if (opportunities.Count == 0)
            {
                Console.WriteLine("No opportunities found");
                return;
            }

            foreach (var o in opportunities)
            {
                var customer = customers.FirstOrDefault(c => c.Id == o.CustomerId);
                string custName = customer != null ? customer.Name : "Customer not found";
                Console.WriteLine($"Id: {o.Id}, Title: {o.Title}, CustomerId: {o.CustomerId} ({custName}), Amount: {o.Amount}, Stage: {o.Stage}");
            }
        }

        public static void UpdateOpportunity()
        {
            Console.Write("Enter the Id of the Opportunity to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id.");
                return;
            }

            var opp = opportunities.FirstOrDefault(o => o.Id == id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }

            Console.Write($"Enter new Title  {opp.Title} ");
            string title = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(title)) opp.Title = title;

            Console.Write($"Enter new Amount {opp.Amount} ");
            string amountInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(amountInput))
            {
                if (decimal.TryParse(amountInput, out decimal newAmount) && newAmount >= 0)
                {
                    opp.Amount = newAmount;
                }
                else
                {
                    Console.WriteLine("Invalid amount");
                }
            }

           
            Console.WriteLine("Opportunity updated successfully!");
        }

        public static void DeleteOpportunity()
        {
            Console.Write("Enter the Id to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id.");
                return;
            }

            var opp = opportunities.FirstOrDefault(o => o.Id == id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }
            opportunities.Remove(opp);
          
        }

        public static void AdvanceOpportunityStage()
        {
            Console.Write("Enter Opportunity Id");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id");
                return;
            }

            var opp = opportunities.FirstOrDefault(o => o.Id == id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found");
                return;
            }

            Console.WriteLine($"Current stage: {opp.Stage}");
            Console.Write("Enter new stage Qualified, Won, Lost ");
            string input = Console.ReadLine() ?? string.Empty;
            if (!Enum.TryParse(input, true, out Stage newStage))
            {
                Console.WriteLine("Invalid stage.");
                return;
            }

            bool allowed = false;
            if (opp.Stage == Stage.New && newStage == Stage.Qualified) allowed = true;
            else if (opp.Stage == Stage.Qualified && (newStage == Stage.Won || newStage == Stage.Lost)) allowed = true;
            else if (opp.Stage == newStage) { 
                Console.WriteLine("Opportunity already in stage"); 
                return;
            }

            if (!allowed)
            {
                Console.WriteLine($"Invalid transition from {opp.Stage}  {newStage}");
                return;
            }

            var previous = opp.Stage;
            opp.Stage = newStage;
            Console.WriteLine($"Opportunity {opp.Id}: {previous} {newStage}");

       
            if (newStage == Stage.Won)
            {
                OpportunityWon?.Invoke(opp.Id, opp.CustomerId, opp.Amount);
                Notify($"Opportunity marked as won.");
            }
            else if (newStage == Stage.Lost)
            {
                OpportunityLost?.Invoke(opp.Id, opp.CustomerId);
                Notify($"Opportunity marked as lost.");
            }
        }

   
        public static DateTime Now() => DateTime.Now;
        public static void Notify(string message) => Console.WriteLine($"Notification: {message}");

  
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine();
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
                        AdvanceOpportunityStage();
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
