namespace MediaExpert
{
    /// <summary>
    /// Marker dla wzorca "Unit of Work"
    /// </summary>
    public interface UnitOfWork : IDisposable
    {
        /// <summary>
        /// Zwróc zmienione encje
        /// </summary>
        /// <typeparam name="TEntity">Typ encji</typeparam>
        /// <returns>Zwraca kolekcję zmienionych encji <see cref="IEnumerable{TEntity}"/></returns>
        IEnumerable<TEntity> GetChangedEntities<TEntity>()
            where TEntity : class, Entity;

        /// <summary>
        /// Zapisz zamiany
        /// </summary>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/></param>
        /// <returns>Zwraca zadanie z liczbą zapisanych encji <see cref="Task{T}"/></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
