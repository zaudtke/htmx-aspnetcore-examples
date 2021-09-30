using Htmx.Examples.Domain.Villains;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.ClickToLoad;

public class ViewVillains
{
    public record Query(int Page = 1) : IRequest<Result>;

    public record Result(IEnumerable<Villain> Villains, string LoadMoreUrl = "");

    public record Villain(int Id, string Name, string Movie, string Status);

    public class QueryHandler : IRequestHandler<Query, Result>
    {
        private readonly VillainService _villainService;

        public QueryHandler(VillainService service) => _villainService = service;

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            // PageSize is 3
            var villains = await _villainService.GetAll();
            var paged = villains.Skip((request.Page - 1) * 3).Take(3).Select(v => new Villain(v.Id, v.Name, v.Movie, v.Status)).ToList();
            return new Result(paged, paged.Count < 3 ? string.Empty : $"/examples/click-to-load/?page={request.Page + 1}");
        }
    }
}
