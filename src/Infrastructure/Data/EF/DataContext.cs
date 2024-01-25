using Bogus;
using Domain.Entities;
using Domain.Enums;
using GestaoProdutos.Infrastructure.EF.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Data.EF
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
           
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());

            // Adicionar produtos fictícios à base de dados em memória
            var fakeProducts = GenerateFakeProducts(30); // Gera produtos fake
            modelBuilder.Entity<Product>().HasData(fakeProducts);

            base.OnModelCreating(modelBuilder);
        }

        private static List<Product> GenerateFakeProducts(int count)
        {
            var productFaker = new Faker<Product>("pt_BR")
                .CustomInstantiator(f => new Product(f.IndexFaker + 1, f.Commerce.ProductName(), f.PickRandom<Status>(),
                f.Date.Past(), f.Date.Future(2)));

            return productFaker.Generate(count);
        }
    }
}
