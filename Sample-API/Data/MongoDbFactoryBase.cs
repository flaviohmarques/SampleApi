using SampleApi.Data.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApi.Data
{
    public abstract class MongoDbFactoryBase
    {
        private readonly IMongoDatabase _database;

        protected MongoDbFactoryBase(IConfiguration configuration)
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("camelCase", pack, t => true);
            _database = new MongoClient(configuration.GetConnectionString("DefaultConnection")).GetDatabase(configuration.GetValue<string>("DatabaseName"));
        }

        private protected static string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        private protected IMongoCollection<T> GetMongoCollection<T>()
        {
            return _database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        public virtual IQueryable<T> AsQueryable<T>()
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            return collection.AsQueryable();
        }

        public async virtual Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filterExpression)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            return await collection.Find(filterExpression).FirstOrDefaultAsync();
        }

        public async virtual Task<List<T>> FindAllAsync<T>(Expression<Func<T, bool>> filterExpression)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            return await collection.Find(filterExpression).ToListAsync();
        }

        public async virtual Task<long> CountDocumentsAsync<T>(Expression<Func<T, bool>> filterExpression)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            return await collection.Find(filterExpression).CountDocumentsAsync();
        }

        public async virtual Task<bool> ExistsOneAsync<T>(Expression<Func<T, bool>> filterExpression)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            return await collection.Find(filterExpression).AnyAsync();
        }

        public async virtual Task<T> FindByIdAsync<T>(string id)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            return await collection.Find(filter).SingleOrDefaultAsync();
        }


        public async virtual Task<T> InsertOneAsync<T>(T document)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            await collection.InsertOneAsync(document);
            return document;
        }

        public virtual async Task InsertManyAsync<T>(ICollection<T> documents)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            await collection.InsertManyAsync(documents);
        }

        public virtual async Task ReplaceOneAsync<T>(T document, ObjectId id)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            var filter = Builders<T>.Filter.Eq("_id", id);
            await collection.FindOneAndReplaceAsync(filter, document);
        }

        public async Task DeleteOneAsync<T>(Expression<Func<T, bool>> filterExpression)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            await collection.FindOneAndDeleteAsync(filterExpression);
        }

        public async Task DeleteByIdAsync<T>(string id)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            await collection.FindOneAndDeleteAsync(filter);
        }

        public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> filterExpression)
        {
            IMongoCollection<T> collection = GetMongoCollection<T>();
            await collection.DeleteManyAsync(filterExpression);
        }
    }
}
