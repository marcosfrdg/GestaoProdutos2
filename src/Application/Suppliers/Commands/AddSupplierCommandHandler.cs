using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Suppliers.Commands
{
    public class AddSupplierCommandHandler : ICommandHandler<AddSupplierCommand, int>
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public AddSupplierCommandHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddSupplierCommand command, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Supplier>(command);

            _repository.Add(supplier);

            return supplier.Id;
        }
    }
}