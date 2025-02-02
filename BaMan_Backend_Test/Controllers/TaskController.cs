using Application.Tasks.Commands.AssignTask;
using Application.Tasks.Commands.ChangeStatus;
using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.UpdateTask;
using Application.Tasks.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BaMan_Backend_Test.Controllers
{
    public class TaskController : BaseController
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
            : base(logger)

        {
            _logger = logger;
        }

        [HttpPost("{ProjectId}")]
        public async Task<IActionResult> Create([FromRoute] int ProjectId, [FromBody] CreateTaskCommand command)
        {
            command.ProjectId = ProjectId;
            return await HandleRequest(command);
        }

        [HttpPatch("{ProjectId}/{TaskId}")]
        public async Task<IActionResult> Update([FromRoute] int ProjectId, [FromRoute] int TaskId, [FromBody] UpdateTaskCommand command)
        {
            command.ProjectId = ProjectId;
            command.TaskId = TaskId;
            return await HandleRequest(command);
        }

        [HttpGet("{ProjectId}")]
        public async Task<IActionResult> Tasks([FromRoute] int ProjectId)
        {
            return await HandleRequest(new GetListTaskQuery { ProjectId = ProjectId });
        }

        [HttpPost("{ProjectId}/{TaskId}/AssignTo")]
        public async Task<IActionResult> Tasks([FromRoute] int ProjectId, [FromRoute] int TaskId, [FromBody] AssignToUserCommand command)
        {
            command.ProjectId = ProjectId;
            command.TaskId = TaskId;
            return await HandleRequest(command);
        }

        [HttpPost("{ProjectId}/{TaskId}/ChangeStatus")]
        public async Task<IActionResult> Tasks([FromRoute] int ProjectId, [FromRoute] int TaskId, [FromBody] ChangeTaskStatusCommand command)
        {
            command.ProjectId = ProjectId;
            command.TaskId = TaskId;
            return await HandleRequest(command);
        }
    }
}
