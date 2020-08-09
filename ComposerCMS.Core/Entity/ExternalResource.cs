using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class ExternalResource : IEntityTracking
    {
        public Guid ID { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; set; }

        public string Extension { get; set; }

        /// <summary>
        /// wwwwroot/composer-cms/[css|js]
        /// </summary>
        public string Href { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}