using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class ExternalResource : IEntityTracking
    {
        public Guid ID { get; set; }

        public Guid? ExternalPackageID { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; set; }

        public string Extension { get; set; }

        /// <summary>
        /// wwwwroot/composer-cms/[css|js]
        /// </summary>
        public string Path { get; set; }

        public int Order { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}