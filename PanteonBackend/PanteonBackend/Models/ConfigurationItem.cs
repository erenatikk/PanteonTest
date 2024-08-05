using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Panteon_Backend.Models
{
    public class ConfigurationItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("buildingType")]
    public string BuildingType { get; set; }

    [BsonElement("buildingCost")]
    public decimal BuildingCost { get; set; }

    [BsonElement("constructionTime")]
    public int ConstructionTime { get; set; }
}

}
