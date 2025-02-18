namespace MediaExpert
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Dystrybutor zdarzeń domenowych.
    /// </summary>
    public interface DomainEventDishpatcher
    {
        /// <summary>
        /// Publikuj zdarzenie domenowe.
        /// </summary>
        /// <typeparam name="TDomainEvent">Typ zdarzenie domenowego.</typeparam>
        /// <param name="domainEvent">Zdarzenie domenowe.</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
        /// <returns>Zwraca zadanie <see cref="Task"/>.</returns>
        Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
            where TDomainEvent : DomainEvent;
    }
}
