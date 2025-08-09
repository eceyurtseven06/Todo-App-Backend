using MediatR;

namespace TodoApi.Commands
{
    public record RegisterCommand(string Username, string Password) : IRequest<bool>;
}