using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoProdutos.Infrastructure.EF.Mapping
{

    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Cnpj);
            builder.Property(p => p.CreatedOnUtc);
            builder.Property(p => p.ModifiedOnUtc);
            builder.Property(p => p.DeletedOnUtc);
        }
    }
}

