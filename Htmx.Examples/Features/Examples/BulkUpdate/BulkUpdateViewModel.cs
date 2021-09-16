using System.Collections.Generic;

namespace Htmx.Examples.Features.Examples.BulkUpdate
{
    public record BulkUpdateViewModel
    {
        public IEnumerable<ContactListItem> Contacts { get; init; } = new List<ContactListItem>();
    }

    public record ContactListItem(int Id, string Name, string Email, string Status, string StatusChangedClass);
}
