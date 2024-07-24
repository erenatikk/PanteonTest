using MediatR;
using Panteon_Backend.Models;

namespace Panteon_Backend.Commands
{
    public class CreateConfigurationCommand : IRequest<ConfigurationItem>
    {
        public string BuildingType { get; set; }
        public decimal BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
