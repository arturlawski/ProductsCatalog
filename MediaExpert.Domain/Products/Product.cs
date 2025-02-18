namespace MediaExpert.Domain.Products
{
    public class Product : ProductBase
    {
        public Product(string name, string code, decimal price)
            : base(name, code, price)
        {
        }
    }
}
