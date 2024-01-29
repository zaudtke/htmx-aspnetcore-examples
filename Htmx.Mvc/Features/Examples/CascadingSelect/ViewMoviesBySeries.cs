using MediatR;

namespace Htmx.Mvc.Features.Examples.CascadingSelect;

public class ViewMoviesBySeries
{
	public record Query(string Series = "Marvel") : IRequest<QueryResult>;

	public record QueryResult(IEnumerable<string> Series, IEnumerable<string> Movies);

	public class QueryHandler : IRequestHandler<Query, QueryResult>
	{
		public Task<QueryResult> Handle(Query request, CancellationToken cancellationToken) => Task.FromResult(new QueryResult(GetCollections(), GetMovies(request.Series)));

		private List<string> GetCollections() =>
		[
			"Marvel",
			"StarWars"
		];

		private List<string> GetMovies(string genre) => genre switch
		{
			"Marvel" => ["Iron Man", "Thor", "Dr. Strange"],
			"StarWars" => ["A New Hope", "The Empire Strikes Back", "Return of the Jedi"],
			_ => []
		};
	}

}
