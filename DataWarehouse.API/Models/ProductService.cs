using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWarehouse.API.Models
{
    public class ProductService
    {
        IMongoCollection<Product> Products;
        public ProductService()
        {
            string connectionString = "mongodb+srv://admin:admin@storedatawarehouse.fylai.mongodb.net/StoreDataWarehouse?retryWrites=true&w=majority";
            var connection = new MongoUrlBuilder(connectionString);

            MongoClient client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            Products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var builder = new FilterDefinitionBuilder<Product>();
            var filter = builder.Empty;

            return await Products.Find(filter).ToListAsync();
        }
        
        public async Task<Product> GetProduct(string id)
        {
            return await Products.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task Create(Product product)
        {
             await Products.InsertOneAsync(product);
        }
        
        public async Task Update(Product product)
        {
            await Products.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(product.Id)), product);
        }
        
        public async Task Remove(string id)
        {
            await Products.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}
