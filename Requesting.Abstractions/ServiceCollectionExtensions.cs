using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediaExpert;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Rozszerzenia dodające mechanizm mediatora wykorzystując rozwiązanie "MediatR".
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private readonly static ConcurrentDictionary<IServiceCollection, IList<Assembly>> _assebliesToScan
            = new ConcurrentDictionary<IServiceCollection, IList<Assembly>>();

        /// <summary>
        /// Dodaj wołające "Assembly" do mechanizmu skanowania rozwiązania "MediatR".
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddToMediatRScan(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var assemblyToScan = Assembly.GetCallingAssembly();

            return services.AddToMediatRScan(assemblyToScan);
        }

        /// <summary>
        /// Dodaj "Assembly" do mechanizmu skanowania rozwiązania "MediatR".
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których przkazane "Assembly" ma być zarejestrowane.</param>
        /// <param name="assemblyToScan">"Assembly" do przeskanowania.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddToMediatRScan(this IServiceCollection services, Assembly assemblyToScan)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assemblyToScan is null)
            {
                throw new ArgumentNullException(nameof(assemblyToScan));
            }

            if (MediatRAlreadyRegistered(services))
            {
                throw new InvalidOperationException("Sorry, but it seems like MediatR has already been configured in this service collection. Add assembly to scan before MediatR registration.");
            }

            _assebliesToScan.AddOrUpdate(
                services,
                _ => new List<Assembly>
                {
                    assemblyToScan
                },
                (_, assemblies) =>
                {
                    assemblies.Add(assemblyToScan);
                    return assemblies.Distinct().ToList();
                });

            return services;
        }

        /// <summary>
        /// Dodaj mechanizm mediatora wykorzystując rozwiązanie "MediatR".
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <param name="commandPipelineBehaviorType">Typ zachowania dla przepływu polecenia.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddMediatR(
            this IServiceCollection services,
            Type commandPipelineBehaviorType = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (MediatRAlreadyRegistered(services))
            {
                throw new InvalidOperationException("Sorry, but it seems like MediatR has already been configured in this service collection.");
            }

            var assemblyToScan = Assembly.GetCallingAssembly();

            services.AddToMediatRScan(assemblyToScan);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(_assebliesToScan[services].ToArray()));

            services.AddTransient<DomainEventDishpatcher, MediatrMessageDispatcher>();
            services.AddTransient<RequestDispatcher, MediatrMessageDispatcher>();

            services.Scan(scan => scan
                .FromAssemblies(_assebliesToScan[services])
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                        .UsingRegistrationStrategy(new AppendAssignableRequestsStrategy())
                        .AsImplementedInterfaces()
                        .WithTransientLifetime());
            

            if (commandPipelineBehaviorType != null)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), commandPipelineBehaviorType);
            }

            return services;
        }

        /// <summary>
        /// Dodaj mechanizm śledzenia rozwiązania "MediatR".
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddMediatRTracing(
            this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestTracingPipelineBehavior<,>));

            services.AddTransient(typeof(INotificationHandler<>), typeof(DomainEventTracingHandler<>));

            return services;
        }

        private static bool MediatRAlreadyRegistered(IServiceCollection services)
        {
            return services.Any(descriptor => descriptor.ServiceType == typeof(IMediator));
        }
    }
}
