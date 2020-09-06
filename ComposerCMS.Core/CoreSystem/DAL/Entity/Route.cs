using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class Route : IEntityTracking
    {
        public Guid ID { get; set; }
        public string EntityID { get; set; }
        public string OriginalEntityText { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }

        /// <summary>
        /// Ignored.
        /// </summary>
        public DateTime DateLastUpdated { get; set; }
    }
}