using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries
{
    public class GetListProjectHandler :
                IRequestHandler<GetListProjectQuery, GetListProjectQueryResult>
    {
        private readonly IApplicationDbContext context;

        public GetListProjectHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<GetListProjectQueryResult> Handle(GetListProjectQuery request, CancellationToken cancellationToken)
        {
            var projects = await context.Projects
                           .Include(t => t.Tasks)
                           .ToListAsync();

            if (projects is null)
                return new(new());

            return new(projects.Select(p => ProjectDto.Create(p)).ToList());
        }
    }
}
