using Application.Projects.Commands;
using Application.Projects.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BaMan_Backend_Test.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ILogger<ProjectController> logger)
            : base(logger)

        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            return await HandleRequest(command);
        }

        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            return await HandleRequest(new GetListProjectQuery());
        }
    }
}
