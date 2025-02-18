using Microsoft.EntityFrameworkCore;

namespace MediaExpert
{
    /// <summary>
    /// Bazowa implementacja kontekstu do bazy danych.
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        private readonly ExtendedModelCreating _extendedModelCreating;

        /// <summary>
        /// Wspomaga inicjalizację nowej instacji kontekstu do bazy danych.
        /// </summary>
        /// <param name="extendedModelCreating"></param>
        /// <param name="options"></param>
        protected DbContextBase(
            ExtendedModelCreating extendedModelCreating,
            DbContextOptions options)
            : base(options)
        {
            _extendedModelCreating = extendedModelCreating;
        }

        /// <inheritdoc/>
        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            _extendedModelCreating(modelBuilder);
        }
    }
}
