
using Htmx.Mvc.Configure;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Htmx.Mvc.Domain.Data;

public class LiteDbContext
{
	public LiteDatabase Database { get; set; }

	public LiteDbContext(IOptions<LiteDbOptions> options)
	{
		Database = new LiteDatabase(options.Value.DatabaseLocation);
	}
}
