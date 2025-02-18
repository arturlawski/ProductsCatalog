using MediaExpert.Abstractions.Application.Queries;
using MediaExpert.Domain.Repositories;

namespace MediaExpert.Application.Queries
{
    public class GetProductsHandler : QueryHandlerBase<GetProducts, GetProductsResponse>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductsHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        }

        protected async override Task<GetProductsResponse> HandleAsync(GetProducts query, CancellationToken cancellationToken)
        {
            var products = await _productsRepository.BrowseAsync(
                predicate: p => true,
                startIndex: query.StartIndex,
                limit: query.Limit,
                asNoTracking: false,
                cancellationToken: cancellationToken
            );

            return new GetProductsResponse
            {
                Products = products.Select(p => new ProductResponse { Name = p.Name, Id = p.Id, Code = p.Code, Price = p.Price }).ToList()
            };

        }
    }
}
