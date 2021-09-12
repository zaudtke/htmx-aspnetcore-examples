
using Htmx.Examples.Configure;
using Htmx.Examples.Data;
using Htmx.Examples.Features.Contacts;
using Htmx.Examples.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("LiteDbOptions"));
builder.Services.AddSingleton<LiteDbContext>();
builder.Services.AddTransient<ContactService>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


var app = builder.Build();

await EnsureDb(app.Services);

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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();


Task EnsureDb(IServiceProvider provider)
{
    var db = provider.CreateScope().ServiceProvider.GetRequiredService<LiteDbContext>();

    var count = db.Database.GetCollection<Contact>("Contacts").Count();

    if(count == 0)
    {
        db.Database.GetCollection<Contact>("Contacts").Insert(new Contact { FirstName = "Allen", LastName = "Zaudtke", Email = "zaudtke@gmail.com" });
    }
    return Task.CompletedTask;
}