using MediatR;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Commands;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand,bool>
    {
        private readonly TodoDbContext _context;

        public RegisterCommandHandler(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Aynı kullanıcı adı varsa hata döndür
            var existingUser = await _context.Users
                .AnyAsync(u => u.Username == request.Username, cancellationToken);

            if (existingUser)
            {
                throw new Exception("Username already exists.");
            }

            // Yeni kullanıcı oluştur
            var user = new User
            {
                Username = request.Username,
                Password = request.Password // NOT: Şifreyi hash’lemelisin
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
