using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace costs.Domain.APIModel
{
    public class CategoryModel
    {
        [BsonId()]
        public string Id { get; set; }
        public string Name { get; set; }
    }    
}