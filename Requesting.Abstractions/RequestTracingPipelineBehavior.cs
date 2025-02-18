using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediaExpert
{
    /// <summary>
    /// Zachowanie dla śledzenia przepływu żądań.
    /// </summary>
    /// <typeparam name="TRequest">Typ żądania.</typeparam>
    /// <typeparam name="TResponse">Typ odpowiedzi.</typeparam>
    public class RequestTracingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestTracingPipelineBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Inicjalizuje nową instację <see cref="RequestTracingPipelineBehavior{TRequest, TResponse}"/>.
        /// </summary>
        public RequestTracingPipelineBehavior(
            ILogger<RequestTracingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obsługuje zachowanie.
        /// </summary>
        /// <param name="request">Żądanie.</param>
        /// <param name="next">Delegat do kolejengo wywołania w przepływie <see cref="RequestHandlerDelegate{TResponse}"/>.</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
        /// <returns>Zwraca zadanie z odpowiedzią <see cref="Task{TResponse}"/>.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopWatch = Stopwatch.StartNew();

            TResponse response = default;
            try
            {
                response = await next();
            }
            finally
            {
                stopWatch.Stop();

                var requestType = request switch
                {
                    Command<TResponse> _ => "Command",
                    Query<TResponse> _ => "Query",
                    _ => "Request"
                };

                _logger.LogTrace(message: "{@RequestType} {@Request} returns {@Response} in {ElapsedMilliseconds}ms", requestType, request, response, stopWatch.ElapsedMilliseconds);
            }

            return response;
        }
    }
}
