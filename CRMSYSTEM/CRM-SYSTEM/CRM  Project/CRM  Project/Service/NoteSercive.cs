using CRM__Project.Clasess;
using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Service
{
    public class NoteSercive : IMenu, INotifier, IClock
    {
        private readonly IConsoleHelper consoleHelper;
        private readonly CrmContext Context;
        public NoteSercive(IConsoleHelper consoleHelper , CrmContext Context)
        {
            this.consoleHelper = consoleHelper;
       
            this.Context = Context;
        }   

        private int nextId = 1;

        public void AddNote()
        {
          
            string relatedToInput = consoleHelper.Prompt<string>("Enter RelatedTo (Customer, Lead): ");

            int relatedId = consoleHelper.Prompt<int>("Enter RelatedId (integer): ");

            var when = consoleHelper.Prompt<DateTime>("Enter Date and Time (yyyy-MM-dd HH:mm): ");
 
            string noteText = consoleHelper.Prompt<string>("Enter Note:");

            if (!Enum.TryParse<RelatedToType>(relatedToInput, true, out var relatedToEnum))
            {
                Console.WriteLine("Invalid RelatedTo type. Defaulting to Customer.");
                relatedToEnum = RelatedToType.Customer;
            }

            var note = new Note(
                nextId++,
                relatedToEnum,
                relatedId,
                noteText,
                when
            );

            Context.notes.Add(note);

            Console.WriteLine("Note added successfully!");
        }
        public void ListNotes()
        {
            if (Context.notes.Count == 0)
            {
                Console.WriteLine("No notes found.");
                return;
            }

            foreach (var n in Context.notes)
            {
                Console.WriteLine($"Id: {n.Id}, RelatedTo: {n.RelatedTo}, RelatedId: {n.RelatedId}, Date: {n.CreatedAt}, Note: {n.Text}");
            }
        }



        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Notes");
                Console.WriteLine("1. Add Note");
                Console.WriteLine("2. List Note");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                       
                        AddNote();
                        break;
                    case "2":

                        ListNotes();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }



    }
}


