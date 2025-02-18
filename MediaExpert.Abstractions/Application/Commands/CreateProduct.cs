namespace MediaExpert.Abstractions.Application.Commands
{
	/// <summary>
	/// Polecenie dodania produktu
	/// </summary>
	public class CreateProduct : IProductCommand<CreateProductResponse>
	{
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
