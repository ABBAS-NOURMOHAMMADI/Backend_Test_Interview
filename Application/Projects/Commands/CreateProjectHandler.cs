using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Projects.Commands
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IApplicationDbContext context;

        public CreateProjectHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new Exception("name is empity");

            var project = new Domain.Entities.Project
            {
                Name = request.Name,
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();

            return ProjectDto.Create(project);
        }
    }
}
