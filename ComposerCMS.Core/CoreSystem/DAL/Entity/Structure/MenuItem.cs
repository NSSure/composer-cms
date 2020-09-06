using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class MenuItem : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid MenuID { get; set; }
        public string DisplayText { get; set; }
        public int Order { get; set; } = 0;
        public Guid RouteID { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
