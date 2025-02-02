using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Name).HasMaxLength(500).IsRequired();
    }
}
