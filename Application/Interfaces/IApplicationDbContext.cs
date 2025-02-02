using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<User> Users { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<Domain.Entities.Task> Tasks { get; set; }
    DbSet<TaskAssignment> TaskAssignments { get; set; }

    ChangeTracker GetChangeTracker();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
