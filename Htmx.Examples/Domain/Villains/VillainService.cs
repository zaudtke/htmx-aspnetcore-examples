using Htmx.Examples.Domain.Data;
using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Htmx.Examples.Domain.Villains
{
    public class VillainService
    {
        private readonly LiteDatabase _liteDatabase;
        private const string VillainCollectionName = "Villains";

        public VillainService(LiteDbContext context)
        {
            _liteDatabase = context.Database;
        }

        public Task<IEnumerable<Villain>> GetAll()
        {
            return Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).FindAll());
        }

        public Task<Villain?> GetById(int id)
        {
            return Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Find(x => x.Id == id).FirstOrDefault());
        }

        public Task<int> Add(Villain contact)
        {
            return Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Insert(contact).AsInt32);
        }

        public Task<bool> Update(Villain contact)
        {
            return Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Update(contact));
        }

        public Task<bool> Delete(int id)
        {
            return Task.FromResult(_liteDatabase.GetCollection<Villain>(VillainCollectionName).Delete(new BsonValue(id)));
        }
    }
}
