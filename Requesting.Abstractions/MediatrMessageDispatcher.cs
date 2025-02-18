using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediaExpert
{
    /// <summary>
    /// Dystrybutor wiadomości w implemetacji opartej o rozwiązanie MediatR
    /// </summary>
    public class MediatrMessageDispatcher : DomainEventDishpatcher, RequestDispatcher
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Inicjalizuje nową instancję <see cref="MediatrMessageDispatcher"/>.
        /// </summary>
        /// <param name="mediator">Mediator <see cref="IMediator"/>.</param>
        public MediatrMessageDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        Task DomainEventDishpatcher.PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return _mediator.Publish(notification: domainEvent, cancellationToken: cancellationToken);
        }

        Task CommandDispatcher.SendAsync(Command command, CancellationToken cancellationToken)
        {
            return _mediator.Send(request: command, cancellationToken: cancellationToken);
        }

        Task<TResponse> CommandWithResponseDispatcher.SendAsync<TResponse>(Command<TResponse> command, CancellationToken cancellationToken)
        {
            return _mediator.Send(request: command, cancellationToken: cancellationToken);
        }

        Task<TResponse> QueryDispatcher.SendAsync<TResponse>(Query<TResponse> query, CancellationToken cancellationToken)
        {
            return _mediator.Send(request: query, cancellationToken: cancellationToken);
        }
    }
}
