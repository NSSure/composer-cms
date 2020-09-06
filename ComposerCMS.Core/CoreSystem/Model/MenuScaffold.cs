using ComposerCMS.Core.Entity;
using System.Collections.Generic;

namespace ComposerCMS.Core.Model
{
    public class MenuScaffold
    {
        public string Name { get; set; }
        public List<MenuItemScaffold> Items { get; set; } = new List<MenuItemScaffold>();
    }

    public class MenuItemScaffold
    {
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
