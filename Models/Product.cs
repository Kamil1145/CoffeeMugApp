using MongoDB.Bson.Serialization.Attributes;

namespace CoffeeMugApp.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Prize")]
        public decimal Prize { get; set; }

    }
}
