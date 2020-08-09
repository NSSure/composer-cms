using System.Collections.Generic;

namespace ComposerCMS.Core.Model
{
    public class RouteSection
    {
        public string Name { get; set; }
        public List<RouteItem> RouteItems { get; set; }
    }
}
