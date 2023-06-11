using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.ActiveSearch;

[Route("examples/active-search")]
public class ActiveSearchController : Controller
{

	private readonly IMediator _mediator;

	public ActiveSearchController(IMediator mediator) => _mediator = mediator;

	[HttpGet, Route("")]
	public async Task<IActionResult> Index()
	{
		var vm = await _mediator.Send(new SearchVillains.Command(string.Empty));
		return View(vm);
	}

	[HttpPost, Route("")]
	public async Task<IActionResult> Index(string search)
	{
		var vm = await _mediator.Send(new SearchVillains.Command(search));
		return PartialView("_SearchResults", vm);
	}
}
