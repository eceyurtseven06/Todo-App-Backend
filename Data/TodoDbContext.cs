using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        // ToDo model
        public DbSet<ToDo> ToDos { get; set; } = null!;
        
        // User model
        public DbSet<User> Users { get; set; } = null!;

    
    }
}