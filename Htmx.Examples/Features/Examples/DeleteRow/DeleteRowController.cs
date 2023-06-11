using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.DeleteRow;

[Route("examples/delete-row")]
public class DeleteRowController : Controller
{

	private readonly IMediator _mediator;

	public DeleteRowController(IMediator mediator) => _mediator = mediator;


	[HttpGet, Route("")]
	public async Task<IActionResult> Index()
	{
		var result = await _mediator.Send(new ViewVillains.Query());
		return View(result);
	}

	[HttpPost, Route("{id:int}")]
	public async Task<IActionResult> Index(int id)
	{
		var result = await _mediator.Send(new DeleteVillain.Command(id));
		return new EmptyResult();
	}
}
