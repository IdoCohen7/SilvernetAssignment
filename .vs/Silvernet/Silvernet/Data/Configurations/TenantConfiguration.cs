using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silvernet.Models;

namespace Silvernet.Data.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                   .IsRequired()     
                   .HasMaxLength(100); 

            builder.Property(t => t.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasIndex(t => t.Email)
                   .IsUnique();

            builder.Property(t => t.Phone)
                   .HasMaxLength(20);

            builder.HasIndex(t => t.Phone)
                .IsUnique();

            builder.Property(t => t.CreationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // one to many 

            builder.HasMany(t => t.Users)     
                   .WithOne(u => u.Tenant)   
                   .HasForeignKey(u => u.TenantId) 
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
