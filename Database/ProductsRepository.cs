using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediaExpert.Domain.Repositories;
using MediaExpert.Domain.Products;
using MediaExpert;
using System.Linq;

namespace MediaExpert.Database
{
    internal sealed class ProductsDbRepository : DbRepositoryBase<Product>, IProductsRepository
    {
        public ProductsDbRepository(ProductsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
