using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.ProductSystem.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComposerCMS.Core.DAL.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "Product");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.UserID);
            builder.Property(p => p.FirstName).HasMaxLength(32);
            builder.Property(p => p.LastName).HasMaxLength(32);
            builder.Property(p => p.Email).HasMaxLength(128);
            builder.Property(p => p.AddressLine1).HasMaxLength(64);
            builder.Property(p => p.AddressLine2).HasMaxLength(64);
            builder.Property(p => p.City).HasMaxLength(64);
            builder.Property(p => p.State).HasMaxLength(32);
            builder.Property(p => p.ZipCode).HasMaxLength(10);
            builder.Property(p => p.Phone).HasMaxLength(12);
            builder.Property(p => p.Notes).HasMaxLength(1024);
            builder.Property(p => p.DateAdded);
            builder.Property(p => p.DateLastUpdated);
            builder.Property(p => p.UserIDAdded);
            builder.Property(p => p.UserIDLastUpdated);

            builder.HasIndex(p => p.Email);
            builder.HasIndex(p => p.FirstName);
            builder.HasIndex(p => p.LastName);
            builder.HasIndex(p => p.DateAdded);
        }
    }
}
