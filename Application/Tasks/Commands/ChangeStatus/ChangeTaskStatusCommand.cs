using Application.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Tasks.Commands.ChangeStatus;

public class ChangeTaskStatusCommand : IRequest<TaskDto>
{
    public int TaskId { get; set; }
    public Status Status { get; set; } = Status.Todo;
    public int ProjectId { get; set; }
}
