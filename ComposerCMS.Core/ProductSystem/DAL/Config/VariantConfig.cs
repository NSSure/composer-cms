using ComposerCMS.Core.ProductSystem.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class VariantConfig : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {
            builder.ToTable("Variant", "Product");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.Key).HasMaxLength(32);
            builder.Property(p => p.Value).HasMaxLength(32);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.Key);
            builder.HasIndex(p => p.Value);
        }
    }
}
