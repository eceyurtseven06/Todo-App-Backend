using MediatR;

namespace TodoApi.Commands
{
    public class DeleteTodoCommand : IRequest
    {
        public int Id { get; }
        public int UserId { get; }

        public DeleteTodoCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }

}