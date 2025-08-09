

namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }  = string.Empty; // In a real application, consider hashing passwords
        public string Username { get; set; } = string.Empty;
 
    }
}