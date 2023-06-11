using Htmx.Mvc.Configure;
using Htmx.Mvc.Configure.FeatureFolders;
using Htmx.Mvc.Configure.LocalDatabase;
using Htmx.Mvc.Domain.Data;
using Htmx.Mvc.Domain.Villains;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

// Disable warnings for Expression Value is never used
// Discard Characters in this file make it "ugly"
#pragma warning disable IDE0058

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("LiteDbOptions"));
builder.Services.AddSingleton<LiteDbContext>();
builder.Services.AddTransient<VillainService>();
builder.Services.AddControllersWithViews();
// Customize ViewLocation for Feature Folders
// Based on older NuGet Package https://github.com/OdeToCode/AddFeatureFolders
builder.Services.Configure<MvcOptions>(o =>
{
	o.Conventions.Add(new FeatureControllerModelConvention());
});
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
	options.ViewLocationFormats.Clear();
	options.ViewLocationFormats.Add("/{Feature}/{0}.cshtml"); // default: /{Feature}/{0}.cshtml
	options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml"); // default: /Features/Shared/{0}.cshtml
	options.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml"); // default: /Features/{0}/{1}.cshtml
	options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
});
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

await LocalDatabaseSetup.EnsureDb(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

	app.UseDeveloperExceptionPage();

}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapControllers();

app.Run();

// Enable warnings for Expression Value is never used
#pragma warning restore IDE0058
