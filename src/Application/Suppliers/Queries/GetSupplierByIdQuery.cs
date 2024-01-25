using Application.Core.Abstractions.Messages;

namespace Application.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IQuery<SupplierResponse>
    {
        public int Id { get; }

        public GetSupplierByIdQuery(int id) => Id = id;
    }
}