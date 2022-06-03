using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

    [HttpDelete, Route("delete")]
    public IActionResult Delete()
    {
        return Ok("");
    }

    [HttpPost, Route("fadein")]
    public IActionResult FadeIn()
    {
        return PartialView("_FadeInButton", "From Server");
    }
}
