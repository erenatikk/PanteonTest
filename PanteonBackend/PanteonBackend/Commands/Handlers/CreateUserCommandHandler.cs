using MediatR;
using Microsoft.AspNetCore.Identity;
using Panteon_Backend.Commands;
using Panteon_Backend.Data;
using Panteon_Backend.Models;
using System.Threading;
using System.Threading.Tasks;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly UserManager<IdentityUser> _userManager;

    public CreateUserCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(new IdentityUser { UserName = user.Username ,  Email = user.Email }, request.Password) ;

        if (result.Succeeded)
        {
            return user;
        }

        throw new System.Exception("User creation failed");
    }
}
