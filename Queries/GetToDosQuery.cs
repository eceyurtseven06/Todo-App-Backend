using MediatR;
using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Queries;


public record GetToDosQuery(int UserId) : IRequest<IEnumerable<ToDo>>;
