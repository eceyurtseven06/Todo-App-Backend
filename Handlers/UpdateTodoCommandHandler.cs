using MediatR;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Commands;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, ToDo>
{
    private readonly TodoDbContext _context;

    public UpdateTodoCommandHandler(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<ToDo> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _context.ToDos.FindAsync(new object[] { request.Todo.Id }, cancellationToken);
        if (todo == null)
            throw new KeyNotFoundException($"Todo with ID {request.Todo.Id} not found.");

        todo.Title = request.Todo.Title;
        todo.Description = request.Todo.Description;
        todo.IsCompleted = request.Todo.IsCompleted;

        await _context.SaveChangesAsync(cancellationToken);

        return todo;
    }
}
