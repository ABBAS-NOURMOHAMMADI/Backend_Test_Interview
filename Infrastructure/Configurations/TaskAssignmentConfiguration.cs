using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class TaskAssignmentConfiguration : IEntityTypeConfiguration<Domain.Entities.TaskAssignment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskAssignment> builder)
        {
            builder.ToTable("TaskAssignments");
            builder.Property(h => h.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.TaskId).IsRequired();

            builder.HasOne(t => t.User).WithMany(t => t.TaskAssignments)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Task).WithMany(t => t.TaskAssignments)
              .HasForeignKey(t => t.TaskId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
