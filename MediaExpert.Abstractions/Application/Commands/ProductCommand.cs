using MediatR;

namespace MediaExpert.Abstractions.Application.Commands
{
	/// <summary>
	/// Polecenie w obsarze obsługi produktów.
	/// </summary>
	public interface IProductCommand : Command, IProductCommand<Unit>
	{
	}

	/// <summary>
	/// Polecenie, które zwraca odpowiedź w obsarze obsługi produktów.
	/// </summary>
	public interface IProductCommand<TResponse> : Command<TResponse>
	{
	}
}
