namespace MediaExpert.Abstractions.Application.Commands
{
	/// <summary>
	/// Bazowa odpowiedź dla polecenia utworzenia produktu
	/// </summary>
	public abstract class CreateProductDataResponseBase
	{
		/// <summary>
		/// Wspomaga inicjowanie nowej instacji potomnej odpowiedzi.
		/// </summary>
		/// <param name="id">Identyfikator.</param>
		protected CreateProductDataResponseBase(
			Guid id)
		{
			Id = id;
		}

		/// <summary>
		/// Identyfikator
		/// </summary>
		public Guid Id { get; }
	}
}
