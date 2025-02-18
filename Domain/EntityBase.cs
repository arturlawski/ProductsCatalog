using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaExpert
{
    /// <summary>
    /// Bazowa encja
    /// </summary>
    public abstract class EntityBase : Entity
    {
        private IList<DomainEvent> _domainEvents;

        /// <inheritdoc/>
        public DomainEvent[] DomainEvents => _domainEvents != null
            ? _domainEvents.ToArray()
            : Array.Empty<DomainEvent>();

        /// <summary>
        /// Dodaj zdarzenie domenowe
        /// </summary>
        /// <param name="domainEvent">Zdarzenie domenowe</param>
        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Usuń zdarzenie domenowe
        /// </summary>
        /// <param name="domainEvent">Zdarzenie domenowe</param>
        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
    }
}
