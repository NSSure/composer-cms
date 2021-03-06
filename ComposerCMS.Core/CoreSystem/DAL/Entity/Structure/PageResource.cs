using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class PageResource : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid PageID { get; set; }
        public Guid ExternalResourceID { get; set; }
        public int Order { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}