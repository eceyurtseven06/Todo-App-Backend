using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Commands;
using TodoApi.Data;

namespace TodoApi.CommandHandlers
{

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
    {
        private readonly TodoDbContext _context;

        public DeleteTodoCommandHandler(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId, cancellationToken);

            if (todo == null)
                throw new KeyNotFoundException($"Todo with ID {request.Id} not found or does not belong to the user.");

            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}