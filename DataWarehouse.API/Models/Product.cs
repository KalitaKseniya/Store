using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataWarehouse.API.Models
{
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string Operation { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        public int ProviderId { get; set; }
    }
}
