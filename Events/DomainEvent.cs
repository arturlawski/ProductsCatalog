using MediatR;

namespace MediaExpert
{
    /// <summary>
    /// Zdarzenie domenowe
    /// </summary>
    public interface DomainEvent : INotification
    {
    }
}
