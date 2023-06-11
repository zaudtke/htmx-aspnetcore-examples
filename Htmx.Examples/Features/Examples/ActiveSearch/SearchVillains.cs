using Htmx.Examples.Domain.Villains;
using MediatR;

namespace Htmx.Examples.Features.Examples.ActiveSearch;

public class SearchVillains
{
	public record Command(string Search) : IRequest<Result>;

	public record Result(IEnumerable<Villain> Villains);

	public record Villain(string Name, string Movie, string Status);

	public class CommandHandler : IRequestHandler<Command, Result>
	{
		private readonly VillainService _service;

		public CommandHandler(VillainService service) => _service = service;

		public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
		{
			var results = await _service.SearchByName(request.Search);
			return new Result(results.Select(v => new Villain(v.Name, v.Movie, v.Status)).ToList());
		}
	}
}
