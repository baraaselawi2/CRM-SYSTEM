using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    public class CrmContext
    {

            public List<Lead> leads = new List<Lead>();
            public List<Opportunity> opportunities = new List<Opportunity>();
            public List<Customer> customers = new List<Customer>();
            public List<Note> notes = new List<Note>();
            public List<Activity> activities = new List<Activity>();
  

    }
}
