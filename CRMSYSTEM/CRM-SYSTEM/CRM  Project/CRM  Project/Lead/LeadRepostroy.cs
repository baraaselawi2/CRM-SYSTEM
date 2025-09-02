using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    public  class LeadRepostroy
    {

        private readonly CrmContext context;

        public LeadRepostroy(CrmContext context)
        {
            this.context = context;
        }
        public void Remove(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {

            }
            context.leads.Remove(existing);
        }

        public void Update(Lead lead)
        {
            var existing = GetById(lead.Id);
            if (existing == null) return;
            existing.Name = lead.Name;
            existing.Email = lead.Email;
            existing.Phone = lead.Phone;
            existing.Source = lead.Source;
            existing.Date = lead.Date;
            existing.LeadStatus = lead.LeadStatus;
        }
        public int GetNextId() =>
          context.leads.Any() ? context.leads.Max(c => c.Id) + 1 : 1;

        public void Add(Lead lead)
        {
            lead.Id = GetNextId();
            context.leads.Add(lead);
        }
        public List<Lead> GetAll() => context.leads.ToList();

        public Lead? GetById(int id) =>
          context.leads.FirstOrDefault(c => c.Id == id);
    }
}
