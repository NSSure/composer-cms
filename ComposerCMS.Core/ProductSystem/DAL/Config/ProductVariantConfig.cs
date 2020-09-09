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
            builder.Property(p => p.Quantity);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.ProductID);
            builder.HasIndex(p => p.VariantID);
        }
    }
}
