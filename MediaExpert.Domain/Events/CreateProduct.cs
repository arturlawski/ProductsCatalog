namespace MediaExpert.Domain.Events
{
    public class CreateProduct : DomainEvent
    {
        /// <summary>
        /// Inicjalizuje nową instancję <see cref="CreateProduct"/>.
        /// </summary>
        /// <param name="id">Id produktu.</param>
        public CreateProduct(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Identyfikator produktu.
        /// </summary>
        public Guid Id { get; }

    }
}
