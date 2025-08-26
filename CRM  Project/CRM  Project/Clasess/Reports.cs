using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    public class Reports : IMenu
    {
        public List<Opportunity> opportunities = new List<Opportunity>();
        public static List<Lead> leads = new List<Lead>();

        public static void GenerateLeadReport()
        {
            if (leads.Count == 0)
            {
                Console.WriteLine("No leads found");
                return;
            }

            var statusGroups = leads.GroupBy(l => l.LeadStatus)
                                    .Select(g => new { Status = g.Key, Count = g.Count() });

            Console.WriteLine("Lead Report ");
            foreach (var group in statusGroups)
            {
                Console.WriteLine($"Status: {group.Status}, Count: {group.Count}");
            }
    
        }

        public static void LeadCount()
        {
            var leadstatusCount = leads.GroupBy(l => l.LeadStatus)
                                       .Select(g => new { Status = g.Key, Count = g.Count() });

            Console.WriteLine("Lead Count by Status ");
            foreach (var group in leadstatusCount)
            {
                Console.WriteLine($"Status: {group.Status}, Count: {group.Count}");
            }
          
        }


        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Activity Management");
                Console.WriteLine("1.GenerateLeadReport");
                Console.WriteLine("2. Lead Count");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        GenerateLeadReport();
                        break;
                    case "2":
                        LeadCount();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}