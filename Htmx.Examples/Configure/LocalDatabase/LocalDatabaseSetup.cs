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
                collection.Insert(new Villain { Name = "Michael Meyers", Movie = "Halloween", Dead = false });
                collection.Insert(new Villain { Name = "Jason Voorhees", Movie = "Friday the 13th", Dead = true });
                collection.Insert(new Villain { Name = "Jigsaw", Movie = "Saw", Dead = false });
                collection.Insert(new Villain { Name = "Captain Spaulding", Movie = "House of 1000 Corpses", Dead = true });
                collection.Insert(new Villain { Name = "Norman Bates", Movie = "Psycho", Dead = true });
                collection.Insert(new Villain { Name = "Freddy Krueger", Movie = "Nightmare on Elm Street", Dead = true });
                collection.Insert(new Villain { Name = "Leatherface", Movie = "Texas Chainsaw Masacre", Dead = true });
                collection.Insert(new Villain { Name = "Chucky", Movie = "Child's Play", Dead = false });
                collection.Insert(new Villain { Name = "Ghostface", Movie = "Scream", Dead = false });
                collection.Insert(new Villain { Name = "Jack Torrance", Movie = "The Shining", Dead = true });
                collection.Insert(new Villain { Name = "Damien", Movie = "The Omen", Dead = true });
                collection.Insert(new Villain { Name = "Pennywise", Movie = "It", Dead = false });
                collection.Insert(new Villain { Name = "Hannibal Lecter", Movie = "Silence of the Lambs", Dead = false });
            }
            return Task.CompletedTask;
        }
    }
}
