using Application.Core.Abstractions.Messages;
using Application.Suppliers.Validators;
using Domain.Abstractions;
using Domain.Enums;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace Application.Suppliers.Commands
{
    public class UpdateSupplierCommand : ICommand<int>
    {
        public UpdateSupplierCommand(int id, string description, string cnpj, Status status)
        {
            Id = id;
            Description = description;
            Cnpj = cnpj;
            Status = status;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Cnpj { get; private set; }
        public Status Status { get; }
    }

    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator(ISupplierRepository repository)
        {
            RuleFor(c => c.Id).Must(SupplierExist(repository))
                .WithMessage((product, context) => string.Format(SupplierValidationMessages.SupplierNotFound, product.Id));

            RuleFor(p => p.Description).NotEmpty().WithMessage(SupplierValidationMessages.DescriptionRequired);
            RuleFor(p => p.Status).IsInEnum().WithMessage(SupplierValidationMessages.StatusInvalid);

            // Validação do CNPJ (somente se fornecido)
            RuleFor(p => p.Cnpj)
            .Must(cnpj => string.IsNullOrEmpty(cnpj) || Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
            .WithMessage(SupplierValidationMessages.InvalidSupplierCnpjFormat);
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