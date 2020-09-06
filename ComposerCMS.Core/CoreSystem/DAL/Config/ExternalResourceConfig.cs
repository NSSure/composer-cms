using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class ExternalResourceConfig : IEntityTypeConfiguration<ExternalResource>
    {
        public void Configure(EntityTypeBuilder<ExternalResource> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.ExternalPackageID).IsRequired(false);
            builder.Property(p => p.Name);
            builder.Property(p => p.Extension);
            builder.Property(p => p.Path);
            builder.Property(p => p.Order);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);
        }
    }
}
