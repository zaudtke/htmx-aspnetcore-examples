using Htmx.Examples.Domain.Data;
using LiteDB;

namespace Htmx.Examples.Domain.Villains;

public class VillainService
{
	private readonly LiteDatabase _liteDatabase;
	private const string VillainCollectionName = "Villains";

	public VillainService(LiteDbContext context)
	{
		_liteDatabase = context.Database;
	}

	public Task<IEnumerable<Villain>> GetAll() => Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).FindAll());

	public Task<Villain> GetById(int id)
	{
		var dbVillain = _liteDatabase.GetCollection<Villain>(VillainCollectionName).Find(x => x.Id == id).FirstOrDefault();

		if (dbVillain is null) throw new Exception($"Can't find Villain {id} in database");

		return Task.FromResult(dbVillain);
	}

	public Task<int> Add(Villain villain) => Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Insert(villain).AsInt32);

	public Task<bool> Update(Villain villain) => Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Update(villain));

	public Task<bool> Delete(int id) => Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Delete(new BsonValue(id)));

	public async Task<IEnumerable<Villain>> SearchByName(string search)
	{
		var collection = _liteDatabase.GetCollection<Villain>(VillainCollectionName);
		_ = collection.EnsureIndex("Name");
		var list = new List<Villain>();
		if (string.IsNullOrEmpty(search))
		{
			var all = await GetAll();
			list = all.ToList();
		}
		else
		{
			list = collection.Find(v => v.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).ToList();
		}

		return list!.FindAll(v => v.Name.Contains(search ?? "", StringComparison.InvariantCultureIgnoreCase));
	}
}
