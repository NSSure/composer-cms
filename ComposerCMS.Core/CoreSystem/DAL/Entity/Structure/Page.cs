using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class Page : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid LayoutID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Path { get; set; }

        public bool IsSystemPage { get; set; }

        /// <summary>
        /// If true the page cannot be navigated to directly.
        /// </summary>
        public bool IsAbstract { get; set; }

        public bool IsPublished { get; set; }

        /// <summary>
        /// If created/restored from an old version note that here.
        /// </summary>
        public Guid? SourceVersionID { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public DateTime? DateLastPublished { get; set; }

        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}