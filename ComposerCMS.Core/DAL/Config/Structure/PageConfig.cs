using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class PageConfig : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.Name);
            builder.Property(p => p.Title);
            builder.Property(p => p.Content);
            builder.Property(p => p.Path);
            builder.Property(p => p.IsSystemPage);
            builder.Property(p => p.IsAbstract);
            builder.Property(p => p.IsPublished);
            builder.Property(p => p.SourceVersionID);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.DateLastPublished).IsRequired(false);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.Path);
            builder.HasIndex(p => p.DateAdded);
        }
    }
}
