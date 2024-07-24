using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Panteon_Backend.Commands;
using Panteon_Backend.Controllers.Data;
using Panteon_Backend.Models;

public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, ConfigurationItem>
{
    private readonly MongoDbContext _context;

    public CreateConfigurationCommandHandler(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<ConfigurationItem> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
    {
        var configurationItem = new ConfigurationItem
        {
            BuildingType = request.BuildingType,
            BuildingCost = request.BuildingCost,
            ConstructionTime = request.ConstructionTime
        };

        await _context.ConfigurationItems.InsertOneAsync(configurationItem, cancellationToken: cancellationToken);

        return configurationItem;
    }
}
