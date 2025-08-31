using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Interface
{
    internal interface IClock
    {
        public static DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
