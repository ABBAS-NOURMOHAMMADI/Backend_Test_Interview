using Application.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<TaskDto>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; } = Status.Todo;
    }
}
