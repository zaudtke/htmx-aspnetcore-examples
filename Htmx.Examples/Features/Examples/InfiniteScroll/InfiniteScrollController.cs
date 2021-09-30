using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.InfiniteScroll;

[Route("examples/infinite-scroll")]
public class InfiniteScrollController : Controller
{
    private readonly IMediator _mediator;

    public InfiniteScrollController(IMediator mediator) => _mediator = mediator;


    [HttpGet, Route("")]
    public async Task<IActionResult> Index(int page = 1)
    {
        var result = await _mediator.Send(new ViewVillains.Query(page));
        return Request.IsHtmx()
            ? PartialView("_Rows", result)
            : View(result);
    }
}
