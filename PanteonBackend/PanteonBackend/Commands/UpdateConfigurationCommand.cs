using MediatR;
using Panteon_Backend.Models;

namespace Panteon_Backend.Commands
{
    public class UpdateConfigurationCommand : IRequest<ConfigurationItem>
    {
        public string Id { get; set; }
        public string BuildingType { get; set; }
        public decimal BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
