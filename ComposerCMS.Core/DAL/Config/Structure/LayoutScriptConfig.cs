using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class LayoutScriptConfig : IEntityTypeConfiguration<LayoutScript>
    {
        public void Configure(EntityTypeBuilder<LayoutScript> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.LayoutID);
            builder.Property(p => p.ExternalResourceID);
            builder.Property(p => p.LoadOrder);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);
        }
    }
}
