using MediatR;

namespace Htmx.Mvc.Features.Examples.InfiniteScroll;

public class ViewVillains
{
	public record Query(int Page = 1) : IRequest<Result>;

	public record Result(IEnumerable<Villain> Villains, string LoadMoreUrl = "");

	public record Villain(int Id, string Name);

	public class QueryHandler : IRequestHandler<Query, Result>
	{
		public Task<Result> Handle(Query request, CancellationToken cancellationToken)
		{
			var start = ((request.Page - 1) * 20) + 1;
			var list = Enumerable.Range(start, 20).Select(x => new Villain(x, $"Villain {x}"));
			return Task.FromResult(new Result(list, $"/examples/infinite-scroll/?page={request.Page + 1}"));
		}
	}
}
