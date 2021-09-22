using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.ClickToEdit;

[Route("examples/click-to-edit")]
public class ClickToEditController : Controller
{
    private readonly IMediator _mediator;

    public ClickToEditController(IMediator mmediator) => _mediator = mmediator;

    [HttpGet, Route("")]
    public async Task<IActionResult> Index()
    {
        var villain = await _mediator.Send(new ViewVillain.Query(1));
        return Request.IsHtmx()
            ? PartialView("_VillainCard", villain)
            : View(villain);
    }

    [HttpGet, Route("/{id:int}/Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _mediator.Send(new EditVillain.Query(id));
        return PartialView("_VillainForm", result);
    }

    [HttpPost, Route("/{id:int}/Edit")]
    public async Task<IActionResult> Edit(int id, Villain villain)
    {
        var result = await _mediator.Send(new EditVillain.Command(villain));
        return RedirectToAction(nameof(Index));
    }
}
