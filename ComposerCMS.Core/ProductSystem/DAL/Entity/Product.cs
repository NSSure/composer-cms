using ComposerCMS.Core.Interface;
using System;

namespace ComposerCMS.Core.Entity.ProductSystem
{
    public class Product : IEntityTracking
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public bool TrackQuantity { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Is true no shipping address when purchasing.
        /// </summary>
        public bool IsPhysical { get; set; }

        public bool HasVariants { get; set; }

        public bool AllowOutOfStockPurchases { get; set; }
        public bool IsPublished { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid UserIDAdded { get; set; }
        public Guid UserIDLastUpdated { get; set; }
    }
}
