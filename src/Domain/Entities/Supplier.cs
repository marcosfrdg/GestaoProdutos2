using Domain.Abstractions;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class Supplier : Entity<int>, IAggregateRoot
    {
        public Supplier(int id, string description, string cnpj, Status status) : base(id)
        {
            Description = description;
            Cnpj = cnpj;
            Status = status;
        }

        protected Supplier() { }

        public string Description { get; private set; }
        public string Cnpj { get; private set; }
        public Status Status { get; private set; }

        public void ChangeStatus(Status statusProduct)
        {
            Status = statusProduct;
        }

        public void SoftDelete()
        {
            Status = Status.Inativo;
        }
    }
}
