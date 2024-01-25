namespace Application.Products.Validators
{
    public static class ProductValidationMessages
    {
        public const string ProductNotFound = "O produto de ID '{0}' não foi encontrado.";
        public const string SupplierNotFound = "O fornecedor de ID '{0}' não foi encontrado.";
        public const string DescriptionRequired = "A descrição do produto é obrigatória.";
        public const string StatusInvalid = "O status do produto deve ser Ativo ou Inativo.";
        public const string ManufacturingDateMustBeBeforeExpiryDate = "A data de fabricação deve ser anterior à data de validade.";
        public const string InvalidSupplierCnpjFormat = "Formato inválido para o CNPJ do fornecedor. 12.345.678/0001-00";
    }
}
