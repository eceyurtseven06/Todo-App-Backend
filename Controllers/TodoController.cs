using Microsoft.AspNetCore.Mvc;
using MediatR;
using TodoApi.Models;
using TodoApi.Commands;
using TodoApi.Queries;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TodoDbContext = TodoApi.Data.TodoDbContext;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly TodoDbContext _context;

    public TodoController(IMediator mediator, TodoDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetAll()
    {
        var user = User;
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var todos = await _mediator.Send(new GetToDosQuery(userId));
        return Ok(todos);
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> Create(ToDo todo)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        todo.UserId = userId; // Set the UserId from the authenticated user
        var created = await _mediator.Send(new AddTodoCommand(todo));
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            await _mediator.Send(new DeleteTodoCommand(id, userId));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ToDo>> Update(int id, ToDo todo)
    {
        if (id != todo.Id)
        {
            return BadRequest("Todo ID mismatch.");
        }

        try
        {
            var userId = int.Parse(User.FindFirstValue("nameid") ?? "0");
            todo.UserId = userId; // Set the UserId from the authenticated user
            var updatedTodo = await _mediator.Send(new UpdateTodoCommand(todo));
            return Ok(updatedTodo);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    // Liste Ekle


}



public class LoginTodoCommand
{
}
 