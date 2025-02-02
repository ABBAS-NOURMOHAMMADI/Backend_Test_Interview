using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Username).HasMaxLength(500).IsRequired();
        builder.Property(t => t.Password).HasMaxLength(500).IsRequired();
        builder.Property(t => t.Email).HasMaxLength(500).IsRequired(false);
    }
}