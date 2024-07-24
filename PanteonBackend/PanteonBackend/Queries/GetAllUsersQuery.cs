using MediatR;
using Panteon_Backend.Models;
using System.Collections.Generic;

namespace Panteon_Backend.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>> { }
}
