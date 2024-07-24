using MediatR;
using Panteon_Backend.Models;

namespace Panteon_Backend.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
