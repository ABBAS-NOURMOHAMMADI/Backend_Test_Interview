using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IApplicationDbContext context;

        public CreateUserHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Username) ||
                string.IsNullOrEmpty(request.Password))
                throw new Exception("empiyu usernamer or password");

            if (await context.Users.AnyAsync(a => a.Username == request.Username.ToUpper()))
                throw new Exception("user is exist");

            // برای سادگی در اینجا گذرواژه هش نشده است
            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
