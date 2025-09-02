using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Interface
{
    public interface IConsoleHelper
    {
        T Prompt<T>(string message);
    }
}

