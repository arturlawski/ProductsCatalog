using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MediaExpert
{
    /// <summary>
    /// Obługa śledzenia zdarzeń domenowych.
    /// </summary>
    /// <typeparam name="TDomainEvent">Typ zdarznia.</typeparam>
    public class DomainEventTracingHandler<TDomainEvent> : DomainEventHandlerBase<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        private readonly ILogger<DomainEventTracingHandler<TDomainEvent>> _logger;

        /// <summary>
        /// Inicjalizuje nową instację <see cref="DomainEventTracingHandler{TDomainEvent}"/>.
        /// </summary>
        public DomainEventTracingHandler(
            ILogger<DomainEventTracingHandler<TDomainEvent>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Obsługa.
        /// </summary>
        /// <param name="domainEvent">Zdarzenie domenowe.</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        protected override Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return Task.Run(() => _logger.LogTrace(message: "Domain event {@DomainEvent} published.", domainEvent));
        }
    }
}
