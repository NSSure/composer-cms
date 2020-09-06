using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity.ProductSystem
{
    public class ProductCategory : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public Guid CategoryID { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
