using ComposerCMS.Core.Entity.ProductSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory", "Product");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.ProductID);
            builder.Property(p => p.CategoryID);

            builder.HasIndex(p => p.ProductID);
            builder.HasIndex(p => p.CategoryID);
        }
    }
}
