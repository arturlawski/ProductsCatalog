namespace MediaExpert.Abstractions.Application.Commands
{
    /// <summary>
    /// Odpowiedź dla polecenia utworzenia produktu
    /// </summary>`
    /// <remarks>
    /// Inicjalizuje nową instancję <see cref="CreateProductResponse"/>
    /// </remarks>
    /// <param name="id">Identyfikator produktu.</param>
    /// <param name="name">nazwa produktu.</param>
    /// <param name="code">kod produktu.</param>
    /// <param name="Price">cena produktu.</param>
    public class CreateProductResponse(
        Guid id,
        string name,
        string code,
        decimal price
    ) : CreateProductDataResponseBase(id: id){
        public string Name { get; set; } = name;
        public string Code { get; set; } = code;
        public decimal Price { get; set; } = price;
    }
}
