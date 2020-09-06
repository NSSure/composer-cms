using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class Post : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool IsPinned { get; set; }
        public bool IsPublished { get; set; }
        public bool IsPublic { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
