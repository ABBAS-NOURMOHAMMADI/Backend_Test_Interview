using Application.Dtos;
using Application.Interfaces;
using Application.MqService;
using MediatR;

namespace Application.Tasks.Commands.ChangeStatus
{
    public class ChangeTaskStatusHandler : IRequestHandler<ChangeTaskStatusCommand, TaskDto>
    {
        private readonly IApplicationDbContext context;
        private readonly PublisherService publisher;

        public ChangeTaskStatusHandler(IApplicationDbContext context, PublisherService publisher)
        {
            this.context = context;
            this.publisher = publisher;
        }

        public async Task<TaskDto> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.FindAsync(request.ProjectId);
            if (project is null)
                throw new Exception("project not found");

            var task = await context.Tasks.FindAsync(request.TaskId);
            if (task is null)
                throw new Exception("task not found");

            if (task.ProjectId != project.Id)
                throw new Exception();

            task.Status = request.Status;
            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            var taskAssignments = context.TaskAssignments.Where(ta => ta.TaskId == task.Id).ToList();
            foreach (var assignment in taskAssignments)
            {
                var message = $"{task.Id}|{request.Status}";
                publisher.SendMessage(message);
            }

            return TaskDto.Create(task);
        }
    }
}
