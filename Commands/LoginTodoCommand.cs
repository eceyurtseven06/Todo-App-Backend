using MediatR;

public record LoginCommand(string Username, string Password) : IRequest<string>;
public class LoginTodoCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}