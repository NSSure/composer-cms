using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity
{
    public class Settings : IEntityTracking
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public Guid DefaultRouteID { get; set; }
        public bool MinimizeJs { get; set; }
        public bool MinimizeCss { get; set; }
        public Guid UserIDLastUpdated { get; set; }
        public Guid? ThemeKey { get; set; }

        /// <summary>
        /// Ignored.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Ignored.
        /// </summary>
        public DateTime DateLastUpdated { get; set; }

        /// <summary>
        /// Ignored.
        /// </summary>
        public Guid UserIDAdded { get; set; }
    }
}
