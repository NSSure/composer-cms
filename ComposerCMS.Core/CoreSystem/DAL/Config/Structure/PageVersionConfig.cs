using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class PageVersionConfig : IEntityTypeConfiguration<PageVersion>
    {
        public void Configure(EntityTypeBuilder<PageVersion> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.PageID);
            builder.Property(p => p.Name);
            builder.Property(p => p.Title);
            builder.Property(p => p.Content);
            builder.Property(p => p.Path);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.UserIDAdded);

            builder.HasIndex(p => p.PageID);
            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Path);
            builder.HasIndex(p => p.DateAdded);

            builder.Ignore(p => p.DateLastUpdated);
            builder.Ignore(p => p.UserIDLastUpdated);
        }
    }
}
