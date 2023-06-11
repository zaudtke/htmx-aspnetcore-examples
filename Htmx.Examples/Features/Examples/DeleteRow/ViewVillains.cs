using Htmx.Examples.Domain.Villains;
using MediatR;

namespace Htmx.Examples.Features.Examples.DeleteRow;

public class ViewVillains
{
	public record Query() : IRequest<Result>;

	public record Result(IEnumerable<Villain> Villains);

	public record Villain(int Id, string Name, string Movie, string Status);

	public class QueryHandler : IRequestHandler<Query, Result>
	{
		private readonly VillainService _villainService;

		public QueryHandler(VillainService service) => _villainService = service;

		public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
		{
			var villains = await _villainService.GetAll();
			return new Result(villains.Select(v => new Villain(v.Id, v.Name, v.Movie, v.Status)));
		}
	}
}
