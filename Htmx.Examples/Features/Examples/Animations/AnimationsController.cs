using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.Animations;

[Route("examples/animations")]
public class AnimationsController : Controller
{
    [HttpGet, Route("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet, Route("colors")]
    public IActionResult Colors(string current = "red")
    {
        var color = current switch
        {
            "red" => "blue",
            "blue" => "green",
            _ => "red"
        };
        return PartialView("_ColorThrob", color);
    }
}
