using Htmx.Examples.Domain.Villains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Htmx.Examples.Features.Examples.BulkUpdate
{
    [Route("examples/bulk-update")]
    public class BulkUpdateController : Controller
    {

        private readonly VillainService _villainService;

        public BulkUpdateController(VillainService service)
        {
            _villainService = service;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index()
        {
            var changedIds = new List<(int, string)>();

            if(TempData.ContainsKey("resurectedIds"))
            {
                var list = TempData["resurectedIds"]?.ToString()?.Split(',').Select(i => (int.Parse(i), "activate"));
                if (list is not null)
                    changedIds.AddRange(list);
            }
            if (TempData.ContainsKey("killedIds"))
            {
                var list = TempData["killedIds"]?.ToString()?.Split(',').Select(i => (int.Parse(i), "deactivate"));
                if (list is not null)
                    changedIds.AddRange(list);
            }

            var villains = await _villainService.GetAll();
            var vm = BuildViewModel(villains, changedIds);
            
            return Request.IsHtmx()
            ? PartialView("_VillainRows", vm.Villains)
            : View(vm);
        }

        [HttpPost, Route("kill")]
        public async Task<IActionResult> Kill(int[] ids)
        {
            foreach(var id in ids)
            {
                var villain = await _villainService.GetById(id);
                if (villain is not null)
                {
                    villain.Status = "Dead";
                    await _villainService.Update(villain);
                }
            }
            TempData["killedIds"] = string.Join(',', ids);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, Route("resurect")]
        public async Task<IActionResult> Resurect(int[] ids)
        {
            var updatedIds = ids.Select(i => (i, "activate")).ToList();
            foreach (var id in ids)
            {
                var villain = await _villainService.GetById(id);
                if (villain is not null)
                {
                    villain.Status = "Alive";
                    await _villainService.Update(villain);
                }
            }
            TempData["resurectedIds"] = string.Join(',', ids);
            return RedirectToAction(nameof(Index));
        }

        private BulkUpdateViewModel BuildViewModel(IEnumerable<Villain> villains, List<(int VillainId, string CssClass)> statusChangedCss)
        {
            var vm = new BulkUpdateViewModel
            {
                Villains = villains.Select(v => new VillainListItem
                (
                    v.Id,
                    v.Name,
                    v.Movie,
                    v.Status,
                    statusChangedCss.FirstOrDefault(t => v.Id == t.VillainId).CssClass ?? string.Empty
                ))
            };

            return vm;
        }
    }
}
