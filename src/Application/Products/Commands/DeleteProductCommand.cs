using Application.Core.Abstractions.Messages;
using Application.Products.Validators;
using Domain.Abstractions;
using FluentValidation;
using System;

namespace Application.Products.Commands
{
    public class DeleteProductCommand : ICommand<bool>
    {
        public int Id { get; private set; }

        public DeleteProductCommand(int id) { 
            Id = id;
        }
    }

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator(IProductRepository repository)
        {
            RuleFor(c => c.Id).Must(ProductExist(repository))
                .WithMessage((product, context) => string.Format(ProductValidationMessages.ProductNotFound, product.Id));
        }

        private static Func<int, bool> ProductExist(IProductRepository repository)
        {
            return (produtoId) =>
            {
                return repository.GetByIdAsync(produtoId).Result is not null;

            };
        }
    }
}
