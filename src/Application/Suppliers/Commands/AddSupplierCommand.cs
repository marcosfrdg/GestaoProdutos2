using Application.Core.Abstractions.Messages;
using Application.Suppliers.Validators;
using Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Suppliers.Commands
{
    public sealed class AddSupplierCommand : ICommand<int>
    {
        public AddSupplierCommand(string description, string cnpj, Status status)
        {
            Description = description;
            Cnpj = cnpj;
            Status = status;
        }

        public string Description { get; private set; }
        public string Cnpj { get; private set; }
        public Status Status { get; }
    }

    public class AddSupplierCommandValidator : AbstractValidator<AddSupplierCommand>
    {
        public AddSupplierCommandValidator()
        {
            RuleFor(p => p.Description).NotEmpty().WithMessage(SupplierValidationMessages.DescriptionRequired);
            RuleFor(p => p.Status).IsInEnum().WithMessage(SupplierValidationMessages.StatusInvalid);

            // Validação do CNPJ (somente se fornecido)
            RuleFor(p => p.Cnpj)
            .Must(cnpj => string.IsNullOrEmpty(cnpj) || Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
            .WithMessage(SupplierValidationMessages.InvalidSupplierCnpjFormat);
        }
    }
}