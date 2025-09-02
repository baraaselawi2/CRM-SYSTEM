using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM__Project.Clasess
{
    /// <summary>
    /// Repository class for managing Opportunity entities within the CRM context.
    /// Provides CRUD operations and ID generation.
    /// </summary>
    public class OpportunityRepostory
    {
        private readonly CrmContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpportunityRepostory"/> class.
        /// </summary>
        /// <param name="context">The CRM context holding opportunity data.</param>
        public OpportunityRepostory(CrmContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the next available unique ID for an opportunity.
        /// </summary>
        /// <returns>The next integer ID.</returns>
        public int GetNextId() =>
            context.opportunities.Any() ? context.opportunities.Max(o => o.Id) + 1 : 1;

        /// <summary>
        /// Adds a new opportunity to the repository.
        /// </summary>
        /// <param name="opportunity">The opportunity to add.</param>
        public void Add(Opportunity opportunity)
        {
            opportunity.Id = GetNextId();
            context.opportunities.Add(opportunity);
        }

        /// <summary>
        /// Removes an opportunity by its ID.
        /// </summary>
        /// <param name="id">The ID of the opportunity to remove.</param>
        public void Remove(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                context.opportunities.Remove(existing);
            }
        }

        /// <summary>
        /// Updates an existing opportunity's data.
        /// </summary>
        /// <param name="opportunity">The updated opportunity object.</param>
        public void Update(Opportunity opportunity)
        {
            var existing = GetById(opportunity.Id);
            if (existing == null) return;

            existing.Title = opportunity.Title;
            existing.Amount = opportunity.Amount;
            existing.Stage = opportunity.Stage;
            existing.CustomerId = opportunity.CustomerId;
        }

        /// <summary>
        /// Retrieves all opportunities.
        /// </summary>
        /// <returns>A list of all opportunities in the repository.</returns>
        public List<Opportunity> GetAll() => context.opportunities.ToList();

        /// <summary>
        /// Retrieves an opportunity by its ID.
        /// </summary>
        /// <param name="id">The ID of the opportunity.</param>
        /// <returns>The opportunity if found; otherwise, null.</returns>
        public Opportunity? GetById(int id) =>
            context.opportunities.FirstOrDefault(o => o.Id == id);
        public Paged<Opportunity> GetPaged(int pageNumber, int pageSize)
        {
            var totalCount = context.opportunities.Count();
            var items = context.opportunities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Paged<Opportunity>(items, pageNumber, pageSize, totalCount);
        }

    }
}
