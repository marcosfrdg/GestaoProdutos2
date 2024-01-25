using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(command);

            _repository.Add(product);

            return product.Id;
        }
    }
}
