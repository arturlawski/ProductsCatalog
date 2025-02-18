using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediaExpert
{
    /// <summary>
    /// Bazowy uchwyt do obsługi zdarzenia domenowego
    /// </summary>
    /// <typeparam name="TDomainEvent">Typ zdarzenia</typeparam>
    public abstract class DomainEventHandlerBase<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        /// <summary>
        /// Obsłuż zadanie domenowe
        /// </summary>
        /// <param name="domainEvent">Zadanie domenowe</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/></param>
        /// <returns>Zwraca zadanie <see cref="Task"/></returns>
        protected abstract Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken);

        Task INotificationHandler<TDomainEvent>.Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            return HandleAsync(domainEvent: notification, cancellationToken: cancellationToken);
        }
    }
}
