using Htmx.Examples.Domain.Villains;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.DeleteRow
{

    [Route("examples/delete-row")]
    public class DeleteRowController : Controller
    {

        private readonly VillainService _villainService;

        public DeleteRowController(VillainService villainService)
        {
            _villainService = villainService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index()
        {
            var villains = await _villainService.GetAll();
            return View(villains);
        }

        [HttpPost, Route("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            await _villainService.Delete(id);
            return new EmptyResult();
        }
    }
}
