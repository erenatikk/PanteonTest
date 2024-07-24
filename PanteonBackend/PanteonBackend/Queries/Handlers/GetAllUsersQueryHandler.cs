using MediatR;
using Microsoft.AspNetCore.Identity;
using Panteon_Backend.Data;
using Panteon_Backend.Models;
using Panteon_Backend.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public GetAllUsersQueryHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public  Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.Select(p => new User { Email = p.Email, Username = p.UserName , Id = p.Id}).ToList();
        return Task.FromResult(users);
    }
}
