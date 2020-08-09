using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class PageScriptConfig : IEntityTypeConfiguration<PageScript>
    {
        public void Configure(EntityTypeBuilder<PageScript> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.PageID);
            builder.Property(p => p.ExternalResourceID);
            builder.Property(p => p.LoadOrder);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);
        }
    }
}
