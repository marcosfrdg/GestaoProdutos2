using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByCondition(p => p.Id.Equals(request.Id))
                                .AsNoTracking().Include(p => p.Supplier)
                                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return product is null
                ? throw new AppException($"Produto de ID {request.Id} não encontrado.")
                : _mapper.Map<ProductResponse>(product);
        }
    }
}
