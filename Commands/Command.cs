namespace MediaExpert
{
    using MediatR;

    /// <summary>
    /// Polecenie
    /// </summary>
    public interface Command : IRequest
    {

    }

    /// <summary>
    /// Polecenie, które zwraca odpowiedź
    /// </summary>
    public interface Command<TResponse> : IRequest<TResponse>
    {

    }
}
