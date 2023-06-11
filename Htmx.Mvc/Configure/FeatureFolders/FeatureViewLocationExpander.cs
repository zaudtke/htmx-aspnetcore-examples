using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Htmx.Mvc.Configure.FeatureFolders;

// Based on outdated NuGet Package
// https://github.com/OdeToCode/AddFeatureFolders/blob/276670117271f603169f9362c34ae2e722666baa/src/OdeToCode.AddFeatureFolders/FeatureControllerModelConvention.cs
public class FeatureViewLocationExpander : IViewLocationExpander
{
	private const string Placeholder = "{Feature}";

	public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
	{
		if (context == null)
			throw new ArgumentNullException(nameof(context));

		if (viewLocations == null)
			throw new ArgumentNullException(nameof(viewLocations));

		var controllerDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
		var featureName = controllerDescriptor?.Properties["feature"] as string;

		foreach (var location in viewLocations)
		{
			yield return location.Replace(Placeholder, featureName);
		}
	}

	public void PopulateValues(ViewLocationExpanderContext context) => context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
}
