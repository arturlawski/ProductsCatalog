using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediaExpert;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Rozszerzenia dodające fabrykę powiadomień o zdarzeniach domenowych.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private readonly static ConcurrentDictionary<IServiceCollection, IList<Assembly>> _assebliesToScan
            = new ConcurrentDictionary<IServiceCollection, IList<Assembly>>();

        private static readonly Type _domainEventNotificatioGenericType = typeof(DomainEventNotification<>);

        /// <summary>
        /// Dodaj wołające "Assembly" do fabryki powiadomień o zdarzeniach domenowych.
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddToDomainEventNotificationFactory(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var assemblyToScan = Assembly.GetCallingAssembly();

            return services.AddToDomainEventNotificationFactory(assemblyToScan);
        }

        /// <summary>
        /// Dodaj "Assembly" do fabryki powiadomień o zdarzeniach domenowych.
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których przkazane "Assembly" ma być zarejestrowane.</param>
        /// <param name="assemblyToScan">"Assembly" do przeskanowania.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddToDomainEventNotificationFactory(this IServiceCollection services, Assembly assemblyToScan)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assemblyToScan is null)
            {
                throw new ArgumentNullException(nameof(assemblyToScan));
            }

            if (DomainEventNotificationFactoryAlreadyRegistered(services))
            {
                throw new InvalidOperationException("Sorry, but it seems like DomainEventNotificationFactory has already been configured in this service collection. Add assembly to scan before DomainEventNotificationFactory registration.");
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
        /// Dodaj mechanizm mapowanie obiektów wykorzystując rozwiązanie "AutoMapper".
        /// </summary>
        /// <param name="services">Kolekcja usług, dla których wołające "Assembly" ma być zarejestrowane.</param>
        /// <returns>Zwraca kolekcję zarejestrowanych usług.</returns>
        public static IServiceCollection AddDomainEventNotificationFactory(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (DomainEventNotificationFactoryAlreadyRegistered(services))
            {
                throw new InvalidOperationException("Sorry, but it seems like DomainEventNotificationFactory has already been configured in this service collection.");
            }

            var assemblyToScan = Assembly.GetCallingAssembly();

            services.AddToDomainEventNotificationFactory(assemblyToScan);

            var domainEventNotificationsMap = GetDomainEventNotificationsMap(_assebliesToScan[services]);

            services.AddTransient(sp => new DomainEventNotificationFactory(domainEventNotificationsMap, sp));            

            return services;
        }

        private static IDictionary<Type, Type> GetDomainEventNotificationsMap(IList<Assembly> assemblies)
        {
            var map = new Dictionary<Type, Type>();

            foreach (var assembly in assemblies)
            {
                var domainEventNotificationTypes= assembly.ExportedTypes
                    .Where(t => t.IsClass && t.GetInterfaces().Any(i => DomainEventNotificationType(i)));

                foreach (var domainEventNotificationType in domainEventNotificationTypes)
                {
                    var domainEventTypes = domainEventNotificationType.GetInterfaces()
                        .Where(i => DomainEventNotificationType(i))
                        .Select(i => i.GenericTypeArguments[0]);

                    foreach (var domainEventType in domainEventTypes)
                    {
                        map.Add(domainEventType, domainEventNotificationType);
                    }
                }
            }

            return map;
        }

        private static bool DomainEventNotificationType(Type serviceType)
        {
            return serviceType.IsInterface
                && serviceType.IsGenericType
                && _domainEventNotificatioGenericType.IsAssignableFrom(serviceType.GetGenericTypeDefinition());
        }

        private static bool DomainEventNotificationFactoryAlreadyRegistered(IServiceCollection services)
        {
            return services.Any(descriptor => descriptor.ServiceType == typeof(DomainEventNotificationFactory));
        }
    }
}
