
using Htmx.Examples.Data;
using Htmx.Examples.Models;
using LiteDB;

namespace Htmx.Examples.Features.Contacts;
public class ContactService
{
    private readonly LiteDatabase _liteDatabase;

    public ContactService(LiteDbContext context)
    {
        _liteDatabase = context.Database;
    }

    public Task<IEnumerable<Contact>> GetContacts()
    {
        return Task.FromResult(_liteDatabase.GetCollection<Contact>("Contacts").FindAll());
    }

    public Task<Contact?> GetContactById(int id)
    {
        return Task.FromResult(_liteDatabase.GetCollection<Contact>("Contacts").Find(x => x.Id == id).FirstOrDefault());
    }

    public Task<int> AddContact(Contact contact)
    {
        return Task.FromResult(_liteDatabase.GetCollection<Contact>("Contacts").Insert(contact).AsInt32);
    }

    public Task<bool> UpdateContact(Contact contact)
    {
        return Task.FromResult(_liteDatabase.GetCollection<Contact>("Contacts").Update(contact));
    }

    public Task<bool> DeleteContact(int id)
    {
        return Task.FromResult(_liteDatabase.GetCollection<Contact>("Contacts").Delete(new BsonValue(id)));
    }
}
