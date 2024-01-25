using Application.Core.Abstractions.Messages;
using Application.Suppliers.Validators;
using Domain.Abstractions;
using FluentValidation;
using System;

namespace Application.Suppliers.Commands
{
    public class DeleteSupplierCommand : ICommand<bool>
    {
        public int Id { get; private set; }

        public DeleteSupplierCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierCommandValidator(ISupplierRepository repository)
        {
            RuleFor(c => c.Id).Must(SupplierExist(repository))
                .WithMessage((supplier, context) => string.Format(SupplierValidationMessages.SupplierNotFound, supplier.Id));
        }

        private static Func<int, bool> SupplierExist(ISupplierRepository repository)
        {
            return (supplierId) =>
            {
                return repository.GetByIdAsync(supplierId).Result is not null;
            };
        }
    }
}