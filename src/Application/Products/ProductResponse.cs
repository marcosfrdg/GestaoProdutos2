using Application.Suppliers;
using Domain.Enums;
using System;

namespace Application.Products
{
    public class ProductResponse
    {
        public ProductResponse(int id, string description, Status status, DateTime manufacturingDate,
            DateTime expiryDate, SupplierResponse supplier = default)
        {
            Id = id;
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpiryDate = expiryDate;
            StatusDescription = Status.GetDescription();
            Supplier = supplier;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public string StatusDescription { get; private set; }
        public DateTime ManufacturingDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public virtual SupplierResponse Supplier { get; private set; }
    }
}
