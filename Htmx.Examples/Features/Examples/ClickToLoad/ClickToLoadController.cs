using Htmx.Examples.Domain.Villains;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Htmx.Examples.Features.Examples.ClickToLoad
{
    [Route("examples/click-to-load")]
    public class ClickToLoadController : Controller
    {
        private readonly VillainService _villainService;

        public ClickToLoadController(VillainService service)
        {
            _villainService = service;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index(int page = 1)
        {
            // PageSize is 3
            var villains = await _villainService.GetAll();
            var paged = villains.Skip((page - 1) * 3).Take(3).ToList();
            var viewModel = new ClickToLoadViewModel
            {
                Villains = paged,
                LoadMoreUrl = ( paged.Count < 3 ? string.Empty : $"/examples/click-to-load/?page={page + 1}")
            };
            return Request.IsHtmx()
                ? PartialView("_VillainRows", viewModel)
                : View(viewModel);
        }
    }
}
