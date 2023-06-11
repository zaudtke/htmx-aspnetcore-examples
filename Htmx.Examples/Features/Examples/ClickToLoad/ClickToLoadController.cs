using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.ClickToLoad;

[Route("examples/click-to-load")]
public class ClickToLoadController : Controller
{
	private readonly IMediator _mediator;

	public ClickToLoadController(IMediator mediator) => _mediator = mediator;

	[HttpGet, Route("")]
	public async Task<IActionResult> Index(int page = 1)
	{
		var result = await _mediator.Send(new ViewVillains.Query(page));
		return Request.IsHtmx()
			? PartialView("_VillainRows", result)
			: View(result);
	}
}
