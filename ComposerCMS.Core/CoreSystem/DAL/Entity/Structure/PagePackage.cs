using System;

namespace ComposerCMS.Core.Entity.Structure
{
    public class PagePackage
    {
        public Guid ID { get; set; }
        public Guid PageID { get; set; }
        public Guid? ExternalResourcePackageID { get; set; }
        public int Order { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
