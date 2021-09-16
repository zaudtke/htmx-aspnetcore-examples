using Htmx.Examples.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

    [HttpGet, Route("privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet, Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
