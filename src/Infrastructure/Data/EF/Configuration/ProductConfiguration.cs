using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoProdutos.Infrastructure.EF.Mapping
{

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.ManufacturingDate);
            builder.Property(p => p.ExpiryDate);
            builder.Property(p => p.CreatedOnUtc).IsRequired();
            builder.Property(p => p.ModifiedOnUtc);
            builder.Property(p => p.DeletedOnUtc);

            builder.OwnsOne(p => p.Supplier);
        }
    }
}

