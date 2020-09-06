using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class SettingsConfig : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.Title);
            builder.Property(p => p.DefaultRouteID);
            builder.Property(p => p.MinimizeCss);
            builder.Property(p => p.MinimizeJs);
            builder.Property(p => p.ThemeKey).IsRequired(false);
            builder.Property(p => p.UserIDLastUpdated);

            builder.Ignore(p => p.DateAdded);
            builder.Ignore(p => p.DateLastUpdated);
            builder.Ignore(p => p.UserIDAdded);
        }
    }
}
