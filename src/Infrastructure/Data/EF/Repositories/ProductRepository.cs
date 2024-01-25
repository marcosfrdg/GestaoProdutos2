using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.EF.Repositories
{
    public class ProductRepository : BaseReporitory<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) 
            : base(context) { }

        public async Task<IEnumerable<Product>> GetPagedProducts(string filter)
        {
            var query = await GetAllAsync();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p =>
                    p.Description.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            return query;
        }
    }
}
