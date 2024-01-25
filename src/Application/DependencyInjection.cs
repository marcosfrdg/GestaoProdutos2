using Application.Core.AutoMapper;
using Application.Core.Behaviors;
using AutoMapper;
using Domain.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data.EF;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(IAssemblyReference).Assembly;

            services.AddScoped<DataContext>();          

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(assembly);
                configuration.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
            services.AddFluentValidationAutoValidation();

            // Configurar AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var repository = serviceProvider.GetRequiredService<ISupplierRepository>();

                cfg.AddProfile(new AutoMapperConfig(repository));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
