﻿using ComposerCMS.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class ExternalPackageConfig : IEntityTypeConfiguration<ExternalPackage>
    {
        public void Configure(EntityTypeBuilder<ExternalPackage> builder)
        {
            builder.HasKey(p => p.ID);

            builder.Property(p => p.Name);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);
        }
    }
}
