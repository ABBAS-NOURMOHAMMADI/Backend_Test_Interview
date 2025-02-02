using MediatR;


namespace Application.Tasks.Commands.AssignTask
{
    public class AssignToUserCommand : IRequest<Unit>
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public List<int> Users { get; set; } = new();
    }
}
