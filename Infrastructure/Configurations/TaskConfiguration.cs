using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.ProjectId).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(500).IsRequired();
        builder.Property(t => t.Status).IsRequired();

        builder.HasIndex(t => t.ProjectId);
        builder.HasIndex(t => t.Name);

        builder.HasOne(t => t.Project).WithMany(t => t.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
