using MediaExpert.Abstractions.Application.Commands;
using MediaExpert.Domain.Products;
using MediaExpert.Domain.Repositories;

namespace MediaExpert.Application.Commands
{

    internal class CreateProductHandler : CommandHandlerBase<CreateProduct, CreateProductResponse>
    {
        private readonly IProductsRepository _productsRepository;

        public CreateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
        }

        protected override async Task<CreateProductResponse> HandleAsync(CreateProduct command, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.AddAsync(new Product(command.Name, command.Code, command.Price), cancellationToken);
            product.AddNew(product.Id);
            return new CreateProductResponse(product.Id,product.Name,product.Code,product.Price);
        }
    }
}

