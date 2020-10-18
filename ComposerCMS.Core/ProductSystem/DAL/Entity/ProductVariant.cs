using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.ProductSystem.DAL.Entity
{
    public class ProductVariant : IProductScaffold, IEntityTracking
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public Guid VariantID { get; set; }

        // Product scaffold properties. These are the overrides for the base product entity.
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public string Vendor { get; set; }
        public Guid? ProductTypeID { get; set; }
        public string SKU { get; set; }
        public bool TrackQuantity { get; set; }
        public bool IsPhysical { get; set; }
        public bool AllowOutOfStockPurchases { get; set; }
        public int? Quantity { get; set; }
        public bool IsPublished { get; set; }

        public DateTime DateAdded  { get; set; }
        public DateTime DateLastUpdated  { get; set; }
        public Guid UserIDAdded  { get; set; }
        public Guid UserIDLastUpdated  { get; set; }
    }
}
