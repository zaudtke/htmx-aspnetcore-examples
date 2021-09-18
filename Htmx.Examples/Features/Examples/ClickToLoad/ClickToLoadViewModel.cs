using Htmx.Examples.Domain.Villains;
using System.Collections.Generic;

namespace Htmx.Examples.Features.Examples.ClickToLoad
{
    public class ClickToLoadViewModel
    {
        public string LoadMoreUrl { get; set; } = "";
        public IEnumerable<Villain> Villains { get; set; } = new List<Villain>();
    }
}
