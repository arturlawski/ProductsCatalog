using MediaExpert.Abstractions.Application.Queries;
using MediaExpert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaExpert.Domain.Repositories;

namespace MediaExpert.Application.Queries
{
    public class CountProductsHandler : QueryHandlerBase<CountProducts, CountProductsResponse>
    {
        private readonly IProductsRepository _productsRepository;

        public CountProductsHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        }

        protected async override Task<CountProductsResponse> HandleAsync(CountProducts query, CancellationToken cancellationToken)
        {
            var count = await _productsRepository.CountAsync(p => true, cancellationToken: cancellationToken);
            return new CountProductsResponse(count);
        }
    }
}
