using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
