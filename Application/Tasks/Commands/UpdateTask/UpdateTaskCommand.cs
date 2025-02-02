using Application.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<TaskDto>
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
    }
}
