using System.Threading;
using System.Threading.Tasks;

namespace MediaExpert
{
    /// <summary>
    /// Dystrybutor zapytań.
    /// </summary>
    public interface QueryDispatcher
    {
        /// <summary>
        /// Wyślij zapytanie.
        /// </summary>
        /// <typeparam name="TResponse">Typ odpowiedzi.</typeparam>
        /// <param name="query">Zapytanie <see cref="Query{TResponse}"/>.</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
        /// <returns>Zwraca zadanie z odpowiedzią <see cref="Task{TResponse}"/>.</returns>
        Task<TResponse> SendAsync<TResponse>(Query<TResponse> query, CancellationToken cancellationToken = default);
    }
}
