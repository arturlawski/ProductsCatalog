namespace MediaExpert
{
    /// <summary>
    /// Dystrybutor polecenia, które zwraca odpowiedź
    /// </summary>
    public interface CommandWithResponseDispatcher
    {
        /// <summary>
        /// Wyślij polecenie
        /// </summary>
        /// <typeparam name="TResponse">Typ odpowiedzi.</typeparam>
        /// <param name="command">Polecenie <see cref="Command{TResponse}"/>.</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
        /// <returns>Zwraca zadanie z odpowiedzią <see cref="Task{TResponse}"/>.</returns>
        Task<TResponse> SendAsync<TResponse>(Command<TResponse> command, CancellationToken cancellationToken = default);
    }
}
