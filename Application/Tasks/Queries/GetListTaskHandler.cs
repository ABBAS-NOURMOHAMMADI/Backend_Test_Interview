using Application.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.Queries
{
    public class GetListTaskHandler :
                IRequestHandler<GetListTaskQuery, GetListTaskQueryResult>
    {
        private readonly IApplicationDbContext context;

        public GetListTaskHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<GetListTaskQueryResult> Handle(GetListTaskQuery request,
                                           CancellationToken cancellationToken)
        {
            var project = await context.Projects.FindAsync(request.ProjectId);

            if (project is null)
                throw new Exception("project not found");

            var tasks = await context.Tasks
                        .Where(t => t.ProjectId == project.Id).ToListAsync();

            if (tasks is null)
                return new(new());

            return new(tasks.Select(t => TaskDto.Create(t)).ToList());
        }
    }
}
