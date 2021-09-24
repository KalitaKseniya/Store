using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataWarehouse.API.Models
{
    public class ProductService
    {
        IMongoCollection<Product> Products;
        IGridFSBucket gridFS;
        public ProductService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDBConnection");
            var connection = new MongoUrlBuilder(connectionString);

            MongoClient client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);

            gridFS = new GridFSBucket(database);
            Products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var builder = new FilterDefinitionBuilder<Product>();
            var filter = builder.Empty;

            return await Products.Find(filter).ToListAsync();
        }
        
        public async Task<Product> GetProductAsync(string id)
        {
            return await Products.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
             await Products.InsertOneAsync(product);
        }
        
        public async Task UpdateAsync(Product product)
        {
            await Products.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(product.Id)), product);
        }
        
        public async Task RemoveAsync(string id)
        {
            await Products.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<byte[]> GetImage(string id, StreamConfigurationProvider imageStream, string imageName)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }  
        
        public async Task StoreImage(string id, Stream imageStream, string imageName)
        {
            Product p = await GetProductAsync(id);
            if (p.HasImage())
            {
                await gridFS.DeleteAsync(new ObjectId(p.ImageId));
            }

            ObjectId imageId = await gridFS.UploadFromStreamAsync(imageName, imageStream);

            p.ImageId = imageId.ToString();
            var filter = Builders<Product>.Filter.Eq("_id", new ObjectId(p.Id));
            var update = Builders<Product>.Update.Set("ImageId", p.ImageId);

            await Products.UpdateOneAsync(filter, update);
        }


    }
}
