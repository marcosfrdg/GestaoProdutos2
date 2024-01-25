using Domain.Enums;

namespace Application.Suppliers
{
    public class SupplierResponse
    {
        public SupplierResponse(int id, string description, string cnpj, Status status)
        {
            Id = id;
            Description = description;
            Cnpj = cnpj;
            Status = status;
            StatusDescription = Status.GetDescription();
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Cnpj { get; private set; }
        public Status Status { get; private set; }
        public string StatusDescription { get; private set; }
    }
}
