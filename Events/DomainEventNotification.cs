namespace MediaExpert
{
    /// <summary>
    /// Powiadomienie o zdarzeniu domenowym.
    /// </summary>
    public interface DomainEventNotification<in TDomainEvent> : EventNotification
        where TDomainEvent : DomainEvent
    {
    }
}
