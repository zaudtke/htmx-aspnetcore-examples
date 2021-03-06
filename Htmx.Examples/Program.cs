
using Htmx.Examples.Configure;
using Microsoft.AspNetCore.Mvc.Razor;
using Htmx.Examples.Configure.FeatureFolders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Htmx.Examples.Configure.LocalDatabase;
using Htmx.Examples.Domain.Data;
using Htmx.Examples.Domain.Villains;
using JetBrains.Annotations;
using MediatR;



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


