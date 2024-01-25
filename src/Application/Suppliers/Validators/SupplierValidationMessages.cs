namespace Application.Suppliers.Validators
{
    public static class SupplierValidationMessages
    {
        public const string SupplierNotFound = "O fornecedor de ID '{0}' não foi encontrado.";
        public const string DescriptionRequired = "A descrição do fornecedor é obrigatória.";
        public const string StatusInvalid = "O status do Fornecedor deve ser Ativo ou Inativo.";
        public const string InvalidSupplierCnpjFormat = "Formato inválido para o CNPJ. 12.345.678/0001-00";
    }
}