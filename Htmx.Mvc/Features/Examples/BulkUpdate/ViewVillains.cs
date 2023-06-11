using Htmx.Mvc.Domain.Villains;
using MediatR;

namespace Htmx.Mvc.Features.Examples.BulkUpdate;

public class ViewVillains
{
	public record Query : IRequest<Result>
	{
		public IEnumerable<(int VillainId, string CssClass)> ChangedVillains { get; init; } = new List<(int VillainId, string CssClass)>();
	}

	public record Result
	{
		public IEnumerable<Villain> Villains { get; init; } = new List<Villain>();
	}

	public record Villain(int Id, string Name, string Movie, string Status, string StatusChangedClass);


	public class QueryHandler : IRequestHandler<Query, Result>
	{
		private readonly VillainService _villainService;


		public QueryHandler(VillainService service) => _villainService = service;

		public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
		{
			var villains = await _villainService.GetAll();

			return new Result
			{
				Villains = villains.Select(v => new Villain
				(
				   v.Id,
				   v.Name,
				   v.Movie,
				   v.Status,
				   request.ChangedVillains.FirstOrDefault(t => v.Id == t.VillainId).CssClass ?? string.Empty
				))
			};
		}
	}
}
