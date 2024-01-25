using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using Domain.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Suppliers.Queries
{
    public class GetSupplierByIdQueryHandler : IQueryHandler<GetSupplierByIdQuery, SupplierResponse>
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SupplierResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            return product is null
                ? throw new AppException($"Fornecedor de ID {request.Id} não encontrado.")
                : _mapper.Map<SupplierResponse>(product);
        }
    }
}