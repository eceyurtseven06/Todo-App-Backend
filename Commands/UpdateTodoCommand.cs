using MediatR;
using TodoApi.Models;

namespace TodoApi.Commands
{
    public record UpdateTodoCommand(ToDo Todo) : IRequest<ToDo>;
}