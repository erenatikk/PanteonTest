using MediatR;
using Panteon_Backend.Models;
using Panteon_Backend.Data;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Panteon_Backend.Controllers.Data;

namespace Panteon_Backend.Commands.Handlers
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, ConfigurationItem>
    {
        private readonly MongoDbContext _context;

        public UpdateConfigurationCommandHandler(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<ConfigurationItem> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<ConfigurationItem>.Filter.Eq(c => c.BuildingType, request.BuildingType);
            var update = Builders<ConfigurationItem>.Update
                .Set(c => c.BuildingType, request.BuildingType)
                .Set(c => c.BuildingCost, request.BuildingCost)
                .Set(c => c.ConstructionTime, request.ConstructionTime);

            var options = new FindOneAndUpdateOptions<ConfigurationItem>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updatedConfig = await _context.ConfigurationItems.FindOneAndUpdateAsync(filter, update, options, cancellationToken);

            if (updatedConfig == null)
            {
                throw new System.Exception("Configuration update failed");
            }

            return updatedConfig;
        }
    }
}
