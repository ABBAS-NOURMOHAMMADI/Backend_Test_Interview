using Application.Dtos;
using MediatR;

namespace Application.Projects.Commands
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
