namespace MediaExpert.Abstractions.Application.Queries
{

    /// <summary>
    /// Query zlicające wszystkie produkty
    /// </summary>
    public class CountProducts : Query<CountProductsResponse>
    {

    }

    public class CountProductsResponse
    {
        public CountProductsResponse(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; set; }
    }
}
