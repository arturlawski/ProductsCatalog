using MediaExpert.Domain.Products;
namespace MediaExpert.Domain.Repositories
{
	/// <summary>
	/// Repozytorium produktów.
	/// </summary>
	public interface IProductsRepository : EntityRepository<Product>
    {
    }
}
