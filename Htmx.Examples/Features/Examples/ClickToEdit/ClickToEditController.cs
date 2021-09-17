using Htmx.Examples.Domain.Villains;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.ClickToEdit;

[Route("examples/click-to-edit")]
public class ClickToEditController : Controller
{
    private readonly VillainService _villainService;

    public ClickToEditController(VillainService service)
    {
        _villainService = service;
    }

    [HttpGet, Route("")]
    public async Task<IActionResult> Index()
    {
        var villain = await _villainService.GetById(1);
        return Request.IsHtmx()
            ? PartialView("_VillainCard", villain)
            : View(villain);
    }

    [HttpGet, Route("/{id:int}/Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var contact = await _villainService.GetById(id);
        return PartialView("_VillainForm", contact);
    }

    [HttpPost, Route("/{id:int}/Edit")]
    public async Task<IActionResult> Edit(int id, Villain contact)
    {
        var updated = await _villainService.Update(contact);
        return RedirectToAction(nameof(Index));
    }
}
