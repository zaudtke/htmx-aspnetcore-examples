using System.Collections.Generic;

namespace Htmx.Examples.Features.Examples.BulkUpdate
{
    public record BulkUpdateViewModel
    {
        public IEnumerable<VillainListItem> Villains { get; init; } = new List<VillainListItem>();
    }

    public record VillainListItem(int Id, string Name, string Movie, string Status, string StatusChangedClass);
}
