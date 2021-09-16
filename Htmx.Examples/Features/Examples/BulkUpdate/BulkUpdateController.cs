using Htmx.Examples.Features.Contacts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Htmx.Examples.Models;

namespace Htmx.Examples.Features.Examples.BulkUpdate
{
    [Route("examples/bulk-update")]
    public class BulkUpdateController : Controller
    {

        private readonly ContactService _contactService;

        public BulkUpdateController(ContactService service)
        {
            _contactService = service;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index()
        {
            var changedIds = new List<(int, string)>();

            if(TempData.ContainsKey("activatedIds"))
            {
                var list = TempData["activatedIds"]?.ToString()?.Split(',').Select(i => (int.Parse(i), "activate"));
                if (list is not null)
                    changedIds.AddRange(list);
            }
            if (TempData.ContainsKey("deactivatedIds"))
            {
                var list = TempData["deactivatedIds"]?.ToString()?.Split(',').Select(i => (int.Parse(i), "deactivate"));
                if (list is not null)
                    changedIds.AddRange(list);
            }

            var contacts = await _contactService.GetContacts();
            var vm = BuildViewModel(contacts, changedIds);
            
            return Request.IsHtmx()
            ? PartialView("_ContactRows", vm.Contacts)
            : View(vm);
        }

        [HttpPost, Route("Activate")]
        public async Task<IActionResult> Activate(int[] ids)
        {
            foreach(var id in ids)
            {
                var contact = await _contactService.GetContactById(id);
                if (contact is not null)
                {
                    contact.Active = true;
                    await _contactService.UpdateContact(contact);
                }
            }
            TempData["activatedIds"] = string.Join(',', ids);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, Route("Deactivate")]
        public async Task<IActionResult> Deactivate(int[] ids)
        {
            var updatedIds = ids.Select(i => (i, "activate")).ToList();
            foreach (var id in ids)
            {
                var contact = await _contactService.GetContactById(id);
                if (contact is not null)
                {
                    contact.Active = false;
                    await _contactService.UpdateContact(contact);
                }
            }
            TempData["deactivatedIds"] = string.Join(',', ids);
            return RedirectToAction(nameof(Index));
        }

        private BulkUpdateViewModel BuildViewModel(IEnumerable<Contact> contacts, List<(int ContactId, string CssClass)> statusChangedCss)
        {
            var vm = new BulkUpdateViewModel
            {
                Contacts = contacts.Select(c => new ContactListItem
                (
                    c.Id,
                    string.Concat(c.FirstName, " ", c.LastName),
                    c.Email,
                    c.Active ? "Active" : "Inactive",
                    statusChangedCss.FirstOrDefault(t => c.Id == t.ContactId).CssClass ?? string.Empty
                ))
            };

            return vm;
        }
    }
}
