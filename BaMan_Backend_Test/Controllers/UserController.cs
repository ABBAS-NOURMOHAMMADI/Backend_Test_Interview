using Application.Users.Commands;
using Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BaMan_Backend_Test.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<TaskController> _logger;

        public UserController(ILogger<TaskController> logger)
            : base(logger)

        {
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand user)
        {
            return await HandleRequest(user);
        }

        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            return await HandleRequest(new GetListUserQuery());
        }
    }
}
