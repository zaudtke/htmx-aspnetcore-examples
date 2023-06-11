using Microsoft.AspNetCore.Mvc;

namespace Htmx.Examples.Features.Examples.ProgressBar;

[Route("examples/progress-bar")]
public class ProgressBarController : Controller
{
	[HttpGet, Route("")]
	public IActionResult Index() => View();

	[HttpPost, Route("start")]
	public IActionResult Start() =>
		// Start some long running process
		RedirectToAction(nameof(Status), new { current = -1 });

	[HttpGet, Route("status/{current:int}")]
	public IActionResult Status(int current)
	{
		var next = GetNextStatus(current);
		ProgressBarModel model;

		if (next >= 100)
		{
			model = new ProgressBarModel(next, "Complete", "none");
		}
		else
		{
			model = new ProgressBarModel(next, "Running", "load delay:600ms");
		}

		return PartialView("_ProgressBar", model);
	}

	private int GetNextStatus(int current) => current switch
	{
		-1 => 0,
		0 => 25,
		25 => 29,
		29 => 55,
		55 => 66,
		66 => 85,
		85 => 98,
		98 => 100,
		_ => 0 // make compiler happy, cover all possibilities
	};
}
