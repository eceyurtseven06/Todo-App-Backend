using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Queries;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Handlers;

public class GetToDosQueryHandler : IRequestHandler<GetToDosQuery, IEnumerable<ToDo>>
{
    private readonly TodoDbContext _context;

    public GetToDosQueryHandler(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ToDo>> Handle(GetToDosQuery request, CancellationToken cancellationToken)
    {
        return await _context.ToDos
            .Where(todo => todo.UserId == request.UserId)
            .ToListAsync(cancellationToken);
    }
}