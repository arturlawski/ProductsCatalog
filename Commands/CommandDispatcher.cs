namespace MediaExpert
{
    /// <summary>
    /// Dystrybutor polecenia
    /// </summary>
    public interface CommandDispatcher
    {
        /// <summary>
        /// Wyślij polecenie
        /// </summary>
        /// <param name="command">Polecenie <see cref="Command"/>.</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
        /// <returns>Zwraca zadanie <see cref="Task"/>.</returns>
        Task SendAsync(Command command, CancellationToken cancellationToken = default);
    }
}
