using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public async Task<string> CreateAsync(Product product)
        {
             await Products.InsertOneAsync(product);
             return product.Id;
        }
        
        public async Task UpdateAsync(Product product)
        {
            await Products.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(product.Id)), product);
        }
        
        public async Task RemoveAsync(string id)
        {
            await Products.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task<byte[]> GetImage(string id)
        {
            return await gridFS.DownloadAsBytesAsync(new ObjectId(id));
        }  
        
        public async Task<string> StoreImage(string id, string url, string imageName)
        {
            if (!IsValidUrl(url))
            {
                return null;
            }
            Product p = await GetProductAsync(id);
            if (p.HasImage())
            {
                await gridFS.DeleteAsync(new ObjectId(p.ImageId));
            }
            byte[] binaryImage = null;
            using (var webClient = new WebClient())
            {
                binaryImage = webClient.DownloadData(url);
            }
            ObjectId imageId = await gridFS.UploadFromBytesAsync(imageName, binaryImage);

            p.ImageId = imageId.ToString();
            var filter = Builders<Product>.Filter.Eq("_id", new ObjectId(p.Id));
            var update = Builders<Product>.Update.Set("ImageId", p.ImageId);

            await Products.UpdateOneAsync(filter, update);
            return p.ImageId;
        }

        private bool IsValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }


    }
}
