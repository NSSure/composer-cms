using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity.ProductSystem
{
    public interface IProductScaffold
    {
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public int? Quantity { get; set; }
        public string Vendor { get; set; }
        public Guid? ProductTypeID { get; set; }
        public string SKU { get; set; }
        public bool TrackQuantity { get; set; }

        /// <summary>
        /// If false no shipping address when purchasing.
        /// </summary>
        public bool IsPhysical { get; set; }

        /// <summary>
        /// If true then the product variant can be
        /// </summary>
        public bool AllowOutOfStockPurchases { get; set; }

        public bool IsPublished { get; set; }
    }

    public class Product : IProductScaffold, IEntityTracking
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasVariants { get; set; }

        // Product scaffold properties. If these are set here they are the defaults for the variants and
        // can be overriden on a per variant basis.

        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public string Vendor { get; set; }
        public Guid? ProductTypeID { get; set; }
        public string SKU { get; set; }
        public bool TrackQuantity { get; set; }
        public bool IsPhysical { get; set; }
        public bool AllowOutOfStockPurchases { get; set; }
        public bool IsPublished { get; set; }
        public int? Quantity { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
