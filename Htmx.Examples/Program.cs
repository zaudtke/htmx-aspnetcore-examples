
using Htmx.Examples.Configure;
using Htmx.Examples.Features.Data;
using Htmx.Examples.Features.Contacts;
using Htmx.Examples.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("LiteDbOptions"));
builder.Services.AddSingleton<LiteDbContext>();
builder.Services.AddTransient<ContactService>();
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


app.MapControllers();

app.Run();


Task EnsureDb(IServiceProvider provider)
{
    var db = provider.CreateScope().ServiceProvider.GetRequiredService<LiteDbContext>();
    var collection = db.Database.GetCollection<Contact>("Contacts");
    var count = collection.Count();

    if(count == 0)
    {
        collection.Insert(new Contact { FirstName = "Allen", LastName = "Zaudtke", Email = "zaudtke@gmail.com" });
        collection.Insert(new Contact { FirstName = "Michael", LastName = "Meyers", Email = "knife@halloween.com" });
        collection.Insert(new Contact { FirstName = "Jason", LastName = "Voorhees", Email = "hockeymask@friday13th.com" });
        collection.Insert(new Contact { FirstName = "John", LastName = "Kramer", Email = "jigsaw@saw.com" });
    }
    return Task.CompletedTask;
}