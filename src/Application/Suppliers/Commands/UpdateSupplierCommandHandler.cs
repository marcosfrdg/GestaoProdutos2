using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Suppliers.Commands
{
    public class UpdateSupplierCommandHandler : ICommandHandler<UpdateSupplierCommand, int>
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSupplierCommandHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(command.Id);

            _repository.Update(_mapper.Map(command, supplier));

            return supplier.Id;
        }
    }
}