using Application.Products;
using Application.Products.Commands;
using Application.Suppliers;
using Application.Suppliers.Commands;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Core.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        private readonly ISupplierRepository _supplierRepository;

        public AutoMapperConfig(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;

            CreateMap<Product, ProductResponse>()
                .ConstructUsing((src, context) => new ProductResponse(src.Id, src.Description, src.Status,
                src.ManufacturingDate, src.ExpiryDate, context.Mapper.Map<SupplierResponse>(src.Supplier)));

            CreateMap<AddProductCommand, Product>()
                .ConstructUsing((src, context) => new Product(default, src.Description, src.Status,
                src.ManufacturingDate, src.ExpiryDate, ReturnSupplier((int)src.SupplierId)))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => ReturnSupplier((int)src.SupplierId)));

            CreateMap<UpdateProductCommand, Product>()
                .ConstructUsing((src, context) => new Product(src.Id, src.Description, src.Status, src.
                ManufacturingDate, src.ExpiryDate, ReturnSupplier((int)src.SupplierId)))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => ReturnSupplier((int)src.SupplierId)));


            CreateMap<Supplier, SupplierResponse>()
                .ConstructUsing((src, context) => new SupplierResponse(src.Id, src.Description, src.Cnpj, src.Status));

            CreateMap<AddSupplierCommand, Supplier>()
                .ConstructUsing((src, context) => new Supplier(default, src.Description, src.Cnpj, src.Status));

            CreateMap<UpdateSupplierCommand, Supplier>()
                .ConstructUsing((src, context) => new Supplier(src.Id, src.Description, src.Cnpj, src.Status));
            
        }

        private Supplier ReturnSupplier(int? supplierId)
        {
            var supplier = _supplierRepository.GetByIdAsync((int)supplierId).Result;

            return supplier;
        }
    }
}
