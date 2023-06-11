
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Mvc.Features.Examples.CascadingSelect;

[Route("examples/cascading-select")]
public class CascadingSelectController : Controller
{
	private readonly IMediator _mediator;

	public CascadingSelectController(IMediator mediator) => _mediator = mediator;

	[HttpGet, Route("")]
	public async Task<IActionResult> Index()
	{
		var query = new ViewMoviesBySeries.Query();
		var result = await _mediator.Send(query);
		return View(result);
	}

	[HttpGet, Route("movies")]
	public async Task<IActionResult> Movies(string series)
	{
		var query = new ViewMoviesBySeries.Query(series);
		var result = await _mediator.Send(query);
		return PartialView("_MovieOptions", result.Movies);
	}
}
