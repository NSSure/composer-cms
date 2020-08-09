using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class MenuItemConfig : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.MenuID);
            builder.Property(p => p.DisplayText).HasMaxLength(128);
            builder.Property(p => p.RouteID).HasMaxLength(256);
            builder.Property(p => p.Order);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.MenuID);
            builder.HasIndex(p => p.RouteID);
        }
    }
}
