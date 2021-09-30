using Htmx.Examples.Domain.Villains;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.ClickToEdit;

public class EditVillain
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

    public record Command(Villain Villain) : IRequest<bool>;

    public class CommandHandler : IRequestHandler<Command, bool>
    {
        private readonly VillainService _villainService;

        public CommandHandler(VillainService service) => _villainService = service;

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = await _villainService.GetById(request.Villain.Id);
            entity.Name = request.Villain.Name;
            entity.Movie = request.Villain.Movie;
            entity.Status = request.Villain.Status;

            return await _villainService.Update(entity);
        }
    }

}
