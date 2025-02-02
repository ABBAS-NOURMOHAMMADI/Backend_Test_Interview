using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly IApplicationDbContext context;

        public CreateTaskHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.FindAsync(request.ProjectId);

            if (project is null)
                throw new Exception("project not found");

            var task = new Domain.Entities.Task
            {
                Name = request.Name,
                ProjectId = project.Id,
                Status = request.Status
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            return TaskDto.Create(task);
        }
    }
}
