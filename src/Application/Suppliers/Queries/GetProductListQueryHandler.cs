using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Suppliers.Queries
{
    public class GetSupplierListQueryHandler : IQueryHandler<GetSupplierListQuery, List<SupplierResponse>>
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public GetSupplierListQueryHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SupplierResponse>> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<List<SupplierResponse>>(await _repository.GetPagedSuppliers(request.Filter));

            return products;
        }
    }
}