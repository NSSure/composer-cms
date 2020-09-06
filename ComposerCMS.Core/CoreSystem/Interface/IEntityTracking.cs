using System;

namespace ComposerCMS.Core.Interface
{
    public interface IEntityTracking
    {
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
