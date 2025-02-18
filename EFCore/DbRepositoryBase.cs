using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MediaExpert
{
    /// <summary>
    /// Bazowe repozytoriu encji w bazie danych operte o rozwiązanie Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">Typ encji.</typeparam>
    public abstract class DbRepositoryBase<TEntity> : EntityRepository<TEntity>
        where TEntity : class, Entity
    {
        private readonly Lazy<DbSet<TEntity>> _dbSetLazy;

        /// <summary>
        /// Wspomaga inicjowanie nowej instacji potomnego repozytorium.
        /// </summary>
        /// <param name="dbContext">Kontekst do bazy danych <see cref="DbContext"/>.</param>
        protected DbRepositoryBase(DbContext dbContext)
        {
            _dbSetLazy = new Lazy<DbSet<TEntity>>(() => dbContext.Set<TEntity>());
        }

        protected DbSet<TEntity> Set => _dbSetLazy.Value;

        public async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entry = await Set.AddAsync(entity: entity, cancellationToken: cancellationToken);

            return entry.Entity;
        }
      
        public virtual async Task<IEnumerable<TEntity>> BrowseAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, bool asNoTracking = false, CancellationToken cancellationToken = default)
            => await (asNoTracking ? Set.AsNoTracking() : Set).Where(predicate).Skip(skip).Take(take).ToListAsync(cancellationToken);

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
            => await (asNoTracking ? Set.AsNoTracking() : Set).CountAsync(predicate, cancellationToken);
    }
}
