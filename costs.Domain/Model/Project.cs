using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace costs.Domain.Model
{
    public class Project
    {
        [BsonId()]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public double? Budget { get; set; }
        public Category? Category { get; set; }
        public List<Service>? Services { get; set; }
        public double? Cost { get; set; }
    }
}