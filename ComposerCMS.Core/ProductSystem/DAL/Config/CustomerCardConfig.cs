using ComposerCMS.Core.ProductSystem.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class CustomerCardConfig : IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.ToTable("CustomerCard", "Product");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.CustomerID);
            builder.Property(p => p.CardToken).HasMaxLength(64);

            builder.HasIndex(p => p.CustomerID);
        }
    }
}
