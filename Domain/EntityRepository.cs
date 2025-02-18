using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MediaExpert
{
    /// <summary>
    /// Repozytorium encji
    /// </summary>
    /// <typeparam name="TEntity">Typ encji.</typeparam>
    public interface EntityRepository<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Dodaj encję do repozytorium
        /// </summary>
        /// <param name="entity">Encja</param>
        /// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/></param>
        ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Przeszukaj encje
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="select"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="cancellationToken"></param>
        Task<IEnumerable<TEntity>> BrowseAsync(Expression<Func<TEntity, bool>> predicate, int startIndex, int limit, bool asNoTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Zlicz wszystkie encje
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);
    }
}
