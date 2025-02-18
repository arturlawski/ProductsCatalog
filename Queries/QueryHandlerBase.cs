using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediaExpert
{
    /// <summary>
    /// Bazowy uchwyt do obsługi zapytania
    /// </summary>
    /// <typeparam name="TQuery">Typ zapytania</typeparam>
    /// <typeparam name="TResponse">Typ odpowiedzi</typeparam>
    public abstract class QueryHandlerBase<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : Query<TResponse>
    {
        /// <summary>
        /// Obsłuż zapytanie
        /// </summary>
        /// <param name="query">Zapytanie</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/></param>
        /// <returns>Zwraca zadanie z odpowiedzią <see cref="Task{TResponse}"/></returns>
        protected abstract Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken);

        Task<TResponse> IRequestHandler<TQuery, TResponse>.Handle(TQuery request, CancellationToken cancellationToken)
        {
            return HandleAsync(query: request, cancellationToken: cancellationToken);
        }
    }
}
