using ComposerCMS.Core.ProductSystem.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class ProductVariantConfig : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariant", "Product");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.ProductID);
            builder.Property(p => p.VariantID);

            builder.Property(p => p.SKU).HasMaxLength(36);
            builder.Property(p => p.Price).IsRequired(false);
            builder.Property(p => p.Cost).IsRequired(false);
            builder.Property(p => p.Quantity).IsRequired(false);
            builder.Property(p => p.Vendor).IsRequired(false);
            builder.Property(p => p.ProductTypeID).IsRequired(false);
            builder.Property(p => p.TrackQuantity).HasDefaultValue(true);
            builder.Property(p => p.Quantity).HasDefaultValue(0);
            builder.Property(p => p.IsPhysical).HasDefaultValue(true);
            builder.Property(p => p.AllowOutOfStockPurchases).HasDefaultValue(false);
            builder.Property(p => p.IsPublished).HasDefaultValue(false);

            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.ProductID);
            builder.HasIndex(p => p.VariantID);
        }
    }
}
