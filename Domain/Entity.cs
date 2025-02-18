namespace MediaExpert
{
    /// <summary>
    /// Encja
    /// </summary>
    public interface Entity
    {
        /// <summary>
        /// Zdarzenia domenowe wygenerowane przez agregat
        /// </summary>
        DomainEvent[] DomainEvents { get; }
    }
}
