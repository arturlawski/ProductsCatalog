using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MediaExpert
{
    /// <summary>
    /// Wraper dla nieśledzonych zapytań do bazy danych
    /// </summary>
    /// <typeparam name="TEntity">Typ encji</typeparam>
    internal class DbNoTrackingQuery<TEntity> : IQueryable<TEntity>
        where TEntity : class, Entity
    {
        private readonly IQueryable<TEntity> _dbQuery;

        public DbNoTrackingQuery(DbContext dbContext)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbQuery = dbContext.Set<TEntity>().AsNoTracking();
        }

        public Type ElementType => _dbQuery.ElementType;

        public Expression Expression => _dbQuery.Expression;

        public IQueryProvider Provider => _dbQuery.Provider;

        public IEnumerator<TEntity> GetEnumerator() => _dbQuery.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _dbQuery.GetEnumerator();
    }
}
