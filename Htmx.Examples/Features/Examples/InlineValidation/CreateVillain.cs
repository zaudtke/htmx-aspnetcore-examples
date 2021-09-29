using Htmx.Examples.Domain.Villains;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.InlineValidation
{
    public class CreateVillain
    {
        // Required is implicit for strings due to being a Record.  Could also use [Required] if explicit is desired
        public record Command(string Name, string Movie, string Status) : IRequest<int>;

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly VillainService _service;

            public CommandHandler(VillainService service) => _service = service;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Villain
                {
                    Name = request.Name,
                    Movie = request.Movie,
                    Status = request.Status
                };
                return await _service.Add(entity);
            }
        }
    }
}
