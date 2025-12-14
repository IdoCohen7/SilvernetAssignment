using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silvernet.Models;

namespace Silvernet.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.Phone)
                   .HasMaxLength(50);

            builder.HasIndex(u => u.Phone)
                .IsUnique();

            builder.Property(u => u.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // fk

            builder.Property(u => u.TenantId)
                   .IsRequired();

            // one to many 

            builder.HasOne(u => u.Tenant)
                   .WithMany(t => t.Users)
                   .HasForeignKey(u => u.TenantId)
                   .IsRequired();
        }
    }
}