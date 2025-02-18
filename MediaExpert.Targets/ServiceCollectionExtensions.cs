using MediaExpert.Abstractions.Application.Commands;
using MediaExpert.Domain.Products;
using MediaExpert.Targets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Rozszerzenie rejestrujące zależności komponentów związanych z produktami
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Dodaj zależności komponentów związanych z produktami
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których zależności mają być zarejestrowane.</param>
        /// <param name="configuration">Konfiguracja <see cref="IConfiguration"/>.</param>
        /// <param name="hostEnvironment">Informacje o środowisku uruchomieniowym <see cref="IHostEnvironment"/>.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddMediaExpertProducts(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (hostEnvironment is null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }
            var domainAssembly = typeof(CreateProduct).Assembly;
            var applicationAssembly = typeof(ProductBase).Assembly;
            services
                .AddToMediatRScan(domainAssembly)
                .AddToMediatRScan(applicationAssembly)
                .AddProductsApplication(configuration)
                .AddInfrastructure(configuration)
                .AddDomainEventNotificationFactory()
                .AddMediatR(typeof(ProductCommandPipelineBehavior<,>));
            return services;
        }
    }
}
