using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.BlogID);
            builder.Property(p => p.Title).HasMaxLength(128);
            builder.Property(p => p.Description).HasMaxLength(512);
            builder.Property(p => p.Content);
            builder.Property(p => p.IsPinned);
            builder.Property(p => p.IsPublished);
            builder.Property(p => p.IsPublic);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.BlogID);
            builder.HasIndex(p => p.Title);
            builder.HasIndex(p => p.IsPinned);
            builder.HasIndex(p => p.IsPublished);
            builder.HasIndex(p => p.IsPublic);
        }
    }
}
