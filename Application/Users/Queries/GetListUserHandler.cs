using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries
{
    public class GetListUserHandler : IRequestHandler<GetListUserQuery, object>
    {
        private readonly IApplicationDbContext context;

        public GetListUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<object> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var users = await context.Users.ToListAsync();

            return users;
        }
    }
}
