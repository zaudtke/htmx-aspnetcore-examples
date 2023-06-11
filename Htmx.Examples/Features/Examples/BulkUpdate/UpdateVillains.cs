using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Htmx.Examples.Domain.Villains;
using MediatR;

namespace Htmx.Examples.Features.Examples.BulkUpdate;

public class UpdateVillains
{
	public record Command(int[] Ids, string Status) : IRequest<int[]>;

	public class CommandHandler : IRequestHandler<Command, int[]>
	{
		private readonly VillainService _villainService;

		public CommandHandler(VillainService service) => _villainService = service;

		public async Task<int[]> Handle(Command request, CancellationToken cancellationToken)
		{
			var updatedIds = new List<int>();
			foreach (var id in request.Ids)
			{
				var villain = await _villainService.GetById(id);
				if (villain is not null)
				{
					villain.Status = request.Status;
					if (await _villainService.Update(villain))
					{
						updatedIds.Add(villain.Id);
					}
				}
			}

			return updatedIds.ToArray();
		}
	}
}
