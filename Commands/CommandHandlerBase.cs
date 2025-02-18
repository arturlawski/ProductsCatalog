using MediatR;
namespace MediaExpert
{

    /// <summary>
    /// Message handler interface. Implement this in order to get to handle messages of a specific type
    /// </summary>
    public interface IHandleMessages<in TMessage>
    {
        /// <summary>
        /// This method will be invoked with a message of type <typeparamref name="TMessage"/>
        /// </summary>
        Task Handle(TMessage message);
    }
    /// <summary>
    /// Bazowy uchwyt do obsługi polecenia
    /// </summary>
    /// <typeparam name="TCommand">Typ polecenia.</typeparam>
    public abstract class CommandHandlerBase<TCommand> : IRequestHandler<TCommand>, IHandleMessages<TCommand>
        where TCommand : Command
    {
        /// <summary>
        /// Obsłuż polecenie
        /// </summary>
        /// <param name="command">Polecenie</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/></param>
        /// <returns>Zwraca zadanie <see cref="Task"/></returns>
        protected abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);

        Task IRequestHandler<TCommand>.Handle(TCommand request, CancellationToken cancellationToken)
            => HandleAsync(command: request, cancellationToken: cancellationToken);

        Task IHandleMessages<TCommand>.Handle(TCommand message)
            => HandleAsync(command: message, CancellationToken.None);
    }

    /// <summary>
    /// Bazowy uchwyt do obsługi polecenia, które zwraca odpowiedź
    /// </summary>
    /// <typeparam name="TCommand">Typ polecenia</typeparam>
    /// <typeparam name="TResponse">Typ odpowiedzi</typeparam>
    public abstract class CommandHandlerBase<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : Command<TResponse>
    {
        /// <summary>
        /// Obsłuż polecenie
        /// </summary>
        /// <param name="command">Polecenie</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/></param>
        /// <returns>Zwraca zadanie z odpowiedzią <see cref="Task{TResponse}"/></returns>
        protected abstract Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken);

        Task<TResponse> IRequestHandler<TCommand, TResponse>.Handle(TCommand request, CancellationToken cancellationToken)
            => HandleAsync(command: request, cancellationToken: cancellationToken);
    }
}
