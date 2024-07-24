using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Panteon_Backend.Models
{
    public class ConfigurationItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string BuildingType { get; set; }
        public decimal BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
