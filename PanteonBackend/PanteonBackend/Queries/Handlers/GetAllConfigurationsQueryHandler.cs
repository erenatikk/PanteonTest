using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Panteon_Backend.Controllers.Data;
using Panteon_Backend.Models;
using Panteon_Backend.Queries;

public class GetAllConfigurationsQueryHandler : IRequestHandler<GetAllConfigurationsQuery, List<ConfigurationItem>>
{
    private readonly MongoDbContext _context;

    public GetAllConfigurationsQueryHandler(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConfigurationItem>> Handle(GetAllConfigurationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ConfigurationItems.Find(_ => true).ToListAsync(cancellationToken);
    }
}
