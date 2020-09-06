using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class ActivityHistoryConfig : IEntityTypeConfiguration<ActivityHistory>
    {
        public void Configure(EntityTypeBuilder<ActivityHistory> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.Entity).HasMaxLength(128);
            builder.Property(p => p.Action).HasMaxLength(16);
            builder.Property(p => p.Data).HasMaxLength(1024);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.UserIDAdded);

            builder.HasIndex(p => p.Entity);
            builder.HasIndex(p => p.Action);
            builder.HasIndex(p => p.DateAdded);
            builder.HasIndex(p => p.UserIDAdded);

            builder.Ignore(p => p.DateLastUpdated);
            builder.Ignore(p => p.UserIDLastUpdated);
        }
    }
}
