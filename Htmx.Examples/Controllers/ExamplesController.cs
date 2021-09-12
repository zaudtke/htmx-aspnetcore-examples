﻿using Htmx.Examples.Features.Contacts;
using Htmx.Examples.Models;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Controllers;

[Route("[controller]")]
public class ExamplesController : Controller
{
    private readonly ContactService _contactService;

    public ExamplesController(ContactService service)
    {
        _contactService = service;
    }

    [HttpGet, Route("click-to-edit")]
    public async Task<IActionResult> ClickToEdit()
    {
        var contact = await _contactService.GetContactById(1);
        return Request.IsHtmx()
            ? PartialView("_ViewContact", contact)
            : View(contact);
    }

    [HttpGet, Route("click-to-edit/{id:int}/Edit")]
    public async Task<IActionResult> EditContact(int id)
    {
        var contact = await _contactService.GetContactById(id);
        return PartialView("_EditContact", contact);
    }

    [HttpPost, Route("click-to-edit/{id:int}/Edit")]
    public async Task<IActionResult> EditContact(int id, Contact contact)
    {
        var updated = await _contactService.UpdateContact(contact);
        return PartialView("_ViewContact", contact);
    }
}
