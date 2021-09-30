using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.InfiniteScroll;

[Route("examples/infinite-scroll")]
public class InfiniteScrollController : Controller
{
    [HttpGet, Route("")]
    public IActionResult Index()
    {
        return View();
    }
}
