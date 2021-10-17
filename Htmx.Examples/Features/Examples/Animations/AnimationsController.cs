using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.Animations;

[Route("examples/animations")]
public class AnimationsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
