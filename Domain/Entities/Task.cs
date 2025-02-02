using Domain.Enums;

namespace Domain.Entities;

public class Task : BaseEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public List<TaskAssignment> TaskAssignments { get; set; } = new();
}
