using Htmx.Examples.Domain.Data;
using Htmx.Examples.Domain.Villains;

namespace Htmx.Examples.Configure.LocalDatabase;

public static class LocalDatabaseSetup
{
	public static Task EnsureDb(IServiceProvider provider)
	{
		var db = provider.CreateScope().ServiceProvider.GetRequiredService<LiteDbContext>();
		var collection = db.Database.GetCollection<Villain>("Villains");
		var count = collection.Count();

		if (count == 0)
		{
			_ = collection.Insert(new Villain { Name = "Michael Meyers", Movie = "Halloween", Status = "Alive" });
			_ = collection.Insert(new Villain { Name = "Jason Voorhees", Movie = "Friday the 13th", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Jigsaw", Movie = "Saw", Status = "Unknown" });
			_ = collection.Insert(new Villain { Name = "Captain Spaulding", Movie = "House of 1000 Corpses", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Norman Bates", Movie = "Psycho", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Freddy Krueger", Movie = "Nightmare on Elm Street", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Leatherface", Movie = "Texas Chainsaw Masacre", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Chucky", Movie = "Child's Play", Status = "Alive" });
			_ = collection.Insert(new Villain { Name = "Ghostface", Movie = "Scream", Status = "Unknown" });
			_ = collection.Insert(new Villain { Name = "Jack Torrance", Movie = "The Shining", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Damien", Movie = "The Omen", Status = "Unknown" });
			_ = collection.Insert(new Villain { Name = "Pennywise", Movie = "It", Status = "Dead" });
			_ = collection.Insert(new Villain { Name = "Hannibal Lecter", Movie = "Silence of the Lambs", Status = "Dead" });
		}
		return Task.CompletedTask;
	}
}
