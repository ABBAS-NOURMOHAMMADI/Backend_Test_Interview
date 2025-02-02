using Domain.Entities;

namespace Application.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TaskDto> Tasks { get; set; } = new();

        public static ProjectDto Create(Project project)
        {
            return new()
            {
                Id = project.Id,
                Name = project.Name,
                Tasks = project.Tasks is null ? new() : project.Tasks.Select(s => TaskDto.Create(s)).ToList(),
            };
        }
    }
}
