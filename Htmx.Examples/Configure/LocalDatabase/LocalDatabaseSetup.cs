using Htmx.Examples.Domain.Data;
using Htmx.Examples.Domain.Villains;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Htmx.Examples.Configure.LocalDatabase
{
    public static class LocalDatabaseSetup
    {
        public static Task EnsureDb(IServiceProvider provider)
        {
            var db = provider.CreateScope().ServiceProvider.GetRequiredService<LiteDbContext>();
            var collection = db.Database.GetCollection<Villain>("Villains");
            var count = collection.Count();

            if (count == 0)
            {
                collection.Insert(new Villain { Name = "Michael Meyers", Movie = "Halloween", Status = "Alive" });
                collection.Insert(new Villain { Name = "Jason Voorhees", Movie = "Friday the 13th", Status = "Dead" });
                collection.Insert(new Villain { Name = "Jigsaw", Movie = "Saw", Status = "Unknown" });
                collection.Insert(new Villain { Name = "Captain Spaulding", Movie = "House of 1000 Corpses", Status = "Dead" });
                collection.Insert(new Villain { Name = "Norman Bates", Movie = "Psycho", Status = "Dead" });
                collection.Insert(new Villain { Name = "Freddy Krueger", Movie = "Nightmare on Elm Street", Status = "Dead" });
                collection.Insert(new Villain { Name = "Leatherface", Movie = "Texas Chainsaw Masacre", Status = "Dead" });
                collection.Insert(new Villain { Name = "Chucky", Movie = "Child's Play", Status = "Alive" });
                collection.Insert(new Villain { Name = "Ghostface", Movie = "Scream", Status = "Unknown" });
                collection.Insert(new Villain { Name = "Jack Torrance", Movie = "The Shining", Status = "Dead" });
                collection.Insert(new Villain { Name = "Damien", Movie = "The Omen", Status = "Unknown" });
                collection.Insert(new Villain { Name = "Pennywise", Movie = "It", Status = "Dead" });
                collection.Insert(new Villain { Name = "Hannibal Lecter", Movie = "Silence of the Lambs", Status = "Dead" });
            }
            return Task.CompletedTask;
        }
    }
}
