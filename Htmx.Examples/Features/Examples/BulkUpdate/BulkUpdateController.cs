using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.BulkUpdate;

[Route("examples/bulk-update")]
public class BulkUpdateController : Controller
{
	private readonly IMediator _mediator;

	public BulkUpdateController(IMediator mediator) => _mediator = mediator;

	[HttpGet, Route("")]
	public async Task<IActionResult> Index()
	{
		var changedIds = new List<(int, string)>();

		if (TempData.ContainsKey("resurectedIds"))
		{
			var list = TempData["resurectedIds"]?.ToString()?.Split(',').Select(i => (int.Parse(i), "activate"));
			if (list is not null)
				changedIds.AddRange(list);
		}
		if (TempData.ContainsKey("killedIds"))
		{
			var list = TempData["killedIds"]?.ToString()?.Split(',').Select(i => (int.Parse(i), "deactivate"));
			if (list is not null)
				changedIds.AddRange(list);
		}

		var vm = await _mediator.Send(new ViewVillains.Query { ChangedVillains = changedIds });

		return Request.IsHtmx()
		? PartialView("_VillainRows", vm.Villains)
		: View(vm);
	}

	[HttpPost, Route("kill")]
	public async Task<IActionResult> Kill(int[] ids)
	{
		var result = await _mediator.Send(new UpdateVillains.Command(ids, "Dead"));
		TempData["killedIds"] = string.Join(',', result);
		return RedirectToAction(nameof(Index));
	}

	[HttpPost, Route("resurect")]
	public async Task<IActionResult> Resurect(int[] ids)
	{
		var result = await _mediator.Send(new UpdateVillains.Command(ids, "Alive"));
		TempData["resurectedIds"] = string.Join(',', result);
		return RedirectToAction(nameof(Index));
	}
}
