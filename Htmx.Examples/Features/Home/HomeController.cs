using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Home;
public class HomeController : Controller
{

    public HomeController()
    {
    }

    [HttpGet, Route("")]
    public IActionResult Index()
    {
        return View();
    }

}
