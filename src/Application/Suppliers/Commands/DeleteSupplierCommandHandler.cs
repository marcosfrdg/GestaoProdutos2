using Application.Core.Abstractions.Messages;
using Domain.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Suppliers.Commands
{
    public class DeleteSupplierCommandHandler : ICommandHandler<DeleteSupplierCommand, bool>
    {
        private readonly ISupplierRepository _repository;

        public DeleteSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(request.Id);

            supplier.SoftDelete();

            _repository.SoftDelete(supplier);

            return true;
        }
    }
}