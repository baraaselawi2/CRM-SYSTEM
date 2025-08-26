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

           public List<Note> notes = new List<Note>();
        private int nextId = 1;

        public void AddNote()
        {
            Console.Write("Enter RelatedTo (Customer, Lead): ");
            string relatedToInput = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter RelatedId (integer): ");
            int relatedId = int.TryParse(Console.ReadLine(), out int rid) ? rid : 0;

            Console.Write("Enter Date and Time (yyyy-MM-dd HH:mm): ");
            DateTime when = DateTime.TryParse(Console.ReadLine(), out DateTime dt) ? dt : DateTime.Now;

            Console.Write("Enter Note: ");
            string noteText = Console.ReadLine() ?? string.Empty;

  
            RelatedToType relatedToEnum = (RelatedToType)Enum.Parse(typeof(RelatedToType), relatedToInput, true);


            var note = new Note(
       nextId++,         
       relatedToEnum,       
       relatedId,           
       noteText,            
       when                
   );
            notes.Add(note);

            Console.WriteLine("Note added successfully!");
        }
        public void ListNotes()
        {
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes found.");
                return;
            }

            foreach (var n in notes)
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


