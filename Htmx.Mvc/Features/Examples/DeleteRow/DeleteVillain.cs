using Htmx.Mvc.Domain.Villains;
using MediatR;

namespace Htmx.Mvc.Features.Examples.DeleteRow;

public class DeleteVillain
{
	public record Command(int Id) : IRequest<bool>;

	public class CommandHandler : IRequestHandler<Command, bool>
	{
		private readonly VillainService _villainService;

		public CommandHandler(VillainService service) => _villainService = service;

		public async Task<bool> Handle(Command request, CancellationToken cancellationToken) => await _villainService.Delete(request.Id);
	}
}
