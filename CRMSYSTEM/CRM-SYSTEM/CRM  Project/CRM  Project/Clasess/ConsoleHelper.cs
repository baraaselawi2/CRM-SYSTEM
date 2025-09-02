using CRM__Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    public class ConsoleHelper : IConsoleHelper
    {
        public T Prompt<T>(string message)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (typeof(T) == typeof(string))
            {
                return (T)(object)(input ?? string.Empty);
            }

            if (typeof(T) == typeof(int))
            {
                return (T)(object)(int.TryParse(input, out int result) ? result : 0);
            }

            if (typeof(T) == typeof(DateTime))
            {
                return (T)(object)(DateTime.TryParse(input, out DateTime result) ? result : DateTime.UtcNow);
            }
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), input ?? string.Empty, true);
            }

            throw new NotSupportedException($"Type {typeof(T)} is not supported.");
        }
    }
}
