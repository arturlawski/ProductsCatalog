using MediatR;
using Microsoft.EntityFrameworkCore;
using MediaExpert.Domain.Repositories;

namespace MediaExpert.Targets
{
	/// <summary>
	/// Zachowanie dla przepływu polecenia z obszaru obsługi produktów.
	/// </summary>
	/// <typeparam name="TCommand">Typ polecenia.</typeparam>
	/// <typeparam name="TResponse">Typ odpowiedzi.</typeparam>
	public class ProductCommandPipelineBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
		where TCommand : IRequest<TResponse>
	{
		private readonly IProductUnitOfWork _unitOfWork;
		private readonly DomainEventDishpatcher _domainEventDispatcher;

        public ProductCommandPipelineBehavior(
            IProductUnitOfWork unitOfWork,
			DomainEventDishpatcher domainEventDispatcher)
		{
			_unitOfWork = unitOfWork;
			_domainEventDispatcher = domainEventDispatcher;
		}

		/// <summary>
		/// Obsługuje zachowanie.
		/// </summary>
		/// <param name="command">Polecenie.</param>
		/// <param name="next">Delegat do kolejengo wywołania w przepływie <see cref="RequestHandlerDelegate{TResponse}"/>.</param>
		/// <param name="cancellationToken">Token anulowania <see cref="CancellationToken"/>.</param>
		/// <returns>Zwraca zadanie z odpowiedzią <see cref="Task{TResponse}"/>.</returns>
		public async Task<TResponse> Handle(TCommand command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var response = await next();

			if (command is Command<TResponse>)
			{
				var domainEvents = _unitOfWork.GetChangedEntities<Entity>()
					.SelectMany(a => a.DomainEvents)
					.ToList();

				foreach (var domainEvent in domainEvents)
				{
					await _domainEventDispatcher.PublishAsync(domainEvent, cancellationToken);
				}

				var changes = ((DbContext)_unitOfWork).ChangeTracker.Entries().Select(x => x.Entity).ToList();
				await _unitOfWork.SaveChangesAsync(cancellationToken);
			}

			return response;
		}
	}
}
