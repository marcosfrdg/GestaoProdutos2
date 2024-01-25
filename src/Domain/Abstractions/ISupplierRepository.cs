using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetPagedSuppliers(string filter);
    }
}
