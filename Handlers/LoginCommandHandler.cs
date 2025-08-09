using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApi.Models;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Commands;
public record LoginCommand(string Username, string Password) : IRequest<string>;
public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly TodoDbContext _context;
    private readonly IConfiguration _config;

    public LoginCommandHandler(TodoDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        // Config değerlerini kontrol et
        var keyString = _config["Jwt:Key"];
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];

        if (string.IsNullOrEmpty(keyString) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            throw new InvalidOperationException("JWT settings are not configured.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // "nameid"
            new Claim(ClaimTypes.Name, user.Username),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
