using Application.Core.Abstractions.Messages;
using Domain.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            product.SoftDelete();

            _repository.SoftDelete(product);

            return true;
        }
    }
}
