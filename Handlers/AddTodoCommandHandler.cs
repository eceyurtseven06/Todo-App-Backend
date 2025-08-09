using MediatR;
using TodoApi.Commands;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Handlers;

public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, ToDo>
{
    private readonly TodoDbContext _context;

    public AddTodoCommandHandler(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<ToDo> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        _context.ToDos.Add(request.Todo);
        await _context.SaveChangesAsync(cancellationToken);
        return request.Todo;
    }
}
