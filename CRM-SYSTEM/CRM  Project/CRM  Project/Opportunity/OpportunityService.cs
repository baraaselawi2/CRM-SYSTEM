using System;
using System.Collections.Generic;
using System.Linq;
using CRM__Project.Clasess;
using CRM__Project.Interface;

namespace CRM__Project.Service
{
    public class OpportunityService : IMenu, INotifier, IClock
    {
        private readonly CrmContext context;
        private readonly IConsoleHelper consoleHelper;

        public OpportunityService(IConsoleHelper consoleHelper, CrmContext context)
        {
            this.consoleHelper = consoleHelper;
            this.context = context;
        }

        public event Action<int, int, decimal>? OpportunityWon;
        public event Action<int, int>? OpportunityLost;

        private int nextId => context.opportunities.Any() ? context.opportunities.Max(o => o.Id) + 1 : 1;

        public void AddOpportunity()
        {
            int id = nextId;

            int customerId = consoleHelper.Prompt<int>("Enter The Customer Id: ");
            var customer = context.customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            string title = consoleHelper.Prompt<string>("Enter The Title: ");
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title is required.");
                return;
            }

            decimal amount = consoleHelper.Prompt<decimal>("Enter The Amount: ");
            if (amount < 0)
            {
                Console.WriteLine("Amount cannot be negative.");
                return;
            }

            string statusInput = consoleHelper.Prompt<string>("Enter The Stage (New, Qualified, Won, Lost): ");
            if (!Enum.TryParse(statusInput, true, out Stage stage))
            {
                stage = Stage.New;
            }

            var opp = new Opportunity(id, customerId, title, amount, stage);
            context.opportunities.Add(opp);

            Console.WriteLine("Opportunity added.");

            if (stage == Stage.Won)
                OpportunityWon?.Invoke(opp.Id, opp.CustomerId, opp.Amount);
            else if (stage == Stage.Lost)
                OpportunityLost?.Invoke(opp.Id, opp.CustomerId);
        }

        public void ListOpportunities()
        {
            if (!context.opportunities.Any())
            {
                Console.WriteLine("No opportunities found.");
                return;
            }

            foreach (var o in context.opportunities)
            {
                var customer = context.customers.FirstOrDefault(c => c.Id == o.CustomerId);
                string custName = customer != null ? customer.Name : "Customer not found";
                Console.WriteLine($"Id: {o.Id}, Title: {o.Title}, CustomerId: {o.CustomerId} ({custName}), Amount: {o.Amount}, Stage: {o.Stage}");
            }
        }

        public void UpdateOpportunity()
        {
            int id = consoleHelper.Prompt<int>("Enter the Id of the Opportunity to update: ");
            var opp = context.opportunities.FirstOrDefault(o => o.Id == id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }

            string title = consoleHelper.Prompt<string>($"Enter new Title ({opp.Title}): ");
            if (!string.IsNullOrWhiteSpace(title)) opp.Title = title;

            string amountInput = consoleHelper.Prompt<string>($"Enter new Amount ({opp.Amount}): ");
            if (!string.IsNullOrWhiteSpace(amountInput) && decimal.TryParse(amountInput, out decimal newAmount) && newAmount >= 0)
            {
                opp.Amount = newAmount;
            }

            Console.WriteLine("Opportunity updated successfully!");
        }

        public void DeleteOpportunity()
        {
            int id = consoleHelper.Prompt<int>("Enter the Id to delete: ");
            var opp = context.opportunities.FirstOrDefault(o => o.Id == id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }
            context.opportunities.Remove(opp);
            Console.WriteLine("Opportunity deleted successfully!");
        }

        public void AdvanceOpportunityStage()
        {
            int id = consoleHelper.Prompt<int>("Enter Opportunity Id: ");
            var opp = context.opportunities.FirstOrDefault(o => o.Id == id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }

            string input = consoleHelper.Prompt<string>($"Current stage: {opp.Stage}. Enter new stage (Qualified, Won, Lost): ");
            if (!Enum.TryParse(input, true, out Stage newStage))
            {
                Console.WriteLine("Invalid stage.");
                return;
            }

            bool allowed = false;
            if (opp.Stage == Stage.New && newStage == Stage.Qualified) allowed = true;
            else if (opp.Stage == Stage.Qualified && (newStage == Stage.Won || newStage == Stage.Lost)) allowed = true;
            else if (opp.Stage == newStage)
            {
                Console.WriteLine("Opportunity already in this stage.");
                return;
            }

            if (!allowed)
            {
                Console.WriteLine($"Invalid transition from {opp.Stage} to {newStage}");
                return;
            }

            var previous = opp.Stage;
            opp.Stage = newStage;
            Console.WriteLine($"Opportunity {opp.Id}: {previous} -> {newStage}");

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

        public DateTime Now() => DateTime.Now;
        public void Notify(string message) => Console.WriteLine($"Notification: {message}");

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
                    case "1": AddOpportunity(); break;
                    case "2": ListOpportunities(); break;
                    case "3": UpdateOpportunity(); break;
                    case "4": DeleteOpportunity(); break;
                    case "5": AdvanceOpportunityStage(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
        }
    }
}
