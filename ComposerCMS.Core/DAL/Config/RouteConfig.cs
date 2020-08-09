using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class RouteConfig : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.EntityID);
            builder.Property(p => p.OriginalEntityText).IsRequired(false);
            builder.Property(p => p.Url);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.Ignore(p => p.DateLastUpdated);
        }
    }
}
