using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class PageVersion : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid PageID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Path { get; set; }

        public DateTime DateAdded { get; set; }
        public Guid UserIDAdded { get; set; }

        /// <summary>
        /// Ignored.
        /// </summary>
        public DateTime DateLastUpdated { get; set; }

        /// <summary>
        /// Ignored.
        /// </summary>
        public Guid UserIDLastUpdated { get; set; }
    }
}
