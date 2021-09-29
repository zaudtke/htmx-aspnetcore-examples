using Htmx.Examples.Domain.Villains;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.InlineValidation
{
    public class DuplicateNameCheck
    {
        public record Command(string Name) : IRequest<bool>;

        public class CommandHandler : IRequestHandler<Command, bool>
        {
            private readonly VillainService _service;

            public CommandHandler(VillainService service) => _service = service;

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var villains = await _service.GetAll();

                return villains.Any(v => v.Name.Equals(request.Name, System.StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}
