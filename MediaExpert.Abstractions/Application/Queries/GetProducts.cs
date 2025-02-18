namespace MediaExpert.Abstractions.Application.Queries
{

    /// <summary>
    /// Query pobierające produkty z paginacją
    /// </summary>
    public class GetProducts : Query<GetProductsResponse>
    {
        public int StartIndex { get; init; }
        public int Limit { get; init; }

        public GetProducts(int startIndex, int limit)
        {
            StartIndex = startIndex;
            Limit = limit;
        }
    }

    /// <summary>
    /// Odpowiedź dla żądania pobrania produktów
    /// </summary>
    public class GetProductsResponse
    {
        public GetProductsResponse()
        {
            Products = new List<ProductResponse>();
        }

        public List<ProductResponse> Products { get; set; }
    }

    public class ProductResponse
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
    }
}
