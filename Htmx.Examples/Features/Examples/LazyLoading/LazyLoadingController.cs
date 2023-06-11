using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.LazyLoading;

[Route("examples/lazy-load")]
public class LazyLoadingController : Controller
{
	[HttpGet, Route("")]
	public IActionResult Index() => View();

	[HttpGet, Route("image")]
	public async Task<IActionResult> Image()
	{
		await Task.Delay(TimeSpan.FromSeconds(1));
		return PartialView("_Image");
	}
}
