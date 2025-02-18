using MediaExpert.Domain.Products;
using MediaExpert.Application.Queries;
using MediatR;
using MediaExpert.Abstractions.Application.Queries;
using MediaExpert.Abstractions.Application.Commands;
using Microsoft.Extensions.Configuration;
using MediaExpert;
using MediaExpert.Application.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Rozszerzenie rejestrujące zależności warstwy aplikacyjnej 
	/// </summary>
	public static class ServiceCollectionExtensions
	{
        public static IServiceCollection AddProductsApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IRequestHandler<GetProducts, GetProductsResponse>, GetProductsHandler>();
			services.AddTransient<IRequestHandler<CountProducts, CountProductsResponse>, CountProductsHandler>();
            services.AddTransient<IRequestHandler<CreateProduct, CreateProductResponse>, CreateProductHandler>();
            services.AddToDomainEventNotificationFactory();
            return services;
		}
	}
}
