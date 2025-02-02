using Domain.Enums;

namespace Application.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Todo;
        public int ProjectId { get; set; }

        public static TaskDto Create(Domain.Entities.Task task)
        {
            return new()
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Status = task.Status,
                Name = task.Name
            };
        }
    }
}
