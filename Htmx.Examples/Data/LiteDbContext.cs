
using Htmx.Examples.Configure;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Htmx.Examples.Data;

public class LiteDbContext
{
    public LiteDatabase Database {  get; set; }

    public LiteDbContext(IOptions<LiteDbOptions> options)
    {
        Database = new LiteDatabase(options.Value.DatabaseLocation);
    }
}
