using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediaExpert;
using MediaExpert.Domain.Repositories;

namespace MediaExpert.Database
{
    /// <summary>
    /// Kontekst do bazy danych dla produktów.
    /// </summary>
    public sealed class ProductsDbContext : DbContextBase, IProductUnitOfWork
    {
        public ProductsDbContext(ExtendedModelCreating extendedModelCreating, DbContextOptions<ProductsDbContext> options)
            : base(extendedModelCreating, options)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetChangedEntities<TEntity>()
            where TEntity : class, Entity
        {
            return ChangeTracker.Entries<TEntity>().Select(e => e.Entity);
        }
    }
}
