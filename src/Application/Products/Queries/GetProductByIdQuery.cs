using Application.Core.Abstractions.Messages;

namespace Application.Products.Queries
{
    public class GetProductByIdQuery : IQuery<ProductResponse>
    {
        public int Id { get; }

        public GetProductByIdQuery(int id) => Id = id;
    }
}
