using Application.Dtos;
using MediatR;

namespace Application.Tasks.Queries
{
    public class GetListTaskQuery : IRequest<GetListTaskQueryResult>
    {
        public required int ProjectId { get; set; }
    }

    public record GetListTaskQueryResult(List<TaskDto> Tasks);
}
