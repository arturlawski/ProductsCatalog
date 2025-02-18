using System.Reflection;
using MediaExpert.Database;
using MediaExpert.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MediaExpert;

namespace Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Rozszerzenie rejestrujące zależności infrastruktury związanej z bazą danych
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Dodaj Entity Framework Core
		/// </summary>
		public static IServiceCollection AddDatabase(
		   this IServiceCollection services,
		   IConfiguration configuration,
		   Assembly? applicationAssembly = null)
		{
			services.AddEntityFrameworkCore<ProductsDbContext, IProductUnitOfWork>(configuration, applicationAssembly);
            services.AddTransient<IProductsRepository, ProductsDbRepository>();

            return services;
		}
	}
}
