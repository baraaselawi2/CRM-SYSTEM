using System;
using System.Collections.Generic;
using System.Linq;
using CRM__Project.Clasess;
using CRM__Project.Interface;

namespace CRM__Project.Service
{
    public class OpportunityService :  INotifier, IClock
    {
        private readonly OpportunityRepostory opportunityRepostory;
        private readonly CustomerRepository customerRepository;
        private readonly IConsoleHelper consoleHelper;

        public OpportunityService(
            IConsoleHelper consoleHelper,
            OpportunityRepostory opportunityRepostory,
            CustomerRepository customerRepository)
        {
            this.consoleHelper = consoleHelper;
            this.opportunityRepostory = opportunityRepostory;
            this.customerRepository = customerRepository;
        }

        public event Action<int, int, decimal>? OpportunityWon;
        public event Action<int, int>? OpportunityLost;

        public void AddOpportunity()
        {
            int id = opportunityRepostory.GetNextId();

            int customerId = consoleHelper.Prompt<int>("Enter The Customer Id: ");
            var customer = customerRepository.GetById(customerId);
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
            opportunityRepostory.Add(opp);

            Console.WriteLine("Opportunity added.");

            if (stage == Stage.Won)
                OpportunityWon?.Invoke(opp.Id, opp.CustomerId, opp.Amount);
            else if (stage == Stage.Lost)
                OpportunityLost?.Invoke(opp.Id, opp.CustomerId);
        }

        public void ListOpportunities()
        {
            var opportunities = opportunityRepostory.GetAll();
            if (!opportunities.Any())
            {
                Console.WriteLine("No opportunities found.");
                return;
            }

            foreach (var o in opportunities)
            {
                var customer = customerRepository.GetById(o.CustomerId);
                string custName = customer != null ? customer.Name : "Customer not found";
                Console.WriteLine($"Id: {o.Id}, Title: {o.Title}, CustomerId: {o.CustomerId} ({custName}), Amount: {o.Amount}, Stage: {o.Stage}");
            }
        }

        public void UpdateOpportunity()
        {
            int id = consoleHelper.Prompt<int>("Enter the Id of the Opportunity to update: ");
            var opp = opportunityRepostory.GetById(id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }

            string title = consoleHelper.Prompt<string>($"Enter new Title ({opp.Title}): ");
            if (!string.IsNullOrWhiteSpace(title)) opp.Title = title;

            string amountInput = consoleHelper.Prompt<string>($"Enter new Amount ({opp.Amount}): ");
            if (!string.IsNullOrWhiteSpace(amountInput) &&
                decimal.TryParse(amountInput, out decimal newAmount) && newAmount >= 0)
            {
                opp.Amount = newAmount;
            }

            opportunityRepostory.Update(opp);
            Console.WriteLine("Opportunity updated successfully!");
        }

        public void DeleteOpportunity()
        {
            int id = consoleHelper.Prompt<int>("Enter the Id to delete: ");
            var opp = opportunityRepostory.GetById(id);
            if (opp == null)
            {
                Console.WriteLine("Opportunity not found.");
                return;
            }

            opportunityRepostory.Remove(id);
            Console.WriteLine("Opportunity deleted successfully!");
        }

        public void AdvanceOpportunityStage()
        {
            int id = consoleHelper.Prompt<int>("Enter Opportunity Id: ");
            var opp = opportunityRepostory.GetById(id);
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
            opportunityRepostory.Update(opp);

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
        public void ListOpportunitiesPaged()
        {
            Console.Write("Enter page number: ");
            int page = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Enter page size: ");
            int size = int.Parse(Console.ReadLine() ?? "5");

            var paged = opportunityRepostory.GetPaged(page, size);

            Console.WriteLine($"Showing page {paged.PageNumber} of {Math.Ceiling((double)paged.TotalCount / paged.PageSize)}");

            foreach (var opp in paged.Items)
            {
                Console.WriteLine($"{opp.Id} - {opp.Title} - {opp.Amount} - {opp.Stage}");
            }
        }


    }
}
