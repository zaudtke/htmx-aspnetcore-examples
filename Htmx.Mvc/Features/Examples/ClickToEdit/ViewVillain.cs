using Htmx.Mvc.Domain.Villains;
using MediatR;

namespace Htmx.Mvc.Features.Examples.ClickToEdit;

public class ViewVillain
{
	public record Query(int Id) : IRequest<Result>;

	public record Result(Villain Villain);


	public class QueryHandler : IRequestHandler<Query, Result>
	{
		private readonly VillainService _villainService;

		public QueryHandler(VillainService service) => _villainService = service;

		public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
		{
			var dbVillain = await _villainService.GetById(request.Id);
			return new Result(new Villain(dbVillain.Id, dbVillain.Name, dbVillain.Movie, dbVillain.Status));
		}
	}
}
