using Application.Core.Abstractions.Messages;
using Application.Products.Validators;
using Domain.Abstractions;
using Domain.Enums;
using FluentValidation;
using System;

namespace Application.Products.Commands
{
    public sealed class AddProductCommand : ICommand<int>
    {
        public AddProductCommand(string description, Status status, DateTime manufacturingDate, 
            DateTime expiryDate, int? supplierId)
        {
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpiryDate = expiryDate;
            SupplierId = supplierId;
        }

        public string Description { get; }
        public Status Status { get; }
        public DateTime ManufacturingDate { get; }
        public DateTime ExpiryDate { get; }
        public int? SupplierId { get; }
    }

    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator(ISupplierRepository supplierRepository)
        {
            RuleFor(product => product.Description).NotEmpty().WithMessage(ProductValidationMessages.DescriptionRequired);
            RuleFor(product => product.Status).IsInEnum().WithMessage(ProductValidationMessages.StatusInvalid);

            // Validação da data de fabricação e validade
            RuleFor(product => product.ManufacturingDate)
                .LessThan(product => product.ExpiryDate)
                .When(product => product.ExpiryDate > DateTime.MinValue) // Verifica se ExpiryDate não é o valor padrão (DateTime.MinValue)
                .WithMessage(ProductValidationMessages.ManufacturingDateMustBeBeforeExpiryDate);

            // Validação do Fornecedor
            RuleFor(product => product.SupplierId).Must(SupplierExist(supplierRepository))
            .WithMessage((product, context) => string.Format(ProductValidationMessages.SupplierNotFound, product.SupplierId));
        }

        private static Func<int?, bool> SupplierExist(ISupplierRepository repository)
        {
            return (supplierId) =>
            {
                if (supplierId is null) return false;

                return repository.GetByIdAsync((int)supplierId).Result is not null;
            };
        }
    }
}
