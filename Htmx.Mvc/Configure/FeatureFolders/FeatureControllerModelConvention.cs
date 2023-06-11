
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Htmx.Mvc.Configure.FeatureFolders;

// Based on outdated NuGet Package
// https://github.com/OdeToCode/AddFeatureFolders/blob/276670117271f603169f9362c34ae2e722666baa/src/OdeToCode.AddFeatureFolders/FeatureControllerModelConvention.cs
public class FeatureControllerModelConvention : IControllerModelConvention
{
	private const string FeaturesFolderName = "Features";

	public void Apply(ControllerModel model)
	{
		if (model == null) throw new ArgumentNullException(nameof(model));

		var featureName = DeriveFeatureFolderName(model);
		model.Properties.Add("feature", featureName);
	}


	private string DeriveFeatureFolderName(ControllerModel model)
	{
		var @namespace = model.ControllerType.Namespace;
		var result = @namespace?.Split('.')
			.SkipWhile(s => s != FeaturesFolderName)
			.Aggregate("", Path.Combine);
		return result ?? string.Empty;
	}
}
