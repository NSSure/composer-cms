using System;
using System.Collections.Generic;
using System.Text;

namespace ComposerCMS.Core.Model
{
    public class Theme
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
