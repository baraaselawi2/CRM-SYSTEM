using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM__Project.Clasess
{
    public class CustomerRepository
    {
        private readonly CrmContext context;

        public CustomerRepository(CrmContext context)
        {
            this.context = context;
        }

        public int GetNextId() =>
            context.customers.Any() ? context.customers.Max(c => c.Id) + 1 : 1;

        public void Add(Customer customer)
        {
            customer.Id = GetNextId();
            context.customers.Add(customer);
        }

        public void Remove(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {

            }
                context.customers.Remove(existing);
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.Id);
            if (existing == null) return;
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.Address = customer.Address;
            existing.Industry = customer.Industry;
            existing.NumberOfEmployees = customer.NumberOfEmployees;
        }

        public List<Customer> GetAll() => context.customers.ToList();

        public Customer? GetById(int id) =>
            context.customers.FirstOrDefault(c => c.Id == id);
    }
}
