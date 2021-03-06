using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.CascadingSelect;

public class ViewMoviesBySeries
{
    public record Query(string Series = "Marvel") : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<string> Series, IEnumerable<string> Movies);

    public class QueryHandler : IRequestHandler<Query, QueryResult>
    {
        public Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new QueryResult(GetCollections(), GetMovies(request.Series)));
        }

        private List<string> GetCollections()
        {
            return new List<string>
            {
                "Marvel",
                "StarWars"
            };
        }

        private List<string> GetMovies(string genre)
        {
            return genre switch
            {
                "Marvel" => new List<string> { "Iron Man", "Thor", "Dr. Strange" },
                "StarWars" => new List<string> { "A New Hope", "The Empire Strikes Back", "Return of the Jedi" },
                _ => new List<string>()
            };
        }
    }

}