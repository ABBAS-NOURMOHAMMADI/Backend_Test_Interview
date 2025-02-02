namespace Domain.Entities;

public class Project : BaseEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; } = new();
}