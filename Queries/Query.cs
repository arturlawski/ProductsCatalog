namespace MediaExpert
{
    using MediatR;

    /// <summary>
    /// Zapytanie.
    /// </summary>
    public interface Query<TResponse> : IRequest<TResponse>
    {
    }
}
