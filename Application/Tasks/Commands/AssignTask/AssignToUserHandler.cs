using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.Commands.AssignTask
{
    public class AssignToUserHandler : IRequestHandler<AssignToUserCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public AssignToUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(AssignToUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.Users.Any())
                throw new Exception("fatal error");

            var project = await context.Projects.FindAsync(command.ProjectId);
            if (project is null)
                throw new Exception("project not found");

            var task = await context.Tasks.FindAsync(command.TaskId);
            if (task is null)
                throw new Exception("task not found");

            if (task.ProjectId != project.Id)
                throw new Exception();

            var users = await context.Users.Where(u => command.Users.Contains(u.Id)).ToListAsync();
            if (!users.Any())
                throw new Exception("users not found");

            context.TaskAssignments.AddRange(users.Select(s => new Domain.Entities.TaskAssignment
            {
                Task = null,
                User = null,
                TaskId = command.TaskId,
                UserId = s.Id
            }).ToList());

            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
