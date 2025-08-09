using Microsoft.AspNetCore.Mvc;
using MediatR;
using TodoApi.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoApi.Models;
using TodoApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Dtos;
using System.Text.RegularExpressions;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly TodoDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _config;

    public AuthController(IMediator mediator, TodoDbContext context, IPasswordHasher<User> passwordHasher, IConfiguration config)
    {
        _mediator = mediator;
        _context = context;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
        if (user == null)
            return Unauthorized("User not found");

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

        if (result != PasswordVerificationResult.Success)
        {
            return Unauthorized("Incorrect password");
        }

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);  // key from appsettings.json
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Password) || userDto.Password.Length < 6)
        {
            return BadRequest("Password must be at least 6 characters long.");
        }

        if (!Regex.IsMatch(userDto.Username, @"[a-zA-Z]") || Regex.IsMatch(userDto.Username, @"^[\d\W_]+$"))
        {
        return BadRequest("Username must contain at least one letter and cannot consist only of numbers or symbols.");
        }
        
        if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
            return BadRequest("Username already exists.");

        var newUser = new User
        {
            Username = userDto.Username
        };

        newUser.Password = _passwordHasher.HashPassword(newUser, userDto.Password);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(newUser);

        return Ok(new { token });
    }
} 
