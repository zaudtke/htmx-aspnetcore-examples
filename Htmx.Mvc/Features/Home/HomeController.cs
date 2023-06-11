using Microsoft.AspNetCore.Mvc;

namespace Htmx.Mvc.Features.Home;
public class HomeController : Controller
{

	public HomeController()
	{
	}

	[HttpGet, Route("")]
	public IActionResult Index() => View();

}
