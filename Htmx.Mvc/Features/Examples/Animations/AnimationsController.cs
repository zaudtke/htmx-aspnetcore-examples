﻿using Microsoft.AspNetCore.Mvc;

namespace Htmx.Mvc.Features.Examples.Animations;

[Route("examples/animations")]
public class AnimationsController : Controller
{
	[HttpGet, Route("")]
	public IActionResult Index() => View();

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
	public IActionResult Delete() => Ok("");

	[HttpPost, Route("fadein")]
	public IActionResult FadeIn() => PartialView("_FadeInButton", "From Server");
}
