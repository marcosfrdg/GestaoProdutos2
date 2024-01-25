using Domain.Abstractions;
using Domain.Enums;
using Domain.Primitives;
using System;

namespace Domain.Entities
{
    public class Product : Entity<int>, IAggregateRoot
    {

        public Product(int id, string description, Status status,
            DateTime manufacturingDate, DateTime expiryDate, Supplier supplier = default) : base(id)
        {
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate.ToUniversalTime();
            ExpiryDate = expiryDate.ToUniversalTime();
            Supplier = supplier;
        }

        // Construtor padrão sem parâmetros é necessário para o EF
        protected Product() { }

        public string Description { get; private set; }
        public Status Status { get; private set; }
        public DateTime ManufacturingDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public virtual Supplier Supplier { get; private set; }

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
