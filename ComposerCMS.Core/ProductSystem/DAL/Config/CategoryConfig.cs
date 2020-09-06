using ComposerCMS.Core.Entity.ProductSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category", "Product");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.Name).HasMaxLength(128);
            builder.Property(p => p.Description).HasMaxLength(512);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.Name);
        }
    }
}
