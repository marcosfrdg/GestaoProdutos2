using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetProductListQueryHandler : IQueryHandler<GetProductListQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var dados = await _repository.GetPagedProducts(request.Filter);
            var products = _mapper.Map<List<ProductResponse>>(dados);

            return products;
        }
    }
}
