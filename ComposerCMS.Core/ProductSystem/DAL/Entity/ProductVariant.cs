using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.ProductSystem.DAL.Entity
{
    public class ProductVariant : IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public Guid VariantID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded  { get; set; }
        public DateTime DateLastUpdated  { get; set; }
        public Guid UserIDAdded  { get; set; }
        public Guid UserIDLastUpdated  { get; set; }
    }
}
