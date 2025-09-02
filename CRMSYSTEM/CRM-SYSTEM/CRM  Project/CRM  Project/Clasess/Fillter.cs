using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    public class Fillter
    {
        public List<T> Filter<T>(List<T> items, Func<T, bool> predicate)
        {
            return items.Where(predicate).ToList();
        }
    }
}
