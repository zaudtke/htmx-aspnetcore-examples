using Htmx.Examples.Features.Contacts;
using Htmx.Examples.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.ClickToEdit;

[Route("examples/click-to-edit")]
public class ClickToEditController : Controller
{
    private readonly ContactService _contactService;

    public ClickToEditController(ContactService service)
    {
        _contactService = service;
    }

    [HttpGet, Route("")]
    public async Task<IActionResult> Index()
    {
        var contact = await _contactService.GetContactById(1);
        return Request.IsHtmx()
            ? PartialView("_ContactCard", contact)
            : View(contact);
    }

    [HttpGet, Route("/{id:int}/Edit")]
    public async Task<IActionResult> EditContact(int id)
    {
        var contact = await _contactService.GetContactById(id);
        return PartialView("_ContactForm", contact);
    }

    [HttpPost, Route("/{id:int}/Edit")]
    public async Task<IActionResult> EditContact(int id, Contact contact)
    {
        var updated = await _contactService.UpdateContact(contact);
        return RedirectToAction(nameof(Index));
    }
}
