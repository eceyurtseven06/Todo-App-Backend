using MediatR;
using TodoApi.Models;

namespace TodoApi.Commands;

public record AddTodoCommand(ToDo Todo) : IRequest<ToDo>;
