using Application.Dtos;
using MediatR;

namespace Application.Projects.Queries
{
    public class GetListProjectQuery : IRequest<GetListProjectQueryResult>
    {
    }

    public record GetListProjectQueryResult(List<ProjectDto> Projects);
}
