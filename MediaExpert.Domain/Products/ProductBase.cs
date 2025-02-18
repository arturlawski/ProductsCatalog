using System.ComponentModel.DataAnnotations;
using MediaExpert;
using MediaExpert.Domain.Events;

namespace MediaExpert.Domain.Products
{
    /// <summary>
    /// Klasa reprezentująca produkt.
    /// </summary>
    public abstract class ProductBase : AggregateRootBase
    {
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <param name="price"></param>
        public ProductBase(string name, string code, decimal price)
        {
            Name = name;
            Price = price;
            Code = code;
        }

        /// <summary>
        /// Identyfikator produktu.
        /// </summary>
        [Required(ErrorMessage = "Identyfikator jest wymagany.")]
        public Guid Id { get; private set; }


        /// <summary>
        /// kod produktu
        /// </summary>
        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Nazwa produktu musi mieć dokładnie 7 znaków.")]
        //[RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{7}$", ErrorMessage = "Nazwa produktu musi zawierać zarówno litery, jak i cyfry.")]
        public string Code { get; private set; }

        /// <summary>
        /// Nazwa produktu
        /// </summary>
        [Required]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Nazwa produktu musi mieć od 3 do 300 znaków.")]
        public string Name { get; private set; }

        /// <summary>
        /// Cena produktu
        /// </summary>
        [Range(0.01, 10000.00, ErrorMessage = "Cena musi wynosić od 0.01 do 250,000.")]
        [DataType(DataType.Currency, ErrorMessage = "Cena musi być wartością numeryczną.")]
        public decimal Price { get; private set; }

        /// <summary>
        /// Wsyła zdazenie o utworzeniu produktu
        /// </summary>
        /// <param name="name">Nowa nazwa produktu.</param>
        public void AddNew(Guid code)
        {
            AddDomainEvent(new CreateProduct(code));
        }
    }
}
