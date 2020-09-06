using ComposerCMS.Core.Entity.Structure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class PageResourcePackageConfig : IEntityTypeConfiguration<PagePackage>
    {
        public void Configure(EntityTypeBuilder<PagePackage> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.PageID);
            builder.Property(p => p.ExternalResourcePackageID);
            builder.Property(p => p.Order);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);
        }
    }
}
