using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htmx.Examples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Htmx.Examples.Pages.Examples;

public class ClickToEditModel : PageModel
{
    [BindProperty]
    public Contact Data { get; set; }
    
    public ClickToEditModel()
    {
        // Initialize to satisfy nullable
        Data = new Contact();
    }

    public void OnGet()
    {
        // Load Data
        // partial set using Tag Helper
        Data = new Contact { Id = 1, FirstName = "Al", LastName = "Zaudtke", Email = "zaudtke@gmail.com" };
    }

    public async Task<IActionResult> OnGetEditForm(int id)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        // Data Lost, reload - Not sure if I like that???
        // Leads me towards idea of View Component
        // Could use normal OnGet and check
        // Request.IsHtmx() to return Page or Partial after loading
        // Here, that is ok, but what about pages with multiple "components"
        var contact = new Contact { Id = 1, FirstName = "Al", LastName = "Zaudtke", Email = "zaudtke@gmail.com" };
        return Partial("Partials/_EditContact", contact);
    }

    public async Task<IActionResult> OnPostSaveForm()
    {
        // Uses Model Binding, so Page Level "Data" is bound

        await Task.Delay(TimeSpan.FromSeconds(1));
        return Partial("Partials/_ViewContact", Data);
    }
}
