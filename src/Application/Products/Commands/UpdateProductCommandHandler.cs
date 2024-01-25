using Application.Core.Abstractions.Messages;
using AutoMapper;
using Domain.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.Id);

            _repository.Update(_mapper.Map(command, product));

            return product.Id;
        }
    }
}
