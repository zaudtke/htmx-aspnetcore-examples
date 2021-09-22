using Htmx.Examples.Domain.Villains;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.DeleteRow
{
    public class DeleteVillain
    {
        public record Command(int Id) : IRequest<bool>;

        public class CommandHandler : IRequestHandler<Command, bool>
        {
            private readonly VillainService _villainService;

            public CommandHandler(VillainService service) => _villainService = service;

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _villainService.Delete(request.Id);
            }
        }
    }
}
