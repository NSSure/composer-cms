using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class ActivityHistory : IEntityTracking
    {
        public Guid ID { get; set; }

        /// <summary>
        /// Page, layout, resource, etc.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Add, update, delete, edit, etc.
        /// </summary>
        public string Action { get; set; }

        public string Data { get; set; }

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
