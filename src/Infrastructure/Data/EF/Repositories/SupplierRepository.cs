using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.EF.Repositories
{
    public class SupplierRepository : BaseReporitory<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Supplier>> GetPagedSuppliers(string filter)
        {
            var query = await GetAllAsync();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p =>
                    p.Description.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

    }
}
