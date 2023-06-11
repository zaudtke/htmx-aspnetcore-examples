using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.InlineValidation;

[Route("examples/inline-validation")]
public class InlineValidationController : Controller
{
	private readonly IMediator _mediator;

	public InlineValidationController(IMediator mediator) => _mediator = mediator;

	[HttpGet, Route("")]
	public IActionResult Index()
	{
		var vm = new CreateVillain.Command("", "", "Alive");
		if (Request.IsHtmx())
		{
			return PartialView("_CreateVillainForm", vm);
		}
		else
		{
			return View(vm);
		}

	}

	[HttpPost, Route("")]
	public async Task<IActionResult> Index(CreateVillain.Command command)
	{
		var result = await _mediator.Send(new DuplicateNameCheck.Command(command.Name));
		if (result)
		{
			ModelState.AddModelError("Name", "Name already exists.");
		}
		if (ModelState.IsValid)
		{
			var id = await _mediator.Send(command);
			return RedirectToAction(nameof(Index));
		}

		return PartialView("_CreateVillainForm", command);
	}

	[HttpPost, Route("namecheck")]
	public async Task<IActionResult> DuplicateNameCheck(CreateVillain.Command command)
	{
		// Don't check Model state.  WIll Fail due to other required fields
		if (!string.IsNullOrWhiteSpace(command.Name))
		{
			var result = await _mediator.Send(new DuplicateNameCheck.Command(command.Name));
			if (result)
			{
				ModelState.AddModelError("Name", "Name already exists.");
			}
		}

		return PartialView("_VillainNameCheck", command);
	}
}
