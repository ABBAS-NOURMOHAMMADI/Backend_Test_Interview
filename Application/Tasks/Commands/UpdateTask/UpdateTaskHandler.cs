using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly IApplicationDbContext context;

        public UpdateTaskHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.FindAsync(request.ProjectId);
            if (project is null)
                throw new Exception("project not found");

            var task = await context.Tasks.FindAsync(request.TaskId);
            if (task is null)
                throw new Exception("task not found");

            if (task.ProjectId != project.Id)
                throw new Exception();

            if (TaskInfoUpdate(request, task))
            {
                context.Projects.Update(project);
                await context.SaveChangesAsync();
            }

            return TaskDto.Create(task);
        }

        private bool TaskInfoUpdate(UpdateTaskCommand request, Domain.Entities.Task task)
        {
            if (request.Name != task.Name)
            {
                task.Name = request.Name;
            }

            var entry = context.GetChangeTracker().Entries<Domain.Entities.Task>();

            foreach (var item in entry)
                if (item.State == EntityState.Modified)
                    return true;

            return false;
        }
    }
}
