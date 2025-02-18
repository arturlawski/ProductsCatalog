using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediaExpert
{
    /// <summary>
    /// Fabryka powiadomień o zdarzeniach domenowych.
    /// </summary>
    public class DomainEventNotificationFactory
    {
        private readonly static ConcurrentDictionary<Type, ConstructorInfo> _domainEventNotificationConstructors
            = new ConcurrentDictionary<Type, ConstructorInfo>();

        private readonly IDictionary<Type, Type> _domainEventNotificationsMap;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Inicjazlizuje nową instację <see cref="DomainEventNotificationFactory"/>.
        /// </summary>
        public DomainEventNotificationFactory(
            IDictionary<Type, Type> domainEventNotificationsMap,
            IServiceProvider serviceProvider)
        {
            _domainEventNotificationsMap = domainEventNotificationsMap ?? throw new ArgumentNullException(nameof(domainEventNotificationsMap));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Spróbuj utworzyć.
        /// </summary>
        /// <param name="domainEvent">Zdarzenie domenowe.</param>
        /// <param name="domainEventNotification">Zdarzenie powiadomienie dla zdarzeia domenowego.</param>
        /// <returns>Zwraca <c>true</c> jeśli istnieje powiadomienie dla zdarzenia domenowego, inaczej <c>false</c>.</returns>
        public bool TryCreate(DomainEvent domainEvent, out EventNotification domainEventNotification)
        {
            if (_domainEventNotificationsMap.TryGetValue(domainEvent.GetType(), out var domainEventNotificationType))
            {
                domainEventNotification = (EventNotification)Create(_serviceProvider, domainEventNotificationType, domainEvent);
                return true;
            }

            domainEventNotification = null;
            return false;
        }

        private static object Create(IServiceProvider provider, Type domainEventNotificationType, DomainEvent domainEvent)
        {
            var constructor = _domainEventNotificationConstructors.GetOrAdd(domainEventNotificationType, type => type
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(c => c.GetParameters().Any(p => p.ParameterType == domainEvent.GetType()))
                .Single());

            var parameters = constructor.GetParameters()
                .Select(p => p.ParameterType == domainEvent.GetType()
                    ? domainEvent
                    : provider.GetService(p.ParameterType))
                .ToArray();

            return constructor.Invoke(parameters);
        }
    }
}
